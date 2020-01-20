using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chinook.Domain.Repositories;
using Chinook.Domain.Entities;

namespace Chinook.MockData.Repositories
{
    public class GenreRepository : IGenreRepository
    {
        public void Dispose()
        {
        }

        public bool GenreExists(int id) => true;

        public async Task<IAsyncEnumerable<Genre>> GetAll()
            => (new List<Genre>
            {new Genre
            {
                GenreId = 1,
                Name = "Foo"
            }}).ToAsyncEnumerable();

        public async Task<Genre> GetById(int id)
            => new Genre
            {
                GenreId = id,
                Name = "Foo"
            };

        public async Task<Genre> Add(Genre newGenre) => newGenre;

        public async Task<bool> Update(Genre genre) => true;

        public async Task<bool> Delete(int id) => true;
    }
}