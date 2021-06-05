using System;
using DiscordRPC;
using Apple_Music.Models;

namespace Apple_Music
{
    public class DiscordRichPresence
    {
        #region Variables

        private SongResponse _data = new SongResponse();
        private DiscordRpcClient _client;
        private string _details;
        private string _state;
        private readonly DataViewModel _dvm = new DataViewModel();

        #endregion
        
        public void Initialize()
        {
            if (_client != null) return;

            _client = new DiscordRpcClient("808063700509786183");
            _client.Initialize();
        }

        public void UpdatePresence(SongResponse newData)
        {
            if (_client == null) return;
            
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
                        LargeImageText = "LISTENING TO APPLE MUSIC"
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
