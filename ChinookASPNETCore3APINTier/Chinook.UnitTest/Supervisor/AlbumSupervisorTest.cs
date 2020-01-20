using System.Linq;
using System.Threading.Tasks;
using Chinook.Domain.Supervisor;
using JetBrains.dotMemoryUnit;
using Xunit;

namespace Chinook.UnitTest.Supervisor
{
    public class AlbumSupervisorTest
    {
        private readonly ChinookSupervisor _super;

        public AlbumSupervisorTest()
        {
            _super = new ChinookSupervisor();
        }

        [DotMemoryUnit(FailIfRunWithoutSupport = false)]
        [Fact]
        public async Task AlbumGetAll()
        {
            // Arrange

            // Act
            var albums = await (await _super.GetAllAlbum()).ToListAsync();

            // Assert
            Assert.Single(albums);
        }
        
        [DotMemoryUnit(FailIfRunWithoutSupport = false)]
        [Fact]
        public async Task AlbumGetOne()
        {
            // Arrange
            var id = 1;

            // Act
            var album = await _super.GetAlbumById(id);

            // Assert
            Assert.Equal(id, album.AlbumId);
        }
    }
}