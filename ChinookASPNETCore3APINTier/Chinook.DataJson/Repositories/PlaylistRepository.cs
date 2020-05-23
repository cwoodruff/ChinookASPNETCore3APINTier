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
    public class PlaylistRepository : IPlaylistRepository
    {
        private readonly SqlConnection _sqlconn;

        public PlaylistRepository(SqlConnection sqlconn)
        {
            _sqlconn = sqlconn;
        }

        public void Dispose()
        {
        }

        private bool PlaylistExists(int id)
        {
            var sqlcomm = new SqlCommand("dbo.sproc_CheckPlaylist", _sqlconn)
            {
                CommandType = CommandType.StoredProcedure
            };
            sqlcomm.Parameters.Add(new SqlParameter("PlaylistId", id));
            var dset = new DataSet();
            var adap = new SqlDataAdapter(sqlcomm);
            adap.Fill(dset);

            return Convert.ToBoolean(dset.Tables[0].Rows[0][0]);
        }

        public List<Playlist> GetAll()
        {
            var sqlcomm = new SqlCommand("dbo.sproc_GetPlaylist", _sqlconn)
            {
                CommandType = CommandType.StoredProcedure
            };
            var dset = new DataSet();
            var adap = new SqlDataAdapter(sqlcomm);
            adap.Fill(dset);
            var converted =
                JsonSerializer.Deserialize(dset.Tables[0].Rows[0][0].ToString(), typeof(List<Playlist>)) as List<Playlist>;
            return converted;
        }

        public Playlist GetById(int id)
        {
            var sqlcomm = new SqlCommand("dbo.sproc_GetPlaylistDetails", _sqlconn)
            {
                CommandType = CommandType.StoredProcedure
            };
            sqlcomm.Parameters.Add(new SqlParameter("PlaylistId", id));
            var dset = new DataSet();
            var adap = new SqlDataAdapter(sqlcomm);
            adap.Fill(dset);
            var converted =
                JsonSerializer.Deserialize(dset.Tables[0].Rows[0][0].ToString(), typeof(List<Playlist>)) as List<Playlist>;

            return converted.FirstOrDefault();
        }

        public Playlist Add(Playlist newPlaylist)
        {
            return newPlaylist;
        }

        public bool Update(Playlist playlist)
        {
            if (!PlaylistExists(playlist.PlaylistId))
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

        public List<Playlist> GetByTrackId(int id)
        {
            var sqlcomm = new SqlCommand("dbo.sproc_GetPlaylistByTrack", _sqlconn)
            {
                CommandType = CommandType.StoredProcedure
            };
            sqlcomm.Parameters.Add(new SqlParameter("TrackId", id));
            var dset = new DataSet();
            var adap = new SqlDataAdapter(sqlcomm);
            adap.Fill(dset);
            var converted =
                JsonSerializer.Deserialize(dset.Tables[0].Rows[0][0].ToString(), typeof(List<Playlist>)) as List<Playlist>;
            return converted;
        }
    }
}