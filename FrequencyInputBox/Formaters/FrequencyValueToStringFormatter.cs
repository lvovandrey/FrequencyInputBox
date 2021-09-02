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
        
        public VAL ConvertStringToVAL(string str) 
        {
            string temp_str = str.Replace(" ", "");


            var mts = Regex.Matches(temp_str, @"(-|)\d+(((\.|,)\d+|)+e(\+|-)\d+|(\.|,)\d+|)");

            var unitsMatches = Regex.Matches(temp_str, @"(Hz$)|(kHz$)");

            var validMts = Regex.Matches(temp_str, @"(-|)\d+(((\.|,)\d+|)+e(\+|-)\d+|(\.|,)\d+|)(Hz$)|(kHz$)");

            if (validMts.Count != 1 || mts.Count !=1) return null;

            VAL v = new VAL();
            foreach (Match mt in mts) 
            { 
                Console.WriteLine(mt.Value); 
                v.value += Double.Parse(mt.Value.Replace(',', '.'), CultureInfo.InvariantCulture); 
            }

            foreach (Match mt in unitsMatches)
            {
                Console.WriteLine(mt.Value);
                v.unit = VAL.ConvertStringToUnitType(mt.Value); 
            }


            return v;
        }
    }

    public enum UnitType
    {
        Hz,
        kHz,
        MHz,
        GHz,
        THz,
        unknown
    }

    public class VAL
    {
        public double value;
        public UnitType unit;

        public double valueInHz
        {
            get
            {
                switch (unit)
                {
                    case UnitType.Hz: return value;
                    case UnitType.kHz: return value * 1000;
                    case UnitType.MHz: return value * 1_000_000;
                    case UnitType.GHz: return value * 1_000_000_000;
                    case UnitType.THz: return value * 1_000_000_000_000;
                    default: return value;
                }
            }
        }

        internal static UnitType ConvertStringToUnitType(string value)
        {
            switch (value)
            {
                case "Hz": return UnitType.Hz;
                case "kHz": return UnitType.kHz;
                default: return UnitType.unknown;
            }
        }
    }
}
