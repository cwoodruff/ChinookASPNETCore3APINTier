using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using Chinook.Domain.Repositories;
using Chinook.Domain.Entities;

namespace Chinook.MockData.Repositories
{
    public class PlaylistRepository : IPlaylistRepository
    {
        public void Dispose()
        {
        }

        public List<Playlist> GetAll()
            => new List<Playlist>
            {new Playlist
            {
                PlaylistId = 1,
                Name = "Foo"
            }};

        public Playlist GetById(int id)
            => new Playlist
            {
                PlaylistId = id,
                Name = "Foo"
            };

        public Playlist Add(Playlist newPlaylist) => newPlaylist;

        public List<Track> GetTrackByPlaylistId(int id)
            => new List<Track>
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
            }};

        public bool Update(Playlist playlist) => true;

        public bool Delete(int id) => true;
    }
}