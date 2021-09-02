using FrequencyInputBox.Formaters;
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
                return VM.frequency.Hz;
            }
            set
            {
                VM.frequency.SetFromDouble(value);
            }
        }

        public static readonly DependencyProperty FrequencyProperty =
            DependencyProperty.Register("Frequency", typeof(double), typeof(FrequencyInputBox));

    }
}
