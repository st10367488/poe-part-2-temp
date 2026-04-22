using ContractMS.Data;
using ContractMS.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContractFlow.Repository
{
    public class ConcreteContractRepository : IContractRepository
    {
        private readonly AppDbContext _context;

        public ConcreteContractRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Contract>> GetAllAsync()
        {
            return await _context.Contracts.Include(c => c.Client).ToListAsync();
        }

        public async Task<Contract?> GetByIdAsync(int id)
        {
            return await _context.Contracts.Include(c => c.Client).FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task AddAsync(Contract contract)
        {
            await _context.Contracts.AddAsync(contract);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Contract contract)
        {
            _context.Contracts.Update(contract);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var contract = await _context.Contracts.FindAsync(id);
            if (contract != null)
            {
                _context.Contracts.Remove(contract);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Contract>> GetAllExpiring(int days = 30)
        {
            var cutoffDate = DateTime.Now.AddDays(days);
            return await _context.Contracts
                .Include(c => c.Client)
                .Where(c => c.EndDate <= cutoffDate && c.EndDate > DateTime.Now)
                .ToListAsync();
        }
    }
}
