using System;
using System.Collections.Generic;
using Chinook.Domain.DbInfo;
using Chinook.Domain.Repositories;
using Chinook.Domain.Entities;

namespace Chinook.DataJson.Repositories
{
    public class PlaylistRepository : IPlaylistRepository
    {
        private readonly DbInfo _dbInfo;

        public PlaylistRepository(DbInfo dbInfo)
        {
            _dbInfo = dbInfo;
        }

        public void Dispose()
        {
        }

        private bool PlaylistExists(int id)
        {
            return true;
        }

        public List<Playlist> GetAll()
        {
            return null;
        }

        public Playlist GetById(int id)
        {
            return null;
        }

        public Playlist Add(Playlist newPlaylist)
        {
            return newPlaylist;
        }

        public List<Track> GetTrackByPlaylistId(int id)
        {
            return null;
        }

        public bool Update(Playlist playlist)
        {
            if (!PlaylistExists(playlist.PlaylistId))
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
    }
}