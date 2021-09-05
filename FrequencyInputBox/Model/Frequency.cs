using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

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
        #region ctor
        public Frequency()
        {
            FormatingValue = 0;
            Unit = UnitType.Hz;
            GeneteateNewInputString();
        }

        public Frequency(double hz, UnitType unitType = UnitType.Hz)
        {
            Unit = unitType;
            CalculateFormatingValueFromHz(hz, unitType);
            GeneteateNewInputString();
        }

        public Frequency(string inputString)
        {
            InputString = inputString;
        }
        #endregion

        #region Properties
        public double FormatingValue { get; private set; }

        public UnitType Unit { get; private set; }

        public double Hz => ConvertToHz();

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
        #endregion

        #region Methods
        private double ConvertToHz()
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

        private void CalculateFormatingValueFromHz(double hz, UnitType unitType)
        {
            switch (Unit)
            {
                case UnitType.Hz: FormatingValue = hz; break;
                case UnitType.kHz: FormatingValue = hz / 1000; break;
                case UnitType.MHz: FormatingValue = hz / 1000_000; break;
                case UnitType.GHz: FormatingValue = hz / 1000_000_000; break;
                default: FormatingValue = hz; break;
            }
        }

        private void GeneteateNewInputString()
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
        #endregion
    }
}

