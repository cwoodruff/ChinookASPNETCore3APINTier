using System;
using System.Collections.Generic;
using Chinook.Domain.Entities;
using Chinook.Domain.Repositories;

namespace Chinook.DataMartendb.Repositories
{
    public class TrackRepository : ITrackRepository
    {
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public List<Track> GetAll()
        {
            throw new NotImplementedException();
        }

        public Track GetById(int id)
        {
            throw new NotImplementedException();
        }

        public List<Track> GetByAlbumId(int id)
        {
            throw new NotImplementedException();
        }

        public List<Track> GetByGenreId(int id)
        {
            throw new NotImplementedException();
        }

        public List<Track> GetByMediaTypeId(int id)
        {
            throw new NotImplementedException();
        }

        public Track Add(Track newTrack)
        {
            throw new NotImplementedException();
        }

        public bool Update(Track track)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<Track> GetByInvoiceId(int id)
        {
            throw new NotImplementedException();
        }

        public List<Track> GetByPlaylistId(int id)
        {
            throw new NotImplementedException();
        }

        public List<Track> GetByArtistId(int id)
        {
            throw new NotImplementedException();
        }
    }
}