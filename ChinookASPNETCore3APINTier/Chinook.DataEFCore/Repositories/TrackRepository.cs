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
    public class TrackRepository : EfRepository<Album>
    {
        public TrackRepository(ChinookContext context) : base(context)
        {
        }

        public List<Track> GetByAlbumId(int id) =>
            _dbContext.Track.Where(a => a.AlbumId == id).ToList();

        public List<Track> GetByGenreId(int id) =>
            _dbContext.Track.Where(a => a.GenreId == id).ToList();

        public List<Track> GetByMediaTypeId(int id) =>
            _dbContext.Track.Where(a => a.MediaTypeId == id).ToList();
        
        public List<Track> GetByPlaylistId(int id) =>
            _dbContext.PlaylistTrack.Where(p => p.PlaylistId == id).Select(p => p.Track).ToList();

        public List<Track> GetByArtistId(int id) => 
            _dbContext.Album.Where(a => a.ArtistId == 5).SelectMany(t => t.Tracks).ToList();

            public List<Track> GetByInvoiceId(int id) =>_dbContext.Track
                .Where(c => c.InvoiceLines.Any(o => o.InvoiceId == id))
                .ToList();
        }
}