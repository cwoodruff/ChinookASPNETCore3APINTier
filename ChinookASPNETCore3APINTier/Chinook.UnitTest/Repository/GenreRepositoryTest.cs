using Chinook.DataEFCore.Repositories;
using Chinook.Domain.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Chinook.UnitTest.Repository
{
    public class GenreRepositoryTest
    {
        private readonly IGenreRepository _repo;

        public GenreRepositoryTest()
        {
        }

        [Fact]
        public void GenreGetAll()
        {
            // Act
            var genres = _repo.GetAll();

            // Assert
            Assert.True(genres.Count > 1, "The number of genres was not greater than 1");
        }
    }
}