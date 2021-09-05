using FrequencyInputBox.Model;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace FrequencyInputBox
{
    public delegate void PropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e);


    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class FrequencyInputBox : UserControl, INotifyPropertyChanged
    {
        #region Fields
        /// <summary>
        /// Флаг, показывающий откуда пришли изменения (из текстового поля или из свойства зависимости FrequencyInHzValue)
        /// </summary>
        private bool IsChangingFromInputString = false;
        private Frequency frequency;
        #endregion

        #region ctor
        public FrequencyInputBox()
        {
            InitializeComponent();
            frequency = new Frequency();
            DataContext = this;
            OnFrequencyInHzValueChanged += FrequencyInputBox_OnFrequencyInHzValueChanged;
        }
        #endregion

        #region FrequencyInHzValue
        /// <summary>
        /// Свойство зависимости - значение частоты в Гц, позволяющее организовать привязку данных из внешнего источника
        /// </summary>
        public double FrequencyInHzValue
        {
            get
            {
                return (double)GetValue(FrequencyInHzValueProperty);
            }
            set
            {
                SetValue(FrequencyInHzValueProperty, value);
            }
        }

        public static readonly DependencyProperty FrequencyInHzValueProperty =
            DependencyProperty.Register("FrequencyInHzValue",
                typeof(double), typeof(FrequencyInputBox),
                new FrameworkPropertyMetadata(
                    new PropertyChangedCallback(FrequencyInHzValuePropertyChangedCallback)));


        private static void FrequencyInHzValuePropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (((FrequencyInputBox)d).OnFrequencyInHzValueChanged != null)
                ((FrequencyInputBox)d).OnFrequencyInHzValueChanged(d, e);
        }
        public event PropertyChanged OnFrequencyInHzValueChanged;

        private void FrequencyInputBox_OnFrequencyInHzValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!IsChangingFromInputString)
            {
                frequency = new Frequency(FrequencyInHzValue);
                InputString = frequency.InputString;
                OnPropertyChanged("InputString");
            }
            else
            {
                frequency = new Frequency(frequency.Hz, frequency.Unit);
            }
            IsChangingFromInputString = false;
        }
        #endregion

        #region InputString

        public string InputString
        {
            get
            {
                return frequency.InputString;
            }
            set
            {
                SetInputString(value);
            }
        }

        private void SetInputString(string str)
        {
            IsChangingFromInputString = true;
            frequency = new Frequency(str);
            FrequencyInHzValue = frequency.Hz;
            OnPropertyChanged("Validity");
        }
        #endregion

        #region Validity Property
        public bool Validity
        {
            get
             {
                return Model.Validation.IsStringValid(InputString);
            }
        }
        #endregion

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
