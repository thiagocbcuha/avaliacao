using CandidateTesting.ThiagoCardosoBarbosaCunha.DBCCompany.Domain.Contracts.Infra;
using CandidateTesting.ThiagoCardosoBarbosaCunha.DBCCompany.Domain.Contracts.Services;
using CandidateTesting.ThiagoCardosoBarbosaCunha.DBCCompany.Domain.Services;
using CandidateTesting.ThiagoCardosoBarbosaCunha.DBCCompany.Model;
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
    public class InputOutputDataServiceTest
    {
        private Mock<IWriter> _writer;
        private Mock<IConfiguration> _configuration;
        private Mock<IReportService> _reportService;
        private Mock<IFileProcessorService> _transformerService;
        private InputOutputDataService _inputOutputDataService;

        [SetUp]
        public void Setup() 
        {
            _writer = new Mock<IWriter>();
            _configuration = new Mock<IConfiguration>();
            _reportService = new Mock<IReportService>();
            _transformerService = new Mock<IFileProcessorService>();

            SetDefaultConfiguration(_configuration);
            _inputOutputDataService = new InputOutputDataService(_configuration.Object, _writer.Object, _transformerService.Object, _reportService.Object);
        }

        [TestCase(true, TestName = "ShouldBeTrueResult")]
        [TestCase(false, TestName = "ShouldBeFalseResult")]
        public async Task ShouldBeDefinedResult(bool expectedResult) 
        {
            var pathIn = "my-drive://localfile";
            var pathOut = "my-drive://localfile";
            var fileLine = GetFileLines(1).First();

            _reportService.Setup(i => i.GetStats(It.IsAny<RetrievedDataModel>())).ReturnsAsync(fileLine);
            _writer.Setup(i => i.SaveData(It.IsAny<LogRequest>())).ReturnsAsync(new LogResponse { Result = true });

            if (!expectedResult)
                _transformerService.Setup(i => i.ExecuteProcess(pathIn)).Throws(new Exception());

            var result = await _inputOutputDataService.Execute(pathIn, pathOut);

            result.Should().Be(expectedResult);
        }
    }
}
