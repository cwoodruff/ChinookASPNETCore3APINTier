using Chinook.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Chinook.Domain.Repositories
{
    public interface IPlaylistRepository : IDisposable
    {
        public bool PlaylistExists(int id);
        Task<IAsyncEnumerable<Playlist>> GetAll();
        Task<Playlist> GetById(int id);
        Task<Playlist> Add(Playlist newPlaylist);
        IAsyncEnumerable<Track> GetTrackByPlaylistId(int id);
        Task<bool> Update(Playlist playlist);
        Task<bool> Delete(int id);
    }
}