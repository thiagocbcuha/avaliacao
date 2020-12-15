using CandidateTesting.ThiagoCardosoBarbosaCunha.DBCCompany.Domain.Contracts.Factory;
using CandidateTesting.ThiagoCardosoBarbosaCunha.DBCCompany.Domain.Factory.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CandidateTesting.ThiagoCardosoBarbosaCunha.DBCCompany.Domain.Factory
{
    public class Costumer : SplitterBase<CostumerModel>
    {
        public override async Task<IDataSplitable> Extract()
        {
            var splitted = Splitted;
            var data = new CostumerModel();

            data.CNPJ = GetValue<string>("CNPJ", splitted);
            data.Name = GetValue<string>("Name", splitted);
            data.BusinessArea = GetValue<string>("BusinessArea", splitted);

            return await Task.FromResult(data);
        }

    }
}
