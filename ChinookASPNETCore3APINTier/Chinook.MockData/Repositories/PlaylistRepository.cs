using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chinook.Domain.Repositories;
using Chinook.Domain.Entities;

namespace Chinook.MockData.Repositories
{
    public class PlaylistRepository : IPlaylistRepository
    {
        public void Dispose()
        {
        }

        public bool PlaylistExists(int id) => true;

        public async Task<IAsyncEnumerable<Playlist>> GetAll()
            => (new List<Playlist>
            {new Playlist
            {
                PlaylistId = 1,
                Name = "Foo"
            }}).ToAsyncEnumerable();

        public async Task<Playlist> GetById(int id)
            => new Playlist
            {
                PlaylistId = id,
                Name = "Foo"
            };

        public async Task<Playlist> Add(Playlist newPlaylist) => newPlaylist;

        public IAsyncEnumerable<Track> GetTrackByPlaylistId(int id)
            => (new List<Track>
            {new Track
            {
                TrackId = 1,
                Name = "foo",
                AlbumId = 1,
                MediaTypeId = 1,
                GenreId = 1,
                Composer = "foo",
                Milliseconds = 1,
                Bytes = 1,
                UnitPrice = 1
            }}).ToAsyncEnumerable();

        public async Task<bool> Update(Playlist playlist) => true;

        public async Task<bool> Delete(int id) => true;
    }
}