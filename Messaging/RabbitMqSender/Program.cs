using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using RabbitMqSender.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace RabbitMqSender
{
    public class Program
    {
        static void Main(string[] args)
        {
            IConfigurationBuilder builder = new ConfigurationBuilder();
            IConfiguration configuration = builder.AddStandardProviders().Build();

            Send.SendMessage();

            
        }

        //public static IHostBuilder CreateHostBuilder(string[] args)
        //{
        //    Host.CreateDefaultBuilder(args)
        //        .ConfigureWebHostDefaults(webBuilder =>
        //        {
        //            webBuilder.UseStartup<Startup>();
        //        });
        //}
    }
}
