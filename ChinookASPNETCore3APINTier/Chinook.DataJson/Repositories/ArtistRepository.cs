using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.Json;
using Chinook.Domain.DbInfo;
using Chinook.Domain.Repositories;
using Chinook.Domain.Entities;
using Microsoft.Data.SqlClient;

namespace Chinook.DataJson.Repositories
{
    public class ArtistRepository : IArtistRepository
    {
        private readonly SqlConnection _sqlconn;

        public ArtistRepository(SqlConnection sqlconn)
        {
            _sqlconn = sqlconn;
        }
        
        public void Dispose()
        {
            
        }

        private bool ArtistExists(int id)
        {
            var sqlcomm = new SqlCommand("dbo.sproc_CheckArtist", _sqlconn)
            {
                CommandType = CommandType.StoredProcedure
            };
            sqlcomm.Parameters.Add(new SqlParameter("ArtistId", id));
            var dset = new DataSet();
            var adap = new SqlDataAdapter(sqlcomm);
            adap.Fill(dset);

            return Convert.ToBoolean(dset.Tables[0].Rows[0][0]);
        }

        public List<Artist> GetAll()
        {
            var sqlcomm = new SqlCommand("dbo.sproc_GetArtist", _sqlconn)
            {
                CommandType = CommandType.StoredProcedure
            };
            var dset = new DataSet();
            var adap = new SqlDataAdapter(sqlcomm);
            adap.Fill(dset);
            var converted =
                JsonSerializer.Deserialize(dset.Tables[0].Rows[0][0].ToString(), typeof(List<Artist>)) as List<Artist>;
            return converted;
        }

        public Artist GetById(int id)
        {
            var sqlcomm = new SqlCommand("dbo.sproc_GetArtistDetails", _sqlconn)
            {
                CommandType = CommandType.StoredProcedure
            };
            sqlcomm.Parameters.Add(new SqlParameter("ArtistId", id));
            var dset = new DataSet();
            var adap = new SqlDataAdapter(sqlcomm);
            adap.Fill(dset);
            var converted =
                JsonSerializer.Deserialize(dset.Tables[0].Rows[0][0].ToString(), typeof(List<Artist>)) as List<Artist>;

            return converted.FirstOrDefault();
        }

        public Artist Add(Artist newArtist)
        {

            return newArtist;
        }

        public bool Update(Artist artist)
        {
            if (!ArtistExists(artist.ArtistId))
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