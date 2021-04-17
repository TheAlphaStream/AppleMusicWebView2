using System;
using DiscordRPC;

namespace Apple_Music
{
    public class DiscordRichPresence
    {
        #region Variables

        private MusicKitResponse _data = new MusicKitResponse();
        private DiscordRpcClient _client;
        private String Details;
        private String State;

        #endregion
        
        public void Initialize()
        {
            _client = new DiscordRpcClient("808063700509786183");
            _client.Initialize();
        }

        public void UpdatePresence(MusicKitResponse newData, String templateFirstLine, String templateSecondLine)
        {
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
                Details = ParseTemplate(templateFirstLine);
                State = ParseTemplate(templateSecondLine);
            }
            else
                _data.State = newData.State;

            // Update Rich Presence only if we have the song's metadata
            if (_data.Name != null && _data.ArtistName != null && _data.AlbumName != null)
            {
                _client.SetPresence(new RichPresence
                {
                    Details = Details,
                    State = State,
                    Assets = new Assets
                    {
                        LargeImageKey = "applemusic_logo",
                        LargeImageText = "Apple Music"
                    }
                });
            }
        }

        private String ParseTemplate(String text)
        {
            String songReplaced = text.Replace("song", _data.Name);
            String artistReplaced = songReplaced.Replace("artist", _data.ArtistName);
            String final = artistReplaced.Replace("album", _data.AlbumName);
            return final;
        }

        public void EndConnection()
        {
            // Catch exception when application is closed as soon as it's opened (Discord RPC hasn't been initialized yet)
            try
            {
                _client.Dispose();
            }
            catch (NullReferenceException)
            {
                // Don't do anything lol
            }
        }
    }
}