using Chinook.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Chinook.Domain.Repositories
{
    public interface ITrackRepository : IDisposable
    {
        List<Track> GetAll();
        Track GetById(int id);
        List<Track> GetByAlbumId(int id);
        List<Track> GetByGenreId(int id);
        List<Track> GetByMediaTypeId(int id);
        Track Add(Track newTrack);
        bool Update(Track track);
        bool Delete(int id);
    }
}