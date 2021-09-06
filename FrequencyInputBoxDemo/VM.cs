using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    }
}
