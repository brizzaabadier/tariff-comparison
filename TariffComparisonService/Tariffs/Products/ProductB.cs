using Tariffs.Common;

namespace Tariffs.Products
{
    public class ProductB : TariffComparison
    {
        private decimal _maximumConsumption = 4000;

        public ProductB()
        {
            Name = "Packaged Tariff";
            AdditionalConsumptionPerkWh = 0.30M;
            BaseCost = 800;
        }
        public override decimal CalculateAnnualConsumption(decimal consumption)
        {
            // 800 euro up to 400kWH/year and above 400kWh/ year additionally 30 cent per kWh
            if (consumption < _maximumConsumption)
            {
                return BaseCost;
            }

            return BaseCost + (consumption - _maximumConsumption) * AdditionalConsumptionPerkWh;
        }
    }
}

