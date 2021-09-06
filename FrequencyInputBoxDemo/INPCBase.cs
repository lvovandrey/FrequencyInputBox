using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FrequencyInputBoxDemo
{
    /// <summary>
    /// Хелпер-класс для реализации INPC интерфейса. От него нужно наследовать всякие VM-ки (в общем все, что нуждается в INPC)
    /// </summary>
    public class INPCBase : INotifyPropertyChanged
    {
        #region INPC
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion INPC
    }
}
