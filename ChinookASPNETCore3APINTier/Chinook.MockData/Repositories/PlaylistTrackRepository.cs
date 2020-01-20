using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chinook.Domain.Repositories;
using Chinook.Domain.Entities;

namespace Chinook.MockData.Repositories
{
    public class PlaylistTrackRepository : IPlaylistTrackRepository
    {
        public void Dispose()
        {
        }

        public bool PlaylistTrackExists(int id) => true;

        public async Task<IAsyncEnumerable<PlaylistTrack>> GetAll()
            => (new List<PlaylistTrack>
            {new PlaylistTrack
            {
                PlaylistId = 1,
                TrackId = 1
            }}).ToAsyncEnumerable();

        public async Task<IAsyncEnumerable<PlaylistTrack>> GetByPlaylistId(int id)
            => (new List<PlaylistTrack>
            {new PlaylistTrack
            {
                PlaylistId = id,
                TrackId = 1
            }}).ToAsyncEnumerable();

        public async Task<IAsyncEnumerable<PlaylistTrack>> GetByTrackId(int id)
            => (new List<PlaylistTrack>
            {new PlaylistTrack
            {
                PlaylistId = 1,
                TrackId = id
            }}).ToAsyncEnumerable();

        public async Task<PlaylistTrack> Add(PlaylistTrack newPlaylistTrack) => newPlaylistTrack;

        public async Task<bool> Update(PlaylistTrack playlistTrack) => true;

        public async Task<bool> Delete(int id) => true;
    }
}