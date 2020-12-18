using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CandidateTesting.ThiagoCardosoBarbosaCunha.DBCCompany.Domain.Contracts.Services
{
    public interface IInputOutputDataService
    {
        Task<bool> Execute(string pathIn, string pathOut);
    }
}
