﻿using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace MicroserviceOne.Domain.ServiceBus
{
    public class ServiceBusSender : IServiceBusSender
    {
        /// <summary>
        /// Configuration
        /// </summary>
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Topic client
        /// </summary>
        private readonly TopicClient _topicClient;

        /// <summary>
        /// Topic name
        /// </summary>
        private const string TOPIC_NAME = "Test_Topic";

        /// <summary>
        /// C'tor
        /// </summary>
        public ServiceBusSender( IConfiguration configuration )
        {
            if ( null == configuration )
                throw new ArgumentNullException( nameof( configuration ) );

            _configuration = configuration;

            _topicClient = new TopicClient( _configuration [ "ServiceBusConnection" ], TOPIC_NAME );
        }

        /// <summary>
        /// Send a message
        /// </summary>
        /// <param name="payload"></param>
        /// <returns></returns>
        public async Task SendMessage( dynamic payload )
        {
            string data = JsonConvert.SerializeObject( payload );
            Message message = new Message( Encoding.UTF8.GetBytes( data ) );

            try
            {
                await _topicClient.SendAsync( message );
            }
            catch
            {
                throw;
            }
        }
    }
}
