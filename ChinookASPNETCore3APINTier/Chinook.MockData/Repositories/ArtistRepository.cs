using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;
using Chinook.Domain.Repositories;
using Chinook.Domain.Entities;

namespace Chinook.MockData.Repositories
{
    public class ArtistRepository : IArtistRepository
    {
        public void Dispose()
        {
        }

        public List<Artist> GetAll()
            => new List<Artist>
            {new Artist
            {
                ArtistId = 1,
                Name = "Foo"
            }};

        public Artist GetById(int id)
            => new Artist
            {
                ArtistId = id,
                Name = "Foo"
            };

        public Artist Add(Artist newArtist) => newArtist;

        public bool Update(Artist artist) => true;

        public bool Delete(int id) => true;
    }
}