using System;
using Chinook.MockData.Repositories;
using System.Threading.Tasks;
using Chinook.Domain.Entities;
using JetBrains.dotMemoryUnit;
using Xunit;

namespace Chinook.UnitTest.Repository
{
    public class PlayListTrackRepositoryTest
    {
        private readonly PlaylistTrackRepository _repo;

        public PlayListTrackRepositoryTest()
        {
            _repo = new PlaylistTrackRepository();
        }

        [DotMemoryUnit(FailIfRunWithoutSupport = false)]
        [Fact]
        public void PlayListTrackGetAll()
        {
            // Act
            var playListTracks = _repo.GetAll();

            // Assert
            Assert.Single(playListTracks);
        }

        [AssertTraffic(AllocatedSizeInBytes = 1000, Types = new[] {typeof(PlaylistTrack)})]
        [Fact]
        public void DotMemoryUnitTest()
        {
            var repo = new PlaylistTrackRepository();

            repo.GetAll();

            dotMemory.Check(memory =>
                Assert.Equal(1, memory.GetObjects(where => where.Type.Is<PlaylistTrack>()).ObjectsCount));

            GC.KeepAlive(repo); // prevent objects from GC if this is implied by test logic
        }
    }
}