using System;
using Chinook.DataEFCore.Repositories;
using Chinook.Domain.Entities;
using Chinook.Domain.Repositories;
using JetBrains.dotMemoryUnit;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Chinook.UnitTest.Repository
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

        [DotMemoryUnit(FailIfRunWithoutSupport = false)]
        [Fact]
        public void AlbumGetAll()
        {
            // Arrange

            // Act
            var albums = _repo.GetAll();

            // Assert
            Assert.True(albums.Count > 1, "The number of albums was not greater than 1");
        }

        [DotMemoryUnit(FailIfRunWithoutSupport = false)]
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

        [AssertTraffic(AllocatedSizeInBytes = 16656, Types = new[] {typeof(Album)})]
        [Fact]
        public void DotMemoryUnitTest()
        {
            //var repo = new AlbumRepository();

            _repo.GetAll();

            dotMemory.Check(memory =>
                Assert.Equal(348, memory.GetObjects(where => where.Type.Is<Album>()).ObjectsCount));

            GC.KeepAlive(_repo); // prevent objects from GC if this is implied by test logic
        }
    }
}