using System;
using Chinook.MockData.Repositories;
using Chinook.Domain.Entities;
using JetBrains.dotMemoryUnit;
using Xunit;

namespace Chinook.UnitTest.Repository
{
    public class TrackRepositoryTest
    {
        private readonly TrackRepository _repo;

        public TrackRepositoryTest()
        {
            _repo = new TrackRepository();
        }

        [DotMemoryUnit(FailIfRunWithoutSupport = false)]
        [Fact]
        public void TrackGetAll()
        {
            // Act
            var tracks = _repo.GetAll();

            // Assert
            Assert.Single(tracks);
        }

        [AssertTraffic(AllocatedSizeInBytes = 1000, Types = new[] {typeof(Track)})]
        [Fact]
        public void DotMemoryUnitTest()
        {
            var repo = new TrackRepository();

            repo.GetAll();

            dotMemory.Check(memory =>
                Assert.Equal(1, memory.GetObjects(where => where.Type.Is<Track>()).ObjectsCount));

            GC.KeepAlive(repo); // prevent objects from GC if this is implied by test logic
        }
    }
}