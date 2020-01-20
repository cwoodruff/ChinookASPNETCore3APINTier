using System;
using System.Collections.Generic;
using Chinook.Domain.ApiModels;
using System.Linq;
using System.Threading.Tasks;
using Chinook.Domain.Extensions;
using Microsoft.Extensions.Caching.Memory;

namespace Chinook.Domain.Supervisor
{
    public partial class ChinookSupervisor
    {
        public bool GenreExists(int id) => _genreRepository.GenreExists(id);
        public async Task<IAsyncEnumerable<GenreApiModel>> GetAllGenre()
        {
            var genres = (await _genreRepository.GetAll()).ConvertAll();
            await foreach (var genre in genres)
            {
                var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(604800));
                _cache.Set(string.Concat("Genre-", genre.GenreId), genre, cacheEntryOptions);
            }
            return genres;
        }

        public async Task<GenreApiModel> GetGenreById(int id)
        {
            var genreApiModelCached = _cache.Get<GenreApiModel>(string.Concat("Genre-", id));

            if (genreApiModelCached != null)
            {
                return genreApiModelCached;
            }
            else
            {
                var genreApiModel = (await _genreRepository.GetById(id)).Convert();
                var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(604800));
                _cache.Set(string.Concat("Genre-", genreApiModel.GenreId), genreApiModel, cacheEntryOptions);
                
                return genreApiModel;
            }
        }

        public async Task<GenreApiModel> AddGenre(GenreApiModel newGenreApiModel)
        {
            var genre = newGenreApiModel.Convert();

            genre = await _genreRepository.Add(genre);
            return genre.Convert();
        }

        public async Task<bool> UpdateGenre(GenreApiModel genreApiModel) => await _genreRepository.Update(genreApiModel.Convert());

        public async Task<bool> DeleteGenre(int id) 
            => await _genreRepository.Delete(id);
    }
}