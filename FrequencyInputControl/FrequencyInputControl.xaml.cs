using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace FrequencyInputControl
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class FrequencyInputControl : UserControl, INotifyPropertyChanged
    {
        public FrequencyInputControl()
        {
            InitializeComponent();
        }

        #region FrequencyValue

        public static readonly DependencyProperty FrequencyValueProperty = DependencyProperty.Register(
       "FrequencyValue", typeof(double), typeof(FrequencyInputControl), new PropertyMetadata(OnFrequencyValueChangedCallback));


        private static void OnFrequencyValueChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {
            var loadIndicator = (FrequencyInputControl)d;
            loadIndicator.OnPropertyChanged("InputString");
        }

        public double FrequencyValue
        {
            get { return (double)GetValue(FrequencyValueProperty); }
            set { SetValue(FrequencyValueProperty, value); }
        }
        #endregion







        public string InputString
        {
            get { return FrequencyValue.ToString() + "Hz"; }
            set { FrequencyValue = double.Parse(value.Replace("Hz", "")); OnPropertyChanged(); }
        }



        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
