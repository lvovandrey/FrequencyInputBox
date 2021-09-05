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
        public double FormatingValue { get; private set; }
        public UnitType Unit { get; private set; }

        public double Hz
        {
            get { return ToHzInDouble(); }
            set { FromHzInDouble(value); }
        }
        private string inputString;
        public string InputString
        {
            get { return inputString; }
            set
            {
                inputString = value;
                FromString(inputString);
            }
        }


        private double ToHzInDouble()
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



        public Frequency()
        {
            FormatingValue = 0;
            Unit = UnitType.Hz;
        }

        public Frequency(double Hz, UnitType unitType = UnitType.Hz)
        {
            Unit = unitType;
            switch (Unit)
            {
                case UnitType.Hz: FormatingValue = Hz; break;
                case UnitType.kHz: FormatingValue = Hz/1000; break;
                case UnitType.MHz: FormatingValue = Hz/1000_000; break;
                case UnitType.GHz: FormatingValue = Hz/1000_000_000; break;
                default: FormatingValue = Hz; break;
            }
        }



        private void FromHzInDouble(double value)
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
                FormatingValue = value / 1000_000;
            }
            if (value >= 1_000_000_000 && value < 1_000_000_000_000)
            {
                Unit = UnitType.GHz;
                FormatingValue = value / 1000_000_000;
            }
        }

        public void GeneteateNewInputString()
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
            inputString = sb.ToString();
        }

        private void FromString(string str)
        {
            var mts = Regex.Matches(str, RegularExpressionPatterns.numberPattern);
            var unitsMatches = Regex.Matches(str, RegularExpressionPatterns.unitsPattern);

            if (mts.Count == 1)
                FormatingValue = double.Parse(mts[0].Value.Replace(',', '.'), CultureInfo.InvariantCulture);
            if (unitsMatches.Count == 1)
                Unit = Frequency.ConvertStringToUnitType(unitsMatches[0].Value);
            if (unitsMatches.Count == 0)
                Unit = UnitType.Hz;
        }

        private static UnitType ConvertStringToUnitType(string value)
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

