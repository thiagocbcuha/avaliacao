using AutoFixture;
using FluentAssertions;
using CandidateTesting.ThiagoCardosoBarbosaCunha.DBCCompany.Model;
using CandidateTesting.ThiagoCardosoBarbosaCunha.DBCCompany.Domain.Extension;
using NUnit.Framework;
using CandidateTesting.ThiagoCardosoBarbosaCunha.DBCCompany.Domain.Factory;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace CandidateTesting.ThiagoCardosoBarbosaCunha.DBCCompany.UnitTest
{
    [TestFixture]
    public class GetterExtensionTest
    {
        private Fixture _fixture;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture();
        }

        private TOut GetValue<TOut>(object value)
        {
            return (TOut)Convert.ChangeType(value, typeof(TOut), CultureInfo.GetCultureInfo("en-US"));
        }

        [Test]
        public void ShouldCorrectValueInPositionForSalesmanModel()
        {
            var saparator = "ç";
            var value = "001ç1234567891234çPedroç50000";

            var objType = typeof(SalesmanModel);
            var splitted = value.Split(saparator);

            var cpf = objType.GetProperty("CPF").GetValueFromLine(splitted);
            var name = objType.GetProperty("Name").GetValueFromLine(splitted);
            var salary = objType.GetProperty("Salary").GetValueFromLine(splitted);

            var data = new SalesmanModel();
            data.CPF = GetValue<string>(cpf);
            data.Name = GetValue<string>(name);
            data.Salary = GetValue<decimal>(salary);
            var dataValidation = new SalesmanModel { CPF = "1234567891234", Name = "Pedro", Salary = 50000 };

            data.CPF.Should().Be(dataValidation.CPF);
            data.Name.Should().Be(dataValidation.Name);
            data.Salary.Should().Be(dataValidation.Salary);
        }

        [Test]
        public void ShouldCorrectValueInPositionForCustomerModel()
        {
            var saparator = "ç";
            var value = "002ç2345675434544345çJose da SilvaçRural";

            var objType = typeof(CustomerModel);
            var splitted = value.Split(saparator);

            var cnpj = objType.GetProperty("CNPJ").GetValueFromLine(splitted);
            var name = objType.GetProperty("Name").GetValueFromLine(splitted);
            var area = objType.GetProperty("BusinessArea").GetValueFromLine(splitted);

            var data = new CustomerModel();
            data.CNPJ = GetValue<string>(cnpj);
            data.Name = GetValue<string>(name);
            data.BusinessArea = GetValue<string>(area);
            var dataValidation = new CustomerModel { Name = "Jose da Silva", BusinessArea = "Rural", CNPJ = "2345675434544345" };

            data.CNPJ.Should().Be(dataValidation.CNPJ);
            data.Name.Should().Be(dataValidation.Name);
            data.BusinessArea.Should().Be(dataValidation.BusinessArea);
        }
    }
}
