using System;
using Chinook.MockData.Repositories;
using System.Threading.Tasks;
using Chinook.Domain.Entities;
using JetBrains.dotMemoryUnit;
using Xunit;

namespace Chinook.UnitTest.Repository
{
    public class PlayListRepositoryTest
    {
        private readonly PlaylistRepository _repo;

        public PlayListRepositoryTest()
        {
            _repo = new PlaylistRepository();
        }

        [DotMemoryUnit(FailIfRunWithoutSupport = false)]
        [Fact]
        public void PlayListGetAll()
        {
            // Act
            var playLists = _repo.GetAll();

            // Assert
            Assert.Single(playLists);
        }

        [AssertTraffic(AllocatedSizeInBytes = 1000, Types = new[] {typeof(Playlist)})]
        [Fact]
        public void DotMemoryUnitTest()
        {
            var repo = new PlaylistRepository();

            repo.GetAll();

            dotMemory.Check(memory =>
                Assert.Equal(1, memory.GetObjects(where => where.Type.Is<Playlist>()).ObjectsCount));

            GC.KeepAlive(repo); // prevent objects from GC if this is implied by test logic
        }
    }
}