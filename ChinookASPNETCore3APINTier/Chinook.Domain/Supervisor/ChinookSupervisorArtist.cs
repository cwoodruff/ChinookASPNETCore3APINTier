using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chinook.Domain.ApiModels;
using Chinook.Domain.Extensions;
using Microsoft.Extensions.Caching.Memory;

namespace Chinook.Domain.Supervisor
{
    public partial class ChinookSupervisor
    {
        public bool ArtistExists(int id) => _artistRepository.ArtistExists(id);
        
        public async Task<IAsyncEnumerable<ArtistApiModel>> GetAllArtist()
        {
            var artists = (await _artistRepository.GetAll()).ConvertAll();
            await foreach (var artist in artists)
            {
                var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(604800));
                _cache.Set(string.Concat("Artist-", artist.ArtistId), artist, cacheEntryOptions);
            }
            return artists;
        }

        public async Task<ArtistApiModel> GetArtistById(int id)
        {
            var artistApiModelCached = _cache.Get<ArtistApiModel>(string.Concat("Artist-", id));

            if (artistApiModelCached != null)
            {
                return artistApiModelCached;
            }
            else
            {
                var artistApiModel = (await _artistRepository.GetById(id)).Convert();
                var cacheEntryOptions =
                    new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(604800));
                _cache.Set(string.Concat("Artist-", artistApiModel.ArtistId), artistApiModel, cacheEntryOptions);

                return artistApiModel;
            }
        }

        public async Task<ArtistApiModel> AddArtist(ArtistApiModel newArtistApiModel)
        {
            var artist = newArtistApiModel.Convert();
            artist = await _artistRepository.Add(artist);
            return artist.Convert();
        }

        public async Task<bool> UpdateArtist(ArtistApiModel artistApiModel) => await _artistRepository.Update(artistApiModel.Convert());

        public async Task<bool> DeleteArtist(int id) => await _artistRepository.Delete(id);
    }
}