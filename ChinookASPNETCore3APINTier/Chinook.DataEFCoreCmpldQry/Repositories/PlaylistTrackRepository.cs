using System;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using Chinook.Domain.Repositories;
using Chinook.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Chinook.DataEFCoreCmpldQry.Repositories
{
    public class PlaylistTrackRepository : IPlaylistTrackRepository
    {
        private readonly ChinookContext _context;

        public PlaylistTrackRepository(ChinookContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        private bool PlaylistTrackExists(int id) =>
            _context.PlaylistTrack.Any(pt => pt.PlaylistId == id);

        public void Dispose() => _context.Dispose();

        public List<PlaylistTrack> GetAll() 
            => _context.GetAllPlaylistTracks();

        public List<PlaylistTrack> GetByPlaylistId(int id) => _context.GetPlaylistTrackByPlaylistId(id);

        public List<PlaylistTrack> GetByTrackId(int id) => _context.GetPlaylistTracksByTrackId(id);

        public PlaylistTrack Add(PlaylistTrack newPlaylistTrack)
        {
            _context.PlaylistTrack.Add(newPlaylistTrack);
            _context.SaveChanges();
            return newPlaylistTrack;
        }

        public bool Update(PlaylistTrack playlistTrack)
        {
            if (!PlaylistTrackExists(playlistTrack.PlaylistId))
                return false;
            _context.PlaylistTrack.Update(playlistTrack);
            _context.SaveChanges();
            return true;
        }

        public bool Delete(int id)
        {
            if (!PlaylistTrackExists(id))
                return false;
            var toRemove = _context.PlaylistTrack.Find(id);
            _context.PlaylistTrack.Remove(toRemove);
            _context.SaveChanges();
            return true;
        }
    }
}