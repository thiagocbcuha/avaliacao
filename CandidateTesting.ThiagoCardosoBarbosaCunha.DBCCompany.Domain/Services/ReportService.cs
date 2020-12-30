using CandidateTesting.ThiagoCardosoBarbosaCunha.DBCCompany.Domain.Contracts.Infra;
using CandidateTesting.ThiagoCardosoBarbosaCunha.DBCCompany.Domain.Contracts.Services;
using CandidateTesting.ThiagoCardosoBarbosaCunha.DBCCompany.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CandidateTesting.ThiagoCardosoBarbosaCunha.DBCCompany.Domain.Extension;
using System.Linq;
using CandidateTesting.ThiagoCardosoBarbosaCunha.DBCCompany.Model.Request;
using Microsoft.Extensions.Configuration;

namespace CandidateTesting.ThiagoCardosoBarbosaCunha.DBCCompany.Domain.Services
{
    public class ReportService : IReportService
    {
        public async Task<FileLine> GetStats(RetrievedDataModel retrived)
        {
            try
            {
                var fileLine = new FileLine();
                if (retrived != null)
                {
                    // Quantidade de vendedores
                    fileLine.SalesmanCount = GetSalesmanLength(retrived.Salesmans);

                    // Quantidade de clientes
                    fileLine.CustomerCount = GetCustomerLength(retrived.Custumers);

                    // Id da venda mais cara
                    fileLine.MostExpensive = GetMostExpensive(retrived.SalesData);

                    // Nome do pior vendedor
                    fileLine.WorseSalesman = GetWorseSalesman(retrived.SalesData);
                }

                return await Task.FromResult(fileLine);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public int GetSalesmanLength(IEnumerable<SalesmanModel> salesmen) 
        {
            return salesmen.Count();
        }

        public int GetCustomerLength(IEnumerable<CustomerModel> customers)
        {
            return customers.Count();
        }

        public int GetMostExpensive(IEnumerable<SalesDataModel> salesData)
        {
            return salesData
               .Select(i => i.Sales.OrderByDescending(ii => ii.Price).First())
               .OrderByDescending(i => i.Price)
               .First().ItemID;
        }

        public string GetWorseSalesman(IEnumerable<SalesDataModel> salesData)
        {
            return salesData
                .Select(i => new { i.SalesmanName, Profit = i.Sales.Select(ii => ii.Quantity * ii.Price).Sum() })
                .OrderBy(i => i.Profit)
                .First().SalesmanName;
        }
    }
}
