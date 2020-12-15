using CandidateTesting.ThiagoCardosoBarbosaCunha.DBCCompany.Domain.Contracts.Factory;
using CandidateTesting.ThiagoCardosoBarbosaCunha.DBCCompany.Domain.Contracts.Services;
using CandidateTesting.ThiagoCardosoBarbosaCunha.DBCCompany.Domain.Extension;
using CandidateTesting.ThiagoCardosoBarbosaCunha.DBCCompany.Domain.Services;
using CandidateTesting.ThiagoCardosoBarbosaCunha.DBCCompany.Model;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static CandidateTesting.ThiagoCardosoBarbosaCunha.DBCCompany.UnitTest.Fixtures.DataFixtures;

namespace CandidateTesting.ThiagoCardosoBarbosaCunha.DBCCompany.UnitTest.Services
{
    public class TransformServiceTest
    {
        private Mock<IReportService> _fileService;
        private Mock<IConfiguration> _configuration;
        private Mock<IRetriveDataService> _retriveDataService;
        private Mock<IBuilderSplitter> _buildSplitter;

        private ITransformerService _transformService;

        [SetUp]
        public void Setup() 
        {
            _fileService = new Mock<IReportService>();
            _configuration = new Mock<IConfiguration>();
            _retriveDataService = new Mock<IRetriveDataService>();
            _buildSplitter = new Mock<IBuilderSplitter>();

            SetDefaultConfiguration(_configuration);
            _transformService = new TransformerService(_configuration.Object, _buildSplitter.Object, _retriveDataService.Object);
        }

        [TestCase(false, TestName = "ShouldBeFalseInReturn")]
        public async Task ShouldBeDefinedResult(bool returnValue) 
        {
            var pathIn = "my-drive://localfile";
            var pathOut = "my-drive://localfile";
            List<string> lines = new List<string>();

            lines.Add("001ç1234567891234çPedroç50000");
            lines.Add("001ç3245678865434çPauloç40000.99");
            lines.Add("002ç2345675434544345çJose da SilvaçRural");
            lines.Add("002ç2345675433444345çEduardo PereiraçRural");
            lines.Add("003ç10ç[1-10-100,2-30-2.50,3-40-3.10]çPedro");
            lines.Add("003ç08ç[1-34-10,2-33-1.50,3-40-0.10]çPaulo");

            _retriveDataService.Setup(i => i.GetData(pathIn)).ReturnsAsync(lines);
            _fileService.Setup(i => i.GetStats(pathIn, pathOut)).ReturnsAsync(returnValue);

            var result = await _transformService.Execute(pathIn);

            result.Should().Be(returnValue);
        }
    }
}
