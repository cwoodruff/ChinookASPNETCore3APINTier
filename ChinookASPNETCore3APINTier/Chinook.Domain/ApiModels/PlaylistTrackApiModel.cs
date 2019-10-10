using System.ComponentModel.DataAnnotations.Schema;
using Chinook.Domain.Converters;
using Chinook.Domain.Entities;

namespace Chinook.Domain.ApiModels
{
    public class PlaylistTrackApiModel : IConvertModel<PlaylistTrackApiModel, PlaylistTrack>
    {

        public int PlaylistId { get; set; }

        public int TrackId { get; set; }

        [NotMapped]
        [Newtonsoft.Json.JsonIgnore]
        public PlaylistApiModel Playlist { get; set; }
        [NotMapped]
        [Newtonsoft.Json.JsonIgnore]
        public TrackApiModel Track { get; set; }
        
        [NotMapped]
        [Newtonsoft.Json.JsonIgnore]
        public PlaylistTrack Convert => new PlaylistTrack
        {
            PlaylistId = PlaylistId,
            TrackId = TrackId
        };
    }
}