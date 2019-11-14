using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;
using Chinook.Domain.Repositories;
using Chinook.Domain.Entities;

namespace Chinook.MockData.Repositories
{
    public class MediaTypeRepository : IMediaTypeRepository
    {
        public void Dispose()
        {
        }

        public List<MediaType> GetAll()
            => new List<MediaType>
            {new MediaType
            {
                MediaTypeId = 1,
                Name = "Foo"
            }};

        public MediaType GetById(int id)
            => new MediaType
            {
                MediaTypeId = id,
                Name = "Foo"
            };

        public MediaType Add(MediaType newMediaType) => newMediaType;

        public bool Update(MediaType mediaType) => true;

        public bool Delete(int id) => true;
    }
}