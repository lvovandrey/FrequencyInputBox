using System;
using System.Collections.Generic;
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
    public partial class FrequencyInputBox : UserControl
    {
        VM VM;
        public FrequencyInputBox()
        {
            InitializeComponent();
            VM = new VM();
            DataContext = VM;
        }

        public double Frequency
        {
            get
            {
                return (double)GetValue(FrequencyProperty);
                return VM.Frequency.Hz;
            }
            set
            {
                SetValue(FrequencyProperty, value);
                VM.SetFrequencyFromDouble(value);
            }
        }

        public static readonly DependencyProperty FrequencyProperty =
            DependencyProperty.Register("Frequency", 
                typeof(double), typeof(FrequencyInputBox),
                new FrameworkPropertyMetadata(new PropertyChangedCallback(FrequencyPropertyChangedCallback)));
        public event PropertyChanged OnFrequencyChanged;
        static void FrequencyPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (((FrequencyInputBox)d).OnFrequencyChanged != null)
                ((FrequencyInputBox)d).OnFrequencyChanged(d, e);
        }

        private void TextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.Key==Key.Enter)
            {
                VM.OnInputStringChanged();
            }
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            VM.OnInputStringChanged();
        }
    }
}
