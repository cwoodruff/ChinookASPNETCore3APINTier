using System.Linq;
using System.Collections.Generic;
using Chinook.Domain.Repositories;
using Chinook.Domain.Entities;

namespace Chinook.DataEFCoreCmpldQry.Repositories
{
    public class PlaylistRepository : IPlaylistRepository
    {
        private readonly ChinookContext _context;

        public PlaylistRepository(ChinookContext context)
        {
            _context = context;
        }

        private bool PlaylistExists(int id) =>
            _context.Playlist.Any(i => i.PlaylistId == id);

        public void Dispose() => _context.Dispose();

        public List<Playlist> GetAll() 
            => _context.GetAllPlaylists();

        public Playlist GetById(int id)
        {
            var playlist = _context.GetPlaylist(id);
            return playlist;
        }

        public Playlist Add(Playlist newPlaylist)
        {
            _context.Playlist.Add(newPlaylist);
            _context.SaveChanges();
            return newPlaylist;
        }

        public bool Update(Playlist playlist)
        {
            if (!PlaylistExists(playlist.PlaylistId))
                return false;
            _context.Playlist.Update(playlist);
            _context.SaveChanges();
            return true;
        }

        public bool Delete(int id)
        {
            if (!PlaylistExists(id))
                return false;
            var toRemove = _context.Playlist.Find(id);
            _context.Playlist.Remove(toRemove);
            _context.SaveChanges();
            return true;
        }

        public List<Playlist> GetByTrackId(int id) => _context.GetPlaylistByTrackId(id);
    }
}