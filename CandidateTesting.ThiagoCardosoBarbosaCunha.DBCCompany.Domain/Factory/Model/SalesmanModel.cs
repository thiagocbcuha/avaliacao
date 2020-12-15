using CandidateTesting.ThiagoCardosoBarbosaCunha.DBCCompany.Domain.Contracts.Factory;
using CandidateTesting.ThiagoCardosoBarbosaCunha.DBCCompany.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace CandidateTesting.ThiagoCardosoBarbosaCunha.DBCCompany.Domain.Factory.Model
{
    public class SalesmanModel : IDataSplitable
    {
        [PositionInLine(0)]
        public DataType DataType { get => DataType.Salesman; }

        [PositionInLine(1)]
        public string CPF { get; set; }

        [PositionInLine(2)]
        public string Name { get; set; }

        [PositionInLine(3)]
        public decimal Salary { get; set; }
    }
}
