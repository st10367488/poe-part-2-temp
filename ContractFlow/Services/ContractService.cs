using ContractFlow.Repository;
using ContractMS.Models;

namespace ContractMS.Services
{
    public class ContractService
    {
        private readonly IContractRepository _repository;

        public ContractService(IContractRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Contract>> GetExpiringContracts(int days = 30)
            => await _repository.GetAllExpiring(days);

        public async Task<Contract?> GetById(int id)
            => await _repository.GetByIdAsync(id);
    }
}