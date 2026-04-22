using System;
using ContractMS.Models;

namespace ContractMS.Observer
{
    public class BoardDashboard : IContractObserver
    {
        public void Update(Contract contract)
        {
            // Simulate updating a dashboard
            Console.WriteLine($"[Board Dashboard] Contract {contract.Id} status is now {contract.Status}. Updating board metrics...");
        }
    }
}
