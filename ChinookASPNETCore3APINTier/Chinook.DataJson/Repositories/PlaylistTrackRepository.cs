using System;
using System.Collections.Generic;
using Chinook.Domain.DbInfo;
using Chinook.Domain.Repositories;
using Chinook.Domain.Entities;

namespace Chinook.DataJson.Repositories
{
    public class PlaylistTrackRepository : IPlaylistTrackRepository
    {
        private readonly DbInfo _dbInfo;

        public PlaylistTrackRepository(DbInfo dbInfo)
        {
            _dbInfo = dbInfo;
        }

        public void Dispose()
        {
        }

        public List<PlaylistTrack> GetAll()
        {
            return null;
        }

        public List<PlaylistTrack> GetByPlaylistId(int id)
        {
            return null;
        }

        public List<PlaylistTrack> GetByTrackId(int id)
        {
            return null;
        }

        public PlaylistTrack Add(PlaylistTrack newPlaylistTrack)
        {
            return newPlaylistTrack;
        }

        public bool Update(PlaylistTrack playlistTrack)
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
    }
}