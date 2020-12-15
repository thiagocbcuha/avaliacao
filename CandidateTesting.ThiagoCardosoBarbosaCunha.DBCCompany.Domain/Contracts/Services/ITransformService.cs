using CandidateTesting.ThiagoCardosoBarbosaCunha.DBCCompany.Domain.Factory.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CandidateTesting.ThiagoCardosoBarbosaCunha.DBCCompany.Domain.Contracts.Services
{
    public interface ITransformerService
    {
        List<CostumerModel> Custumers { get; }
        List<SalesmanModel> Salesmans { get; }
        List<SalesDataModel> SalesData { get; }
        Task<bool> Execute(string pathIn);
    }
}
