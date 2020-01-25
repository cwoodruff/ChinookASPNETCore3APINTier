using System;
using System.Collections.Generic;
using Chinook.Domain.Extensions;
using Chinook.Domain.ApiModels;
using System.Linq;
using Microsoft.Extensions.Caching.Memory;

namespace Chinook.Domain.Supervisor
{
    public partial class ChinookSupervisor
    {
        public IEnumerable<GenreApiModel> GetAllGenre()
        {
            var genres = _genreRepository.GetAll().ConvertAll();

            foreach (var genre in genres)
            {
                var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(604800));
                _cache.Set(string.Concat("Genre-", genre.GenreId), genre, cacheEntryOptions);
            }
            
            return genres;
        }

        public GenreApiModel GetGenreById(int id)
        {
            var genreApiModelCached = _cache.Get<GenreApiModel>(string.Concat("Genre-", id));

            if (genreApiModelCached != null)
            {
                return genreApiModelCached;
            }
            else
            {
                var genre = _genreRepository.GetById(id);
                if (genre != null) return null;
                var genreApiModel = genre.Convert();
                genreApiModel.Tracks = (GetTrackByGenreId(genreApiModel.GenreId)).ToList();
                
                var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(604800));
                _cache.Set(string.Concat("Genre-", genreApiModel.GenreId), genreApiModel, cacheEntryOptions);
                
                return genreApiModel;
            }
        }

        public GenreApiModel AddGenre(GenreApiModel newGenreApiModel)
        {
            var genre = newGenreApiModel.Convert();

            genre = _genreRepository.Add(genre);
            newGenreApiModel.GenreId = genre.GenreId;
            return newGenreApiModel;
        }

        public bool UpdateGenre(GenreApiModel genreApiModel)
        {
            var genre = _genreRepository.GetById(genreApiModel.GenreId);

            if (genre == null) return false;
            genre.GenreId = genreApiModel.GenreId;
            genre.Name = genreApiModel.Name;

            return _genreRepository.Update(genre);
        }

        public bool DeleteGenre(int id) 
            => _genreRepository.Delete(id);
    }
}