using System.Collections.Generic;
using Chinook.Domain.Repositories;
using Chinook.Domain.Entities;

namespace Chinook.DataMock.Repositories
{
    public class GenreRepository : IGenreRepository
    {
        public void Dispose()
        {
        }

        public List<Genre> GetAll()
            => new List<Genre>
            {new Genre
            {
                GenreId = 1,
                Name = "Foo"
            }};

        public Genre GetById(int id)
            => new Genre
            {
                GenreId = id,
                Name = "Foo"
            };

        public Genre Add(Genre newGenre) => newGenre;

        public bool Update(Genre genre) => true;

        public bool Delete(int id) => true;
    }
}