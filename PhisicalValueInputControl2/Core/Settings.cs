using System.Collections.Generic;

namespace PhisicalValueInputControl.Core
{


    static class Settings
    {
        public static string numberPattern = @"\d+(((\.|,)\d+|)+e(\+|-)\d+|(\.|,)\d+|)"; //вообще-то должно работать с экспоненциальной записью, но что-то не работает

        public static string unitsPattern = @"(Hz$)|(kHz$)|(k$)|(M$)|(MHz$)|(G$)|(GHz$)";

        public static string validationPattern = @"^((\d+(((\.|,)\d+|)+e(\+|-)\d+|(\.|,)\d+|))+)?((Hz$)|(kHz$)|(k$)|(M$)|(MHz$)|(G$)|(GHz$)?)$";


        public static List<UnitInfo> DefaultUnitsInfoes => new List<UnitInfo>
        {
            new UnitInfo(1, new string[]{"Hz", "H", "Гц"}),
            new UnitInfo(1000, new string[]{"kHz", "k", "кГц" }),
            new UnitInfo(1000_000, new string[]{"MHz", "M", "МГц" }),
            new UnitInfo(1000_000_000, new string[]{"GHz", "G", "ГГц" })
        };

        public static List<UnitInfo> _unitsInfoes;
        public static List<UnitInfo> UnitsInfoes
        { 
            get { return _unitsInfoes; }
            set { _unitsInfoes = value; RefreshRegexPatterns(_unitsInfoes); } 
        }




        private static void RefreshRegexPatterns(List<UnitInfo> unitsInfoes)
        {
            unitsPattern = GentrateUnitsPattern(UnitsInfoes);
            validationPattern = GenerateValidationPattern(numberPattern, unitsPattern);
        }

        #region privateMethods
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
        #endregion

    }
}
