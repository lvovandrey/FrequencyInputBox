using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrequencyInputControl.Core
{

    public class UnitInfo
    {
        public double coefficient{ get; private set; }
        public string[] presentationStrings { get; private set; }
        public string defaultString { get; private set; }

        public UnitInfo(double coefficient, string[] presentationStrings, string defaultString=null)
        {
            this.coefficient = coefficient;
            this.presentationStrings = presentationStrings;
            if(defaultString==null)
            this.defaultString = presentationStrings.FirstOrDefault();
        }
    }
}
