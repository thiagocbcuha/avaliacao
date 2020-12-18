using CandidateTesting.ThiagoCardosoBarbosaCunha.DBCCompany.Domain.Contracts.Factory;
using CandidateTesting.ThiagoCardosoBarbosaCunha.DBCCompany.Domain.Contracts.Infra;
using CandidateTesting.ThiagoCardosoBarbosaCunha.DBCCompany.Domain.Contracts.Services;
using CandidateTesting.ThiagoCardosoBarbosaCunha.DBCCompany.Domain.Services;
using CandidateTesting.ThiagoCardosoBarbosaCunha.DBCCompany.Infra;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace CandidateTesting.ThiagoCardosoBarbosaCunha.DBCCompany.IoC
{
    public static class DependencyInjections
    {
        public static IServiceCollection AddDependecy(this IServiceCollection services) 
        {
            services.AddInfra();
            services.AddServices();

            return services;
        }

        private static IServiceCollection AddServices(this IServiceCollection services) 
        {
            services.AddScoped<IBuilderSplitter, Domain.Factory.BuilderSplitter>();
            services.AddScoped<IReportService, ReportService>();
            services.AddScoped<IFileProcessorService, FileProcessorService>();
            services.AddScoped<IRetrieveDataService, RetrievedDataService>();
            services.AddScoped<IInputOutputDataService, InputOutputDataService>();

            return services;
        }

        private static IServiceCollection AddInfra(this IServiceCollection services) 
        {
            services.AddScoped<IReader, FileRepository>();
            services.AddScoped<IWriter, FileRepository>();

            return services;
        }
    }
}
