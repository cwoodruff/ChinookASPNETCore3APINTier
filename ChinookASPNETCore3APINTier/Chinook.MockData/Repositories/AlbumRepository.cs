using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Chinook.Domain.Repositories;
using Chinook.Domain.Entities;

namespace Chinook.MockData.Repositories
{
    public class AlbumRepository : IAlbumRepository
    {
        public void Dispose()
        {
        }

        public bool AlbumExists(int id) => true;

        public async Task<IAsyncEnumerable<Album>> GetAll()
            => (new List<Album>
            {new Album
            {
                AlbumId = 1,
                ArtistId = 1,
                Title = "Hello World"
            }}).ToAsyncEnumerable();

        public async Task<Album> GetById(int id)
            => new Album
            {
                AlbumId = id,
                ArtistId = 1,
                Title = "Hello World"
            };

        public async Task<Album> Add(Album newAlbum)
        {
            newAlbum.AlbumId = 1;
            return newAlbum;
        }

        public async Task<bool> Update(Album album) => true;

        public async Task<bool> Delete(int id) => true;

        public async Task<IAsyncEnumerable<Album>> GetByArtistId(int id)
            => (new List<Album>
            {
                new Album
                {
                    Title = "hello World",
                    ArtistId = 1,
                    AlbumId = 1
                }
            }).ToAsyncEnumerable();
    }
}