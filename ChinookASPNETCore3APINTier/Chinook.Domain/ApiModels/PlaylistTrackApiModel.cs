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
        public PlaylistApiModel Playlist { get; set; }
        [NotMapped]
        public TrackApiModel Track { get; set; }

        public PlaylistTrack Convert() =>
            new PlaylistTrack
            {
                PlaylistId = PlaylistId,
                TrackId = TrackId
            };
    }
}