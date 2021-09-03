using FrequencyInputBox.Helpers;
using FrequencyInputBox.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FrequencyInputBox
{
    public class VM : INPCBase
    {
        public VM(DependencyProperty FrequencyProperty)
        {
            frequencyProperty = FrequencyProperty;
            InputString = "0";
            frequency = new Frequency();
            OnPropertyChanged("Validity");
        }

        private DependencyProperty frequencyProperty;

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
                if (Validity)
                    Frequency = Frequency.Parse(InputString);
                OnPropertyChanged("Validity");
                OnPropertyChanged("Frequency");
            }
        }

        private Frequency frequency;
        public Frequency Frequency
        {
            get
            {
                return frequency;
            }
            set
            {
                frequency = value;
            }
        }

        internal void SetFrequencyFromDouble(double value)
        {
            Frequency.SetFromDouble(value);
            InputString = Frequency.ToString();
            OnPropertyChanged("Frequency");
            OnPropertyChanged("InputString");
        }



        public bool Validity
        {
            get
            {
                return Validation.IsStringValid(InputString);
            }
        }

        public void OnInputStringChanged()
        {
            InputString = Frequency.ToString();

            OnPropertyChanged("Validity");
            OnPropertyChanged("Frequency");
            OnPropertyChanged("InputString");
        }


    }
}
