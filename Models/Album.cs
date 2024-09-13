using System;

namespace SanatciKayit.Models
{
    public class Album
    {
        public int Id { get; set; }
        public int ArtistId { get; set; }
        public string Name { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int DurationInSeconds { get; set; }
        public int SalesCount { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
