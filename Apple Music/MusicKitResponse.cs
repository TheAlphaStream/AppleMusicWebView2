namespace Apple_Music
{
    public class MusicKitResponse
    {
        public short? State { get; set; }
        public string AlbumName { get; set; }
        public string ArtistName { get; set; }
        public int DurationInMillis { get; set; }
        public string Name { get; set; }
    }
}