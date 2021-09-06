using PhisicalValueInputControl.Core;
using NewInput.Core;
using System.Collections.Generic;

namespace FrequencyInputBoxDemo
{
    public class VM: INPCBase
    {
        private double frequencyTest;
        public double FrequencyTest 
        {
            get 
            {
                return frequencyTest;
            }
            set
            {
                frequencyTest = value;
                OnPropertyChanged("FrequencyTest");
            }
        }


        private double Hz;

        public double HZ
        {
            get { return Hz; }
            set { Hz = value; OnPropertyChanged(); }
        }

        private List<PhisicalValueInputControl.Core.UnitInfo> unitsInfoes;

        public List<PhisicalValueInputControl.Core.UnitInfo> UnitsInfoes
        {
            get { return unitsInfoes; }
            set { unitsInfoes = value; OnPropertyChanged(); }
        }

        private List<NewInput.Core.UnitInfo> unitsInfoes2;

        public List<NewInput.Core.UnitInfo> UnitsInfoes2
        {
            get { return unitsInfoes2; }
            set { unitsInfoes2 = value; OnPropertyChanged(); }
        }

    }
}
