using Chinook.Domain.Converters;
using Chinook.Domain.ApiModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Chinook.Domain.Entities
{
    public class Genre : IConvertModel<Genre, GenreApiModel>
    {
        public Genre()
        {
            Tracks = new HashSet<Track>();
        }

        public int GenreId { get; set; }
        public string Name { get; set; }

        [JsonIgnore]
        public virtual ICollection<Track> Tracks { get; set; }
        
        public GenreApiModel Convert() =>
            new GenreApiModel
            {
                GenreId = GenreId,
                Name = Name
            };
    }
}