using CandidateTesting.ThiagoCardosoBarbosaCunha.DBCCompany.Domain.Contracts.Infra;
using CandidateTesting.ThiagoCardosoBarbosaCunha.DBCCompany.Domain.Contracts.Services;
using CandidateTesting.ThiagoCardosoBarbosaCunha.DBCCompany.Domain.Services;
using CandidateTesting.ThiagoCardosoBarbosaCunha.DBCCompany.Model.Request;
using CandidateTesting.ThiagoCardosoBarbosaCunha.DBCCompany.Model.Response;
using CandidateTesting.ThiagoCardosoBarbosaCunha.DBCCompany.UnitTest.Fixtures;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using System.Text;
using System.Threading.Tasks;
using static CandidateTesting.ThiagoCardosoBarbosaCunha.DBCCompany.UnitTest.Fixtures.DataFixtures;

namespace CandidateTesting.ThiagoCardosoBarbosaCunha.DBCCompany.UnitTest.Services
{
    public class ReportServiceTest
    {
        private IReportService _reportService;

        [SetUp]
        public void Setup() 
        {
            _reportService = new ReportService();
        }

        [Test]
        public async Task ShouldBeDefinedResult()
        {
            var retrived = GetRetrivedDataModel(10, 10, 10);

            var result = await _reportService.GetStats(retrived);

            result.MostExpensive.Should().Be(_reportService.GetMostExpensive(retrived.SalesData));
            result.WorseSalesman.Should().Be(_reportService.GetWorseSalesman(retrived.SalesData));
            result.CustomerCount.Should().Be(_reportService.GetCustomerLength(retrived.Custumers));
            result.SalesmanCount.Should().Be(_reportService.GetSalesmanLength(retrived.Salesmans));
        }
    }
}
