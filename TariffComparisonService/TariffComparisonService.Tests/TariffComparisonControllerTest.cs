
using Moq;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using TariffComparison.Api.Controllers;
using Business.Interface;
using Microsoft.AspNetCore.Http;

namespace TariffComparisonService.Tests;


[TestClass]
public class TariffComparissonControllerTest
{
    private TariffComparisonController _controller;
    private readonly ITariffComparisonBusiness _service = Mock.Of<ITariffComparisonBusiness>();
    private readonly ILogger<TariffComparisonController> _logger = Mock.Of<ILogger<TariffComparisonController>>();

    public TariffComparissonControllerTest()
    {
        _controller = new TariffComparisonController(_service, _logger);
    }

    [TestMethod]
    public void Get_WhenCalled_ReturnsOkResult()
    {
        // Act
        var okResult = _controller.GetAllTariff(4500);

        // Assert
        Assert.IsInstanceOfType(okResult,typeof(OkObjectResult));
    }

    [TestMethod]
    public void Get_WhenCalled_ReturnsBadResult()
    {
        // Act
        var badResult = _controller.GetAllTariff(-1);

        // Assert
        Assert.IsInstanceOfType(badResult, typeof(BadRequestObjectResult));
    }

    [TestMethod]
    public void Get_WhenCalled_ReturnsException()
    {
        var exceptionObj = new Exception("Something went wrong");
        var faultyService = new Mock<ITariffComparisonBusiness>();
        faultyService.Setup(s => s.GetAllTariffs(0)).Throws(exceptionObj);

        _controller = new TariffComparisonController(faultyService.Object, _logger);

        // Act
        var result = _controller.GetAllTariff(0);

        // Assert
        Assert.IsInstanceOfType(result, typeof(ObjectResult));
    }
}
