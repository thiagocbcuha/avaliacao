using System;
using System.Collections.Generic;
using System.Text;

namespace CandidateTesting.ThiagoCardosoBarbosaCunha.DBCCompany.Model
{
    [System.AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = true)]
    public class PositionInLineAttribute : Attribute
    {
        private readonly int positionalString;

        public PositionInLineAttribute(int positionalString)
        {
            this.positionalString = positionalString;
        }

        public int PositionalString
        {
            get { return positionalString; }
        }
    }
}
