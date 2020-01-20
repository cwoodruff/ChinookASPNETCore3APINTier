using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Chinook.Domain.Repositories;
using Chinook.Domain.Entities;

namespace Chinook.Data.Repositories
{
    public class AlbumRepository : IAlbumRepository
    {
        private readonly ChinookContext _context;

        public AlbumRepository(ChinookContext context)
        {
            _context = context;
        }

        public bool AlbumExists(int id) => _context.Album.Any(a => a.AlbumId == id);

        public void Dispose() => _context.Dispose();

        public async Task<IAsyncEnumerable<Album>> GetAll() => await _context.GetAllAlbums();

        public async Task<Album> GetById(int id) => await _context.GetAlbum(id);

        public async Task<Album> Add(Album newAlbum)
        {
            if (newAlbum == null)
                return null;
            var album = await _context.Album.AddAsync(newAlbum);
            await _context.SaveChangesAsync();
            return album.Entity;
        }

        public async Task<bool> Update(Album album)
        {
            if (!AlbumExists(album.AlbumId))
                return false;
            _context.Album.Update(album);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(int id)
        {
            if (!AlbumExists(id))
                return false;
            var toRemove = _context.Album.Find(id);
            _context.Album.Remove(toRemove);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IAsyncEnumerable<Album>> GetByArtistId(int id) => await _context.GetAlbumsByArtistId(id);
    }
}