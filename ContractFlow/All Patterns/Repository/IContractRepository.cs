using ContractMS.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContractFlow.Repository
{
    public interface IContractRepository
    {
        Task<IEnumerable<Contract>> GetAllAsync();
        Task<Contract?> GetByIdAsync(int id);
        Task AddAsync(Contract contract);
        Task UpdateAsync(Contract contract);
        Task DeleteAsync(int id);
        Task<List<Contract>> GetAllExpiring(int days = 30);
    }
}
