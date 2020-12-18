using CandidateTesting.ThiagoCardosoBarbosaCunha.DBCCompany.Domain.Contracts.Factory;
using CandidateTesting.ThiagoCardosoBarbosaCunha.DBCCompany.Model;
using CandidateTesting.ThiagoCardosoBarbosaCunha.DBCCompany.Model.Factory;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CandidateTesting.ThiagoCardosoBarbosaCunha.DBCCompany.Domain.Factory
{
    public class CustomerSplitter : SplitterBase<CustomerModel>
    {
        public override async Task<IDataSplitable> Extract()
        {
            var splitted = Splitted;
            var data = new CustomerModel();

            data.CNPJ = GetValue<string>("CNPJ", splitted);
            data.Name = GetValue<string>("Name", splitted);
            data.BusinessArea = GetValue<string>("BusinessArea", splitted);

            return await Task.FromResult(data);
        }

    }
}
