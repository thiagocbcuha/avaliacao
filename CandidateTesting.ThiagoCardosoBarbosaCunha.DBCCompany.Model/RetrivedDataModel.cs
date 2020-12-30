using System;
using System.Collections.Generic;
using System.Text;

namespace CandidateTesting.ThiagoCardosoBarbosaCunha.DBCCompany.Model
{
    public class RetrievedDataModel
    {
        public List<CustomerModel> Custumers { get; private set; }
            = new List<CustomerModel>();
        public List<SalesmanModel> Salesmans { get; private set; }
            = new List<SalesmanModel>();
        public List<SalesDataModel> SalesData { get; private set; }
            = new List<SalesDataModel>();
    }
}
