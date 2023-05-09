using Tariffs.Common;

namespace Tariffs.Products
{
    public class ProductA : TariffComparison
    {
        public ProductA()
        {
            Name = "Basic Electricity Tariff";
            AdditionalConsumptionPerkWh  = 0.22M;
            BaseCost  = 5;
        }
        public override decimal CalculateAnnualConsumption(decimal consumption)
        {
            // base costs per month 5 + consumption costs 22 cent/kWh;
           return (BaseCost * 12) + (consumption * AdditionalConsumptionPerkWh);
        }
    }
}

