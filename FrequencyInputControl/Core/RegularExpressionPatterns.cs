namespace FrequencyInputControl.Core
{
    static class RegularExpressionPatterns
    {
        public static string numberPattern = @"\d+(((\.|,)\d+|)+e(\+|-)\d+|(\.|,)\d+|)";

        public static string unitsPattern = @"(Hz$)|(kHz$)|(k$)|(M$)|(MHz$)|(G$)|(GHz$)";

        public static string fullStrValidationPattern = @"^((\d+(((\.|,)\d+|)+e(\+|-)\d+|(\.|,)\d+|))+)?((Hz$)|(kHz$)|(k$)|(M$)|(MHz$)|(G$)|(GHz$)?)$";
    }
}
