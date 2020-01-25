using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Chinook.Domain.Converters;
using Chinook.Domain.Entities;

namespace Chinook.Domain.ApiModels
{
    public class MediaTypeApiModel : IConvertModel<MediaTypeApiModel, MediaType>
    {
        public int MediaTypeId { get; set; }
        public string Name { get; set; }
        public IList<TrackApiModel> Tracks { get; set; }

        public MediaType Convert() =>
            new MediaType
            {
                MediaTypeId = MediaTypeId,
                Name = Name
            };
    }
}