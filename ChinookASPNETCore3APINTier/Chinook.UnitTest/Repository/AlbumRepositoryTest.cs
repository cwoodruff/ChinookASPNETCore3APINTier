using System;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using Chinook.MockData.Repositories;
using Chinook.Domain.Entities;
using JetBrains.dotMemoryUnit;
using Xunit;

namespace Chinook.UnitTest.Repository
{
    public class AlbumRepositoryTest
    {
        private readonly AlbumRepository _repo;

        public AlbumRepositoryTest()
        {
            _repo = new AlbumRepository();
        }

        [DotMemoryUnit(FailIfRunWithoutSupport = false)]
        [Fact]
        public async Task AlbumGetAll()
        {
            // Arrange

            // Act
            var albums= await (await _repo.GetAll()).ToListAsync();
            
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
            var album = await _repo.GetById(id);

            // Assert
            Assert.Equal(id, album.AlbumId);
        }

        [AssertTraffic(AllocatedSizeInBytes = 1000, Types = new[] {typeof(Album)})]
        [Fact]
        public void DotMemoryUnitTest()
        {
            var repo = new AlbumRepository();

            repo.GetAll();

            dotMemory.Check(memory =>
                Assert.Equal(1, memory.GetObjects(where => where.Type.Is<Album>()).ObjectsCount));

            GC.KeepAlive(repo); // prevent objects from GC if this is implied by test logic
        }
    }
}