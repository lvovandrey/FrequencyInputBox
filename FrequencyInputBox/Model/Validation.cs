using FrequencyInputBox.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FrequencyInputBox.Model
{
    public static class Validation
    {
        public static bool IsStringValid(string InputString)
        {
            var tmpStr = InputString.Replace(" ", "");

            var validationMatches = Regex.Matches(tmpStr, RegularExpressionPatterns.fullStrValidationPattern);
            if (validationMatches.Count != 1)
                return false;
            
            validationMatches = Regex.Matches(tmpStr, RegularExpressionPatterns.unitsPattern);
            if (validationMatches.Count > 1)
                return false;

            return true;
        }


    }

    
}
