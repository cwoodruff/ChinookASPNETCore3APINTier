using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chinook.Domain.Repositories;
using Chinook.Domain.Entities;

namespace Chinook.Data.Repositories
{
    public class PlaylistTrackRepository : IPlaylistTrackRepository
    {
        private readonly ChinookContext _context;

        public PlaylistTrackRepository(ChinookContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public bool PlaylistTrackExists(int id) => _context.PlaylistTrack.Any(pt => pt.PlaylistId == id);

        public void Dispose() => _context.Dispose();

        public Task<IAsyncEnumerable<PlaylistTrack>> GetAll() => _context.GetAllPlaylistTracks();

        public async Task<IAsyncEnumerable<PlaylistTrack>> GetByPlaylistId(int id) => await _context.GetPlaylistTrackByPlaylistId(id);

        public async Task<IAsyncEnumerable<PlaylistTrack>> GetByTrackId(int id) => await _context.GetPlaylistTracksByTrackId(id);

        public async Task<PlaylistTrack> Add(PlaylistTrack newPlaylistTrack)
        {
            if (newPlaylistTrack == null)
                return null;
            var playlistTrack = await _context.PlaylistTrack.AddAsync(newPlaylistTrack);
            await _context.SaveChangesAsync();
            return playlistTrack.Entity;
        }

        public async Task<bool> Update(PlaylistTrack playlistTrack)
        {
            if (!PlaylistTrackExists(playlistTrack.PlaylistId))
                return false;
            _context.PlaylistTrack.Update(playlistTrack);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(int id)
        {
            if (!PlaylistTrackExists(id))
                return false;
            var toRemove = _context.PlaylistTrack.Find(id);
            _context.PlaylistTrack.Remove(toRemove);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}