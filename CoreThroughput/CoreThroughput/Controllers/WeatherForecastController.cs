﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreThroughput.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            var lExit = Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();

            lExit[0].Summary = ".NET 5 Version #3";

            return lExit;
        }

        // GET: WeatherForecast/SystemDetails
        [HttpGet]
        public IActionResult SystemDetails()
        {
            IActionResult returnValue = null;

            try
            {
                var lSystemInfo = $"Processor count: {Environment.ProcessorCount}, OS: {Environment.OSVersion}";
                returnValue = new OkObjectResult(lSystemInfo);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Could not SystemDetails. Exception thrown: {ex.Message}");

                returnValue = new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }

            return returnValue;
        }
    }
}
