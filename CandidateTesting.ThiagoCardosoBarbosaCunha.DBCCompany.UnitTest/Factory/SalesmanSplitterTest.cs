using AutoFixture;
using CandidateTesting.ThiagoCardosoBarbosaCunha.DBCCompany.Domain.Contracts.Factory;
using CandidateTesting.ThiagoCardosoBarbosaCunha.DBCCompany.Domain.Factory;
using CandidateTesting.ThiagoCardosoBarbosaCunha.DBCCompany.Model;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;

namespace CandidateTesting.ThiagoCardosoBarbosaCunha.DBCCompany.UnitTest.Factory
{
    public class SalesmanSplitterTest
    {
        private Fixture _fixture;
        private ISplitter _salesmanSplitter;

        [SetUp]
        public void Setup() 
        {
            _fixture = new Fixture();
            _salesmanSplitter = new SalesmanSplitter();
        }

        [Test]
        public async Task ShouldReturnCorrectDataFromLine() 
        {
            var separator = "ç";
            var salesmanModel = _fixture.Create<SalesmanModel>();
            var line = $"{ (int)salesmanModel.DataType }{ separator }" +
                $"{ salesmanModel.CPF }{ separator }" +
                $"{ salesmanModel.Name }{ separator }" +
                $"{ salesmanModel.Salary }";
            
            _salesmanSplitter.LineToSplit = line;
            _salesmanSplitter.Separator = separator;

            var data = (SalesmanModel)await _salesmanSplitter.Extract();

            data.CPF.Should().Be(salesmanModel.CPF);
            data.Name.Should().Be(salesmanModel.Name);
            data.Salary.Should().Be(salesmanModel.Salary);
            data.DataType.Should().Be(salesmanModel.DataType);
        }
    }
}
