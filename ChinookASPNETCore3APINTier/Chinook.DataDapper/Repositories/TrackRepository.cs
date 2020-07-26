using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Chinook.Domain.DbInfo;
using Chinook.Domain.Repositories;
using Chinook.Domain.Entities;
using Dapper;
using Dapper.Contrib.Extensions;

namespace Chinook.DataDapper.Repositories
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
            using (IDbConnection cn = Connection)
            {
                cn.Open();
                var tracks = Connection.Query<Track>("Select * From Track");
                return tracks.ToList();
            }
        }

        public Track GetById(int id)
        {
            using (var cn = Connection)
            {
                cn.Open();
                return cn.QueryFirstOrDefault<Track>("Select * From Track WHERE TrackId = @Id", new {id});
            }
        }

        public Track Add(Track newTrack)
        {
            using (var cn = Connection)
            {
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
            }

            return newTrack;
        }

        public bool Update(Track track)
        {
            if (!TrackExists(track.TrackId))
                return false;

            try
            {
                using (var cn = Connection)
                {
                    cn.Open();
                    return cn.Update(track);
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
                    return cn.Delete(new Track {TrackId = id});
                }  
            }
            catch(Exception)
            {
                return false;
            }
        }

        public List<Track> GetByInvoiceId(int id)
        {
            throw new NotImplementedException();
        }

        public List<Track> GetByPlaylistId(int id)
        {
            throw new NotImplementedException();
        }

        public List<Track> GetByArtistId(int id)
        {
            throw new NotImplementedException();
        }

        public List<Track> GetByAlbumId(int id)
        {
            using (var cn = Connection)
            {
                cn.Open();
                var tracks = cn.Query<Track>("Select * From Track WHERE AlbumId = @Id", new { id });
                return tracks.ToList();
            }
        }

        public List<Track> GetByGenreId(int id)
        {
            using (var cn = Connection)
            {
                cn.Open();
                var tracks = cn.Query<Track>("Select * From Track WHERE GenreId = @Id", new { id });
                return tracks.ToList();
            }
        }

        public List<Track> GetByMediaTypeId(int id)
        {
            using (var cn = Connection)
            {
                cn.Open();
                var tracks = cn.Query<Track>("Select * From Track WHERE MediaTypeId = @Id", new { id });
                return tracks.ToList();
            }
        }
    }
}