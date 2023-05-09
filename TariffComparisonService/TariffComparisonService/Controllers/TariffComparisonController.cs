using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using System.Collections.Generic;
using System.IO;
using System;
using Microsoft.AspNetCore.Http.HttpResults;
using Business.Interface;
using Tariffs.ViewModel;

namespace TariffComparison.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class TariffComparisonController : ControllerBase
{
    private readonly ILogger<TariffComparisonController> _logger;
    private readonly ITariffComparisonBusiness _service;
    public TariffComparisonController(ITariffComparisonBusiness service, ILogger<TariffComparisonController> logger)
    {
        _logger = logger;
        _service = service;
    }


    /// <summary>
    /// Returns a list of tariffs with order of prices from low to high depending on the given consumption per year
    ///</summary>
    /// <param name="consumption">The total electricity consumption in kilowatt hours per year</param>
    [HttpGet]
    [Route("/{consumption}")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(BadRequest), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(IEnumerable<TariffComparisonViewModel>), StatusCodes.Status200OK)]
    [EndpointName("Returns a list of tariffs with order of prices from low to high depending on the given consumption per year")]
    public IActionResult GetAllTariff([FromRoute] decimal consumption)
    {
        // TODO - if it has to be extended for changing the interval of calcuation from annual to monthly, and so on,
        // it is best to put the consumtion as body param
        try
        {
            if (consumption < 0)
            {
                return BadRequest("Consumption should be a postive number");
            }

            return Ok(_service.GetAllTariffs(consumption));
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unable to get the tariff comparison with the given consumption - {consumption} kwH", ex);
            return StatusCode(500, ex);
        }
     }
}

