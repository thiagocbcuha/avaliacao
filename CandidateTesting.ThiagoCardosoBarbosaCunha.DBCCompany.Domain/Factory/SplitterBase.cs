using CandidateTesting.ThiagoCardosoBarbosaCunha.DBCCompany.Domain.Contracts.Factory;
using CandidateTesting.ThiagoCardosoBarbosaCunha.DBCCompany.Domain.Extension;
using CandidateTesting.ThiagoCardosoBarbosaCunha.DBCCompany.Model.Factory;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;

namespace CandidateTesting.ThiagoCardosoBarbosaCunha.DBCCompany.Domain.Factory
{
    public abstract class SplitterBase<TIn> : ISplitter where TIn : IDataSplitable
    {
        public string Separator { get; set; }
        public string LineToSplit { get; set; }

        public abstract Task<IDataSplitable> Extract();

        public string[] Splitted
        {
            get
            {
                if(!String.IsNullOrEmpty(LineToSplit) && !String.IsNullOrEmpty(Separator))
                    return LineToSplit.Split(Separator);

                return null;
            }
        }
        public TOut GetValue<TOut>(string name, IEnumerable<string> values)
        {
            var value = typeof(TIn).GetProperty(name).GetValueFromLine(values);
            return (TOut)Convert.ChangeType(value, typeof(TOut), CultureInfo.GetCultureInfo("en-US"));
        }

        public TOut GetValue<TIn, TOut>(string name, IEnumerable<string> values) where TIn : class
        {
            var value = typeof(TIn).GetProperty(name).GetValueFromLine(values);
            return (TOut)Convert.ChangeType(value, typeof(TOut), CultureInfo.GetCultureInfo("en-US"));
        }
    }
}
