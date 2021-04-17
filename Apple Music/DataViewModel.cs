using System.ComponentModel;

namespace Apple_Music
{
    public class DataViewModel: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
		
        protected virtual void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        private string _windowTitle = "Apple Music";
        
        public string WindowTitle
        {
            get => _windowTitle;
            set
            {
                _windowTitle = value;
                OnPropertyChanged("WindowTitle");
            }
        }
    }
}