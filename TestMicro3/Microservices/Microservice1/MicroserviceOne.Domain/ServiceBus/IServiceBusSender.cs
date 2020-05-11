using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MicroserviceOne.Domain.ServiceBus
{
    public interface IServiceBusSender
    {
        Task SendMessage( dynamic payload );
    }
}
