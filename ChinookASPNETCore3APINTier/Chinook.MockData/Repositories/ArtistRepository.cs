using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chinook.Domain.Repositories;
using Chinook.Domain.Entities;

namespace Chinook.MockData.Repositories
{
    public class ArtistRepository : IArtistRepository
    {
        public void Dispose()
        {
        }

        public bool ArtistExists(int id) => true;

        public async Task<IAsyncEnumerable<Artist>> GetAll()
            => (new List<Artist>
            {new Artist
            {
                ArtistId = 1,
                Name = "Foo"
            }}).ToAsyncEnumerable();

        public async Task<Artist> GetById(int id)
            => new Artist
            {
                ArtistId = id,
                Name = "Foo"
            };

        public async Task<Artist> Add(Artist newArtist) => newArtist;

        public async Task<bool> Update(Artist artist) => true;

        public async Task<bool> Delete(int id) => true;
    }
}