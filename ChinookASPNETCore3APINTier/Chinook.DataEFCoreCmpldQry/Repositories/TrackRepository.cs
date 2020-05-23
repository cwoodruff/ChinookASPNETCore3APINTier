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
    public class TrackRepository : EfRepository<Album>
    {
        public TrackRepository(ChinookContext context) : base(context)
        {
        }

        public List<Track> GetAll() 
            => _dbContext.GetAllTracks();

        public Track GetById(int id)
        {
            var track = _dbContext.GetTrack(id);
            return track;
        }

        public Track Add(Track newTrack)
        {
            _dbContext.Track.Add(newTrack);
            _dbContext.SaveChanges();
            return newTrack;
        }

        public bool Update(Track track)
        {
            if (!Exists(track.TrackId))
                return false;
            _dbContext.Track.Update(track);
            _dbContext.SaveChanges();
            return true;
        }

        public bool Delete(int id)
        {
            if (!Exists(id))
                return false;
            var toRemove = _dbContext.Track.Find(id);
            _dbContext.Track.Remove(toRemove);
            _dbContext.SaveChanges();
            return true;
        }

        public List<Track> GetByAlbumId(int id) 
            => _dbContext.GetTracksByAlbumId(id);

        public List<Track> GetByGenreId(int id) 
            => _dbContext.GetTracksByGenreId(id);

        public List<Track> GetByMediaTypeId(int id) 
            => _dbContext.GetTracksByMediaTypeId(id);
        
        public List<Track> GetByPlaylistId(int id)
        {
            var tracks = _dbContext.PlaylistTrack.Where(p => p.PlaylistId == id).Select(p => p.Track);
            return tracks.ToList();
        }

        public List<Track> GetByArtistId(int id) => _dbContext.GetTracksByArtistId(id);

        public List<Track> GetByInvoiceId(int id) => _dbContext.GetTracksByInvoiceId(id);
    }
}