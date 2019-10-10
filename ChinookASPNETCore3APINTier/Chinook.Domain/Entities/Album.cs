using Chinook.Domain.Converters;
using Chinook.Domain.ApiModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Chinook.Domain.Entities
{
    public sealed class Album : IConvertModel<Album, AlbumApiModel>
    {
        private Artist _artist;


        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int AlbumId { get; set; }


        public string Title { get; set; }

        public int ArtistId { get; set; }

        [NotMapped]
        [Newtonsoft.Json.JsonIgnore]
        public ICollection<Track> Tracks { get; set; } = new HashSet<Track>();

        [NotMapped]
        [Newtonsoft.Json.JsonIgnore]
        public Artist Artist
        {
            get => _artist;
            set => _artist = value;
        }

        [NotMapped]
        [Newtonsoft.Json.JsonIgnore]
        public AlbumApiModel Convert => new AlbumApiModel
        {
            AlbumId = AlbumId,
            ArtistId = ArtistId,
            Title = Title
        };
    }
}