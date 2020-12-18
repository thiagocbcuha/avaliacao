using CandidateTesting.ThiagoCardosoBarbosaCunha.DBCCompany.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace CandidateTesting.ThiagoCardosoBarbosaCunha.DBCCompany.Domain.Contracts.Services
{
    public interface IReportService
    {
        Task<FileLine> GetStats(RetrivedDataModel retrived);
        int GetSalesmanLength(IEnumerable<SalesmanModel> salesmen);
        int GetCustomerLength(IEnumerable<CustomerModel> customers);
        int GetMostExpensive(IEnumerable<SalesDataModel> salesData);
        string GetWorseSalesman(IEnumerable<SalesDataModel> salesData);
    }
}
