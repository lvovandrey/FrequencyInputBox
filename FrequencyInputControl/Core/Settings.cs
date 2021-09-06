using System.Collections.Generic;

namespace FrequencyInputControl.Core
{


    static class Settings
    {
        public static string numberPattern = @"\d+(((\.|,)\d+|)+e(\+|-)\d+|(\.|,)\d+|)";

        public static string unitsPattern = @"(Hz$)|(kHz$)|(k$)|(M$)|(MHz$)|(G$)|(GHz$)";

        public static string validationPattern = @"^((\d+(((\.|,)\d+|)+e(\+|-)\d+|(\.|,)\d+|))+)?((Hz$)|(kHz$)|(k$)|(M$)|(MHz$)|(G$)|(GHz$)?)$";

        public static List<UnitInfo> unitsInfoes = new List<UnitInfo>
        {
            new UnitInfo(UnitType.Hz, 1, new string[]{"Hz", "H", "Гц"},"Hz"),
            new UnitInfo(UnitType.kHz, 1000, new string[]{"kHz", "k", "кГц" },"kHz"),
            new UnitInfo(UnitType.MHz, 1000_000, new string[]{"MHz", "M", "МГц" },"MHz"),
            new UnitInfo(UnitType.GHz, 1000_000_000, new string[]{"GHz", "G", "ГГц" },"GHz"),
        };

        public static void RefreshRegexPatterns()
        {
            unitsPattern = GentrateUnitsPattern(unitsInfoes);
            validationPattern = GenerateValidationPattern(numberPattern, unitsPattern);
        }

        private static string GentrateUnitsPattern(List<UnitInfo> unitsInfoes)
        {
            string newUnitsPattern = "";
            foreach (var info in unitsInfoes)
            {
                newUnitsPattern += @"(" + info.presentationStrings[0] + @"$)";
                for (int i = 1; i < info.presentationStrings.Length; i++)
                {
                    newUnitsPattern += @"|(" + info.presentationStrings[i] + @"$)";
                }
                newUnitsPattern += @"|";
            }
            return newUnitsPattern.Remove(newUnitsPattern.Length - 1, 1);
        }
        private static string GenerateValidationPattern(string newNumberPattern, string newUnitsPattern)
        {
            string newValidationPattern = "";
            newValidationPattern += @"^((" + newNumberPattern + @")+)?";
            newValidationPattern += @"(" + newUnitsPattern + @"?)$";
            return newValidationPattern;
        }

    }
}
