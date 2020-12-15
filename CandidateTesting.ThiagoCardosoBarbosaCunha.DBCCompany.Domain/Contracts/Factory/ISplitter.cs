using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CandidateTesting.ThiagoCardosoBarbosaCunha.DBCCompany.Domain.Contracts.Factory
{
    public interface ISplitter
    {
        string Separator { get; set; }
        string LineToSplit { get; set; }
        Task<IDataSplitable> Extract();
    }
}
