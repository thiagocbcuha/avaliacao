using CandidateTesting.ThiagoCardosoBarbosaCunha.DBCCompany.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace CandidateTesting.ThiagoCardosoBarbosaCunha.DBCCompany.Domain.Factory.Model
{
    public class SalesItemModel
    {
        [PositionInLine(0)]
        public int ItemID { get; set; }

        [PositionInLine(1)]
        public int Quantity { get; set; }

        [PositionInLine(2)]
        public decimal Price { get; set; }
    }
}
