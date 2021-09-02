using FrequencyInputBox.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FrequencyInputBox.Formaters
{
    public class FrequencyValueToStringFormatter
    {
        static string numberRegexString = @"(-|)\d+(((\.|,)\d+|)+e(\+|-)\d+|(\.|,)\d+|)";

        static string unitsRegexString = @"(Hz$)|(kHz$)";

        static string[] validationRegexStrings = { @"(-|)\d+(((\.|,)\d+|)+e(\+|-)\d+|(\.|,)\d+|)(Hz$)|(kHz$)", unitsRegexString };

        string InputString;

        public FrequencyValueToStringFormatter(string inputString)
        {
            InputString = inputString;
        }

        public bool IsStringValid()
        {
            foreach (var regexString in validationRegexStrings)
            {
                var validationMatches = Regex.Matches(InputString, regexString);
                if (validationMatches.Count!=1) return false;
            }
            return true;
        }

        public Frequency ConvertStringToFrequency(string str) 
        {
            string temp_str = str.Replace(" ", "");


            var mts = Regex.Matches(temp_str, numberRegexString);

            var unitsMatches = Regex.Matches(temp_str, unitsRegexString);



            Frequency v = new Frequency();
            foreach (Match mt in mts) 
            { 
                Console.WriteLine(mt.Value); 
                v.value += Double.Parse(mt.Value.Replace(',', '.'), CultureInfo.InvariantCulture); 
            }

            foreach (Match mt in unitsMatches)
            {
                Console.WriteLine(mt.Value);
                v.unit = Frequency.ConvertStringToUnitType(mt.Value); 
            }


            return v;
        }
    }

    
}
