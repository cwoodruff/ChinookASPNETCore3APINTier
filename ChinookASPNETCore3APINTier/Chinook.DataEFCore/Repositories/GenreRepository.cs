using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Chinook.Domain.Repositories;
using Chinook.Domain.Entities;

namespace Chinook.DataEFCore.Repositories
{
    public class GenreRepository : IGenreRepository
    {
        private readonly ChinookContext _context;

        public GenreRepository(ChinookContext context)
        {
            _context = context;
        }

        private bool GenreExists(int id) =>
            _context.Genre.Any(g => g.GenreId == id);

        public void Dispose() => _context.Dispose();

        public List<Genre> GetAll() =>
            _context.Genre.AsNoTracking().ToList();

        public Genre GetById(int id)
        {
            var dbGenre = _context.Genre.Find(id);
            return dbGenre;
        }

        public Genre Add(Genre newGenre)
        {
            _context.Genre.Add(newGenre);
            _context.SaveChanges();
            return newGenre;
        }

        public bool Update(Genre genre)
        {
            if (!GenreExists(genre.GenreId))
                return false;
            _context.Genre.Update(genre);
            _context.SaveChanges();
            return true;
        }

        public bool Delete(int id)
        {
            if (!GenreExists(id))
                return false;
            var toRemove = _context.Genre.Find(id);
            _context.Genre.Remove(toRemove);
            _context.SaveChanges();
            return true;
        }
    }
}