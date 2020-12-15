using CandidateTesting.ThiagoCardosoBarbosaCunha.DBCCompany.Domain.Contracts.Factory;
using CandidateTesting.ThiagoCardosoBarbosaCunha.DBCCompany.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace CandidateTesting.ThiagoCardosoBarbosaCunha.DBCCompany.Domain.Factory.Model
{
    public class SalesDataModel : IDataSplitable
    {
        [PositionInLine(0)]
        public DataType DataType { get => DataType.SalesData; }

        [PositionInLine(1)]
        public string SalesID { get; set; }

        [PositionInLine(3)]
        public string SalesmanName { get; set; }

        [PositionInLine(2)]
        public List<SalesItemModel> Sales { get; set; } = new List<SalesItemModel>();
    }
}
