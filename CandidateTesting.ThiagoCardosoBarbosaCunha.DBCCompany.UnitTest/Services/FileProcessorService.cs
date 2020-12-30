using CandidateTesting.ThiagoCardosoBarbosaCunha.DBCCompany.Domain.Contracts.Factory;
using CandidateTesting.ThiagoCardosoBarbosaCunha.DBCCompany.Domain.Contracts.Services;
using CandidateTesting.ThiagoCardosoBarbosaCunha.DBCCompany.Domain.Extension;
using CandidateTesting.ThiagoCardosoBarbosaCunha.DBCCompany.Domain.Factory;
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
        private Mock<IRetrieveDataService> _retriveDataService;
        private Mock<IBuilderSplitter> _buildSplitter;

        private IFileProcessorService _transformService;

        [SetUp]
        public void Setup() 
        {
            _fileService = new Mock<IReportService>();
            _configuration = new Mock<IConfiguration>();
            _retriveDataService = new Mock<IRetrieveDataService>();
            _buildSplitter = new Mock<IBuilderSplitter>();

            SetDefaultConfiguration(_configuration);
            _transformService = new FileProcessorService(_configuration.Object, _buildSplitter.Object, _retriveDataService.Object);
        }

        [TestCase(true, TestName = "ShouldReturnValues")]
        [TestCase(false, TestName = "ShouldNotReturnValues")]
        public async Task ShouldBeDefinedResult(bool returnValue = true) 
        {
            var pathIn = "my-drive://localfile";
            var pathOut = "my-drive://localfile";
            List<string> lines = new List<string>();

            lines.Add("001ç1234567891234çPedroç50000");
            lines.Add("002ç2345675434544345çJose da SilvaçRural");
            lines.Add("003ç10ç[1-10-100,2-30-2.50,3-40-3.10]çPedro");

            if (returnValue)
            {
                _buildSplitter.Setup(i => i.GetSplitter(lines[0], "ç")).Returns(new SalesmanSplitter { LineToSplit = lines[0], Separator = "ç" });
                _buildSplitter.Setup(i => i.GetSplitter(lines[1], "ç")).Returns(new CustomerSplitter { LineToSplit = lines[1], Separator = "ç" });
                _buildSplitter.Setup(i => i.GetSplitter(lines[2], "ç")).Returns(new SalesDataSplitter { LineToSplit = lines[2], Separator = "ç" });
            }

            else
                _buildSplitter.Setup(i => i.GetSplitter(It.IsAny<string>(), It.IsAny<string>())).Throws(new Exception());

            _retriveDataService.Setup(i => i.GetData(pathIn)).ReturnsAsync(lines);

            var result = await _transformService.ExecuteProcess(pathIn);

            if (returnValue)
            {
                result.Custumers.Should().HaveCount(1);
                result.Salesmans.Should().HaveCount(1);
                result.SalesData.Should().HaveCount(1);
            }
            else
                result.Should().BeNull();
        }
    }
}
