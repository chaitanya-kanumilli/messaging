using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace RabbitMqSender.Extensions
{
    internal static class ConfigurationExtensions
    {
        public static IConfigurationBuilder AddStandardProviders(this IConfigurationBuilder configurationBuilder) 
            => configurationBuilder.AddJsonFile("appSettings.json");
    }
}
