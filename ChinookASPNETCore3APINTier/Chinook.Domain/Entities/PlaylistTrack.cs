using System.ComponentModel.DataAnnotations.Schema;
using Chinook.Domain.Converters;
using Chinook.Domain.ApiModels;

namespace Chinook.Domain.Entities
{
    public class PlaylistTrack : IConvertModel<PlaylistTrack, PlaylistTrackApiModel>
    {
        public int PlaylistId { get; set; }
        public int TrackId { get; set; }

        [NotMapped]
        [Newtonsoft.Json.JsonIgnore]
        public Playlist Playlist { get; set; }
        [NotMapped]
        [Newtonsoft.Json.JsonIgnore]
        public Track Track { get; set; }

        [NotMapped]
        [Newtonsoft.Json.JsonIgnore]
        public PlaylistTrackApiModel Convert => new PlaylistTrackApiModel
        {
            PlaylistId = PlaylistId,
            TrackId = TrackId
        };
    }
}