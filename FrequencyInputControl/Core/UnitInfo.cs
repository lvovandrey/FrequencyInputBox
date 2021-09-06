using System.Linq;

namespace PhisicalValueInputControl.Core
{
    /// <summary>
    /// Дополнительный класс - информация о единице измерения (ее размерность, наименование и строки, которыми она может представляться)
    /// </summary>
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
