using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace MicroserviceTwo.Services
{
    public class ServiceBusSubscriber : IServiceBusSubscriber
    {
        /// <summary>
        /// Configuration
        /// </summary>
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Processor
        /// </summary>
        private readonly IProcessor _processor;

        /// <summary>
        /// Topic name
        /// </summary>
        private readonly string _topicName;

        /// <summary>
        /// Subscription name
        /// </summary>
        private readonly string _subscriptionName;

        /// <summary>
        /// Subscription client
        /// </summary>
        private readonly SubscriptionClient _subscriptionClient;

        /// <summary>
        /// C'tor
        /// </summary>
        public ServiceBusSubscriber( IConfiguration configuration, IProcessor processor )
        {
            _configuration = configuration;
            _processor = processor;

            _topicName = _configuration [ "TopicName" ];
            _subscriptionName = _configuration [ "SubscriptionName" ];

            _subscriptionClient = new SubscriptionClient(
                _configuration [ "ServiceBusConnection" ], _topicName, _subscriptionName );
        }

        /// <summary>
        /// Register message handler
        /// </summary>
        public void RegisterMessageHandler()
        {
            _subscriptionClient.RegisterMessageHandler( ProcessMessages,
                new MessageHandlerOptions( ExceptionReceivedHandler ) { AutoComplete = false, MaxConcurrentCalls = 1 } );
        }

        /// <summary>
        /// Process messages
        /// </summary>
        /// <param name="message"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        private async Task ProcessMessages( Message message, CancellationToken token )
        {
            var data = JsonConvert.DeserializeObject<dynamic>( Encoding.UTF8.GetString( message.Body ) );
            await _processor.ProcessData( data );

            await _subscriptionClient.CompleteAsync( message.SystemProperties.LockToken );
        }

        /// <summary>
        /// Error occured
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        private Task ExceptionReceivedHandler( ExceptionReceivedEventArgs args )
        {
            var context = args.ExceptionReceivedContext;
            // log this somewhere....

            return Task.CompletedTask;
        }

        /// <summary>
        /// Close this subscriber
        /// </summary>
        /// <returns></returns>
        public async Task CloseSubscriber()
        {
            await _subscriptionClient.CloseAsync();
        }
    }
}
