using System.Windows;

namespace Apple_Music
{
    public partial class Settings : Window
    {
        #region Variables

        private readonly DataViewModel _dvm = new DataViewModel();
        
        #endregion
        public Settings()
        {
            InitializeComponent();
            DataContext = _dvm;
            RpcGroup.IsEnabledChanged += (sender, args) => _dvm.IsDiscordRpcEnabled = RpcGroup.IsEnabled;
        }
    }
}