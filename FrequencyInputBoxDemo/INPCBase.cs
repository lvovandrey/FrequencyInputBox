using System.ComponentModel;

namespace FrequencyInputBoxDemo
{
    /// <summary>
    /// Хелпер-класс для реализации INPC интерфейса. От него нужно наследовать всякие VM-ки (в общем все, что нуждается в INPC)
    /// </summary>
    public class INPCBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
