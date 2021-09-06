using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace FrequencyInputControl.Core
{
    internal class Frequency
    {
        #region ctor


        public Frequency(double hz, UnitInfo unitInfo)
        {
            UnitInfo = unitInfo;
            CalculateFormatingValueFromHz(hz, unitInfo);
            GeneteateNewInputString();
        }

        public Frequency(double hz) : this(hz, Settings.unitsInfoes.FirstOrDefault())
        {

        }

        public Frequency() : this(0, Settings.unitsInfoes.FirstOrDefault())
        {

        }

        public Frequency(string inputString)
        {
            InputString = inputString;
        }
        #endregion

        #region Properties
        public double FormatingValue { get; private set; }

        public UnitInfo UnitInfo { get; private set; }

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
            return FormatingValue * UnitInfo.coefficient;
        }

        private void CalculateFormatingValueFromHz(double hz, UnitInfo unitInfo)
        {
            FormatingValue = hz / unitInfo.coefficient;
        }

        private void GeneteateNewInputString()
        {
            StringBuilder sb = new StringBuilder("");
            sb.Append(FormatingValue.ToString());
            sb.Append(UnitInfo.defaultString);
            inputString = sb.ToString();
        }

        private void FromString(string str)
        {
            var mts = Regex.Matches(str, Settings.numberPattern);
            var unitsMatches = Regex.Matches(str, Settings.unitsPattern);

            if (mts.Count == 1)
                FormatingValue = double.Parse(mts[0].Value.Replace(',', '.'), CultureInfo.InvariantCulture);
            if (unitsMatches.Count == 1)
                UnitInfo = ConvertStringToUnitInfo(unitsMatches[0].Value);
            if (unitsMatches.Count == 0)
                UnitInfo = Settings.unitsInfoes.FirstOrDefault();
        }

        private static UnitInfo ConvertStringToUnitInfo(string value)
        {
            var result = Settings.unitsInfoes.FirstOrDefault();
            foreach (var uinfo in Settings.unitsInfoes)
                foreach (var ps in uinfo.presentationStrings)
                    if (ps == value)
                        result =  uinfo;
            return result;
        }
        #endregion
    }
}
