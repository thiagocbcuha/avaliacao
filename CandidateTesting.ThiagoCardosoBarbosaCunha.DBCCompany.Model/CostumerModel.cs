using CandidateTesting.ThiagoCardosoBarbosaCunha.DBCCompany.Model.Factory;
using System;
using System.Collections.Generic;
using System.Text;

namespace CandidateTesting.ThiagoCardosoBarbosaCunha.DBCCompany.Model
{
    public class CustomerModel : IDataSplitable
    {
        [PositionInLine(0)]
        public DataType DataType { get => DataType.Costumer; }

        [PositionInLine(1)]
        public string CNPJ { get; set; }

        [PositionInLine(2)]
        public string Name { get; set; }

        [PositionInLine(3)]
        public string BusinessArea { get; set; }
    }
}
