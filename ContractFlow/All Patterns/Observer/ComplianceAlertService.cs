using System;
using ContractMS.Models;

namespace ContractMS.Observer
{
    public class ComplianceAlertService : IContractObserver
    {
        public void Update(Contract contract)
        {
            if (contract.Status == ContractStatus.Expired)
            {
                Console.WriteLine($"[Compliance Alert] Warning: Contract {contract.Id} has expired. Compliance check required.");
            }
        }
    }
}
