using System.Reflection;
using Tariffs.Common;
using Tariffs.Products;
using System.Collections.Generic;
using System.IO;
using System;
using Tariffs.ViewModel;
using Business.Interface;
using System.Globalization;

namespace Business.Service
{
    public class TariffComparisonBusiness : ITariffComparisonBusiness
    {
        public IEnumerable<TariffComparisonViewModel> GetAllTariffs(decimal consumption)
        {
            var tariffs = Assembly.Load("Tariffs").GetTypes().Where(t => t.IsSubclassOf(typeof(TariffComparison)) && !t.IsAbstract);
            IList<TariffComparisonModel> products = new List<TariffComparisonModel>();
            foreach (Type type in tariffs)
            {
                var instance = Activator.CreateInstance(type) as TariffComparison;
                var price = instance!.CalculateAnnualConsumption(consumption);

                products.Add(new TariffComparisonModel()
                {
                    TariffName = instance.Name,
                    AnnualCosts = Math.Round(price,2)
                });
            }

            var cultureInfo = CultureInfo.GetCultureInfo("de-De");
            return products
                .OrderBy(c => c.AnnualCosts)
                .Select(p =>
                    new TariffComparisonViewModel()
                        {
                            TariffName = p.TariffName,
                            AnnualCosts = string.Format(cultureInfo, "{0:C}", p.AnnualCosts)
                        });
        }
    }
}

