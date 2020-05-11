using System;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Chinook.Domain.Repositories;
using Chinook.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Chinook.DataEFCoreCmpldQry.Repositories
{
    public class PlaylistRepository : IPlaylistRepository
    {
        private readonly ChinookContext _context;

        public PlaylistRepository(ChinookContext context)
        {
            _context = context;
        }

        public PlaylistRepository()
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

        private bool PlaylistExists(int id) =>
            _context.Playlist.Any(i => i.PlaylistId == id);

        public void Dispose() => _context.Dispose();

        public List<Playlist> GetAll() 
            => _context.GetAllPlaylists();

        public Playlist GetById(int id)
        {
            var playlist = _context.GetPlaylist(id);
            return playlist;
        }

        public Playlist Add(Playlist newPlaylist)
        {
            _context.Playlist.Add(newPlaylist);
            _context.SaveChanges();
            return newPlaylist;
        }

        public bool Update(Playlist playlist)
        {
            if (!PlaylistExists(playlist.PlaylistId))
                return false;
            _context.Playlist.Update(playlist);
            _context.SaveChanges();
            return true;
        }

        public bool Delete(int id)
        {
            if (!PlaylistExists(id))
                return false;
            var toRemove = _context.Playlist.Find(id);
            _context.Playlist.Remove(toRemove);
            _context.SaveChanges();
            return true;
        }

        public List<Playlist> GetByTrackId(int id) => _context.GetPlaylistByTrackId(id);
    }
}