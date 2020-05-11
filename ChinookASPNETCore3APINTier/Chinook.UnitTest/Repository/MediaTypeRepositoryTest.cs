using System;
using Chinook.DataEFCore.Repositories;
using Chinook.Domain.Entities;
using Chinook.Domain.Repositories;
using JetBrains.dotMemoryUnit;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Chinook.UnitTest.Repository
{
    public class MediaTypeRepositoryTest
    {
        private readonly IMediaTypeRepository _repo;

        public MediaTypeRepositoryTest()
        {
            var services = new ServiceCollection();
            services.AddTransient<IMediaTypeRepository, MediaTypeRepository>();

            var serviceProvider = services.BuildServiceProvider();

            _repo = serviceProvider.GetService<IMediaTypeRepository>();
        }

        [DotMemoryUnit(FailIfRunWithoutSupport = false)]
        [Fact]
        public void MediaTypeGetAll()
        {
            // Act
            var mediaTypes = _repo.GetAll();

            // Assert
            Assert.True(mediaTypes.Count > 1, "The number of media types was not greater than 1");
        }

        [AssertTraffic(AllocatedSizeInBytes = 1000, Types = new[] {typeof(MediaType)})]
        [Fact]
        public void DotMemoryUnitTest()
        {
            var repo = new MediaTypeRepository();

            repo.GetAll();

            dotMemory.Check(memory =>
                Assert.Equal(5, memory.GetObjects(where => where.Type.Is<MediaType>()).ObjectsCount));

            GC.KeepAlive(repo); // prevent objects from GC if this is implied by test logic
        }
    }
}