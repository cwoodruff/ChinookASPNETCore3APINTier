using System;
using System.Collections.Generic;
using Chinook.Domain.Entities;
using Chinook.Domain.Repositories;

namespace Chinook.DataMartendb.Repositories
{
    public class AlbumRepository : IAlbumRepository
    {
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public List<Album> GetAll()
        {
            throw new NotImplementedException();
        }

        public Album GetById(int id)
        {
            throw new NotImplementedException();
        }

        public List<Album> GetByArtistId(int id)
        {
            throw new NotImplementedException();
        }

        public Album Add(Album newAlbum)
        {
            throw new NotImplementedException();
        }

        public bool Update(Album album)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}