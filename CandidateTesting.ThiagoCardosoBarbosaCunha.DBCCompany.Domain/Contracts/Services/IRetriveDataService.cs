using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CandidateTesting.ThiagoCardosoBarbosaCunha.DBCCompany.Domain.Contracts.Services
{
    public interface IRetrieveDataService
    {
        Task<IEnumerable<string>> GetData(string path);
    }
}
