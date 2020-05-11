using System;
using Chinook.DataEFCore.Repositories;
using Chinook.Domain.Entities;
using Chinook.Domain.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Chinook.UnitTest.JsonRepository
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

        [Fact]
        public void TrackGetAll()
        {
            // Act
            var tracks = _repo.GetAll();

            // Assert
            Assert.True(tracks.Count > 1, "The number of tracks was not greater than 1");
        }
    }
}