using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices;
using Microsoft.EntityFrameworkCore;
using Chinook.Domain.Repositories;
using Chinook.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace Chinook.DataEFCore.Repositories
{
    public class TrackRepository : ITrackRepository
    {
        private readonly ChinookContext _context;

        public TrackRepository(ChinookContext context)
        {
            _context = context;
        }

        public TrackRepository()
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

        private bool TrackExists(int id) =>
            _context.Track.Any(i => i.TrackId == id);

        public void Dispose() => _context.Dispose();

        public List<Track> GetAll() =>
            _context.Track.AsNoTracking().ToList();

        public Track GetById(int id) =>
            _context.Track.Find(id);

        public Track Add(Track newTrack)
        {
            _context.Track.Add(newTrack);
            _context.SaveChanges();
            return newTrack;
        }

        public bool Update(Track track)
        {
            if (!TrackExists(track.TrackId))
                return false;
            _context.Track.Update(track);
            _context.SaveChanges();
            return true;
        }

        public bool Delete(int id)
        {
            if (!TrackExists(id))
                return false;
            var toRemove = _context.Track.Find(id);
            _context.Track.Remove(toRemove);
            _context.SaveChanges();
            return true;
        }

        public List<Track> GetByAlbumId(int id) =>
            _context.Track.Where(a => a.AlbumId == id).ToList();

        public List<Track> GetByGenreId(int id) =>
            _context.Track.Where(a => a.GenreId == id).ToList();

        public List<Track> GetByMediaTypeId(int id) =>
            _context.Track.Where(a => a.MediaTypeId == id).ToList();
        
        public List<Track> GetByPlaylistId(int id) =>
            _context.PlaylistTrack.Where(p => p.PlaylistId == id).Select(p => p.Track).ToList();

        public List<Track> GetByArtistId(int id) => 
            _context.Album.Where(a => a.ArtistId == 5).SelectMany(t => t.Tracks).ToList();

            public List<Track> GetByInvoiceId(int id) =>_context.Track
                .Where(c => c.InvoiceLines.Any(o => o.InvoiceId == id))
                .ToList();
        }
}