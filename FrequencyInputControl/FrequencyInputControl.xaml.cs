﻿using FrequencyInputControl.Core;
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

namespace FrequencyInputControl
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class FrequencyInputControl : UserControl, INotifyPropertyChanged
    {
        /// <summary>
        /// Флаг, показывающий откуда пришли изменения (из текстового поля или из свойства зависимости FrequencyInHzValue)
        /// </summary>
        private bool IsChangingFromInputString = false;
        private Frequency frequency;

        public FrequencyInputControl()
        {
            Settings.RefreshRegexPatterns();
            frequency = new Frequency();
            InitializeComponent();
        }

        #region FrequencyValue

        public static readonly DependencyProperty FrequencyValueProperty = DependencyProperty.Register(
       "FrequencyValue", typeof(double), typeof(FrequencyInputControl), new PropertyMetadata(OnFrequencyValueChangedCallback));


        private static void OnFrequencyValueChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {
            var This = (FrequencyInputControl)d;
            if (!This.IsChangingFromInputString)
            {
                This.frequency = new Frequency(This.FrequencyValue);
                This.InputString = This.frequency.InputString;
                This.OnPropertyChanged("InputString");
            }
            else
            {
                This.frequency = new Frequency(This.frequency.Hz, This.frequency.Unit);
            }
            This.IsChangingFromInputString = false;
        }

        public double FrequencyValue
        {
            get { return (double)GetValue(FrequencyValueProperty); }
            set { SetValue(FrequencyValueProperty, value); }
        }
        #endregion

        #region UnitsInfoes

        public static readonly DependencyProperty UnitsInfoesProperty = DependencyProperty.Register(
       "UnitsInfoes", typeof(List<UnitInfo>), typeof(FrequencyInputControl), 
       new PropertyMetadata(OnUnitsInfoesChangedCallback));


        private static void OnUnitsInfoesChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {
            Settings.RefreshRegexPatterns();
        }

        public List<UnitInfo> UnitsInfoes
        {
            get { return (List<UnitInfo>)GetValue(UnitsInfoesProperty); }
            set { 
                SetValue(UnitsInfoesProperty, value);
                Settings.unitsInfoes = value;
                Settings.RefreshRegexPatterns();
            }
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
                OnPropertyChanged();
            }
        }

        private void SetInputString(string str)
        {
            IsChangingFromInputString = true;
            frequency = new Frequency(str);
            FrequencyValue = frequency.Hz;
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