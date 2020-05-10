using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace MicroserviceOne.Controllers
{
    [ApiController]
    [Route( "[controller]" )]
    public class TestController : ControllerBase
    {
        /// <summary>
        /// Class logger
        /// </summary>
        private readonly ILogger<TestController> _logger;

        /// <summary>
        /// Configuration
        /// </summary>
        private readonly IConfiguration _configuration;

        /// <summary>
        /// C'tor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="configuration"></param>
        public TestController( ILogger<TestController> logger, IConfiguration configuration )
        {
            _logger = logger;

            if ( null == configuration )
                throw new ArgumentNullException( nameof( configuration ) );

            _configuration = configuration;
        }

        [HttpGet]
        public ActionResult<string> Get()
        {
            string version = _configuration [ "Version" ] ?? "0.0";

            return new JsonResult( $"MicroserviceOne.TestController, v. {version}, machine: {Environment.MachineName}" );
        }
    }
}
