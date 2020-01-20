using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chinook.Domain.Repositories;
using Chinook.Domain.Entities;

namespace Chinook.MockData.Repositories
{
    public class TrackRepository : ITrackRepository
    {
        public void Dispose()
        {
        }

        public bool TrackExists(int id) => true;

        public async Task<IAsyncEnumerable<Track>> GetAll()
            => (new List<Track>
            {new Track
            {
                TrackId = 1,
                Name = "Foo"
            }}).ToAsyncEnumerable();

        public async Task<Track> GetById(int id)
            => new Track
            {
                TrackId = id
            };

        public async Task<Track> Add(Track newTrack) => newTrack;

        public async Task<bool> Update(Track track) => true;

        public async Task<bool> Delete(int id) => true;

        public async Task<IAsyncEnumerable<Track>> GetByAlbumId(int id)
            => (new List<Track>
            {new Track
            {
                TrackId = id
            }}).ToAsyncEnumerable();

        public async Task<IAsyncEnumerable<Track>> GetByGenreId(int id)
            => (new List<Track>
            {new Track
            {
                TrackId = id
            }}).ToAsyncEnumerable();

        public async Task<IAsyncEnumerable<Track>> GetByMediaTypeId(int id)
            => (new List<Track>
            {new Track
            {
                TrackId = id
            }}).ToAsyncEnumerable();
    }
}