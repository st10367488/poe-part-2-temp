using ContractMS.Models;

namespace ContractFlow.Strategy
{
    public class RouteOptimisationContext
    {
        private IRouteStrategy _strategy;

        public RouteOptimisationContext(IRouteStrategy strategy)
        {
            _strategy = strategy;
        }

        public void SetStrategy(IRouteStrategy strategy)
        {
            _strategy = strategy;
        }

        public decimal ExecuteCalculation(ServiceRequest request)
        {
            return _strategy.CalculateCost(request);
        }

        public string GetCurrentRouteInfo()
        {
            return _strategy.GetRouteDescription();
        }
    }
}
