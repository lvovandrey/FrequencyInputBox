using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace FrequencyInputBoxDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            double summ = 0;
            string s = "Числа бывают разные: могут быть такие - 666, могут быть отр0.666ицательными -42, могут5e+1 быть дробными148.78 могут быть записаны 1602e+1 экспоненциально 1.602e-1";
            var mts = Regex.Matches(s, @"(-|)\d+(((\.|,)\d+|)+e(\+|-)\d+|(\.|,)\d+|)");
            foreach (Match mt in mts) { Console.WriteLine(mt.Value); summ += Double.Parse(mt.Value.Replace(',', '.'), CultureInfo.InvariantCulture); }
            Console.WriteLine("Сумма равна " + summ);
            Console.Read();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            FrequencyInputBox1.Frequency = 777_000;
        }
    }
}
