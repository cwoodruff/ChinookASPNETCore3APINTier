using System;
using Chinook.DataEFCore.Repositories;
using Chinook.Domain.Entities;
using Chinook.Domain.Repositories;
using JetBrains.dotMemoryUnit;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Chinook.UnitTest.Repository
{
    public class TrackRepositoryTest
    {
        private readonly ITrackRepository _repo;

        public TrackRepositoryTest()
        {
            var services = new ServiceCollection();
            services.AddTransient<ITrackRepository, TrackRepository>();

            var serviceProvider = services.BuildServiceProvider();

            _repo = serviceProvider.GetService<ITrackRepository>();
        }

        [DotMemoryUnit(FailIfRunWithoutSupport = false)]
        [Fact]
        public void TrackGetAll()
        {
            // Act
            var tracks = _repo.GetAll();

            // Assert
            Assert.True(tracks.Count > 1, "The number of tracks was not greater than 1");
        }

        [AssertTraffic(AllocatedSizeInBytes = 420360, Types = new[] {typeof(Track)})]
        [Fact]
        public void DotMemoryUnitTest()
        {
            var repo = new TrackRepository();

            repo.GetAll();

            dotMemory.Check(memory =>
                Assert.Equal(3503, memory.GetObjects(where => where.Type.Is<Track>()).ObjectsCount));

            GC.KeepAlive(repo); // prevent objects from GC if this is implied by test logic
        }
    }
}