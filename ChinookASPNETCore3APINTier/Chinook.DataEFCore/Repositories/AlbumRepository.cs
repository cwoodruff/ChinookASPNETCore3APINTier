using System;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Microsoft.EntityFrameworkCore;
using Chinook.Domain.Repositories;
using Chinook.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace Chinook.DataEFCore.Repositories
{
    public class AlbumRepository : EfRepository<Album>
    {
        public AlbumRepository(ChinookContext context) : base(context)
        {
        }

        public List<Album> GetByArtistId(int id) =>
            _dbContext.Album.Where(a => a.ArtistId == id).ToList();
    }
}