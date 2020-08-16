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
        }

        private IDbConnection Connection => new SqlConnection(_dbInfo.ConnectionStrings);

        public void Dispose()
        {
            
        }

        private bool PlaylistExists(int id) =>
            Connection.ExecuteScalar<bool>("select count(1) from Playlist where PlaylistId = @id", new {id});

        public List<Playlist> GetAll()
        {
            using (IDbConnection cn = Connection)
            {
                cn.Open();
                var playlists = Connection.QueryAll<Playlist>();
                return playlists.ToList();
            }
        }

        public Playlist GetById(int id)
        {
            using (var cn = Connection)
            {
                cn.Open();
                return cn.Query<Playlist>(p => p.PlaylistId == id).FirstOrDefault();
            }
        }

        public Playlist Add(Playlist newPlaylist)
        {
            using (var cn = Connection)
            {
                cn.Open();

                newPlaylist.PlaylistId = (int) cn.Insert(new Playlist {Name = newPlaylist.Name});
            }

            return newPlaylist;
        }

        public List<Track> GetTrackByPlaylistId(int id)
        {
            using (var cn = Connection)
            {
                cn.Open();
                // "SELECT Track.* FROM Playlist INNER JOIN PlaylistTrack ON Playlist.PlaylistId = PlaylistTrack.PlaylistId INNER JOIN Track ON PlaylistTrack.TrackId = Track.TrackId WHERE Playlist.PlaylistId = @Id", new { id });

                var tracks = cn.Query<Track>(t => t.TrackId == id);
                return tracks.ToList();
            }
        }

        public bool Update(Playlist playlist)
        {
            if (!PlaylistExists(playlist.PlaylistId))
                return false;

            try
            {
                using (var cn = Connection)
                {
                    cn.Open();
                    return (cn.Update(playlist) > 0);
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
                    return cn.Delete(new Playlist {PlaylistId = id}) > 0;
                }  
            }
            catch(Exception)
            {
                return false;
            }
        }

        public List<Playlist> GetByTrackId(int id)
        {
            using (var cn = Connection)
            {
                cn.Open();
                var playlists = cn.Query<Playlist>(t => t.PlaylistId == id);
                return playlists.ToList();
            }
        }
    }
}