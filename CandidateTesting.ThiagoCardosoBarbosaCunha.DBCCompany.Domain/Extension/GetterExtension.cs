using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace CandidateTesting.ThiagoCardosoBarbosaCunha.DBCCompany.Domain.Extension
{
    public static class GetterExtension
    {
        public static object GetValueFromLine(this PropertyInfo property, IEnumerable<string> values) 
        {
            var index = property.GetPositionInLine();
            if(index.HasValue)
                return values.ElementAtOrDefault(index.Value);

            return null;
        }
    }
}
