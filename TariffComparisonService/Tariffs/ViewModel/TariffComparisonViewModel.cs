using System;
namespace Tariffs.ViewModel
{
	public class TariffComparisonModel
	{
		public string TariffName { get; set; } = string.Empty;
        public decimal AnnualCosts { get; set; }
    }

    public class TariffComparisonViewModel
    {
        public string TariffName { get; set; } = string.Empty;
        public string AnnualCosts { get; set; } = string.Empty;
    }
}

