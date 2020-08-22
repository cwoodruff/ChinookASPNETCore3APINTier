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
    public class PlaylistRepository : IPlaylistRepository
    {
        private readonly DbInfo _dbInfo;

        public PlaylistRepository(DbInfo dbInfo)
        {
            _dbInfo = dbInfo;
            RepoDb.SqlServerBootstrap.Initialize();
        }

        private IDbConnection Connection => new SqlConnection(_dbInfo.ConnectionStrings);

        public void Dispose()
        {
            
        }

        private bool PlaylistExists(int id) =>
            Connection.Exists("select count(1) from Playlist where PlaylistId = @id", new {id});

        public List<Playlist> GetAll()
        {
            using IDbConnection cn = Connection;
            cn.Open();
            var playlists = cn.QueryAll<Playlist>();
            return playlists.ToList();
        }

        public Playlist GetById(int id)
        {
            using var cn = Connection;
            cn.Open();
            return cn.Query<Playlist>(id).FirstOrDefault();
        }

        public Playlist Add(Playlist newPlaylist)
        {
            using var cn = Connection;
            cn.Open();
            cn.Insert(newPlaylist);
            return newPlaylist;
        }

        public List<Track> GetTrackByPlaylistId(int id)
        {
            using var cn = Connection;
            cn.Open();
            var tracks = cn.ExecuteQuery<Track>("SELECT Track.* FROM Playlist INNER JOIN PlaylistTrack ON Playlist.PlaylistId = PlaylistTrack.PlaylistId INNER JOIN Track ON PlaylistTrack.TrackId = Track.TrackId WHERE Playlist.PlaylistId = @Id", new {id});
            return tracks.ToList();
        }

        public bool Update(Playlist playlist)
        {
            if (!PlaylistExists(playlist.PlaylistId))
                return false;

            try
            {
                using var cn = Connection;
                cn.Open();
                return (cn.Update(playlist) > 0);
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
                return cn.Delete(new Playlist {PlaylistId = id}) > 0;
            }
            catch(Exception)
            {
                return false;
            }
        }

        public List<Playlist> GetByTrackId(int id)
        {
            using var cn = Connection;
            cn.Open();
            var playlists = cn.ExecuteQuery<Playlist>("SELECT PL.PlaylistId, PL.Name FROM Playlist AS PL INNER JOIN PlaylistTrack PLT ON PL.PlaylistId = PLT.PlaylistId WHERE PLT.TrackID = @Id", new {id});
            return playlists.ToList();
        }
    }
}