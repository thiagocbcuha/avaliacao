using CandidateTesting.ThiagoCardosoBarbosaCunha.DBCCompany.Domain.Contracts.Infra;
using CandidateTesting.ThiagoCardosoBarbosaCunha.DBCCompany.Domain.Contracts.Services;
using CandidateTesting.ThiagoCardosoBarbosaCunha.DBCCompany.Domain.Services;
using CandidateTesting.ThiagoCardosoBarbosaCunha.DBCCompany.Model.Request;
using CandidateTesting.ThiagoCardosoBarbosaCunha.DBCCompany.Model.Response;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using System;
using System.Linq;
using System.Threading.Tasks;
using static CandidateTesting.ThiagoCardosoBarbosaCunha.DBCCompany.UnitTest.Fixtures.DataFixtures;

namespace CandidateTesting.ThiagoCardosoBarbosaCunha.DBCCompany.UnitTest.Services
{
    public class RetriveDataServiceTest
    {
        private Mock<IReader> _reader;
        private Mock<IConfiguration> _configuration;
        private IRetrieveDataService _retrivedataservice;

        [SetUp]
        public void Setup() 
        {
            _reader = new Mock<IReader>();
            _configuration = new Mock<IConfiguration>();
            SetDefaultConfiguration(_configuration);
            _retrivedataservice = new RetrievedDataService(_reader.Object, _configuration.Object);
        }

        [Test]
        public async Task ShouldBeDefinedResult() 
        {
            var path = "my-drive://localfile";
            var lines = GetStrings(10);
            var stringBuilder = GetStringBuilderFromListString(lines);
            var logResponse = new FileResponse {  Value = stringBuilder.ToString() };

            _reader.Setup(i => i.GetData(It.IsAny<FileRequest>())).ReturnsAsync(logResponse);

            var result = await _retrivedataservice.GetData(path);

            for (int i = 0; i < lines.Count(); i++)
                result.ElementAt(i).Should().Be(lines.ElementAt(i));
        }
    }
}
