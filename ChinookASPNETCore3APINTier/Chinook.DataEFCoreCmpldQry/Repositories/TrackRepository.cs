using System.Linq;
using System.Collections.Generic;
using Chinook.Domain.Repositories;
using Chinook.Domain.Entities;

namespace Chinook.DataEFCoreCmpldQry.Repositories
{
    public class TrackRepository : ITrackRepository
    {
        private readonly ChinookContext _context;

        public TrackRepository(ChinookContext context)
        {
            _context = context;
        }

        private bool TrackExists(int id) =>
            _context.Track.Any(i => i.TrackId == id);

        public void Dispose() => _context.Dispose();

        public List<Track> GetAll() 
            => _context.GetAllTracks();

        public Track GetById(int id)
        {
            var track = _context.GetTrack(id);
            return track;
        }

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

        public List<Track> GetByAlbumId(int id) 
            => _context.GetTracksByAlbumId(id);

        public List<Track> GetByGenreId(int id) 
            => _context.GetTracksByGenreId(id);

        public List<Track> GetByMediaTypeId(int id) 
            => _context.GetTracksByMediaTypeId(id);
        
        public List<Track> GetByPlaylistId(int id)
        {
            var tracks = _context.PlaylistTrack.Where(p => p.PlaylistId == id).Select(p => p.Track);
            return tracks.ToList();
        }

        public List<Track> GetByArtistId(int id) => _context.GetTracksByArtistId(id);

        public List<Track> GetByInvoiceId(int id) => _context.GetTracksByInvoiceId(id);
    }
}