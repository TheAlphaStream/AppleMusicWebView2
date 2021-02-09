﻿using System;
using DiscordRPC;

class DiscordRichPresence
{
    private DiscordRpcClient client;
    private MusicKitResponse data = new MusicKitResponse();

    public void Initialize()
    {
        client = new DiscordRpcClient("808063700509786183");
        client.Initialize();
    }

    public void UpdatePresence(MusicKitResponse newData)
    {
        // If music is paused, clear presence
        if (newData.State != null && newData.State != 2)
        {
            client.ClearPresence();
            return;
        }

        // If song's metadata isn't null, update presence with it. Otherwise, just update the playing state.
        if (newData.Name != null && newData.ArtistName != null && newData.AlbumName != null)
        {
            data = newData;
        } else
        {
            data.State = newData.State;
        }

        // Update Rich Presence only if we have the song's metadata
        if (data.Name != null && data.ArtistName != null && data.AlbumName != null)
        {
            client.SetPresence(new RichPresence()
            {
                Details = $"🎵 {data.Name}",
                State = $"🎤{data.ArtistName} 💽 {data.AlbumName}",
                Assets = new Assets()
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
            client.Dispose();
        }
        catch (NullReferenceException e)
        {
            // Don't do anything lol
        }
    }
}