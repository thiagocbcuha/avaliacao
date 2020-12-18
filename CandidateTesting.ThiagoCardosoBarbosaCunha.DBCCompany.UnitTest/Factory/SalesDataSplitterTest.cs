using AutoFixture;
using CandidateTesting.ThiagoCardosoBarbosaCunha.DBCCompany.Domain.Contracts.Factory;
using CandidateTesting.ThiagoCardosoBarbosaCunha.DBCCompany.Domain.Factory;
using CandidateTesting.ThiagoCardosoBarbosaCunha.DBCCompany.Model;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateTesting.ThiagoCardosoBarbosaCunha.DBCCompany.UnitTest.Factory
{
    public class SalesDataSplitterTest
    {
        private Fixture _fixture;
        private ISplitter _salesDataSplitter;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture();
            _salesDataSplitter = new SalesDataSplitter();
        }

        [Test]
        public async Task ShouldReturnCorrectDataFromLine()
        {
            var separator = "ç";
            var salesmanModel = _fixture.Create<SalesDataModel>();

            var salesStr = "";
            foreach (var item in salesmanModel.Sales.Select(i => $"{ i.ItemID }-{ i.Quantity }-{ i.Price }"))
                salesStr += $"{ item },";

            var line = $"{ (int)salesmanModel.DataType }{ separator }" +
                $"{ salesmanModel.SalesID }{ separator }" +
                $"[{ salesStr.Substring(0, salesStr.Length - 1) }]{ separator }" + 
                $"{ salesmanModel.SalesmanName }";            

            _salesDataSplitter.LineToSplit = line;
            _salesDataSplitter.Separator = separator;

            var data = (SalesDataModel)await _salesDataSplitter.Extract();

            data.SalesID.Should().Be(salesmanModel.SalesID);
            data.DataType.Should().Be(salesmanModel.DataType);
            data.SalesmanName.Should().Be(salesmanModel.SalesmanName);
            data.Sales.Should().HaveCount(salesmanModel.Sales.Count);

            for (int i = 0; i < data.Sales.Count; i++)
            {
                data.Sales[i].Price.Should().Be(salesmanModel.Sales[i].Price);
                data.Sales[i].ItemID.Should().Be(salesmanModel.Sales[i].ItemID);
                data.Sales[i].Quantity.Should().Be(salesmanModel.Sales[i].Quantity);
            }
        }
    }
}
