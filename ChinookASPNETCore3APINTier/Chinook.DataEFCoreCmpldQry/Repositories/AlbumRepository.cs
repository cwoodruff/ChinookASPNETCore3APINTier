using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Chinook.Domain.Repositories;
using Chinook.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Chinook.DataEFCoreCmpldQry.Repositories
{
    public class AlbumRepository : EfRepository<Album>
    {
        public AlbumRepository(ChinookContext context) : base(context)
        {
        }

        public List<Album> GetAll() 
            => _dbContext.GetAllAlbums();

        public Album GetById(int id)
        {
            var album = _dbContext.GetAlbum(id);
            return album;
        }

        public Album Add(Album newAlbum)
        {
            _dbContext.Album.Add(newAlbum);
            _dbContext.SaveChanges();
            return newAlbum;
        }

        public bool Update(Album album)
        {
            if (!Exists(album.AlbumId))
                return false;
            _dbContext.Album.Update(album);

            _dbContext.Update(album);
            _dbContext.SaveChanges();
            return true;
        }

        public bool Delete(int id)
        {
            if (!Exists(id))
                return false;
            var toRemove = _dbContext.Album.Find(id);
            _dbContext.Album.Remove(toRemove);
            _dbContext.SaveChanges();
            return true;
        }

        public List<Album> GetByArtistId(int id) 
            => _dbContext.GetAlbumsByArtistId(id);
    }
}