using ContractMS.Models;

namespace ContractFlow.Strategy
{
    public interface IRouteStrategy
    {
        decimal CalculateCost(ServiceRequest request);
        string GetRouteDescription();
    }
}
