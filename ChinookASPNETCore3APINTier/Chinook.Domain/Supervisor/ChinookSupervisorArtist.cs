using System;
using System.Collections.Generic;
using System.Linq;
using Chinook.Domain.Extensions;
using Chinook.Domain.ApiModels;
using Chinook.Domain.Entities;
using Microsoft.Extensions.Caching.Memory;

namespace Chinook.Domain.Supervisor
{
    public partial class ChinookSupervisor
    {
        public IEnumerable<ArtistApiModel> GetAllArtist()
        {
            var artists = _artistRepository.GetAll();
            foreach (var artist in artists)
            {
                var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(604800));
                _cache.Set(artist.ArtistId, artist, cacheEntryOptions);
            }
            return artists.ConvertAll();
        }

        public ArtistApiModel GetArtistById(int id)
        {
            var artist = _cache.Get<Artist>(id);

            if (artist != null)
            {
                var artistApiModel = artist.Convert;
                artistApiModel.Albums = (GetAlbumByArtistId(artistApiModel.ArtistId)).ToList();
                return artistApiModel;
            }
            else
            {
                var artistApiModel = (_artistRepository.GetById(id)).Convert;
                artistApiModel.Albums = (GetAlbumByArtistId(artistApiModel.ArtistId)).ToList();

                var cacheEntryOptions =
                    new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(604800));
                _cache.Set(artistApiModel.ArtistId, artistApiModel, cacheEntryOptions);

                return artistApiModel;
            }
        }

        public ArtistApiModel AddArtist(ArtistApiModel newArtistApiModel)
        {
            var artist = newArtistApiModel.Convert;

            artist = _artistRepository.Add(artist);
            newArtistApiModel.ArtistId = artist.ArtistId;
            return newArtistApiModel;
        }

        public bool UpdateArtist(ArtistApiModel artistApiModel)
        {
            var artist = _artistRepository.GetById(artistApiModel.ArtistId);

            if (artist == null) return false;
            artist.ArtistId = artistApiModel.ArtistId;
            artist.Name = artistApiModel.Name;

            return _artistRepository.Update(artist);
        }

        public bool DeleteArtist(int id) 
            => _artistRepository.Delete(id);
    }
}