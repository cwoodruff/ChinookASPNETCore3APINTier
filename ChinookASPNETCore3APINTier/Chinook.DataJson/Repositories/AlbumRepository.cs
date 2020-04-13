using System;
using System.Collections.Generic;
using Chinook.Domain.DbInfo;
using Chinook.Domain.Entities;
using Chinook.Domain.Repositories;

namespace Chinook.DataJson.Repositories
{
    public class AlbumRepository : IAlbumRepository
    {
        private readonly DbInfo _dbInfo;

        public AlbumRepository(DbInfo dbInfo)
        {
            _dbInfo = dbInfo;
        }

        public void Dispose()
        {
        }

        private bool AlbumExists(int id)
        {
            return true;
        }

        public List<Album> GetAll()
        {
            return null;
        }

        public Album GetById(int id)
        {
            return null;
        }

        public List<Album> GetByArtistId(int id)
        {
            return null;
        }

        public Album Add(Album newAlbum)
        {
            return newAlbum;
        }

        public bool Update(Album album)
        {
            if (!AlbumExists(album.AlbumId))
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