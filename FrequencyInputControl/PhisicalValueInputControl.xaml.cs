using PhisicalValueInputControl.Core;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace PhisicalValueInputControl
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class PhisicalValueInputControl : UserControl, INotifyPropertyChanged
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
        public PhisicalValueInputControl()
        {
            UnitsInfoes = Settings.DefaultUnitsInfoes;
            unit = new Unit();
            InitializeComponent();
            PhisicalValueName = "Частота";
        }
        #endregion

        #region Value

        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(
       "Value", typeof(double), typeof(PhisicalValueInputControl), new PropertyMetadata(OnValueChangedCallback));


        private static void OnValueChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {
            var This = (PhisicalValueInputControl)d;
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
       "UnitsInfoes", typeof(List<UnitInfo>), typeof(PhisicalValueInputControl), 
       new PropertyMetadata(OnUnitsInfoesChangedCallback));


        private static void OnUnitsInfoesChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {
            var newUnitsInfoes = (List<UnitInfo>)args.NewValue;
            if(newUnitsInfoes!=null)
                Settings.UnitsInfoes = (List<UnitInfo>)args.NewValue;
            ((PhisicalValueInputControl)d).OnPropertyChanged("InputString");
            ((PhisicalValueInputControl)d).OnPropertyChanged("Validity");
        }

        public List<UnitInfo> UnitsInfoes
        {
            get { return (List<UnitInfo>)GetValue(UnitsInfoesProperty); }
            set { 
                SetValue(UnitsInfoesProperty, value);
                Settings.UnitsInfoes = value;
             }
        }
        #endregion

        #region PhisicalValueName

        public static readonly DependencyProperty PhisicalValueNameProperty = DependencyProperty.Register(
       "PhisicalValueName", typeof(string), typeof(PhisicalValueInputControl),
       new PropertyMetadata(OnPhisicalValueNameChangedCallback));


        private static void OnPhisicalValueNameChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {
            ((PhisicalValueInputControl)d).Label1.Content = (string)args.NewValue;
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
                SetInputString(value);
                OnPropertyChanged();
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
                return Core.Validation.IsStringValid(InputString);
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
    }
}
