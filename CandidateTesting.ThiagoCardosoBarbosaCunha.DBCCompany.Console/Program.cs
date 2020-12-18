using CandidateTesting.ThiagoCardosoBarbosaCunha.DBCCompany.Domain.Contracts.Services;
using CandidateTesting.ThiagoCardosoBarbosaCunha.DBCCompany.IoC;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;

namespace CandidateTesting.ThiagoCardosoBarbosaCunha.DBCCompany.Console
{
    class Program
    {
        static Task Main(string[] args)
        {
            using IHost host = CreateHostBuilder(args).Build();

            using IServiceScope serviceScope = host.Services.CreateScope();
            IServiceProvider provider = serviceScope.ServiceProvider;
            provider.GetRequiredService<IInputOutputDataService>().Execute(args[0], args[1]);

            return host.RunAsync();
        }

        static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args).
                ConfigureServices((_, services) => services.AddDependecy()).
                ConfigureAppConfiguration((_, config) => {
                    config.AddJsonFile("appsettings.json", optional: true);
                    config.AddEnvironmentVariables();
                });
    }
}
