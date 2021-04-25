using System.Collections.Generic;
using System.ComponentModel;
using Apple_Music.Models;

namespace Apple_Music
{
    public class DataViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        private string _windowTitle = "Apple Music";
        private bool _isDiscordRpcEnabled = Properties.Settings.Default.EnableDiscordRpc;
        private List<Lyric> _lyrics;
        private bool _showLyrics = false;
        private bool _enableLyrics = false;

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

        public List<Lyric> Lyrics
        {
            get => _lyrics;
            set
            {
                _lyrics = value;
                OnPropertyChanged("Lyrics");
            }
        }

        public bool ShowLyrics
        {
            get => _showLyrics;
            set
            {
                _showLyrics = value;
                OnPropertyChanged("ShowLyrics");
            }
        }

        public bool EnableLyrics
        {
            get => _enableLyrics;
            set
            {
                _enableLyrics = value;
                OnPropertyChanged("EnableLyrics");
            }
        }
    }
}