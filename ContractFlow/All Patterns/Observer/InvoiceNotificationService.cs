using System;
using ContractMS.Models;
using ContractMS.Observer;

namespace ContractFlow.Observer
{
    public class InvoiceNotificationService : IContractObserver
    {
        public void Update(Contract contract)
        {
            if (contract.Status == ContractStatus.Active)
            {
                Console.WriteLine($"[Invoice Service] Contract {contract.Id} is active. Generating initial invoices...");
            }
        }
    }
}
