using FrequencyInputControl.Core;
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

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            vm.UnitsInfoes = new List<UnitInfo>
            {
            new UnitInfo(UnitType.Hz, 0.001, new string[]{"mg", "мг"},"mg"),
            new UnitInfo(UnitType.Hz, 1, new string[]{"g", "г"},"g"),
            new UnitInfo(UnitType.kHz, 1000, new string[]{"kg", "кг" },"kg"),
            new UnitInfo(UnitType.MHz, 1000_000, new string[]{"t", "т"},"t"),
            new UnitInfo(UnitType.GHz, 1000_000_000, new string[]{"kt", "кт" },"kt")
            };
        }
    }
}
