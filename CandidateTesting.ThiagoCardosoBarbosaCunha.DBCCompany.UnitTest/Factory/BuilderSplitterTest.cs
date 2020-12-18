using CandidateTesting.ThiagoCardosoBarbosaCunha.DBCCompany.Domain.Contracts.Factory;
using CandidateTesting.ThiagoCardosoBarbosaCunha.DBCCompany.Domain.Factory;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace CandidateTesting.ThiagoCardosoBarbosaCunha.DBCCompany.UnitTest.Factory
{
    public class BuilderSplitterTest
    {
        private IBuilderSplitter _builderSplitter;

        [SetUp]
        public void Setup() 
        {
            _builderSplitter = new BuilderSplitter();
        }

        [TestCase(typeof(SalesmanSplitter), "001ç1234567891234çPedroç50000", "ç", TestName = "ShouldBeSalesmanSplitter")]
        [TestCase(typeof(CustomerSplitter), "002ç2345675434544345çJose da SilvaçRural", "ç", TestName = "ShouldBeCustomerSplitter")]
        [TestCase(typeof(SalesDataSplitter), "003ç10ç[1-10-100,2-30-2.50,3-40-3.10]çPedro", "ç", TestName = "ShouldBeSalesDataSplitter")]
        public void ShouldGetCorrectSplitter(Type splitterType, string line, string separator) 
        {
            ISplitter splitter = _builderSplitter.GetSplitter(line, separator);

            splitter.GetType().Should().Be(splitterType);
        }
    }
}
