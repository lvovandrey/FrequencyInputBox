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
        unknown
    }

    public class Frequency
    {
        public double FormatingValue;
        public UnitType Unit;

        public double ToHzInDouble
        {
            get
            {
                switch (Unit)
                {
                    case UnitType.Hz: return FormatingValue;
                    case UnitType.kHz: return FormatingValue * 1000;
                    case UnitType.MHz: return FormatingValue * 1_000_000;
                    case UnitType.GHz: return FormatingValue * 1_000_000_000;
                    default: return FormatingValue;
                }
            }
        }

        public void FromHzInDouble(double value)
        {
            if (value >= 0 && value < 1000)
            {
                Unit = UnitType.Hz;
                FormatingValue = value;
            }
            if (value >= 1000 && value < 1_000_000)
            {
                Unit = UnitType.kHz;
                FormatingValue = value / 1000;
            }
            if (value >= 1_000_000 && value < 1_000_000_000)
            {
                Unit = UnitType.MHz;
                FormatingValue = value/1000_000;
            }
            if (value >= 1_000_000_000 && value < 1_000_000_000_000)
            {
                Unit = UnitType.GHz;
                FormatingValue = value/1000_000_000;
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder("");
            sb.Append(FormatingValue.ToString());
            switch (Unit)
            {
                case UnitType.Hz:
                    sb.Append(" Hz");
                    break;
                case UnitType.kHz:
                    sb.Append(" kHz");
                    break;
                case UnitType.MHz:
                    sb.Append(" MHz");
                    break;
                case UnitType.GHz:
                    sb.Append(" GHz");
                    break;
                case UnitType.unknown:
                    sb.Append(" Unknown");
                    break;
                default:
                    sb.Append(" Unknown");
                    break;
            }
            return sb.ToString();
        }

        public static Frequency FromString(string InputString)
        {
            var mts = Regex.Matches(InputString, RegularExpressionPatterns.numberPattern);
            var unitsMatches = Regex.Matches(InputString, RegularExpressionPatterns.unitsPattern);

            Frequency v = new Frequency();
            if (mts.Count == 1)
                v.FormatingValue += double.Parse(mts[0].Value.Replace(',', '.'), CultureInfo.InvariantCulture);
            if (unitsMatches.Count == 1)
                v.Unit = Frequency.ConvertStringToUnitType(unitsMatches[0].Value);

            return v;
        }

        internal static UnitType ConvertStringToUnitType(string value)
        {
            switch (value)
            {
                case "Hz":
                case "Гц":
                case "H":
                case "h": return UnitType.Hz;
                case "kHz":
                case "кГц":
                case "К":
                case "k": return UnitType.kHz;
                case "MHz":
                case "МГц":
                case "M": return UnitType.MHz;
                case "GHz":
                case "ГГц":
                case "G": return UnitType.GHz;
                default: return UnitType.unknown;
            }
        }
    }
}

