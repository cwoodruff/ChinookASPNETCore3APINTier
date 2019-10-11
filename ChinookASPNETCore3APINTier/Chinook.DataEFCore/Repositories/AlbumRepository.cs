using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Chinook.Domain.Repositories;
using Chinook.Domain.Entities;

namespace Chinook.DataEFCore.Repositories
{
    public class AlbumRepository : IAlbumRepository
    {
        private readonly ChinookContext _context;

        public AlbumRepository(ChinookContext context)
        {
            _context = context;
        }

        private bool AlbumExists(int id) =>
            _context.Album.Any(a => a.AlbumId == id);

        public void Dispose() => _context.Dispose();

        public List<Album> GetAll() => _context.Album.AsNoTracking().ToList();

        public Album GetById(int id)
        {
            var dbAlbum = _context.Album.Find(id);
            return dbAlbum;
        }

        public Album Add(Album newAlbum)
        {
            _context.Album.Add(newAlbum);
            _context.SaveChanges();
            return newAlbum;
        }

        public bool Update(Album album)
        {
            if (!AlbumExists(album.AlbumId))
                return false;
            _context.Album.Update(album);
            _context.SaveChanges();
            return true;
        }

        public bool Delete(int id)
        {
            if (!AlbumExists(id))
                return false;
            var toRemove = _context.Album.Find(id);
            _context.Album.Remove(toRemove);
            _context.SaveChanges();
            return true;
        }

        public List<Album> GetByArtistId(int id) =>
            _context.Album.Where(a => a.ArtistId == id).ToList();
    }
}