using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Chinook.Domain.Repositories;
using Chinook.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Chinook.Data.Repositories
{
    public class PlaylistRepository : IPlaylistRepository
    {
        private readonly ChinookContext _context;

        public PlaylistRepository(ChinookContext context)
        {
            _context = context;
        }

        public bool PlaylistExists(int id) => _context.Playlist.Any(i => i.PlaylistId == id);

        public void Dispose() => _context.Dispose();

        public async Task<IAsyncEnumerable<Playlist>> GetAll() => await _context.GetAllPlaylists();

        public async Task<Playlist> GetById(int id) => await _context.GetPlaylist(id);

        public IAsyncEnumerable<Track> GetTrackByPlaylistId(int id)
        {
            var query = _context.PlaylistTrack
                .Include(plt => plt.Track)
                .Where(plt => plt.PlaylistId == id)
                .AsNoTracking();
            IAsyncEnumerable<Track> tracks = query.Select(plt => plt.Track).AsAsyncEnumerable();
            return tracks;
        }

        public async Task<Playlist> Add(Playlist newPlaylist)
        {
            if (newPlaylist == null)
                return null;
            var playList = await _context.Playlist.AddAsync(newPlaylist);
            await _context.SaveChangesAsync();
            return playList.Entity;
        }

        public async Task<bool> Update(Playlist playlist)
        {
            if (!PlaylistExists(playlist.PlaylistId))
                return false;
            _context.Playlist.Update(playlist);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(int id)
        {
            if (!PlaylistExists(id))
                return false;
            var toRemove = _context.Playlist.Find(id);
            _context.Playlist.Remove(toRemove);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}