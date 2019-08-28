using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using basic_auth.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace basic_auth.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class RequiresAuthenticationController : ControllerBase
    {
        private readonly ILogger<RequiresAuthenticationController> _logger;

        public RequiresAuthenticationController(ILogger<RequiresAuthenticationController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<SalesFigures> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new SalesFigures
            {
                Date = DateTime.Now.AddDays(index),
                SalesMade = rng.Next(20),
                Profit = (decimal)rng.NextDouble() * 200
            })
            .ToArray();
        }
    }
}
