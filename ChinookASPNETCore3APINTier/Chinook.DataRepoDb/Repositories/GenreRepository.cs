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
    public class GenreRepository : IGenreRepository
    {
        private readonly DbInfo _dbInfo;

        public GenreRepository(DbInfo dbInfo)
        {
            _dbInfo = dbInfo;
        }

        private IDbConnection Connection => new SqlConnection(_dbInfo.ConnectionStrings);

        public void Dispose()
        {
            
        }

        private bool GenreExists(int id) =>
            Connection.ExecuteScalar<bool>("select count(1) from Genre where GenreId = @id", new {id});

        public List<Genre> GetAll()
        {
            using (IDbConnection cn = Connection)
            {
                cn.Open();
                var genres = Connection.QueryAll<Genre>();
                return genres.ToList();
            }
        }

        public Genre GetById(int id)
        {
            using (var cn = Connection)
            {
                cn.Open();
                return cn.Query<Genre>(@where: g => g.GenreId == id).FirstOrDefault();
            }
        }

        public Genre Add(Genre newGenre)
        {
            using (var cn = Connection)
            {
                cn.Open();

                newGenre.GenreId = (int) cn.Insert(new Genre {Name = newGenre.Name});
            }

            return newGenre;
        }

        public bool Update(Genre genre)
        {
            if (!GenreExists(genre.GenreId))
                return false;

            try
            {
                using (var cn = Connection)
                {
                    cn.Open();
                    return (cn.Update(genre) > 0);
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
                    return cn.Delete(new Genre {GenreId = id}) > 0;
                }  
            }
            catch(Exception)
            {
                return false;
            }
        }
    }
}