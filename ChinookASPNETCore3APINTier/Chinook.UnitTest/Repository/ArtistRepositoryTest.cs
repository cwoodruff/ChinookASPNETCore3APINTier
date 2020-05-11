using System;
using Chinook.DataEFCore.Repositories;
using Chinook.Domain.Entities;
using Chinook.Domain.Repositories;
using JetBrains.dotMemoryUnit;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

[assembly: SuppressXUnitOutputException]
[assembly: EnableDotMemoryUnitSupport]
[assembly: CollectionBehavior(DisableTestParallelization = true)]

namespace Chinook.UnitTest.Repository
{
    public class ArtistRepositoryTest
    {
        private readonly IArtistRepository _repo;

        public ArtistRepositoryTest()
        {
            var services = new ServiceCollection();
            services.AddTransient<IArtistRepository, ArtistRepository>();

            var serviceProvider = services.BuildServiceProvider();

            _repo = serviceProvider.GetService<IArtistRepository>();
        }

        [DotMemoryUnitAttribute(FailIfRunWithoutSupport = false)]
        [Fact]
        public void ArtistGetAll()
        {
            // Act
            var artists = _repo.GetAll();

            // Assert
            Assert.True(artists.Count > 1, "The number of artists was not greater than 1");
        }

        [AssertTraffic(AllocatedSizeInBytes = 11000, Types = new[] {typeof(Artist)})]
        [Fact]
        public void DotMemoryUnitTest()
        {
            var repo = new ArtistRepository();

            repo.GetAll();

            dotMemory.Check(memory =>
                Assert.Equal(275, memory.GetObjects(where => where.Type.Is<Artist>()).ObjectsCount));

            GC.KeepAlive(repo); // prevent objects from GC if this is implied by test logic
        }
    }
}