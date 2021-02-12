using System;
using DiscordRPC;

internal class DiscordRichPresence
{
    #region Variables
    private MusicKitResponse _data = new MusicKitResponse();
    private DiscordRpcClient _client;
    #endregion

    public void Initialize()
    {
        _client = new DiscordRpcClient("808063700509786183");
        _client.Initialize();
    }

    public void UpdatePresence(MusicKitResponse newData)
    {
        // If music is paused, clear presence
        if (newData.State != null && newData.State != 2)
        {
            _client.ClearPresence();
            return;
        }

        // If song's metadata isn't null, update presence with it. Otherwise, just update the playing state.
        if (newData.Name != null && newData.ArtistName != null && newData.AlbumName != null)
            _data = newData;
        else
            _data.State = newData.State;

        // Update Rich Presence only if we have the song's metadata
        if (_data.Name != null && _data.ArtistName != null && _data.AlbumName != null)
        {
            _client.SetPresence(new RichPresence
            {
                Details = $"🎵 {_data.Name}",
                State = $"🎤 {_data.ArtistName} 💽 {_data.AlbumName}",
                Assets = new Assets
                {
                    LargeImageKey = "applemusic_logo",
                    LargeImageText = "Apple Music"
                }
            });
        }
    }

    public void EndConnection()
    {
        // Catch exception when application is closed as soon as it's opened (Discord RPC hasn't been initialized yet)
        try
        {
            _client.Dispose();
        }
        catch (NullReferenceException e)
        {
            // Don't do anything lol
        }
    }
}