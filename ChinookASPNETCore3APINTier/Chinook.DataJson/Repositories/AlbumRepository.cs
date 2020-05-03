using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.Json;
using Chinook.Domain.DbInfo;
using Chinook.Domain.Entities;
using Chinook.Domain.Repositories;
using Microsoft.Data.SqlClient;

namespace Chinook.DataJson.Repositories
{
    public class AlbumRepository : IAlbumRepository
    {
        private readonly SqlConnection _sqlconn;

        public AlbumRepository(SqlConnection sqlconn)
        {
            _sqlconn = sqlconn;
        }

        public void Dispose()
        {
        }

        private bool AlbumExists(int id)
        {
            var sqlcomm = new SqlCommand("dbo.sproc_CheckAlbum", _sqlconn)
            {
                CommandType = CommandType.StoredProcedure
            };
            sqlcomm.Parameters.Add(new SqlParameter("AlbumId", id));
            var dset = new DataSet();
            var adap = new SqlDataAdapter(sqlcomm);
            adap.Fill(dset);

            return Convert.ToBoolean(dset.Tables[0].Rows[0][0]);
        }

        public List<Album> GetAll()
        {
            var sqlcomm = new SqlCommand("dbo.sproc_GetAlbum", _sqlconn)
            {
                CommandType = CommandType.StoredProcedure
            };
            var dset = new DataSet();
            var adap = new SqlDataAdapter(sqlcomm);
            adap.Fill(dset);
            var converted =
                JsonSerializer.Deserialize(dset.Tables[0].Rows[0][0].ToString(), typeof(List<Album>)) as List<Album>;
            return converted;
        }

        public Album GetById(int id)
        {
            var sqlcomm = new SqlCommand("dbo.sproc_GetAlbumDetails", _sqlconn)
            {
                CommandType = CommandType.StoredProcedure
            };
            sqlcomm.Parameters.Add(new SqlParameter("AlbumId", id));
            var dset = new DataSet();
            var adap = new SqlDataAdapter(sqlcomm);
            adap.Fill(dset);
            var converted =
                JsonSerializer.Deserialize(dset.Tables[0].Rows[0][0].ToString(), typeof(List<Album>)) as List<Album>;

            return converted.FirstOrDefault();
        }

        public List<Album> GetByArtistId(int id)
        {
            var sqlcomm = new SqlCommand("dbo.sproc_GetAlbumByArtist", _sqlconn)
            {
                CommandType = CommandType.StoredProcedure
            };
            sqlcomm.Parameters.Add(new SqlParameter("ArtistId", id));
            var dset = new DataSet();
            var adap = new SqlDataAdapter(sqlcomm);
            adap.Fill(dset);
            var converted =
                JsonSerializer.Deserialize(dset.Tables[0].Rows[0][0].ToString(), typeof(List<Album>)) as List<Album>;
            return converted;
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