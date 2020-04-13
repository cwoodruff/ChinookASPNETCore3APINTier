using System;
using System.Collections.Generic;
using Chinook.Domain.DbInfo;
using Chinook.Domain.Repositories;
using Chinook.Domain.Entities;

namespace Chinook.DataJson.Repositories
{
    public class TrackRepository : ITrackRepository
    {
        private readonly DbInfo _dbInfo;

        public TrackRepository(DbInfo dbInfo)
        {
            _dbInfo = dbInfo;
        }

        public void Dispose()
        {
        }

        private bool TrackExists(int id)
        {
            return true;
        }

        public List<Track> GetAll()
        {
            return null;
        }

        public Track GetById(int id)
        {
            return null;
        }

        public Track Add(Track newTrack)
        {
            return newTrack;
        }

        public bool Update(Track track)
        {
            if (!TrackExists(track.TrackId))
                return false;

            try
            {
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<Track> GetByAlbumId(int id)
        {
            return null;
        }

        public List<Track> GetByGenreId(int id)
        {
            return null;
        }

        public List<Track> GetByMediaTypeId(int id)
        {
            return null;
        }
    }
}