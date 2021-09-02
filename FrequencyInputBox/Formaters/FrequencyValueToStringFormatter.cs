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
        static string numberRegexString = @"\d+(((\.|,)\d+|)+e(\+|-)\d+|(\.|,)\d+|)";

        static string unitsRegexString = @"(Hz$)|(kHz$)";

        static string validationRegexString = @"^((\d+(((\.|,)\d+|)+e(\+|-)\d+|(\.|,)\d+|))+)?((Hz)|(kHz)?)$";

        string InputString;

        public FrequencyValueToStringFormatter(string inputString)
        {
            InputString = inputString.Replace(" ", "");
        }

        public bool IsStringValid()
        {
            var validationMatches = Regex.Matches(InputString, validationRegexString);
            if (validationMatches.Count != 1)
                return false;
            
            validationMatches = Regex.Matches(InputString, unitsRegexString);
            if (validationMatches.Count > 1)
                return false;

            return true;
        }

        public Frequency ConvertStringToFrequency() 
        {
            var mts = Regex.Matches(InputString, numberRegexString);
            var unitsMatches = Regex.Matches(InputString, unitsRegexString);

            Frequency v = new Frequency();
            if (mts.Count == 1)
                v.value += double.Parse(mts[0].Value.Replace(',', '.'), CultureInfo.InvariantCulture);
            if (unitsMatches.Count == 1)
                v.unit = Frequency.ConvertStringToUnitType(unitsMatches[0].Value);

            return v;
        }
    }

    
}
