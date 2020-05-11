using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Microsoft.EntityFrameworkCore;
using Chinook.Domain.Repositories;
using Chinook.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace Chinook.DataEFCore.Repositories
{
    public class GenreRepository : IGenreRepository
    {
        private readonly ChinookContext _context;

        public GenreRepository(ChinookContext context)
        {
            _context = context;
        }

        public GenreRepository()
        {
            var services = new ServiceCollection();
            
            var connection = String.Empty;
            
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                connection = "Server=.;Database=Chinook;Trusted_Connection=True;Application Name=ChinookASPNETCoreAPINTier";
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux) || RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                connection = "Server=localhost,1433;Database=Chinook;User=sa;Password=P@55w0rd;Trusted_Connection=False;Application Name=ChinookASPNETCoreAPINTier";
            }

            services.AddDbContextPool<ChinookContext>(options => options.UseSqlServer(connection));
            
            var serviceProvider = services.BuildServiceProvider();

            _context = serviceProvider.GetService<ChinookContext>();
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