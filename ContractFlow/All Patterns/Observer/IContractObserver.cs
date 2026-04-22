using ContractMS.Models;

namespace ContractMS.Observer
{
    public interface IContractObserver
    {
        void Update(Contract contract);
    }
}
