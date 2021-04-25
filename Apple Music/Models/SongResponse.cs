using System.Collections.Generic;

namespace Apple_Music.Models
{
    public class SongResponse
    {
        public short? State { get; set; }
        public string AlbumName { get; set; }
        public string ArtistName { get; set; }
        public int DurationInMillis { get; set; }
        public string Name { get; set; }
    }
}