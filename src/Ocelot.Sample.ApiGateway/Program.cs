using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace Ocelot.Sample.ApiGateway
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
		        .ConfigureAppConfiguration((hostingContext, config) =>
		        {
			        config
				        .SetBasePath(hostingContext.HostingEnvironment.ContentRootPath)
				        .AddJsonFile("appsettings.json", true, true)
				        .AddJsonFile($"appsettings.{hostingContext.HostingEnvironment.EnvironmentName}.json", true, true)
				        .AddJsonFile("ocelot.json")
				        .AddEnvironmentVariables();
		        })
		        .ConfigureServices(s =>
		        {
			        s.AddOcelot();
		        })
		        .Configure(a =>
		        {
			        a.UseOcelot().Wait();
		        })
		        .Build();
	}
}
