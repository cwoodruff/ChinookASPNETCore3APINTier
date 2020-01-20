using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chinook.Domain.Repositories;
using Chinook.Domain.Entities;

namespace Chinook.Data.Repositories
{
    public class ArtistRepository : IArtistRepository
    {
        private readonly ChinookContext _context;        

        public ArtistRepository(ChinookContext context)
        {
            _context = context;
        }

        public bool ArtistExists(int id) => _context.Artist.Any(a => a.ArtistId == id);

        public void Dispose() => _context.Dispose();

        public async Task<IAsyncEnumerable<Artist>> GetAll() => await _context.GetAllArtists();

        public async Task<Artist> GetById(int id) => await _context.GetArtist(id);

        public async Task<Artist> Add(Artist newArtist)
        {
            if (newArtist == null)
                return null;
            var artist = await _context.Artist.AddAsync(newArtist);
            await _context.SaveChangesAsync();
            return artist.Entity;
        }

        public async Task<bool> Update(Artist artist)
        {
            if (!ArtistExists(artist.ArtistId))
                return false;
            _context.Artist.Update(artist);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(int id)
        {
            if (!ArtistExists(id))
                return false;
            var toRemove = _context.Artist.Find(id);
            _context.Artist.Remove(toRemove);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}