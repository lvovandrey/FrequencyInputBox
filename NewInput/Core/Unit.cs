using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace NewInput.Core
{
    internal class Unit
    {
        #region ctor
        public Unit(double value, UnitInfo unitInfo)
        {
            UnitInfo = unitInfo;
            Value = value;
            GeneteateNewInputString(UnitInfo);
        }

        public Unit(double value) : this(value, Settings.UnitsInfoes.FirstOrDefault()) { }

        public Unit() : this(0, Settings.UnitsInfoes.FirstOrDefault()) { }

        public Unit(string inputString)
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

        public void GeneteateNewInputString(UnitInfo unitInfo)
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
