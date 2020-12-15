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
    public class FileServiceTest
    {
        private Mock<IWriter> _writer;
        private Mock<IConfiguration> _configuration;
        private Mock<ITransformerService> _transformerService;

        private IReportService _fileService;

        [SetUp]
        public void Setup() 
        {
            _writer = new Mock<IWriter>();
            _transformerService = new Mock<ITransformerService>();
            _configuration = new Mock<IConfiguration>();

            SetDefaultConfiguration(_configuration);

            _fileService = new ReportService(_configuration.Object, _writer.Object, _transformerService.Object);
        }

        [TestCase(true, TestName = "ShouldBeTrueInReturn")]
        [TestCase(false, TestName = "ShouldBeFalseInReturn")]
        public async Task ShouldBeDefinedResult(bool returnValue)
        {
            var pathIn = "my-drive://localfile";
            var pathOut = "my-drive://localfile";
            var fileLines = GetFileLines(10);
            var logResponse = new LogResponse { Result = returnValue };
            _writer.Setup(i => i.SaveData(It.IsAny<LogRequest>())).ReturnsAsync(logResponse);

            var result = await _fileService.GetStats(pathIn, pathOut);

            result.Should().Be(returnValue);
        }
    }
}
