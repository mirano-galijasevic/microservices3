using System;
using System.Threading.Tasks;
using MicroserviceOne.Domain.DomainServices;
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
        /// SomeService
        /// </summary>
        private readonly ISomeService _someService;

        /// <summary>
        /// C'tor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="configuration"></param>
        public TestController( ILogger<TestController> logger, IConfiguration configuration, ISomeService someService )
        {
            _logger = logger;

            if ( null == configuration )
                throw new ArgumentNullException( nameof( configuration ) );

            _configuration = configuration;

            if ( null == someService )
                throw new ArgumentNullException( nameof( someService ) );

            _someService = someService;
        }

        [HttpGet]
        public async Task<ActionResult<string>> Get()
        {
            string version = _configuration [ "Version" ] ?? "0.0";

            // Just simulate some real work done in the domain model, and its services
            await _someService.Run();

            return new JsonResult( $"MicroserviceOne.TestController, v. {version}, machine: {Environment.MachineName}" );
        }
    }
}
