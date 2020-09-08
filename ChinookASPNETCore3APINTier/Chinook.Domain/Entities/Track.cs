using Chinook.Domain.Converters;
using Chinook.Domain.ApiModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Chinook.Domain.Entities
{
    public class Track : IConvertModel<Track, TrackApiModel>
    {
        public Track()
        {
            InvoiceLines = new HashSet<InvoiceLine>();
            PlaylistTracks = new HashSet<PlaylistTrack>();
        }

        public int TrackId { get; set; }
        public string Name { get; set; }
        public int AlbumId { get; set; }
        public int MediaTypeId { get; set; }
        public int? GenreId { get; set; }
        public string Composer { get; set; }
        public int Milliseconds { get; set; }
        public int? Bytes { get; set; }
        
        [Range(1, 9.99)]
        [DataType(DataType.Currency)]
        public decimal UnitPrice { get; set; }

        [JsonIgnore]
        public virtual Album Album { get; set; }
        [JsonIgnore]
        public virtual Genre Genre { get; set; }
        [JsonIgnore]
        
        [Display(Name = "Media Type")]
        public virtual MediaType MediaType { get; set; }
        [JsonIgnore]
        
        [Display(Name = "Invoice Lines")]
        public virtual ICollection<InvoiceLine> InvoiceLines { get; set; }
        [JsonIgnore]
        public virtual ICollection<PlaylistTrack> PlaylistTracks { get; set; }
        
        public TrackApiModel Convert() =>
            new TrackApiModel
            {
                TrackId = TrackId,
                Name = Name,
                AlbumId = AlbumId,
                MediaTypeId = MediaTypeId,
                GenreId = GenreId,
                Composer = Composer,
                Milliseconds = Milliseconds,
                Bytes = Bytes,
                UnitPrice = UnitPrice
            };
    }
}