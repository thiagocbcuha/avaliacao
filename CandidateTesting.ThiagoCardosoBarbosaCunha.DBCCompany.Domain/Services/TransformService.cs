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
using CandidateTesting.ThiagoCardosoBarbosaCunha.DBCCompany.Domain.Factory.Model;

namespace CandidateTesting.ThiagoCardosoBarbosaCunha.DBCCompany.Domain.Services
{
    public class TransformerService : ITransformerService
    {
        private readonly string _separator;
        private readonly IBuilderSplitter _builderSplitter;
        private readonly IRetriveDataService _retriveDataService;

        public List<CostumerModel> Custumers { get; private set; } 
            = new List<CostumerModel>();
        public List<SalesmanModel> Salesmans { get; private set; } 
            = new List<SalesmanModel>();
        public List<SalesDataModel> SalesData { get; private set; } 
            = new List<SalesDataModel>();

        public TransformerService(IConfiguration configuration, IBuilderSplitter builderSplitter, IRetriveDataService retriveDataService)
        {
            _separator = "ç";
            _builderSplitter = builderSplitter;
            _retriveDataService = retriveDataService;
        }
        public async Task<bool> Execute(string pathIn)
        {
            try
            {
                var fileLines = new List<FileLine>();
                var lines = await _retriveDataService.GetData(pathIn);

                foreach (var line in lines)
                {
                    var splitter = _builderSplitter.GetSplitter(line, _separator);
                    var result = await splitter.Extract();

                    if (result is CostumerModel)
                        Custumers.Add((CostumerModel)result);

                    else if (result is SalesmanModel)
                        Salesmans.Add((SalesmanModel)result);

                    else if (result is SalesDataModel)
                        SalesData.Add((SalesDataModel)result);
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
