using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Chinook.Domain.Repositories;
using Chinook.Domain.Entities;

namespace Chinook.Data.Repositories
{
    public class TrackRepository : ITrackRepository
    {
        private readonly ChinookContext _context;

        public TrackRepository(ChinookContext context)
        {
            _context = context;
        }

        public bool TrackExists(int id) => _context.Track.Any(i => i.TrackId == id);

        public void Dispose() => _context.Dispose();

        public async Task<IAsyncEnumerable<Track>> GetAll() => await _context.GetAllTracks();

        public async Task<Track> GetById(int id) => await _context.GetTrack(id);

        public async Task<Track> Add(Track newTrack)
        {
            if (newTrack == null)
                return null;
            var track = await _context.Track.AddAsync(newTrack);
            await _context.SaveChangesAsync();
            return track.Entity;
        }

        public async Task<bool> Update(Track track)
        {
            if (!TrackExists(track.TrackId))
                return false;
            _context.Track.Update(track);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(int id)
        {
            if (!TrackExists(id))
                return false;
            var toRemove = _context.Track.Find(id);
            _context.Track.Remove(toRemove);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IAsyncEnumerable<Track>> GetByAlbumId(int id) => await _context.GetTracksByAlbumId(id);

        public async Task<IAsyncEnumerable<Track>> GetByGenreId(int id) => await _context.GetTracksByGenreId(id);

        public async Task<IAsyncEnumerable<Track>> GetByMediaTypeId(int id) => await _context.GetTracksByMediaTypeId(id);
    }
}