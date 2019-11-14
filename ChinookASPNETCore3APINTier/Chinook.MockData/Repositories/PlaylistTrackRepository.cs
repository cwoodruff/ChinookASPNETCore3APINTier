using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;
using Chinook.Domain.Repositories;
using Chinook.Domain.Entities;

namespace Chinook.MockData.Repositories
{
    public class PlaylistTrackRepository : IPlaylistTrackRepository
    {
        public void Dispose()
        {
        }

        public List<PlaylistTrack> GetAll()
            => new List<PlaylistTrack>
            {new PlaylistTrack
            {
                PlaylistId = 1,
                TrackId = 1
            }};

        public List<PlaylistTrack> GetByPlaylistId(int id)
            => new List<PlaylistTrack>
            {new PlaylistTrack
            {
                PlaylistId = id,
                TrackId = 1
            }};

        public List<PlaylistTrack> GetByTrackId(int id)
            => new List<PlaylistTrack>
            {new PlaylistTrack
            {
                PlaylistId = 1,
                TrackId = id
            }};

        public PlaylistTrack Add(PlaylistTrack newPlaylistTrack) => newPlaylistTrack;

        public bool Update(PlaylistTrack playlistTrack) => true;

        public bool Delete(int id) => true;
    }
}