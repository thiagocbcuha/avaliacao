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

            var result = fileLine.ToString("{CustomerCount}ç{SalesmanCount}ç{MostExpensive}ç{WorseSalesman}");

            result.Contains(fileLine.WorseSalesman).Should().BeTrue();
            result.Contains(fileLine.CustomerCount.ToString()).Should().BeTrue();
            result.Contains(fileLine.SalesmanCount.ToString()).Should().BeTrue();
            result.Contains(fileLine.MostExpensive.ToString()).Should().BeTrue();
        }

        [Test]
        public void ShouldBeCorrectDescription()
        {
            var fileLine = new FileLine();

            var validResult = $"Nº ClientesçNº VendedoresçVenda Mais CaraçPior Vendedor";
            var result = fileLine.GetDescription("{CustomerCount}ç{SalesmanCount}ç{MostExpensive}ç{WorseSalesman}");

            result.Should().Be(validResult);
        }
    }
}
