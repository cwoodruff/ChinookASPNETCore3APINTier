using Chinook.DataEFCoreCmpldQry.Repositories;
using Chinook.Domain.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Chinook.UnitTest.CmpledQryRepository
{
    public class AlbumRepositoryTest
    {
        private readonly IAlbumRepository _repo;

        public AlbumRepositoryTest()
        {
            var services = new ServiceCollection();
            services.AddTransient<IAlbumRepository, AlbumRepository>();

            var serviceProvider = services.BuildServiceProvider();

            _repo = serviceProvider.GetService<IAlbumRepository>();
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