using System.Text.RegularExpressions;

namespace FrequencyInputControl.Core
{
    internal static class Validation
    {
        public static bool IsStringValid(string InputString)
        {
            var tmpStr = InputString.Replace(" ", "");

            var validationMatches = Regex.Matches(tmpStr, Settings.validationPattern);
            if (validationMatches.Count != 1)
                return false;

            validationMatches = Regex.Matches(tmpStr, Settings.unitsPattern);
            if (validationMatches.Count > 1)
                return false;

            return true;
        }


    }
}
