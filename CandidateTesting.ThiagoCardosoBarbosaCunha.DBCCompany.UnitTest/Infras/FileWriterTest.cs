using CandidateTesting.ThiagoCardosoBarbosaCunha.DBCCompany.Domain.Contracts.Infra;
using CandidateTesting.ThiagoCardosoBarbosaCunha.DBCCompany.Infra;
using CandidateTesting.ThiagoCardosoBarbosaCunha.DBCCompany.Model.Request;
using CandidateTesting.ThiagoCardosoBarbosaCunha.DBCCompany.Model.Response;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;
using static CandidateTesting.ThiagoCardosoBarbosaCunha.DBCCompany.UnitTest.Fixtures.DataFixtures;

namespace CandidateTesting.ThiagoCardosoBarbosaCunha.DBCCompany.UnitTest.Services
{
    public class FileWriterTest
    {
        private IWriter _fileWriter;
        private Mock<IConfiguration> _configuration;

        [SetUp]
        public void Setup() 
        {
            _configuration = new Mock<IConfiguration>();
            SetDefaultConfiguration(_configuration);
            _fileWriter = new FileRepository(_configuration.Object);
        }

        [Test]
        public async Task ShouldBeDefinedResult()
        {
            var path = @"C:\temp\test.txt";
            var fileLines = GetFileLines(10);
            var stringBuilder = GetStringBuilderFromFileLines(fileLines);

            var logRequest = new LogRequest
            {
                Path = path,
                Content = stringBuilder
            };

            var result = await _fileWriter.SaveData(logRequest);

            result.Result.Should().Be(true);
        }
    }
}
