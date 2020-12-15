﻿using CandidateTesting.ThiagoCardosoBarbosaCunha.DBCCompany.Domain.Contracts.Factory;
using CandidateTesting.ThiagoCardosoBarbosaCunha.DBCCompany.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace CandidateTesting.ThiagoCardosoBarbosaCunha.DBCCompany.Domain.Factory.Model
{
    public class CostumerModel : IDataSplitable
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