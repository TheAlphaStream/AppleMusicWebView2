using System;
using DiscordRPC;

namespace Apple_Music
{
    public class DiscordRichPresence
    {
        #region Variables

        private MusicKitResponse _data = new MusicKitResponse();
        public DiscordRpcClient _client;
        private string _details;
        private string _state;

        #endregion
        
        public void Initialize()
        {
            _client = new DiscordRpcClient("808063700509786183");
            _client.Initialize();
        }

        public void UpdatePresence(MusicKitResponse newData)
        {
            if (!_client.IsInitialized || _client == null) return;
            
            // If music is paused, clear presence
            if (newData.State != null && newData.State != 2)
            {
                _client.ClearPresence();
                return;
            }

            // If song's metadata isn't null, update presence with it. Otherwise, just update the playing state.
            if (newData.Name != null && newData.ArtistName != null && newData.AlbumName != null)
            {
                _data = newData;
                _details = ParseTemplate(Properties.Settings.Default.DiscordRpcFirstLine);
                _state = ParseTemplate(Properties.Settings.Default.DiscordRpcSecondLine);
            }
            else
                _data.State = newData.State;

            // Update Rich Presence only if we have the song's metadata
            if (_data.Name != null && _data.ArtistName != null && _data.AlbumName != null)
            {
                _client.SetPresence(new RichPresence
                {
                    Details = _details,
                    State = _state,
                    Assets = new Assets
                    {
                        LargeImageKey = "applemusic_logo",
                        LargeImageText = "Apple Music"
                    }
                });
            }
        }


        private string ParseTemplate(String text)
        {
            var songReplaced = text.Replace("song", _data.Name);
            var artistReplaced = songReplaced.Replace("artist", _data.ArtistName);
            var final = artistReplaced.Replace("album", _data.AlbumName);
            return final;
        }

        public void PausePresence() => _client.ClearPresence();

        public void ResumePresence() => UpdatePresence(_data);

        public void EndConnection()
        {
            // Catch exception when application is closed as soon as it's opened (Discord RPC hasn't been initialized yet)
            try
            {
                _client.Dispose();
            }
            catch (Exception) // stfu
            {
                // Don't do anything lol
            }
        }
    }
}