using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cardgamesAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace cardgamesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class cardsController : ControllerBase
    {

        private readonly ILogger<cardsController> _logger;

        public cardsController(ILogger<cardsController> logger)
        {
            _logger = logger;
        }

        private void header()
        {
            ControllerContext.HttpContext
                  .Response
                  .Headers
                  .Add("Access-Control-Allow-Origin", "http://localhost:4200");

            ControllerContext.HttpContext
            .Response
            .Headers
            .Add("Access-Control-Allow-Methods", "GET, POST, OPTIONS");

            ControllerContext.HttpContext
            .Response
            .Headers
            .Add("Access-Control-Allow-Headers", "Origin, Content-Type, Accept");

        }
 

        [HttpGet("cardsPlayer")]
        public List<cardsPlayers> Get(string id)
        {
            this.header();
            
            List<cardsPlayers> b = new card().shufflePlayers();

            return b;
        }
    }
}