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
    public class ArtistRepository : IArtistRepository
    {
        private readonly ChinookContext _context;        

        public ArtistRepository(ChinookContext context)
        {
            _context = context;
        }

        private bool ArtistExists(int id) =>
            _context.Artist.Any(a => a.ArtistId == id);

        public void Dispose() => _context.Dispose();

        public List<Artist> GetAll() 
            => _context.GetAllArtists();

        public Artist GetById(int id)
        {
            var artist = _context.GetArtist(id);
            return artist;
        }

        public Artist Add(Artist newArtist)
        {
            _context.Artist.Add(newArtist);
            _context.SaveChanges();
            return newArtist;
        }

        public bool Update(Artist artist)
        {
            if (!ArtistExists(artist.ArtistId))
                return false;
            _context.Artist.Update(artist);
            _context.SaveChanges();
            return true;
        }

        public bool Delete(int id)
        {
            if (!ArtistExists(id))
                return false;
            var toRemove = _context.Artist.Find(id);
            _context.Artist.Remove(toRemove);
            _context.SaveChanges();
            return true;
        }
    }
}