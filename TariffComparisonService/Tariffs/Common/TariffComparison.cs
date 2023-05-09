

namespace Tariffs.Common
{

    public abstract class TariffComparison
    {
        // TODO - if the products list grow bigger it's better to use float/double as decimal can be slow in performance
        public string Name { get; set; }
        public decimal AdditionalConsumptionPerkWh { get; set; }
        public decimal BaseCost { get; set; }
        public abstract decimal CalculateAnnualConsumption(decimal consumption);
    }

}