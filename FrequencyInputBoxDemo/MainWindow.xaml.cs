using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
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
using System.Windows.Threading;

namespace FrequencyInputBoxDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        VM vm;
        public MainWindow()
        {
            InitializeComponent();
            vm = new VM();
            DataContext = vm;
        }


        #region Тест для контрола - 
        DispatcherTimer timer;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            timer = new DispatcherTimer() { Interval = new TimeSpan(0, 0, 0, 0, 10) };
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        
        private void Timer_Tick(object sender, object e)
        {
            vm.HZ++;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            timer?.Stop();
        }
        #endregion
    }
}
