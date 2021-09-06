using System.Text.RegularExpressions;

namespace FrequencyInputControl.Core
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
