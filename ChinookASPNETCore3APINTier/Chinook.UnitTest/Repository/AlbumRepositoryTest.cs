using Chinook.DataEFCore.Repositories;
using Chinook.Domain.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Chinook.UnitTest.Repository
{
    public class AlbumRepositoryTest
    {
        private readonly IAlbumRepository _repo;

        public AlbumRepositoryTest()
        {
        }

        [Fact]
        public void AlbumGetAll()
        {
            // Arrange

            // Act
            var albums = _repo.GetAll();

            // Assert
            Assert.True(albums.Count > 1, "The number of albums was not greater than 1");
        }

        [Fact]
        public void AlbumGetOne()
        {
            // Arrange
            var id = 1;

            // Act
            var album = _repo.GetById(id);

            // Assert
            Assert.Equal(id, album.AlbumId);
        }
    }
}