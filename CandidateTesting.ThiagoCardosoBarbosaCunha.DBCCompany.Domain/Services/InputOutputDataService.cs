using CandidateTesting.ThiagoCardosoBarbosaCunha.DBCCompany.Domain.Contracts.Infra;
using CandidateTesting.ThiagoCardosoBarbosaCunha.DBCCompany.Domain.Contracts.Services;
using CandidateTesting.ThiagoCardosoBarbosaCunha.DBCCompany.Domain.Extension;
using CandidateTesting.ThiagoCardosoBarbosaCunha.DBCCompany.Model.Request;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CandidateTesting.ThiagoCardosoBarbosaCunha.DBCCompany.Domain.Services
{
    public class InputOutputDataService : IInputOutputDataService
    {
        private readonly string _format;
        private readonly IWriter _writer;
        private readonly IReportService _reportService;
        private readonly IFileProcessorService _transformerService;

        public InputOutputDataService(IConfiguration configuration, IWriter writer, IFileProcessorService transformerService, IReportService reportService)
        {
            _writer = writer;
            _reportService = reportService;
            _transformerService = transformerService;
            _format = configuration.GetSection("FormatToFile").Value;
        }
        public async Task<bool> Execute(string pathIn, string pathOut)
        {
            try
            {
                var stringBuilder = new StringBuilder();

                // Processando arquivo de entrada.
                var retrived = await _transformerService.ExecuteProcess(pathIn);

                // Obtendo as estatísticas do arquivo de entrada.
                var fileLine = await _reportService.GetStats(retrived);

                stringBuilder.AppendLine(fileLine.GetDescription(_format));
                stringBuilder.AppendLine(fileLine.ToString(_format));

                var logRequest = new LogRequest
                {
                    Path = pathOut,
                    Content = stringBuilder
                };

                //Salvando arquivo de saída.
                var logResponse = await _writer.SaveData(logRequest);

                return logResponse.Result;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
