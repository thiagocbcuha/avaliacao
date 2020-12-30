using CandidateTesting.ThiagoCardosoBarbosaCunha.DBCCompany.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CandidateTesting.ThiagoCardosoBarbosaCunha.DBCCompany.Domain.Contracts.Services
{
    public interface IFileProcessorService
    {
        Task<RetrievedDataModel> ExecuteProcess(string pathIn);
    }
}
