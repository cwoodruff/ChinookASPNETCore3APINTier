using System;
using Chinook.DataEFCore.Repositories;
using Chinook.Domain.Entities;
using Chinook.Domain.Repositories;
using JetBrains.dotMemoryUnit;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Chinook.UnitTest.Repository
{
    public class PlayListRepositoryTest
    {
        private readonly IPlaylistRepository _repo;

        public PlayListRepositoryTest()
        {
            var services = new ServiceCollection();
            services.AddTransient<IPlaylistRepository, PlaylistRepository>();

            var serviceProvider = services.BuildServiceProvider();

            _repo = serviceProvider.GetService<IPlaylistRepository>();
        }

        [DotMemoryUnit(FailIfRunWithoutSupport = false)]
        [Fact]
        public void PlayListGetAll()
        {
            // Act
            var playLists = _repo.GetAll();

            // Assert
            Assert.True(playLists.Count > 1, "The number of play lists was not greater than 1");
        }

        [AssertTraffic(AllocatedSizeInBytes = 1000, Types = new[] {typeof(Playlist)})]
        [Fact]
        public void DotMemoryUnitTest()
        {
            var repo = new PlaylistRepository();

            repo.GetAll();

            dotMemory.Check(memory =>
                Assert.Equal(18, memory.GetObjects(where => where.Type.Is<Playlist>()).ObjectsCount));

            GC.KeepAlive(repo); // prevent objects from GC if this is implied by test logic
        }
    }
}