using CandidateTesting.ThiagoCardosoBarbosaCunha.DBCCompany.Domain.Contracts.Factory;
using System;
using System.Collections.Generic;
using System.Text;

namespace CandidateTesting.ThiagoCardosoBarbosaCunha.DBCCompany.Domain.Factory
{
    public class BuilderSplitter : IBuilderSplitter
    {
        private static ISplitter _salesMan;
        private static ISplitter _costumer;
        private static ISplitter _salesData;

        public ISplitter Salesman { 
            get 
            {
                if (_salesMan == null)
                    _salesMan = new SalesmanSplitter();

                return _salesMan;
            }
        }

        public ISplitter Costumer
        {
            get
            {
                if (_costumer == null)
                    _costumer = new CustomerSplitter();

                return _costumer;
            }
        }

        public ISplitter SalesData
        {
            get
            {
                if (_salesData == null)
                    _salesData = new SalesDataSplitter();

                return _salesData;
            }
        }
        public ISplitter GetSplitter(string line, string separator)
        {
            var type = line.Substring(0, 3);

            var splitter =  type switch
            {
                "001" => Salesman,
                "002" => Costumer,
                "003" => SalesData,
                _ => Salesman,
            };

            splitter.LineToSplit = line;
            splitter.Separator = separator;

            return splitter;
        }
    }
}
