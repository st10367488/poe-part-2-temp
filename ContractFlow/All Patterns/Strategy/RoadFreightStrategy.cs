using ContractMS.Models;

namespace ContractFlow.Strategy
{
    public class RoadFreightStrategy : IRouteStrategy
    {
        public decimal CalculateCost(ServiceRequest request)
        {
            // Road freight standard cost
            return request.Cost * 1.0m;
        }

        public string GetRouteDescription() => "Road Freight Route";
    }
}
