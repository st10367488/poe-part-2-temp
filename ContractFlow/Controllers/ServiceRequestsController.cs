using ContractMS.Data;
using ContractMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;

namespace ContractMS.Controllers
{
    public class ServiceRequestsController : Controller
    {
        private readonly AppDbContext _context;
        public ServiceRequestsController(AppDbContext context) => _context = context;

        public async Task<IActionResult> Index()
            => View(await _context.ServiceRequests
                .Include(sr => sr.Contract).ThenInclude(c => c!.Client)
                .ToListAsync());

        public IActionResult Create()
        {
            // Only show contracts that are Active — workflow logic guard in the UI
            var activeContracts = _context.Contracts
                .Include(c => c.Client)
                .Where(c => c.Status == ContractStatus.Active)
                .Select(c => new { c.Id, Display = $"{c.Client!.Name} — {c.ServiceLevel}" })
                .ToList();

            ViewBag.Contracts = new SelectList(activeContracts, "Id", "Display");
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ServiceRequest request)
        {
            // ── Workflow Logic: block creation if contract is Expired or OnHold ──
            var contract = await _context.Contracts.FindAsync(request.ContractId);

            if (contract == null)
            {
                ModelState.AddModelError("ContractId", "Contract not found.");
            }
            else if (contract.Status == ContractStatus.Expired ||
                     contract.Status == ContractStatus.OnHold)
            {
                ModelState.AddModelError("ContractId",
                    $"Cannot create a Service Request for a contract with status '{contract.Status}'. " +
                    "Only Active contracts are allowed.");
            }

            if (!ModelState.IsValid)
            {
                var activeContracts = _context.Contracts
                    .Include(c => c.Client)
                    .Where(c => c.Status == ContractStatus.Active)
                    .Select(c => new { c.Id, Display = $"{c.Client!.Name} — {c.ServiceLevel}" })
                    .ToList();
                ViewBag.Contracts = new SelectList(activeContracts, "Id", "Display", request.ContractId);
                return View(request);
            }

            _context.ServiceRequests.Add(request);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var sr = await _context.ServiceRequests.FindAsync(id);
            if (sr != null) { _context.ServiceRequests.Remove(sr); await _context.SaveChangesAsync(); }
            return RedirectToAction(nameof(Index));
        }
    }
}