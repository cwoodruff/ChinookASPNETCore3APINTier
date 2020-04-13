using Chinook.Domain.Converters;
using Chinook.Domain.ApiModels;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Chinook.Domain.Entities
{
    public class Artist : IConvertModel<Artist, ArtistApiModel>
    {
        public Artist()
        {
            Albums = new HashSet<Album>();
        }

        public int ArtistId { get; set; }
        public string Name { get; set; }
        [JsonIgnore]
        public virtual ICollection<Album> Albums { get; set; }
        
        public ArtistApiModel Convert() =>
            new ArtistApiModel
            {
                ArtistId = ArtistId,
                Name = Name
            };
    }
}