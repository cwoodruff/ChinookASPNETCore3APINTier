using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.Json;
using Chinook.Domain.Repositories;
using Chinook.Domain.Entities;
using Microsoft.Data.SqlClient;

namespace Chinook.DataJson.Repositories
{
    public class GenreRepository : IGenreRepository
    {
        private readonly SqlConnection _sqlconn;

        public GenreRepository(SqlConnection sqlconn)
        {
            _sqlconn = sqlconn;
        }

        public void Dispose()
        {
        }

        private bool GenreExists(int id)
        {
            var sqlcomm = new SqlCommand("dbo.sproc_CheckGenre", _sqlconn)
            {
                CommandType = CommandType.StoredProcedure
            };
            sqlcomm.Parameters.Add(new SqlParameter("GenreId", id));
            var dset = new DataSet();
            var adap = new SqlDataAdapter(sqlcomm);
            adap.Fill(dset);

            return Convert.ToBoolean(dset.Tables[0].Rows[0][0]);
        }

        public List<Genre> GetAll()
        {
            var sqlcomm = new SqlCommand("dbo.sproc_GetGenre", _sqlconn)
            {
                CommandType = CommandType.StoredProcedure
            };
            var dset = new DataSet();
            var adap = new SqlDataAdapter(sqlcomm);
            adap.Fill(dset);
            var converted =
                JsonSerializer.Deserialize(dset.Tables[0].Rows[0][0].ToString(), typeof(List<Genre>)) as List<Genre>;
            return converted;
        }

        public Genre GetById(int id)
        {
            var sqlcomm = new SqlCommand("dbo.sproc_GetGenreDetails", _sqlconn)
            {
                CommandType = CommandType.StoredProcedure
            };
            sqlcomm.Parameters.Add(new SqlParameter("GenreId", id));
            var dset = new DataSet();
            var adap = new SqlDataAdapter(sqlcomm);
            adap.Fill(dset);
            var converted =
                JsonSerializer.Deserialize(dset.Tables[0].Rows[0][0].ToString(), typeof(List<Genre>)) as List<Genre>;

            return converted.FirstOrDefault();
        }

        public Genre Add(Genre newGenre)
        {
            return newGenre;
        }

        public bool Update(Genre genre)
        {
            if (!GenreExists(genre.GenreId))
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