using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrequencyInputControl.Core
{
    //public enum UnitType
    //{
    //    Hz,
    //    kHz,
    //    MHz,
    //    GHz,
    //    unknown
    //}

    public class UnitInfo
    {
        //public UnitType unitType { get; set; }
        public double coefficient{ get; set; }
        public string[] presentationStrings { get; set; }
        public string defaultString { get; set; }

        public UnitInfo(double coefficient, string[] presentationStrings, string defaultString)
        {
          //  this.unitType = unitType;
            this.coefficient = coefficient;
            this.presentationStrings = presentationStrings;
            this.defaultString = defaultString;
        }
        public UnitInfo()
        {
            //unitType = UnitType.Hz;
            coefficient = 1;
            presentationStrings = new string[] { "Hz", "H", "Гц" };
        }

    }
}
