using System.Collections.Generic;
using Chinook.Domain.Repositories;
using Chinook.Domain.Entities;

namespace Chinook.DataMock.Repositories
{
    public class TrackRepository : ITrackRepository
    {
        public void Dispose()
        {
        }

        public List<Track> GetAll()
            => new List<Track>
            {new Track
            {
                TrackId = 1,
                Name = "Foo"
            }};

        public Track GetById(int id)
            => new Track
            {
                TrackId = id
            };

        public Track Add(Track newTrack) => newTrack;

        public bool Update(Track track) => true;

        public bool Delete(int id) => true;
        public List<Track> GetByInvoiceId(int id)
        {
            throw new System.NotImplementedException();
        }

        public List<Track> GetByPlaylistId(int id)
        {
            throw new System.NotImplementedException();
        }

        public List<Track> GetByArtistId(int id)
        {
            throw new System.NotImplementedException();
        }

        public List<Track> GetByAlbumId(int id)
            => new List<Track>
            {new Track
            {
                TrackId = id
            }};

        public List<Track> GetByGenreId(int id)
            => new List<Track>
            {new Track
            {
                TrackId = id
            }};

        public List<Track> GetByMediaTypeId(int id)
            => new List<Track>
            {new Track
            {
                TrackId = id
            }};
    }
}