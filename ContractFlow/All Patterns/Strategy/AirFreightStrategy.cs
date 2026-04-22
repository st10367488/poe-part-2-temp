using ContractMS.Models;

namespace ContractFlow.Strategy
{
    public class AirFreightStrategy : IRouteStrategy
    {
        public decimal CalculateCost(ServiceRequest request)
        {
            // Air freight is faster but more expensive.
            return request.Cost * 1.5m; 
        }

        public string GetRouteDescription() => "Air Freight Route";
    }
}
