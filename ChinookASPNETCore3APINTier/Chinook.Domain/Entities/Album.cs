using Chinook.Domain.Converters;
using Chinook.Domain.ApiModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Chinook.Domain.Entities
{
    public class Album : IConvertModel<Album, AlbumApiModel>
    {
        public Album()
        {
            Tracks = new HashSet<Track>();
        }

        public int AlbumId { get; set; }
        public string Title { get; set; }
        public int ArtistId { get; set; }

        [JsonIgnore]
        public virtual Artist Artist { get; set; }
        [JsonIgnore]
        public virtual ICollection<Track> Tracks { get; set; }

        public AlbumApiModel Convert() =>
            new AlbumApiModel
            {
                AlbumId = AlbumId,
                ArtistId = ArtistId,
                Title = Title
            };
    }
}