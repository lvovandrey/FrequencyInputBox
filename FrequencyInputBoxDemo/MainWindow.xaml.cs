using PhisicalValueInputControl.Core;
using System;
using System.Collections.Generic;
using System.Windows;
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


        #region Тест для контрола 
        DispatcherTimer timer;
        private void Button_Start_Click(object sender, RoutedEventArgs e)
        {
            if (timer == null)
            {
                timer = new DispatcherTimer() { Interval = new TimeSpan(0, 0, 0, 0, 500) };
                timer.Tick += Timer_Tick;
            }
            timer.Start();
        }

        private void Timer_Tick(object sender, object e)
        {
            vm.HZ = vm.HZ*10;
        }

        private void Button_Stop_Click(object sender, RoutedEventArgs e)
        {
            timer?.Stop();
        }
        #endregion


        // Замена физических единиц (шкалы по сути) осуществляется через dp UnitsInfoes. Здесь оно вяжется с VM приложения.
        // Отдельно нужно прописать название величины - это свойство PhisicalValueName.
        private void Button_ChangeUnits_Click(object sender, RoutedEventArgs e)
        {
            vm.UnitsInfoes = new List<UnitInfo>
            {
            new UnitInfo(1, new string[]{"g", "г"}),
            new UnitInfo(0.001, new string[]{"mg", "мг"}),
            new UnitInfo(1000, new string[]{"kg", "кг" }),
            new UnitInfo(1000_000, new string[]{"t", "т"}),
            new UnitInfo(1000_000_000, new string[]{"kt", "кт" })
            };
            LoadingIndicator1.PhisicalValueName = "Вес";
        }

        private void Button_ChangeUnits2_Click(object sender, RoutedEventArgs e)
        {
            vm.UnitsInfoes = new List<UnitInfo>
            {
            new UnitInfo(1, new string[]{"Hz", "H", "Гц"}),
            new UnitInfo(1000, new string[]{"kHz", "k", "кГц" }),
            new UnitInfo(1000_000, new string[]{"MHz", "M", "МГц" }),
            new UnitInfo(1000_000_000, new string[]{"GHz", "G", "ГГц" }),
            };
            LoadingIndicator1.PhisicalValueName = "Частота";
        }
    }
}
