using System;
using Chinook.DataEFCore.Repositories;
using Chinook.Domain.Entities;
using Chinook.Domain.Repositories;
using JetBrains.dotMemoryUnit;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Chinook.UnitTest.Repository
{
    public class GenreRepositoryTest
    {
        private readonly IGenreRepository _repo;

        public GenreRepositoryTest()
        {
            var services = new ServiceCollection();
            services.AddTransient<IGenreRepository, GenreRepository>();

            var serviceProvider = services.BuildServiceProvider();

            _repo = serviceProvider.GetService<IGenreRepository>();
        }

        [DotMemoryUnit(FailIfRunWithoutSupport = false)]
        [Fact]
        public void GenreGetAll()
        {
            // Act
            var genres = _repo.GetAll();

            // Assert
            Assert.True(genres.Count > 1, "The number of genres was not greater than 1");
        }

        [AssertTraffic(AllocatedSizeInBytes = 1000, Types = new[] {typeof(Genre)})]
        [Fact]
        public void DotMemoryUnitTest()
        {
            var repo = new GenreRepository();

            repo.GetAll();

            dotMemory.Check(memory =>
                Assert.Equal(25, memory.GetObjects(where => where.Type.Is<Genre>()).ObjectsCount));

            GC.KeepAlive(repo); // prevent objects from GC if this is implied by test logic
        }
    }
}