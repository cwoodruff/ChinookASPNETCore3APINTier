using System;
using System.Collections.Generic;
using Chinook.Domain.Extensions;
using Chinook.Domain.ApiModels;
using Chinook.Domain.Entities;
using Microsoft.Extensions.Caching.Memory;

namespace Chinook.Domain.Supervisor
{
    public partial class ChinookSupervisor
    {
        public IEnumerable<TrackApiModel> GetAllTrack()
        {
            var tracks = _trackRepository.GetAll();
            foreach (var track in tracks)
            {
                var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(604800));
                _cache.Set(track.TrackId, track, cacheEntryOptions);
            }
            return tracks.ConvertAll();
        }

        public TrackApiModel GetTrackById(int id)
        {
            var track = _cache.Get<Track>(id);

            if (track != null)
            {
                var trackApiModel = track.Convert;
                trackApiModel.Genre = GetGenreById(trackApiModel.GenreId.GetValueOrDefault());
                trackApiModel.Album = GetAlbumById(trackApiModel.TrackId);
                trackApiModel.MediaType = GetMediaTypeById(trackApiModel.MediaTypeId);
                trackApiModel.AlbumName = trackApiModel.Album.Title;
                trackApiModel.MediaTypeName = trackApiModel.MediaType.Name;
                trackApiModel.GenreName = trackApiModel.Genre.Name;
                return trackApiModel;
            }
            else
            {
                var trackApiModel = (_trackRepository.GetById(id)).Convert;
                trackApiModel.Genre = GetGenreById(trackApiModel.GenreId.GetValueOrDefault());
                trackApiModel.Album = GetAlbumById(trackApiModel.TrackId);
                trackApiModel.MediaType = GetMediaTypeById(trackApiModel.MediaTypeId);
                trackApiModel.AlbumName = trackApiModel.Album.Title;
                trackApiModel.MediaTypeName = trackApiModel.MediaType.Name;
                trackApiModel.GenreName = trackApiModel.Genre.Name;

                var cacheEntryOptions =
                    new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(604800));
                _cache.Set(trackApiModel.TrackId, trackApiModel, cacheEntryOptions);

                return trackApiModel;
            }
        }

        public IEnumerable<TrackApiModel> GetTrackByAlbumId(int id)
        {
            var tracks = _trackRepository.GetByAlbumId(id);
            return tracks.ConvertAll();
        }

        public IEnumerable<TrackApiModel> GetTrackByGenreId(int id)
        {
            var tracks = _trackRepository.GetByGenreId(id);
            return tracks.ConvertAll();
        }

        public IEnumerable<TrackApiModel> GetTrackByMediaTypeId(int id)
        {
            var tracks = _trackRepository.GetByMediaTypeId(id);
            return tracks.ConvertAll();
        }

        public IEnumerable<TrackApiModel> GetTrackByPlaylistIdId(int id)
        {
            var tracks = _playlistRepository.GetTrackByPlaylistId(id);
            return tracks.ConvertAll();
        }

        public TrackApiModel AddTrack(TrackApiModel newTrackApiModel)
        {
            /*var track = new Track
            {
                TrackId = newTrackApiModel.TrackId,
                Name = newTrackApiModel.Name,
                AlbumId = newTrackApiModel.AlbumId,
                MediaTypeId = newTrackApiModel.MediaTypeId,
                GenreId = newTrackApiModel.GenreId,
                Composer = newTrackApiModel.Composer,
                Milliseconds = newTrackApiModel.Milliseconds,
                Bytes = newTrackApiModel.Bytes,
                UnitPrice = newTrackApiModel.UnitPrice
            };*/

            var track = newTrackApiModel.Convert;

            _trackRepository.Add(track);
            newTrackApiModel.TrackId = track.TrackId;
            return newTrackApiModel;
        }

        public bool UpdateTrack(TrackApiModel trackApiModel)
        {
            var track = _trackRepository.GetById(trackApiModel.TrackId);

            if (track == null) return false;
            track.TrackId = trackApiModel.TrackId;
            track.Name = trackApiModel.Name;
            track.AlbumId = trackApiModel.AlbumId;
            track.MediaTypeId = trackApiModel.MediaTypeId;
            track.GenreId = trackApiModel.GenreId;
            track.Composer = trackApiModel.Composer;
            track.Milliseconds = trackApiModel.Milliseconds;
            track.Bytes = trackApiModel.Bytes;
            track.UnitPrice = trackApiModel.UnitPrice;

            return _trackRepository.Update(track);
        }

        public bool DeleteTrack(int id) 
            => _trackRepository.Delete(id);
    }
}