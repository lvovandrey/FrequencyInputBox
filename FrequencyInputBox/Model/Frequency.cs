using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FrequencyInputBox.Model
{
    public enum UnitType
    {
        Hz,
        kHz,
        MHz,
        GHz,
        THz,
        unknown
    }

    public class Frequency
    {
        public double value;
        public UnitType unit;

        public double Hz
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

        public void SetFromDouble(double value) 
        {
            this.value = value;
            if (value >= 0 && value < 1000)
                unit = UnitType.Hz;
            if (value >= 1000 && value < 1_000_000)
                unit = UnitType.kHz;
            if (value >= 1_000_000 && value < 1_000_000_000)
                unit = UnitType.MHz;
            if (value >= 1_000_000_000 && value < 1_000_000_000_000)
                unit = UnitType.GHz;
            if (value >= 1_000_000_000_000 && value < 1_000_000_000_000_000)
                unit = UnitType.THz;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder("");
            sb.Append(value.ToString());
            switch (unit)
            {
                case UnitType.Hz: sb.Append(" Hz");
                    break;
                case UnitType.kHz: sb.Append(" kHz");
                    break;
                case UnitType.MHz: sb.Append(" MHz");
                    break;
                case UnitType.GHz: sb.Append(" GHz");
                    break;
                case UnitType.THz: sb.Append(" THz");
                    break;
                case UnitType.unknown: sb.Append(" Unknown");
                    break;
                default: sb.Append(" Unknown");
                    break;
            }
            return sb.ToString();
        }

        internal static UnitType ConvertStringToUnitType(string value)
        {
            switch (value)
            {
                case "Hz": 
                case "Гц":
                case "Г":
                case "H":
                case "г":
                case "h": return UnitType.Hz;
                case "kHz":
                case "кГц":
                case "К":
                case "K":
                case "к":
                case "k": return UnitType.kHz;
                default: return UnitType.unknown;
            }
        }

        public static Frequency Parse(string InputString)
        {
            var mts = Regex.Matches(InputString, RegularExpressionPatterns.numberPattern);
            var unitsMatches = Regex.Matches(InputString, RegularExpressionPatterns.unitsPattern);

            Frequency v = new Frequency();
            if (mts.Count == 1)
                v.value += double.Parse(mts[0].Value.Replace(',', '.'), CultureInfo.InvariantCulture);
            if (unitsMatches.Count == 1)
                v.unit = Frequency.ConvertStringToUnitType(unitsMatches[0].Value);

            return v;
        }
    }
}

