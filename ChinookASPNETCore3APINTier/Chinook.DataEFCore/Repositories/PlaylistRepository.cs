using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices;
using Microsoft.EntityFrameworkCore;
using Chinook.Domain.Repositories;
using Chinook.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace Chinook.DataEFCore.Repositories
{
    public class PlaylistRepository : EfRepository<Album>
    {
        public PlaylistRepository(ChinookContext context) : base(context)
        {
        }

        public List<Playlist> GetByTrackId(int id)
        {
            return _dbContext.Playlist
                .Where(c => c.PlaylistTracks.Any(o => o.TrackId == id))
                .ToList();
        }
    }
}