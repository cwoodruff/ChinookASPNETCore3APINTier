using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Chinook.Domain.DbInfo;
using Chinook.Domain.Entities;
using Chinook.Domain.Repositories;
using RepoDb;

namespace Chinook.DataRepoDb.Repositories
{
    public class ArtistRepository : IArtistRepository
    {
        private readonly DbInfo _dbInfo;

        public ArtistRepository(DbInfo dbInfo)
        {
            _dbInfo = dbInfo;
            RepoDb.SqlServerBootstrap.Initialize();
        }

        private IDbConnection Connection => new SqlConnection(_dbInfo.ConnectionStrings);
        
        public void Dispose()
        {
            
        }

        private bool ArtistExists(int id) =>
            Connection.Exists("select count(1) from Artist where ArtistId = @id", new {id});

        public List<Artist> GetAll()
        {
            using IDbConnection cn = Connection;
            cn.Open();
            var artists = cn.QueryAll<Artist>();
            return artists.ToList();
        }

        public Artist GetById(int id)
        {
            using var cn = Connection;
            cn.Open();
            return cn.Query<Artist>(id).FirstOrDefault();
        }

        public Artist Add(Artist newArtist)
        {
            using var cn = Connection;
            cn.Open();
            cn.Insert(newArtist);
            return newArtist;
        }

        public bool Update(Artist artist)
        {
            if (!ArtistExists(artist.ArtistId))
                return false;

            try
            {
                using var cn = Connection;
                cn.Open();
                return (cn.Update(artist) > 0);
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
                using var cn = Connection;
                cn.Open();
                return (cn.Delete(new Artist {ArtistId = id}) > 0);
            }
            catch(Exception)
            {
                return false;
            }
        }
    }
}