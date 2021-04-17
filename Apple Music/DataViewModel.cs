using System.ComponentModel;

namespace Apple_Music
{
    public class DataViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        private string _windowTitle = "Apple Music";
        private bool _isDiscordRpcEnabled = Properties.Settings.Default.EnableDiscordRpc;

        public string WindowTitle
        {
            get => _windowTitle;
            set
            {
                _windowTitle = value;
                OnPropertyChanged("WindowTitle");
            }
        }

        public bool IsDiscordRpcEnabled
        {
            get => _isDiscordRpcEnabled;
            set
            {
                _isDiscordRpcEnabled = value;
                Properties.Settings.Default.EnableDiscordRpc = value;
                OnPropertyChanged("IsDiscordRpcEnabled");
            }
        }
    }
}