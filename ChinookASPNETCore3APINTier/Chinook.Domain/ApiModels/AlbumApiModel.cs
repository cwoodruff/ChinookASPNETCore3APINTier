using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Chinook.Domain.Converters;
using Chinook.Domain.Entities;

namespace Chinook.Domain.ApiModels
{
    public class AlbumApiModel : IConvertModel<AlbumApiModel, Album>
    {

        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int AlbumId { get; set; }

        public string Title { get; set; }

        public int ArtistId { get; set; }
        
        [NotMapped]
        [Newtonsoft.Json.JsonIgnore]
        public string ArtistName { get; set; }

        [NotMapped]
        [Newtonsoft.Json.JsonIgnore]
        public ArtistApiModel Artist { get; set; }
        
        [NotMapped]
        [Newtonsoft.Json.JsonIgnore]
        public IList<TrackApiModel> Tracks { get; set; }
        
        [NotMapped]
        [Newtonsoft.Json.JsonIgnore]
        public Album Convert => new Album
        {
            AlbumId = AlbumId,
            ArtistId = ArtistId,
            Title = Title
        };
    }
}