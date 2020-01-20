using Chinook.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Chinook.Domain.Repositories
{
    public interface IPlaylistTrackRepository : IDisposable
    {
        public bool PlaylistTrackExists(int id);
        Task<IAsyncEnumerable<PlaylistTrack>> GetAll();
        Task<IAsyncEnumerable<PlaylistTrack>> GetByPlaylistId(int id);
        Task<IAsyncEnumerable<PlaylistTrack>> GetByTrackId(int id);
        Task<PlaylistTrack> Add(PlaylistTrack newPlaylistTrack);
        Task<bool> Update(PlaylistTrack playlistTrack);
        Task<bool> Delete(int id);
    }
}