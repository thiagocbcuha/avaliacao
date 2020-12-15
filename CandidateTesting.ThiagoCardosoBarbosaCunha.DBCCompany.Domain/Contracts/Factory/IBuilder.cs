using System;
using System.Collections.Generic;
using System.Text;

namespace CandidateTesting.ThiagoCardosoBarbosaCunha.DBCCompany.Domain.Contracts.Factory
{
    public interface IBuilderSplitter
    {
        ISplitter GetSplitter(string line, string separator);
    }
}
