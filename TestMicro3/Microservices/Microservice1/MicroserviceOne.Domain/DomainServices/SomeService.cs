using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MicroserviceOne.Domain.ServiceBus;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Serialization;

namespace MicroserviceOne.Domain.DomainServices
{
    public class SomeService : ISomeService
    {
        /// <summary>
        /// Service bus sender
        /// </summary>
        private readonly IServiceBusSender _serviceBusSender;

        /// <summary>
        /// C'tor
        /// </summary>
        /// <param name="serviceBusSender"></param>
        public SomeService( IServiceBusSender serviceBusSender )
        {
            if ( null == serviceBusSender )
                throw new ArgumentNullException( nameof( serviceBusSender ) );

            _serviceBusSender = serviceBusSender;
        }

        /// <summary>
        /// Run method
        /// </summary>
        /// <returns></returns>
        public async Task Run()
        {
            // Here we process some business rules....

            // Then we send domain events, if there is a need for that

            // And now we publish a message notifying other microservices of this, if anybody is interested
            await _serviceBusSender.SendMessage( new { Id = 1, Val1 = "value1", Val2 = "value2" } );
        }
    }
}
