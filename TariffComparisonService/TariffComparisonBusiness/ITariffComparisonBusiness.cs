using System;
using Tariffs.ViewModel;

namespace Business.Interface
{
	public interface ITariffComparisonBusiness
    {
		IEnumerable<TariffComparisonViewModel> GetAllTariffs(decimal consumption);
    }
}

