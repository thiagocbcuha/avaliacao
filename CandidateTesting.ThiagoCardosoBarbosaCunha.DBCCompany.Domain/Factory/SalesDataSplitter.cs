using CandidateTesting.ThiagoCardosoBarbosaCunha.DBCCompany.Model;
using CandidateTesting.ThiagoCardosoBarbosaCunha.DBCCompany.Model.Factory;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CandidateTesting.ThiagoCardosoBarbosaCunha.DBCCompany.Domain.Factory
{
    public class SalesDataSplitter : SplitterBase<SalesDataModel>
    {
        public override async Task<IDataSplitable> Extract()
        {
            var splitted = Splitted;
            var data = new SalesDataModel();

            data.SalesID = GetValue<string>("SalesID", splitted);
            data.SalesmanName = GetValue<string>("SalesmanName", splitted);

            foreach (var salesItem in splitted[2].Split(","))
            {
                var items = salesItem
                    .Replace("[", "")
                    .Replace("]", "")
                    .Split("-");

                var sales = new SalesItemModel();
                sales.ItemID = GetValue<SalesItemModel, int>("ItemID", items);
                sales.Price = GetValue<SalesItemModel, decimal>("Price", items);
                sales.Quantity = GetValue<SalesItemModel, int>("Quantity", items);

                data.Sales.Add(sales);
            }

            return await Task.FromResult(data);
        }
    }
}
