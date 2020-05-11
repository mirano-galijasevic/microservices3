using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroserviceTwo.Services
{
    public class MessageProcessor : IProcessor
    {
        /// <summary>
        /// Service bus sender
        /// </summary>
        private readonly IServiceBusSender _serviceBusSender;

        /// <summary>
        /// C'tor
        /// </summary>
        /// <param name="serviceBusSender"></param>
        public MessageProcessor( IServiceBusSender serviceBusSender )
        {
            if ( serviceBusSender == null )
                throw new ArgumentNullException( nameof( serviceBusSender ) );

            _serviceBusSender = serviceBusSender;
        }

        /// <summary>
        /// Process received data
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task ProcessData( dynamic data )
        {
            await _serviceBusSender.SendMessage( data );
        }
    }
}
