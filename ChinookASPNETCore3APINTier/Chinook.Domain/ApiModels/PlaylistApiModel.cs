using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Chinook.Domain.Converters;
using Chinook.Domain.Entities;

namespace Chinook.Domain.ApiModels
{
    public class PlaylistApiModel : IConvertModel<PlaylistApiModel, Playlist>
    {

        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int PlaylistId { get; set; }

        public string Name { get; set; }
        [NotMapped]
        [Newtonsoft.Json.JsonIgnore]
        public IList<TrackApiModel> Tracks { get; set; }
        [NotMapped]
        [Newtonsoft.Json.JsonIgnore]
        public IList<PlaylistTrackApiModel> PlaylistTracks { get; set; }
        
        [NotMapped]
        [Newtonsoft.Json.JsonIgnore]
        public Playlist Convert => new Playlist
        {
            PlaylistId = PlaylistId,
            Name = Name
        };
    }
}