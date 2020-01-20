using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Threading.Tasks;
using Chinook.Domain.Supervisor;
using Chinook.Domain.ApiModels;
using Microsoft.AspNetCore.Cors;

namespace Chinook.API.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("CorsPolicy")]
    [ApiController]
    public class AlbumController : ControllerBase
    {
        private readonly IChinookSupervisor _chinookSupervisor;

        public AlbumController(IChinookSupervisor chinookSupervisor)
        {
            _chinookSupervisor = chinookSupervisor;
        }

        
        // GET api/values
        /// <summary>
        /// Get Album Value
        /// </summary>
        /// <remarks>This API will get the values.</remarks>
        [HttpGet]
        [Produces(typeof(List<AlbumApiModel>))]
        public async Task<ActionResult<List<AlbumApiModel>>> Get()
        {
            try
            {
                return new ObjectResult(await _chinookSupervisor.GetAllAlbum());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpGet("{id}")]
        [Produces(typeof(AlbumApiModel))]
        public async Task<ActionResult<AlbumApiModel>> Get(int id)
        {
            try
            {
                var album = await _chinookSupervisor.GetAlbumById(id);
                if (album == null)
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

        [HttpGet("artist/{id}")]
        [Produces(typeof(List<AlbumApiModel>))]
        public async Task<ActionResult<List<AlbumApiModel>>> GetByArtistId(int id)
        {
            try
            {
                var artist = await _chinookSupervisor.GetArtistById(id);
                if ( artist == null)
                {
                    return NotFound();
                }

                return Ok(artist);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPost]
        public async Task<ActionResult<AlbumApiModel>> Post([FromBody] AlbumApiModel input)
        {
            try
            {
                if (input == null)
                    return BadRequest();

                return StatusCode(201, await _chinookSupervisor.AddAlbum(input));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<AlbumApiModel>> Put(int id, [FromBody] AlbumApiModel input)
        {
            try
            {
                if (input == null)
                    return BadRequest();
                if (!_chinookSupervisor.AlbumExists(id))
                {
                    return NotFound();
                }

                var errors = JsonConvert.SerializeObject(ModelState.Values
                    .SelectMany(state => state.Errors)
                    .Select(error => error.ErrorMessage));
                Debug.WriteLine(errors);

                if (await _chinookSupervisor.UpdateAlbum(input))
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
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                if (!_chinookSupervisor.AlbumExists(id))
                {
                    return NotFound();
                }

                if (await _chinookSupervisor.DeleteAlbum(id))
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