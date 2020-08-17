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
    public class TrackRepository : ITrackRepository
    {
        private readonly DbInfo _dbInfo;

        public TrackRepository(DbInfo dbInfo)
        {
            _dbInfo = dbInfo;
        }

        private IDbConnection Connection => new SqlConnection(_dbInfo.ConnectionStrings);
        
        public void Dispose()
        {
            
        }

        private bool TrackExists(int id) =>
            Connection.ExecuteScalar<bool>("select count(1) from Track where TrackId = @id", new {id});

        public List<Track> GetAll()
        {
            using IDbConnection cn = Connection;
            cn.Open();
            var tracks = Connection.QueryAll<Track>();
            return tracks.ToList();
        }

        public Track GetById(int id)
        {
            using var cn = Connection;
            cn.Open();
            return cn.Query<Track>(t => t.TrackId == id).FirstOrDefault();
        }

        public Track Add(Track newTrack)
        {
            using var cn = Connection;
            cn.Open();

            newTrack.TrackId = (int) cn.Insert(
                new Track
                {
                    Name = newTrack.Name,
                    AlbumId = newTrack.AlbumId,
                    MediaTypeId = newTrack.MediaTypeId,
                    GenreId = newTrack.GenreId,
                    Composer = newTrack.Composer,
                    Milliseconds = newTrack.Milliseconds,
                    Bytes = newTrack.Bytes,
                    UnitPrice = newTrack.UnitPrice
                });

            return newTrack;
        }

        public bool Update(Track track)
        {
            if (!TrackExists(track.TrackId))
                return false;

            try
            {
                using var cn = Connection;
                cn.Open();
                return (cn.Update(track) > 0);
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
                return cn.Delete(new Track {TrackId = id}) > 0;
            }
            catch(Exception)
            {
                return false;
            }
        }

        public List<Track> GetByInvoiceId(int id)
        {
            using var cn = Connection;
            cn.Open();
            var tracks = cn.ExecuteQuery<Track>("SELECT T.TrackId, T.Name, T.AlbumId, T.MediaTypeId, T.GenreId, T.Composer, T.Milliseconds, T.Bytes, T.UnitPrice FROM Track AS T INNER JOIN InvoiceLine AS IL ON T.TrackId = IL.TrackId WHERE IL.InvoiceID = @Id", new {id});
            return tracks.ToList();
        }

        public List<Track> GetByPlaylistId(int id)
        {
            using var cn = Connection;
            cn.Open();
            var tracks = cn.ExecuteQuery<Track>("SELECT T.TrackId, T.Name, T.AlbumId, T.MediaTypeId, T.GenreId, T.Composer, T.Milliseconds, T.Bytes, T.UnitPrice FROM Track AS T INNER JOIN PlaylistTrack AS PLT ON T.TrackId = PLT.TrackId WHERE PLT.PlatListId = @Id", new {id});
            return tracks.ToList();
        }

        public List<Track> GetByArtistId(int id)
        {
            using var cn = Connection;
            cn.Open();
            var tracks = cn.ExecuteQuery<Track>("SELECT T.TrackId, T.Name, T.AlbumId, T.MediaTypeId, T.GenreId, T.Composer, T.Milliseconds, T.Bytes, T.UnitPrice FROM Track AS T INNER JOIN Album AS A ON T.AlbumId = A.AlbumId WHERE A.ArtistId = @Id", new {id});
            return tracks.ToList();
        }

        public List<Track> GetByAlbumId(int id)
        {
            using var cn = Connection;
            cn.Open();
            var tracks = cn.Query<Track>(t => t.AlbumId == id);
            return tracks.ToList();
        }

        public List<Track> GetByGenreId(int id)
        {
            using var cn = Connection;
            cn.Open();
            var tracks = cn.Query<Track>(t => t.GenreId == id);
            return tracks.ToList();
        }

        public List<Track> GetByMediaTypeId(int id)
        {
            using var cn = Connection;
            cn.Open();
            var tracks = cn.Query<Track>(t => t.MediaTypeId == id);
            return tracks.ToList();
        }
    }
}