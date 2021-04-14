# Apple Music (WebView2)

This is the Apple Music webapp, in a WebView2 (Microsoft Edge - Chromium) container. Integrated with Discord Rich Presence.

Requires the WebView2 Runtime ([Download](https://go.microsoft.com/fwlink/p/?LinkId=2124703) | [Info](https://developer.microsoft.com/en-us/microsoft-edge/webview2/)) or [Microsoft Edge Canary](https://www.microsoftedgeinsider.com/en-us/download)!

[Download latest release (portable)](https://github.com/idkwuu/AppleMusicWebView2/releases/latest/download/AppleMusicWebView2-release.zip)

![Preview](https://i.imgur.com/IdFsR7w.png)

## Usage

- Run with "Apple Music.exe"
- To change settings, edit "Apple Music.exe.config" in any text editor (Notepad)

### Settings

- enableDiscordRPC - enable/disable Rich Presence in Discord (show what's playing) - True/False
    
- discordRpcFirstLine/discordRpcSecondLine - change the text that's displayed on Discord. The following words are replaced with the corresponding text:
    * song - show song title
    * artist - show artist's name
    * album - show album's name

```
discordRpcFirstLine: 🎵 song
discordRpcSecondLine: 🎤 artist 💽 album 

discordRpcFirstLine: song
discordRpcSecondLine: album by artist
```

## Planned things

- Migrate to WPF
- Settings UI
- Last.fm scrobbling
- Lyrics

## Credits

- [Apple-Music-Electron by iiFir3z](https://github.com/iiFir3z/Apple-Music-Electron/) - main idea and JS code to get info from MusicKit and remove the "Open With iTunes" button
- [discord-rpc-csharp by Lachee](https://github.com/Lachee/discord-rpc-csharp) - Discord RPC Library

## Build requirements

- Visual Studio 2019 or JetBrains Rider
- [WebView2 Runtime](https://developer.microsoft.com/en-us/microsoft-edge/webview2/) or [Microsoft Edge Canary](https://www.microsoftedgeinsider.com/en-us/download)

## License

MIT
