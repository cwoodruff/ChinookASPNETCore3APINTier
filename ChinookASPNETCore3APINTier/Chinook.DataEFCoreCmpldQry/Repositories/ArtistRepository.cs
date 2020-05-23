using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Chinook.Domain.Repositories;
using Chinook.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Chinook.DataEFCoreCmpldQry.Repositories
{
    public class ArtistRepository : EfRepository<Album>
    {
        public ArtistRepository(ChinookContext context) : base(context)
        {
        }

        public List<Artist> GetAll() 
            => _dbContext.GetAllArtists();

        public Artist GetById(int id)
        {
            var artist = _dbContext.GetArtist(id);
            return artist;
        }

        public Artist Add(Artist newArtist)
        {
            _dbContext.Artist.Add(newArtist);
            _dbContext.SaveChanges();
            return newArtist;
        }

        public bool Update(Artist artist)
        {
            if (!Exists(artist.ArtistId))
                return false;
            _dbContext.Artist.Update(artist);
            _dbContext.SaveChanges();
            return true;
        }

        public bool Delete(int id)
        {
            if (!Exists(id))
                return false;
            var toRemove = _dbContext.Artist.Find(id);
            _dbContext.Artist.Remove(toRemove);
            _dbContext.SaveChanges();
            return true;
        }
    }
}