using FrequencyInputBox.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FrequencyInputBox
{
    public delegate void PropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e);


    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class FrequencyInputBox : UserControl, INotifyPropertyChanged
    {
        bool IsInputStrSource = false;
        public FrequencyInputBox()
        {
            InitializeComponent();
            Frequency = new Frequency();
            DataContext = this;
            OnFrequencyValueChanged += FrequencyInputBox_OnFrequencyValueChanged;
        }

        #region FrequencyValue
        public double FrequencyValue
        {
            get
            {
                return (double)GetValue(FrequencyValueProperty);
            }
            set
            {
                SetValue(FrequencyValueProperty, value);
            }
        }

        public static readonly DependencyProperty FrequencyValueProperty =
            DependencyProperty.Register("FrequencyValue", 
                typeof(double), typeof(FrequencyInputBox),
                new FrameworkPropertyMetadata(new PropertyChangedCallback(FrequencyValuePropertyChangedCallback)));


        private static void FrequencyValuePropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (((FrequencyInputBox)d).OnFrequencyValueChanged != null)
                ((FrequencyInputBox)d).OnFrequencyValueChanged(d, e);
        }
        public event PropertyChanged OnFrequencyValueChanged;

        private void FrequencyInputBox_OnFrequencyValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!IsInputStrSource)
            {
                Frequency = new Frequency(FrequencyValue);
                Frequency.GeneteateNewInputString();
                InputString = Frequency.InputString;
                OnPropertyChanged("InputString");
            }
            else
            {
                Frequency = new Frequency(FrequencyValue, Frequency.Unit);//?? 
                Frequency.GeneteateNewInputString();
            }
            IsInputStrSource = false;
        }
        #endregion

        #region InputString


        public string InputString
        {
            get
            {
                return Frequency.InputString;
            }
            set
            {
                IsInputStrSource = true;
                Frequency.InputString = value;

                FrequencyValue = Frequency.Hz;
            }
        }
        #endregion

        private Frequency Frequency;
        //public Frequency Frequency
        //{
        //    get
        //    {
        //        return frequency;
        //    }
        //    set
        //    {
        //        frequency = value;
        //    }
        //}



        //internal void SetFrequencyFromDouble(double value)
        //{
        //    Frequency.SetFromDouble(value);
        //    InputString = Frequency.ToString();
        //    OnPropertyChanged("Frequency");
        //    OnPropertyChanged("InputString");
        //}



        //public bool Validity
        //{
        //    get
        //    {
        //        return Validation.IsStringValid(InputString);
        //    }
        //}

        //public void OnInputStringChanged()
        //{
        //    InputString = Frequency.ToString();

        //    OnPropertyChanged("Validity");
        //    OnPropertyChanged("Frequency");
        //    OnPropertyChanged("InputString");
        //}

        //private void TextBox_KeyUp(object sender, KeyEventArgs e)
        //{
        //    if(e.Key==Key.Enter)
        //    {
        //        VM.OnInputStringChanged();
        //    }
        //}

        //private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        //{
        //    VM.OnInputStringChanged();
        //}

        #region INPCImlementation
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));

        }
        #endregion
    }
}
