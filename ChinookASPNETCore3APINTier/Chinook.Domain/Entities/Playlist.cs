using Chinook.Domain.Converters;
using Chinook.Domain.ApiModels;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Chinook.Domain.Entities
{
    public class Playlist : IConvertModel<Playlist, PlaylistApiModel>
    {
        public Playlist()
        {
            PlaylistTracks = new HashSet<PlaylistTrack>();
        }

        public int PlaylistId { get; set; }
        public string Name { get; set; }

        [JsonIgnore]
        public virtual ICollection<PlaylistTrack> PlaylistTracks { get; set; }
        
        public PlaylistApiModel Convert() =>
            new PlaylistApiModel
            {
                PlaylistId = PlaylistId,
                Name = Name
            };
    }
}