using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Primitives;

namespace Chocolate.AspNetCore.Configuration.Consul
{
    internal interface IConsulConfigurationClient
    {
        Task<byte[]> GetConfig(bool optional);

        IChangeToken Watch(Action<ConsulWatchExceptionContext> onException);
    }
}