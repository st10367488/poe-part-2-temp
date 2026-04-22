using ContractMS.Data;
using ContractMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;

namespace ContractMS.Controllers
{
    public class ContractsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public ContractsController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // Search/Filter with LINQ (LU3 Workflow Logic requirement)
        public async Task<IActionResult> Index(
            DateTime? startDate, DateTime? endDate, ContractStatus? status)
        {
            var query = _context.Contracts
                .Include(c => c.Client)
                .AsQueryable();

            if (startDate.HasValue)
                query = query.Where(c => c.StartDate >= startDate.Value);

            if (endDate.HasValue)
                query = query.Where(c => c.EndDate <= endDate.Value);

            if (status.HasValue)
                query = query.Where(c => c.Status == status.Value);

            ViewBag.StartDate = startDate?.ToString("yyyy-MM-dd");
            ViewBag.EndDate = endDate?.ToString("yyyy-MM-dd");
            ViewBag.Status = status;
            ViewBag.Statuses = Enum.GetValues<ContractStatus>();

            return View(await query.ToListAsync());
        }

        public IActionResult Create()
        {
            ViewBag.Clients = new SelectList(_context.Clients, "Id", "Name");
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Contract contract, IFormFile? agreementFile)
        {
            string? errorMessage = null;

            if (ModelState.IsValid)
            {
                // File Handling
                if (agreementFile != null && agreementFile.Length > 0)
                {
                    if (Path.GetExtension(agreementFile.FileName).ToLower() != ".pdf")
                    {
                        errorMessage = "Only PDF files are accepted.";
                        ModelState.AddModelError("SignedAgreementPath", errorMessage);
                    }
                    else
                    {
                        var uploadsDir = Path.Combine(_env.WebRootPath, "uploads", "agreements");
                        Directory.CreateDirectory(uploadsDir);
                        var fileName = $"{Guid.NewGuid()}_{Path.GetFileName(agreementFile.FileName)}";
                        var filePath = Path.Combine(uploadsDir, fileName);

                        using var stream = new FileStream(filePath, FileMode.Create);
                        await agreementFile.CopyToAsync(stream);

                        contract.SignedAgreementPath = $"/uploads/agreements/{fileName}";
                    }
                }

                if (string.IsNullOrEmpty(errorMessage))
                {
                    _context.Contracts.Add(contract);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }

            ViewBag.Clients = new SelectList(_context.Clients, "Id", "Name", contract.ClientId);
            return View(contract);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var contract = await _context.Contracts.FindAsync(id);
            if (contract == null) return NotFound();
            ViewBag.Clients = new SelectList(_context.Clients, "Id", "Name", contract.ClientId);
            return View(contract);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Contract contract, IFormFile? agreementFile)
        {
            if (id != contract.Id) return BadRequest();
            string? errorMessage = null;

            if (ModelState.IsValid)
            {
                if (agreementFile != null && agreementFile.Length > 0)
                {
                    if (Path.GetExtension(agreementFile.FileName).ToLower() != ".pdf")
                    {
                        errorMessage = "Only PDF files are accepted.";
                        ModelState.AddModelError("SignedAgreementPath", errorMessage);
                    }
                    else
                    {
                        var uploadsDir = Path.Combine(_env.WebRootPath, "uploads", "agreements");
                        Directory.CreateDirectory(uploadsDir);
                        var fileName = $"{Guid.NewGuid()}_{Path.GetFileName(agreementFile.FileName)}";
                        var filePath = Path.Combine(uploadsDir, fileName);

                        using var stream = new FileStream(filePath, FileMode.Create);
                        await agreementFile.CopyToAsync(stream);

                        contract.SignedAgreementPath = $"/uploads/agreements/{fileName}";
                    }
                }

                if (string.IsNullOrEmpty(errorMessage))
                {
                    _context.Update(contract);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }

            ViewBag.Clients = new SelectList(_context.Clients, "Id", "Name", contract.ClientId);
            return View(contract);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var contract = await _context.Contracts.FindAsync(id);
            if (contract != null) { _context.Contracts.Remove(contract); await _context.SaveChangesAsync(); }
            return RedirectToAction(nameof(Index));
        }
    }
}