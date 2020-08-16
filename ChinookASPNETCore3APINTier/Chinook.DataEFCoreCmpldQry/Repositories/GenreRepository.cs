using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Chinook.Domain.Repositories;
using Chinook.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Chinook.DataEFCoreCmpldQry.Repositories
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

        public List<Genre> GetAll() 
            => _context.GetAllGenres();

        public Genre GetById(int id)
        {
            var genres = _context.GetGenre(id);
            return genres;
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