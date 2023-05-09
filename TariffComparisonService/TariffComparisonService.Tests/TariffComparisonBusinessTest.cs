
using Moq;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using TariffComparison.Api.Controllers;
using Business.Interface;
using Microsoft.AspNetCore.Http;
using Tariffs.Products;
using Tariffs.ViewModel;
using Business.Service;
using System.Globalization;

namespace TariffComparisonService.Tests;


[TestClass]
public class TariffComparisonBusinessTest
{
    private readonly ITariffComparisonBusiness _service = new TariffComparisonBusiness();
    private readonly ProductA _productA = new ProductA();
    private readonly ProductB _productB = new ProductB();

    [TestMethod]
    public void ListOfTariffPer4500Kwh()
    {
        const decimal consumption = 4500;
        // Act
        var actualResult = _service.GetAllTariffs(consumption);

        var expectedList = new List<TariffComparisonModel>();

        var productAModel = new TariffComparisonModel()
        {
            TariffName = _productA.Name,
            AnnualCosts = _productA.CalculateAnnualConsumption(consumption)
        };
        expectedList.Add(productAModel);
        var productBModel = new TariffComparisonModel()
        {
            TariffName = _productB.Name,
            AnnualCosts = _productB.CalculateAnnualConsumption(consumption)
        };
        expectedList.Add(productBModel);

        var cultureInfo = CultureInfo.GetCultureInfo("de-De");
        var expectedSortedList = expectedList.OrderBy(l => l.AnnualCosts)
             .Select(p =>
                new TariffComparisonViewModel()
                {
                    TariffName = p.TariffName,
                    AnnualCosts = string.Format(cultureInfo, "{0:C}", p.AnnualCosts)
                }).ToList();
        // Assert
        Assert.AreEqual(expectedSortedList[0].TariffName, productBModel.TariffName);
        Assert.AreEqual(expectedSortedList[0].AnnualCosts, string.Format(cultureInfo, "{0:C}", productBModel.AnnualCosts));
        Assert.AreEqual(expectedSortedList[1].TariffName, productAModel.TariffName);
        Assert.AreEqual(expectedSortedList[1].AnnualCosts, string.Format(cultureInfo, "{0:C}", productAModel.AnnualCosts));

    }

}
