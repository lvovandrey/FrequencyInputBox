using FrequencyInputBox.Formaters;
using FrequencyInputBox.Helpers;
using FrequencyInputBox.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrequencyInputBox
{
    public class VM:INPCBase
    {
        FrequencyValueToStringFormatter frequencyValueToStringFormatter;
        

        public VM()
        {
            InputString = "";
            frequency = new Frequency();
            frequencyValueToStringFormatter = new FrequencyValueToStringFormatter(InputString);
            OnPropertyChanged("Validity");
        }

        private string inputString;
        public string InputString 
        {
            get 
            {
                return inputString;
            }
            set 
            {
                inputString = value;
                frequencyValueToStringFormatter = new FrequencyValueToStringFormatter(inputString);
                frequencyValueToStringFormatter.ConvertStringToFrequency();
                OnPropertyChanged("Validity");
                OnPropertyChanged("frequency");
            } 
        }

        public Frequency frequency { get; set; }


        public bool Validity
        {
            get
            {
                return frequencyValueToStringFormatter.IsStringValid();
            }
        }



    }
}
