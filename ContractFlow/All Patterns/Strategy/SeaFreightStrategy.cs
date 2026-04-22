using ContractMS.Models;

namespace ContractFlow.Strategy
{
    public class SeaFreightStrategy : IRouteStrategy
    {
        public decimal CalculateCost(ServiceRequest request)
        {
            // Sea freight is slower but cheaper
            return request.Cost * 0.8m;
        }

        public string GetRouteDescription() => "Sea Freight Route";
    }
}
