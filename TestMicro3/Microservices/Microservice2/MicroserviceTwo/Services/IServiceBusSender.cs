using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroserviceTwo.Services
{
    public interface IServiceBusSender
    {
        Task SendMessage( dynamic payload );
    }
}
