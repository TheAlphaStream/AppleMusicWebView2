using System;
using System.Windows;

namespace Apple_Music
{
    public partial class Settings : Window
    {
        #region Variables
    
        private readonly DataViewModel _dvm = new DataViewModel();
        private readonly DiscordRichPresence _rpc = new DiscordRichPresence();

        #endregion

        public Settings()
        {
            InitializeComponent();
            DataContext = _dvm;

            SetSavedValues();

            RpcGroup.IsEnabledChanged += RpcEnabledChanged;
            SettingsDiscordFirstLine.LostFocus += SaveSettings;
            SettingsDiscordSecondLine.LostFocus += SaveSettings;
            SettingsWebUrl.LostFocus += SaveSettings;
        }

        private void SetSavedValues()
        {
            SettingsDiscordFirstLine.Text = Properties.Settings.Default.DiscordRpcFirstLine;
            SettingsDiscordSecondLine.Text = Properties.Settings.Default.DiscordRpcSecondLine;
            SettingsWebUrl.Text = Properties.Settings.Default.WebUrl;
        }

        private void RpcEnabledChanged(object sender, DependencyPropertyChangedEventArgs args)
        {
            _dvm.IsDiscordRpcEnabled = RpcGroup.IsEnabled;
            RpcGroup.Height = RpcGroup.IsEnabled ? double.NaN : 0;
        }

        private void SaveSettings(object sender, dynamic args)
        {
            if (SettingsDiscordFirstLine.Text == "" || SettingsDiscordSecondLine.Text == "" ||
                SettingsWebUrl.Text == "")
            {
                MessageBox.Show("Input can't be empty");
                SetSavedValues();
                return;
            }

            if (!SettingsWebUrl.Text.Contains("music.apple.com"))
            {
                MessageBox.Show("Initial URL has to be from Apple Music");
                SetSavedValues();
                return;
            }
            
            Properties.Settings.Default.DiscordRpcFirstLine = SettingsDiscordFirstLine.Text;
            Properties.Settings.Default.DiscordRpcSecondLine = SettingsDiscordSecondLine.Text;
            Properties.Settings.Default.WebUrl = SettingsWebUrl.Text;
            Properties.Settings.Default.Save();
        }
    }
}