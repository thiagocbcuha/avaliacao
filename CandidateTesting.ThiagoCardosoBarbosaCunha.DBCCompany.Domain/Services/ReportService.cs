using CandidateTesting.ThiagoCardosoBarbosaCunha.DBCCompany.Domain.Contracts.Infra;
using CandidateTesting.ThiagoCardosoBarbosaCunha.DBCCompany.Domain.Contracts.Services;
using CandidateTesting.ThiagoCardosoBarbosaCunha.DBCCompany.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CandidateTesting.ThiagoCardosoBarbosaCunha.DBCCompany.Domain.Extension;
using System.Linq;
using CandidateTesting.ThiagoCardosoBarbosaCunha.DBCCompany.Model.Request;
using Microsoft.Extensions.Configuration;

namespace CandidateTesting.ThiagoCardosoBarbosaCunha.DBCCompany.Domain.Services
{
    public class ReportService : IReportService
    {
        private readonly string _format;
        private readonly IWriter _writer;
        private readonly ITransformerService _transformerService;

        public ReportService(IConfiguration configuration, IWriter writer, ITransformerService transformerService)
        {
            _writer = writer;
            _transformerService = transformerService;
            _format = configuration.GetSection("FormatToFile").Value;
        }

        public async Task<bool> GetStats(string pathIn, string pathOut)
        {
            var fileLine = new FileLine();
            var stringBuilder = new StringBuilder();

            if (await _transformerService.Execute(pathIn))
            {
                // Quantidade de vendedores
                fileLine.SalesmanCount = _transformerService.Salesmans.Count;

                // Quantidade de clientes
                fileLine.CustomerCount = _transformerService.Custumers.Count;

                // Id da venda mais cara
                fileLine.MostExpensive = _transformerService.SalesData
                    .Select(i => i.Sales.OrderByDescending(ii => ii.Price).First())
                    .OrderByDescending(i => i.Price)
                    .First().ItemID;

                // Nome do pior vendedor
                fileLine.WorseSalesman = _transformerService.SalesData
                    .Select(i => new { i.SalesmanName, Profit = i.Sales.Select(ii => ii.Quantity * ii.Price).Sum() })
                    .OrderBy(i => i.Profit)
                    .First().SalesmanName;

                stringBuilder.AppendLine(fileLine.GetDescription(_format));
                stringBuilder.AppendLine(fileLine.ToString(_format)); 
            }

            var logRequest = new LogRequest
            {
                Path = pathOut,
                Content = stringBuilder
            };

            var logResponse = await _writer.SaveData(logRequest);

            return logResponse.Result;
        }
    }
}
