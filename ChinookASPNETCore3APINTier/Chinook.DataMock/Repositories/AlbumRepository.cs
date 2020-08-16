using System.Collections.Generic;
using Chinook.Domain.Repositories;
using Chinook.Domain.Entities;

namespace Chinook.DataMock.Repositories
{
    public class AlbumRepository : IAlbumRepository
    {
        public void Dispose()
        {
        }

        public List<Album> GetAll()
            => new List<Album>
            {new Album
            {
                AlbumId = 1,
                ArtistId = 1,
                Title = "Hello World"
            }};

        public Album GetById(int id)
            => new Album
            {
                AlbumId = id,
                ArtistId = 1,
                Title = "Hello World"
            };

        public Album Add(Album newAlbum)
        {
            newAlbum.AlbumId = 1;
            return newAlbum;
        }

        public bool Update(Album album) => true;

        public bool Delete(int id) => true;

        public List<Album> GetByArtistId(int id)
            => new List<Album>
            {new Album
            {
                Title = "hello World",
                ArtistId = 1,
                AlbumId = 1
            }};
    }
}