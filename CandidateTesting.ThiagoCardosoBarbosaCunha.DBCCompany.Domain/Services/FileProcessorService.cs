using CandidateTesting.ThiagoCardosoBarbosaCunha.DBCCompany.Domain.Contracts.Services;
using CandidateTesting.ThiagoCardosoBarbosaCunha.DBCCompany.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CandidateTesting.ThiagoCardosoBarbosaCunha.DBCCompany.Domain.Extension;
using Microsoft.Extensions.Configuration;
using CandidateTesting.ThiagoCardosoBarbosaCunha.DBCCompany.Domain.Contracts.Factory;

namespace CandidateTesting.ThiagoCardosoBarbosaCunha.DBCCompany.Domain.Services
{
    public class FileProcessorService : IFileProcessorService
    {
        private readonly string _separator;
        private readonly IBuilderSplitter _builderSplitter;
        private readonly IRetrieveDataService _retrieveDataService;

        public FileProcessorService(IConfiguration configuration, IBuilderSplitter builderSplitter, IRetrieveDataService retriveDataService)
        {
            _separator = "ç";
            _builderSplitter = builderSplitter;
            _retrieveDataService = retriveDataService;
        }
        public async Task<RetrievedDataModel> ExecuteProcess(string pathIn)
        {
            try
            {
                var retrievedDataModel = new RetrievedDataModel();
                var fileLines = new List<FileLine>();
                var lines = await _retrieveDataService.GetData(pathIn);

                foreach (var line in lines)
                {
                    var splitter = _builderSplitter.GetSplitter(line, _separator);
                    var result = await splitter.Extract();

                    if (result is CustomerModel)
                        retrievedDataModel.Custumers.Add((CustomerModel)result);

                    else if (result is SalesmanModel)
                        retrievedDataModel.Salesmans.Add((SalesmanModel)result);

                    else if (result is SalesDataModel)
                        retrievedDataModel.SalesData.Add((SalesDataModel)result);
                }

                return retrievedDataModel;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
