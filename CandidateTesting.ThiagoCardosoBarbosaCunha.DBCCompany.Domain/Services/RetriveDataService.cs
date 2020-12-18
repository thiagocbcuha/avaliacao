using CandidateTesting.ThiagoCardosoBarbosaCunha.DBCCompany.Domain.Contracts.Infra;
using CandidateTesting.ThiagoCardosoBarbosaCunha.DBCCompany.Domain.Contracts.Services;
using CandidateTesting.ThiagoCardosoBarbosaCunha.DBCCompany.Model.Request;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateTesting.ThiagoCardosoBarbosaCunha.DBCCompany.Domain.Services
{
    public class RetrievedDataService : IRetrieveDataService
    {
        private readonly IReader _reader;
        private readonly string _defaultType;

        public RetrievedDataService(IReader reader, IConfiguration configuration)
        {
            _reader = reader;
            _defaultType = configuration.GetSection("defaultType").Value;
        }

        public async Task<IEnumerable<string>> GetData(string path)
        {
            var request = new FileRequest { Path = path, Type = _defaultType };
            var result = await _reader.GetData(request);

            if (result.Value != null)
                return result.Value.Split(Environment.NewLine).Where(i => !String.IsNullOrEmpty(i));

            return null;
        }
    }
}
