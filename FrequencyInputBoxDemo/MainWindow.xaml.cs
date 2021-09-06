using NewInput.Core;
using Core2 = PhisicalValueInputControl.Core;
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
            if (vm.HZ <= 0) vm.HZ = 0.001;
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
            vm.UnitsInfoes = new List<Core2.UnitInfo> //можно конечно и вынести core в отдельную либу, или сделать оба компонента в одной либе, но как-то я заранее не догадался
            {
            new Core2.UnitInfo(1, new string[]{"g", "г"}),  
            new Core2.UnitInfo(0.001, new string[]{"mg", "мг"}),
            new Core2.UnitInfo(1000, new string[]{"kg", "кг" }),
            new Core2.UnitInfo(1000_000, new string[]{"t", "т"}),
            new Core2.UnitInfo(1000_000_000, new string[]{"kt", "кт" })
            };
            Indicator1.PhisicalValueName = "Вес";

            vm.UnitsInfoes2 = new List<UnitInfo>
            {
            new UnitInfo(1, new string[]{"g", "г"}),
            new UnitInfo(0.001, new string[]{"mg", "мг"}),
            new UnitInfo(1000, new string[]{"kg", "кг" }),
            new UnitInfo(1000_000, new string[]{"t", "т"}),
            new UnitInfo(1000_000_000, new string[]{"kt", "кт" })
            };
            Indicator2.PhisicalValueName = "Масса";
        }

        private void Button_ChangeUnits2_Click(object sender, RoutedEventArgs e)
        {
            vm.UnitsInfoes = new List<Core2.UnitInfo>
            {
            new Core2.UnitInfo(1, new string[]{"Hz", "H", "Гц"}),
            new Core2.UnitInfo(1000, new string[]{"kHz", "k", "кГц" }),
            new Core2.UnitInfo(1000_000, new string[]{"MHz", "M", "МГц" }),
            new Core2.UnitInfo(1000_000_000, new string[]{"GHz", "G", "ГГц" }),
            };
            Indicator1.PhisicalValueName = "Частота";

            vm.UnitsInfoes2 = new List<UnitInfo>
            {
            new UnitInfo(1, new string[]{"Hz", "H", "Гц"}),
            new UnitInfo(1000, new string[]{"kHz", "k", "кГц" }),
            new UnitInfo(1000_000, new string[]{"MHz", "M", "МГц" }),
            new UnitInfo(1000_000_000, new string[]{"GHz", "G", "ГГц" }),
            };
            Indicator2.PhisicalValueName = "Частота";
        }
    }
}
