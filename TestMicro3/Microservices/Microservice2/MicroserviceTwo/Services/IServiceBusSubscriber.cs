using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroserviceTwo.Services
{
    public interface IServiceBusSubscriber
    {
        void RegisterMessageHandler();
        Task CloseSubscriber();
    }
}
