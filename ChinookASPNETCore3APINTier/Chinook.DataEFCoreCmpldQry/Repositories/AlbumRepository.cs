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
    public class AlbumRepository : IAlbumRepository
    {
        private readonly ChinookContext _context;

        public AlbumRepository(ChinookContext context)
        {
            _context = context;
        }

        public AlbumRepository()
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

        private bool AlbumExists(int id) =>
            _context.Album.Any(a => a.AlbumId == id);

        public void Dispose() => _context.Dispose();

        public List<Album> GetAll() 
            => _context.GetAllAlbums();

        public Album GetById(int id)
        {
            var album = _context.GetAlbum(id);
            return album;
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

            _context.Update(album);
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

        public List<Album> GetByArtistId(int id) 
            => _context.GetAlbumsByArtistId(id);
    }
}