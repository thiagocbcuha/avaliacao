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
    public class CustomerSplitterTest
    {
        private Fixture _fixture;
        private ISplitter _customerSplitter;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture();
            _customerSplitter = new CustomerSplitter();
        }

        [Test]
        public async Task ShouldReturnCorrectDataFromLine()
        {
            var separator = "ç";
            var salesmanModel = _fixture.Create<CustomerModel>();
            var line = $"{ (int)salesmanModel.DataType }{ separator }" +
                $"{ salesmanModel.CNPJ }{ separator }" +
                $"{ salesmanModel.Name }{ separator }" +
                $"{ salesmanModel.BusinessArea }";

            _customerSplitter.LineToSplit = line;
            _customerSplitter.Separator = separator;

            var data = (CustomerModel)await _customerSplitter.Extract();

            data.CNPJ.Should().Be(salesmanModel.CNPJ);
            data.Name.Should().Be(salesmanModel.Name);
            data.DataType.Should().Be(salesmanModel.DataType);
            data.BusinessArea.Should().Be(salesmanModel.BusinessArea);
        }
    }
}
