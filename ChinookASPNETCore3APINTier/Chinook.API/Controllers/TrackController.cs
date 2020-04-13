using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Chinook.Domain.Supervisor;
using Chinook.Domain.ApiModels;
using Microsoft.AspNetCore.Cors;

namespace Chinook.API.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("CorsPolicy")]
    [ApiController]
    public class TrackController : ControllerBase
    {
        private readonly IChinookSupervisor _chinookSupervisor;

        public TrackController(IChinookSupervisor chinookSupervisor)
        {
            _chinookSupervisor = chinookSupervisor;
        }

        [HttpGet]
        [Produces(typeof(List<TrackApiModel>))]
        public ActionResult<List<TrackApiModel>> Get()
        {
            try
            {
                return new ObjectResult(_chinookSupervisor.GetAllTrack());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpGet("{id}")]
        [Produces(typeof(TrackApiModel))]
        public ActionResult<TrackApiModel> Get(int id)
        {
            try
            {
                var track = _chinookSupervisor.GetTrackById(id);
                if ( track == null)
                {
                    return NotFound();
                }

                return Ok(track);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpGet("album/{id}")]
        [Produces(typeof(List<TrackApiModel>))]
        public ActionResult<TrackApiModel> GetByAlbumId(int id)
        {
            try
            {
                var album = _chinookSupervisor.GetAlbumById(id);
                if ( album == null)
                {
                    return NotFound();
                }

                return Ok(album);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpGet("mediatype/{id}")]
        [Produces(typeof(List<TrackApiModel>))]
        public ActionResult<TrackApiModel> GetByMediaTypeId(int id)
        {
            try
            {
                var mediaType = _chinookSupervisor.GetMediaTypeById(id);
                if ( mediaType == null)
                {
                    return NotFound();
                }

                return Ok(mediaType);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpGet("genre/{id}")]
        [Produces(typeof(List<TrackApiModel>))]
        public ActionResult<TrackApiModel> GetByGenreId(int id)
        {
            try
            {
                var genre = _chinookSupervisor.GetGenreById(id);
                if (genre == null)
                {
                    return NotFound();
                }

                return Ok(genre);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPost]
        public ActionResult<TrackApiModel> Post([FromBody] TrackApiModel input)
        {
            try
            {
                if (input == null)
                    return BadRequest();

                return StatusCode(201, _chinookSupervisor.AddTrack(input));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPut("{id}")]
        public ActionResult<TrackApiModel> Put(int id, [FromBody] TrackApiModel input)
        {
            try
            {
                if (input == null)
                    return BadRequest();
                if (_chinookSupervisor.GetTrackById(id) == null)
                {
                    return NotFound();
                }

                // var errors = JsonConvert.SerializeObject(ModelState.Values
                //     .SelectMany(state => state.Errors)
                //     .Select(error => error.ErrorMessage));
                // Debug.WriteLine(errors);

                if (_chinookSupervisor.UpdateTrack(input))
                {
                    return Ok(input);
                }

                return StatusCode(500);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                if (_chinookSupervisor.GetTrackById(id) == null)
                {
                    return NotFound();
                }

                if (_chinookSupervisor.DeleteTrack(id))
                {
                    return Ok();
                }

                return StatusCode(500);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}