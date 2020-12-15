using CandidateTesting.ThiagoCardosoBarbosaCunha.DBCCompany.Domain.Contracts.Factory;
using CandidateTesting.ThiagoCardosoBarbosaCunha.DBCCompany.Domain.Factory.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CandidateTesting.ThiagoCardosoBarbosaCunha.DBCCompany.Domain.Factory
{
    public class Salesman : SplitterBase<SalesmanModel>
    {
        public override async Task<IDataSplitable> Extract()
        {
            var splitted = Splitted;
            var data = new SalesmanModel();

            data.CPF = GetValue<string>("CPF", splitted);
            data.Name = GetValue<string>("Name", splitted);
            data.Salary = GetValue<decimal>("Salary", splitted);

            return await Task.FromResult(data);
        }
    }
}
