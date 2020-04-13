using System;
using System.Collections.Generic;
using Chinook.Domain.DbInfo;
using Chinook.Domain.Repositories;
using Chinook.Domain.Entities;

namespace Chinook.DataJson.Repositories
{
    public class GenreRepository : IGenreRepository
    {
        private readonly DbInfo _dbInfo;

        public GenreRepository(DbInfo dbInfo)
        {
            _dbInfo = dbInfo;
        }

        public void Dispose()
        {
        }

        private bool GenreExists(int id)
        {
            return true;
        }

        public List<Genre> GetAll()
        {
            return null;
        }

        public Genre GetById(int id)
        {
            return null;
        }

        public Genre Add(Genre newGenre)
        {
            return newGenre;
        }

        public bool Update(Genre genre)
        {
            if (!GenreExists(genre.GenreId))
                return false;

            try
            {
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}