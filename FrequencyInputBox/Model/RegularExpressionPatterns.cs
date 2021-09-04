using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrequencyInputBox.Model
{
    static class RegularExpressionPatterns
    {
        public static string numberPattern = @"\d+(((\.|,)\d+|)+e(\+|-)\d+|(\.|,)\d+|)";

        public static string unitsPattern = @"(Hz$)|(kHz$)|(k$)|(M$)|(MHz$)|(G$)|(GHz$)";

        public static string fullStrValidationPattern = @"^((\d+(((\.|,)\d+|)+e(\+|-)\d+|(\.|,)\d+|))+)?((Hz$)|(kHz$)|(k$)|(M$)|(MHz$)|(G$)|(GHz$)?)$";
    }
}
