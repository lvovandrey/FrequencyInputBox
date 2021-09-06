using PhisicalValueInputControl.Core;
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

        private List<UnitInfo> unitsInfoes;

        public List<UnitInfo> UnitsInfoes
        {
            get { return unitsInfoes; }
            set { unitsInfoes = value; OnPropertyChanged(); }
        }

    }
}
