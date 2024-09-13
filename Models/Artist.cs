using System;

namespace SanatciKayit.Models
{
    public class Artist
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Email { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }

    public class ArtistDetail
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Email { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int TotalAlbums { get; set; }
        public int TotalDurationInSeconds { get; set; }
        public int TotalSalesCount { get; set; }
        public int TotalHours { get; set; }
        public int TotalMinutes { get; set; }
    }

}
