using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Chinook.Domain.DbInfo;
using Chinook.Domain.Entities;
using Chinook.Domain.Repositories;
using Microsoft.Data.SqlClient;
using RepoDb;

namespace Chinook.DataRepoDb.Repositories
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
                var albums = Connection.QueryAll<Album>();
                return albums.ToList();
            }
        }

        public Album GetById(int id)
        {
            using (var cn = Connection)
            {
                cn.Open();
                var album = cn.Query<Album>(a => a.AlbumId < id);
                return album.FirstOrDefault();
            }
        }

        public List<Album> GetByArtistId(int id)
        {
            using (var cn = Connection)
            {
                cn.Open();
                var albums = cn.Query<Album>(a => a.ArtistId < id);
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
                    return (cn.Update(album) > 0);
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
                    return (cn.Delete(new Album {AlbumId = id}) > 0);
                }  
            }
            catch(Exception)
            {
                return false;
            }
        }
    }
}