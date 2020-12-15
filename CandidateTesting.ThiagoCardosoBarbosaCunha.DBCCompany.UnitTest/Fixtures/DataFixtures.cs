using AutoFixture;
using CandidateTesting.ThiagoCardosoBarbosaCunha.DBCCompany.Domain.Extension;
using CandidateTesting.ThiagoCardosoBarbosaCunha.DBCCompany.Model;
using Microsoft.Extensions.Configuration;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace CandidateTesting.ThiagoCardosoBarbosaCunha.DBCCompany.UnitTest.Fixtures
{
    public static class DataFixtures
    {
        public static IEnumerable<FileLine> GetFileLines(int quantity) => 
            new Fixture().Build<FileLine>()
            .With(i => i.WorseSalesman, "WorseSalesmanDescription")
            .With(i => i.MostExpensive, 1000)
            .With(i => i.SalesmanCount, 10)
            .With(i => i.CustomerCount, 10)
            .CreateMany(quantity);

        public static StringBuilder GetStringBuilderFromListString(IEnumerable<string> lines) {
            var stringBuilder = new StringBuilder();

            foreach (var line in lines)
                stringBuilder.AppendLine(line);

            return stringBuilder;
        }

        public static StringBuilder GetStringBuilderFromFileLines(IEnumerable<FileLine> fileLines)
        {
            var stringBuilder = new StringBuilder();

            foreach (var fileLine in fileLines)
                stringBuilder.AppendLine(fileLine.ToString("{ResponseSize}|{StatusCode}|{CacheStatus}|\"{HttpMethod} {UriPath} HTTP/1.1\"|{TimeTaken}"));

            return stringBuilder;
        }

        
        public static IEnumerable<string> GetStrings(int quantity) => new Fixture().CreateMany<string>(10);

        public static void SetDefaultConfiguration(Mock<IConfiguration> _configuration) 
        {
            _configuration.Setup(i => i.GetSection("FileName")).Returns(new ConfigurationSample { Value = "stats{0}.done.dat" });
            _configuration.Setup(i => i.GetSection("defaultType")).Returns(new ConfigurationSample { Value = "dat" });
            _configuration.Setup(i => i.GetSection("Separator")).Returns(new ConfigurationSample { Value = "ç" });
            _configuration.Setup(i => i.GetSection("FormatToFile"))
                .Returns(new ConfigurationSample { Value = "{SalesmanCount}\t{CustomerCount}\t{MostExpensive}\t{WorseSalesman}" });
        }
    }
}
