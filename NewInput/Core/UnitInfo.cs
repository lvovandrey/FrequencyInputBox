using System.Linq;

namespace NewInput.Core
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
