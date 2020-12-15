using CandidateTesting.ThiagoCardosoBarbosaCunha.DBCCompany.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace CandidateTesting.ThiagoCardosoBarbosaCunha.DBCCompany.Domain.Extension
{
    public static class FileLineExtension
    {
        public static string ToString(this FileLine fileLine, string format)
        {
            var words = GetWords(format);
            var @properties = fileLine.GetType().GetProperties();
            foreach (var word in words)
            {
                var property = @properties.FirstOrDefault(i => i.Name.EndsWith(word));
                if (property != null)
                {
                    var value = property.GetValue(fileLine).ToString();

                    if (property.PropertyType.IsEnum)
                        value = ((int)Enum.Parse(property.PropertyType, value)).ToString();

                    format = format.Replace(String.Concat("{", $"{ word }", "}"), value.ToString());
                    format = format.Replace(word, value.ToString());
                }
            }

            return format;
        }

        public static string GetDescription(this FileLine fileLine, string format)
        {
            var words = GetWords(format);
            var @properties = fileLine.GetType().GetProperties();
            foreach (var word in words)
            {
                var value = "";
                var property = @properties.FirstOrDefault(i => i.Name.EndsWith(word));

                if (property != null)
                {
                    var descriptionAttribute = property
                                .GetCustomAttributes(typeof(DescriptionAttribute), false)
                                .FirstOrDefault() as DescriptionAttribute;

                    if (descriptionAttribute != null)
                        value = descriptionAttribute.Description;

                    format = format.Replace(String.Concat("{", $"{ word }", "}"), value.ToString());
                    format = format.Replace(word, value.ToString()); 
                }
            }

            return format;
        }

        public static int? GetPositionInLine(this PropertyInfo property)
        {
            var attribute = property
                                .GetCustomAttributes(attributeType: typeof(PositionInLineAttribute), false)
                                .FirstOrDefault() as PositionInLineAttribute;

            if (attribute != null)
                return attribute.PositionalString;

            return null;
        }

        private static string[] GetWords(string input)
        {
            MatchCollection matches = Regex.Matches(input, @"\b[\w']*\b");

            var words = from m in matches.Cast<Match>()
                        where !string.IsNullOrEmpty(m.Value)
                        select TrimSuffix(m.Value);

            return words.ToArray();
        }

        private static string TrimSuffix(string word)
        {
            int apostropheLocation = word.IndexOf('\'');
            if (apostropheLocation != -1)
            {
                word = word.Substring(0, apostropheLocation);
            }

            return word;
        }
    }
}
