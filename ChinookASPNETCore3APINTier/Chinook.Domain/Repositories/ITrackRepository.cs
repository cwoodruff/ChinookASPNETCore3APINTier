using Chinook.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Chinook.Domain.Repositories
{
    public interface ITrackRepository : IDisposable
    {
        public bool TrackExists(int id);
        Task<IAsyncEnumerable<Track>> GetAll();
        Task<Track> GetById(int id);
        Task<IAsyncEnumerable<Track>> GetByAlbumId(int id);
        Task<IAsyncEnumerable<Track>> GetByGenreId(int id);
        Task<IAsyncEnumerable<Track>> GetByMediaTypeId(int id);
        Task<Track> Add(Track newTrack);
        Task<bool> Update(Track track);
        Task<bool> Delete(int id);
    }
}