using System.Threading.Tasks;
using System.Windows;
using Microsoft.Web.WebView2.Core;
using Newtonsoft.Json;

namespace Apple_Music
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Variables
        
        private readonly DataViewModel _dvm;
        private readonly DiscordRichPresence _rpc = new DiscordRichPresence();
        
        #endregion
        
        public MainWindow()
        {
            InitializeComponent();
            
            // Initialize view model
            _dvm = new DataViewModel();
            DataContext = _dvm;
            
            InitializeWebView();
        }

        private async void InitializeWebView()
        {
            // Initialize WebView
            await AmWebView.EnsureCoreWebView2Async();
            // Change window title when document title changes
            AmWebView.CoreWebView2.DocumentTitleChanged += AppleMusic_TitleChanged;
            // Remove extra shit from the website (like the Open in iTunes button) when page loads
            AmWebView.CoreWebView2.SourceChanged += AppleMusic_RemoveExtraElements;
            AmWebView.CoreWebView2.NavigationCompleted += AppleMusic_RemoveExtraElements;
            // Start getting data from now playing
            //webView.CoreWebView2.SourceChanged += AppleMusic_InitDiscordRPC;
            AmWebView.CoreWebView2.NavigationCompleted += InitDiscordRpc;
            // Receive messages from webapp
            AmWebView.CoreWebView2.WebMessageReceived += UpdateRichPresence;
        }
        
        private void AppleMusic_TitleChanged(object sender, object e) => _dvm.WindowTitle = AmWebView.CoreWebView2.DocumentTitle;

        private async void AppleMusic_RemoveExtraElements(object sender, object e)
        {
            await RunJsCode("const elements = document.getElementsByClassName('web-navigation__native-upsell'); while (elements.length > 0) elements[0].remove();");
            await RunJsCode("while (elements.length > 0) elements[0].remove();");
        }
        
        // Credit for the JS code: https://github.com/iiFir3z/Apple-Music-Electron/
        private async void InitDiscordRpc(object sender, object e)
        {
            // yeah...
            _rpc.Initialize();
            // Get playing state
            await RunJsCode(
                "MusicKit.getInstance().addEventListener( MusicKit.Events.playbackStateDidChange, (a) => {" +
                "window.chrome.webview.postMessage({state: a.state});" +
                "});");
            // Get music data. I'm so sorry for making you see this
            await RunJsCode(
                "MusicKit.getInstance().addEventListener( MusicKit.Events.mediaItemStateDidChange, function() {" +
                "const nowPlayingItem =  MusicKit.getInstance().nowPlayingItem; let attributes  = {}; if (nowPlayingItem != null) { attributes = nowPlayingItem.attributes; }" +
                "attributes.name = attributes.name ? attributes.name : null;" +
                "attributes.durationInMillis = attributes.durationInMillis ? attributes.durationInMillis : 0;" +
                "attributes.albumName = attributes.albumName ? attributes.albumName : null;" +
                "attributes.artistName = attributes.artistName ? attributes.artistName : null;" +
                "window.chrome.webview.postMessage(attributes);" +
                "})");
        }
        
        private void UpdateRichPresence(object sender, CoreWebView2WebMessageReceivedEventArgs args)
        {
            if (!Properties.Settings.Default.EnableDiscordRpc) return;
            var response = JsonConvert.DeserializeObject<MusicKitResponse>(args.WebMessageAsJson);
            _rpc.UpdatePresence(response, Properties.Settings.Default.DiscordRpcFirstLine, Properties.Settings.Default.DiscordRpcSecondLine);
        }

        private async Task RunJsCode(string code) => await AmWebView.CoreWebView2.ExecuteScriptAsync(code);
    }
}
