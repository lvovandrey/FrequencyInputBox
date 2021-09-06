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
            Value = hz;
            GeneteateNewInputString();
        }

        public Frequency(double hz) : this(hz, Settings.UnitsInfoes.FirstOrDefault())
        {

        }

        public Frequency() : this(0, Settings.UnitsInfoes.FirstOrDefault())
        {

        }

        public Frequency(string inputString)
        {
            InputString = inputString;
        }
        #endregion

        #region Properties
        public double FormatingValue
        {
            get { return Value / UnitInfo.coefficient; }
            set { Value = value * UnitInfo.coefficient; }
        }

        public UnitInfo UnitInfo { get; private set; }

        public double Value { get; private set; }

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

        //public void CalculateFormatingValueFromHz(double hz, UnitInfo unitInfo)
        //{
        //    FormatingValue = hz / unitInfo.coefficient;
        //}

        private void GeneteateNewInputString()
        {
            StringBuilder sb = new StringBuilder("");
            sb.Append(FormatingValue.ToString());
            sb.Append(UnitInfo.defaultString);
            inputString = sb.ToString();
        }

        private void GeneteateNewInputString(UnitInfo unitInfo)
        {
            StringBuilder sb = new StringBuilder("");
            sb.Append(FormatingValue.ToString());
            sb.Append(unitInfo.defaultString);
            inputString = sb.ToString();
        }

        private void FromString(string str)
        {
            var mts = Regex.Matches(str, Settings.numberPattern);
            var unitsMatches = Regex.Matches(str, Settings.unitsPattern);

            if (unitsMatches.Count == 1)
                UnitInfo = ConvertStringToUnitInfo(unitsMatches[0].Value);
            if (unitsMatches.Count == 0)
                UnitInfo = Settings.UnitsInfoes.FirstOrDefault();
            if (mts.Count == 1)
                FormatingValue = double.Parse(mts[0].Value.Replace(',', '.'), CultureInfo.InvariantCulture);

        }

        private static UnitInfo ConvertStringToUnitInfo(string value)
        {
            var result = Settings.UnitsInfoes.FirstOrDefault();
            foreach (var uinfo in Settings.UnitsInfoes)
                foreach (var ps in uinfo.presentationStrings)
                    if (ps == value)
                        result =  uinfo;
            return result;
        }

        private static UnitInfo CalculateBestUnitsForValue(double value)
        { 
            var UInfos = from u in Settings.UnitsInfoes
                  orderby u.coefficient
                  select u;
            UnitInfo U = UInfos.FirstOrDefault();
            foreach (var u in UInfos)
            {
                if (u.coefficient > value)
                    break;
                U = u;
            }
            return U;
        }

        public void FormatInBestUnits()
        {
            UnitInfo = CalculateBestUnitsForValue(Value);
            GeneteateNewInputString(UnitInfo);
        }

        #endregion
    }
}
