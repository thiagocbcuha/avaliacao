using AutoFixture;
using FluentAssertions;
using CandidateTesting.ThiagoCardosoBarbosaCunha.DBCCompany.Model;
using CandidateTesting.ThiagoCardosoBarbosaCunha.DBCCompany.Domain.Extension;
using NUnit.Framework;

namespace CandidateTesting.ThiagoCardosoBarbosaCunha.DBCCompany.UnitTest
{
    [TestFixture]
    public class FileLineExtensionTest
    {
        private Fixture _fixture;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture();
        }

        [Test]
        public void ShouldBeCorrectFormat()
        {
            var fileLine = new FileLine
            {
                CustomerCount = 10,
                SalesmanCount = 10,
                MostExpensive = 1000,
                WorseSalesman = "Teste"
            };

            var result = fileLine.ToString("{CustomerCount} {SalesmanCount}  {MostExpensive}  {WorseSalesman}");

            result.Contains(fileLine.WorseSalesman).Should().BeTrue();
            result.Contains(fileLine.CustomerCount.ToString()).Should().BeTrue();
            result.Contains(fileLine.SalesmanCount.ToString()).Should().BeTrue();
            result.Contains(fileLine.MostExpensive.ToString()).Should().BeTrue();
        }
    }
}
