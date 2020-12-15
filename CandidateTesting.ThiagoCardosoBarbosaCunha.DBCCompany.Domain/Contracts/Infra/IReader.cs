using CandidateTesting.ThiagoCardosoBarbosaCunha.DBCCompany.Model.Request;
using CandidateTesting.ThiagoCardosoBarbosaCunha.DBCCompany.Model.Response;
using System;
using System.Threading.Tasks;

namespace CandidateTesting.ThiagoCardosoBarbosaCunha.DBCCompany.Domain.Contracts.Infra
{
    public interface IReader
    {
        Task<FileResponse> GetData(FileRequest fileRequest);
    }
}
