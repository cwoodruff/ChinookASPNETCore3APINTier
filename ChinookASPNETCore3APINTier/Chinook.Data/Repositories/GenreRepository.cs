using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chinook.Domain.Repositories;
using Chinook.Domain.Entities;

namespace Chinook.Data.Repositories
{
    public class GenreRepository : IGenreRepository
    {
        private readonly ChinookContext _context;

        public GenreRepository(ChinookContext context)
        {
            _context = context;
        }

        public bool GenreExists(int id) => _context.Genre.Any(g => g.GenreId == id);

        public void Dispose() => _context.Dispose();

        public async Task<IAsyncEnumerable<Genre>> GetAll() => await _context.GetAllGenres();

        public async Task<Genre> GetById(int id) => await _context.GetGenre(id);

        public async Task<Genre> Add(Genre newGenre)
        {
            if (newGenre == null)
                return null;
            var genre = await _context.Genre.AddAsync(newGenre);
            await _context.SaveChangesAsync();
            return genre.Entity;
        }

        public async Task<bool> Update(Genre genre)
        {
            if (!GenreExists(genre.GenreId))
                return false;
            _context.Genre.Update(genre);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(int id)
        {
            if (!GenreExists(id))
                return false;
            var toRemove = _context.Genre.Find(id);
            _context.Genre.Remove(toRemove);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}