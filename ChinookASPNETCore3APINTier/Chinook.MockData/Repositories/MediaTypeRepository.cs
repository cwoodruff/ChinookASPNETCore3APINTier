using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chinook.Domain.Repositories;
using Chinook.Domain.Entities;

namespace Chinook.MockData.Repositories
{
    public class MediaTypeRepository : IMediaTypeRepository
    {
        public void Dispose()
        {
        }

        public bool MediaTypeExists(int id) => true;

        public async Task<IAsyncEnumerable<MediaType>> GetAll()
            => (new List<MediaType>
            {new MediaType
            {
                MediaTypeId = 1,
                Name = "Foo"
            }}).ToAsyncEnumerable();

        public async Task<MediaType> GetById(int id)
            => new MediaType
            {
                MediaTypeId = id,
                Name = "Foo"
            };

        public async Task<MediaType> Add(MediaType newMediaType) => newMediaType;

        public async Task<bool> Update(MediaType mediaType) => true;

        public async Task<bool> Delete(int id) => true;
    }
}