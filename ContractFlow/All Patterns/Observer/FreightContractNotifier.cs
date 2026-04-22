using System;
using ContractMS.Models;

namespace ContractMS.Observer
{
    public class FreightContractNotifier : IContractObserver
    {
        public void Update(Contract contract)
        {
            if (contract.Status == ContractStatus.Active)
            {
                Console.WriteLine($"[Freight Notifier] Contract {contract.Id} is active. Preparing freight scheduling logistics...");
            }
        }
    }
}
