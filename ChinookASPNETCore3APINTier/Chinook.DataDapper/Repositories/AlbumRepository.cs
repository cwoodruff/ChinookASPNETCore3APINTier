using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Chinook.Domain.DbInfo;
using Chinook.Domain.Entities;
using Chinook.Domain.Repositories;
using Dapper;
using Dapper.Contrib.Extensions;

namespace Chinook.DataDapper.Repositories
{
    public class AlbumRepository : IAlbumRepository
    {
        private readonly DbInfo _dbInfo;

        public AlbumRepository(DbInfo dbInfo)
        {
            _dbInfo = dbInfo;
        }

        private IDbConnection Connection => new SqlConnection(_dbInfo.ConnectionStrings);

        public void Dispose()
        {
            
        }

        private bool AlbumExists(int id) =>
            Connection.ExecuteScalar<bool>("select count(1) from Album where AlbumId = @id", new {id});

        public List<Album> GetAll()
        { 
            using (IDbConnection cn = Connection)
            {
                cn.Open();
                var albums = Connection.Query<Album>("Select * From Album");
                return albums.ToList();
            }
        }

        public Album GetById(int id)
        {
            using (var cn = Connection)
            {
                cn.Open();
                var album = cn.QueryFirstOrDefault<Album>("Select * From Album WHERE Id = @Id", new {id});
                return album;
            }
        }

        public List<Album> GetByArtistId(int id)
        {
            using (var cn = Connection)
            {
                cn.Open();
                var albums = cn.Query<Album>("Select * From Album WHERE ArtistId = @Id", new {id});
                return albums.ToList();
            }
        }

        public Album Add(Album newAlbum)
        {
            using (var cn = Connection)
            {
                cn.Open();
                var albumId = cn.Insert(newAlbum);
                newAlbum.AlbumId = (int) albumId;
            }

            return newAlbum;
        }

        public bool Update(Album album)
        {
            if (!AlbumExists(album.AlbumId))
                return false;

            try
            {
                using (var cn = Connection)
                {
                    cn.Open();
                    return cn.Update(album);
                }
            }
            catch(Exception)
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                using (var cn = Connection)
                {
                    cn.Open();
                    return cn.Delete(new Album {AlbumId = id});
                }  
            }
            catch(Exception)
            {
                return false;
            }
        }
    }
}