using NewInput.Core;
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

namespace NewInput
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class UserControl1 : UserControl
    {
        #region Fields
        /// <summary>
        /// Флаг, показывающий откуда пришли изменения (из текстового поля или из свойства зависимости Value)
        /// У меня не получилось избавиться от цикличности при передачи значения из приложения и из текстового поля самого контроля
        /// без этого костыля. Очень буду благодарен, если расскажете как от этого флага избавиться.
        /// </summary>
        private bool IsChangingFromInputString = false;
        private Unit unit;
        #endregion

        #region ctor
        public UserControl1()
        {
            UnitsInfoes = Settings.DefaultUnitsInfoes;
            unit = new Unit();
            InitializeComponent();
            PhisicalValueName = "Частота";
        }
        #endregion

        #region Value

        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(
       "Value", typeof(double), typeof(UserControl1), new PropertyMetadata(OnValueChangedCallback));


        private static void OnValueChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {
            var This = (UserControl1)d;
            if (!This.IsChangingFromInputString)
            {
                This.unit = new Unit(This.Value);
                This.unit.FormatInBestUnits();
                This.InputString = This.unit.InputString;
                This.OnPropertyChanged("InputString");
            }
            else
            {
                This.unit = new Unit(This.unit.Value, This.unit.UnitInfo);
            }
            This.IsChangingFromInputString = false;
        }

        public double Value
        {
            get { return (double)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }
        #endregion

        #region UnitsInfoes

        public static readonly DependencyProperty UnitsInfoesProperty = DependencyProperty.Register(
       "UnitsInfoes", typeof(List<UnitInfo>), typeof(UserControl1),
       new PropertyMetadata(OnUnitsInfoesChangedCallback));


        private static void OnUnitsInfoesChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {
            var newUnitsInfoes = (List<UnitInfo>)args.NewValue;
            if (newUnitsInfoes != null)
                Settings.UnitsInfoes = (List<UnitInfo>)args.NewValue;
            ((UserControl1)d).OnPropertyChanged("InputString");
            ((UserControl1)d).OnPropertyChanged("Validity");
        }

        public List<UnitInfo> UnitsInfoes
        {
            get { return (List<UnitInfo>)GetValue(UnitsInfoesProperty); }
            set
            {
                SetValue(UnitsInfoesProperty, value);
                Settings.UnitsInfoes = value;
            }
        }
        #endregion

        #region PhisicalValueName

        public static readonly DependencyProperty PhisicalValueNameProperty = DependencyProperty.Register(
       "PhisicalValueName", typeof(string), typeof(UserControl1),
       new PropertyMetadata(OnPhisicalValueNameChangedCallback));


        private static void OnPhisicalValueNameChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {
            ((UserControl1)d).Label1.Content = (string)args.NewValue;
        }

        public string PhisicalValueName
        {
            get { return (string)GetValue(PhisicalValueNameProperty); }
            set { SetValue(PhisicalValueNameProperty, value); }
        }
        #endregion

        #region InputString
        public string InputString
        {
            get
            {
                return unit.InputString;
            }
            set
            {
                refresh();
            }
        }

        private void SetInputString(string str)
        {
            IsChangingFromInputString = true;
            unit = new Unit(str);
            Value = unit.Value;
            OnPropertyChanged("Validity");
        }
        #endregion

        #region Validity Property
        public bool Validity
        {
            get
            {
                return NewInput.Core.Validation.IsStringValid(InputString);
            }
        }
        #endregion

        #region INPC
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        private void TxtBlock1_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.Key==Key.Enter)
            {
                SetInputString(TxtBlock1.Text);
                TxtBlock1.Text= unit.InputString;
                OnPropertyChanged("InputString");
                OnPropertyChanged("Validity");

            }
        }

        private void refresh()
        {
            TxtBlock1.Text = unit.InputString;
            OnPropertyChanged("Validity");
        }
    }
}
