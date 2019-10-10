using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Chinook.Domain.Converters;
using Chinook.Domain.Entities;

namespace Chinook.Domain.ApiModels
{
    public sealed class TrackApiModel : IConvertModel<TrackApiModel, Track>
    {

        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int TrackId { get; set; }

        public string Name { get; set; }

        public int AlbumId { get; set; }
        public string AlbumName { get; set; }
        public int MediaTypeId { get; set; }
        public string MediaTypeName { get; set; }
        public int? GenreId { get; set; }
        public string GenreName { get; set; }
        public string Composer { get; set; }
        public int Milliseconds { get; set; }
        public int Bytes { get; set; }
        public decimal UnitPrice { get; set; }

        [NotMapped]
        [Newtonsoft.Json.JsonIgnore]
        public IList<InvoiceLineApiModel> InvoiceLines { get; set; }
        
        [NotMapped]
        [Newtonsoft.Json.JsonIgnore]
        public IList<PlaylistTrackApiModel> PlaylistTracks { get; set; }
        
        [NotMapped]
        [Newtonsoft.Json.JsonIgnore]
        public AlbumApiModel Album { get; set; }
        
        [NotMapped]
        [Newtonsoft.Json.JsonIgnore]
        public GenreApiModel Genre { get; set; }
        
        [NotMapped]
        [Newtonsoft.Json.JsonIgnore]
        public MediaTypeApiModel MediaType { get; set; }
        
        [NotMapped]
        [Newtonsoft.Json.JsonIgnore]
        public Track Convert => new Track
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