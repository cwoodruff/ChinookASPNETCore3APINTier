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
    public class GenreRepository : EfRepository<Album>
    {
        public GenreRepository(ChinookContext context) : base(context)
        {
        }

        public List<Genre> GetAll() 
            => _dbContext.GetAllGenres();

        public Genre GetById(int id)
        {
            var genres = _dbContext.GetGenre(id);
            return genres;
        }

        public Genre Add(Genre newGenre)
        {
            _dbContext.Genre.Add(newGenre);
            _dbContext.SaveChanges();
            return newGenre;
        }

        public bool Update(Genre genre)
        {
            if (!Exists(genre.GenreId))
                return false;
            _dbContext.Genre.Update(genre);
            _dbContext.SaveChanges();
            return true;
        }

        public bool Delete(int id)
        {
            if (!Exists(id))
                return false;
            var toRemove = _dbContext.Genre.Find(id);
            _dbContext.Genre.Remove(toRemove);
            _dbContext.SaveChanges();
            return true;
        }
    }
}