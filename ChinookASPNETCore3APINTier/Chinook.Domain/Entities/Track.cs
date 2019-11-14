using Chinook.Domain.Converters;
using Chinook.Domain.ApiModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Chinook.Domain.Entities
{
    public class Track : IConvertModel<Track, TrackApiModel>
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int TrackId { get; set; }

        public string Name { get; set; }

        public int AlbumId { get; set; }
        public int MediaTypeId { get; set; }
        public int? GenreId { get; set; }
        public string Composer { get; set; }
        public int Milliseconds { get; set; }
        public int Bytes { get; set; }
        public decimal UnitPrice { get; set; }

        [NotMapped]
        public ICollection<InvoiceLine> InvoiceLines { get; set; } = new HashSet<InvoiceLine>();
        [NotMapped]
        public ICollection<PlaylistTrack> PlaylistTracks { get; set; } = new HashSet<PlaylistTrack>();
        [NotMapped]
        public Album Album { get; set; }
        [NotMapped]
        public Genre Genre { get; set; }
        [NotMapped]
        public MediaType MediaType { get; set; }

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