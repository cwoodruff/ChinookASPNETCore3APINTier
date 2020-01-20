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
    public class ArtistController : ControllerBase
    {
        private readonly IChinookSupervisor _chinookSupervisor;

        public ArtistController(IChinookSupervisor chinookSupervisor)
        {
            _chinookSupervisor = chinookSupervisor;
        }

        [HttpGet]
        [Produces(typeof(List<ArtistApiModel>))]
        public ActionResult<List<ArtistApiModel>> Get()
        {
            try
            {
                return new ObjectResult(_chinookSupervisor.GetAllArtist());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpGet("{id}")]
        [Produces(typeof(ArtistApiModel))]
        public ActionResult<ArtistApiModel> Get(int id)
        {
            try
            {
                var artist = _chinookSupervisor.GetArtistById(id);
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
        public ActionResult<ArtistApiModel> Post([FromBody] ArtistApiModel input)
        {
            try
            {
                if (input == null)
                    return BadRequest();

                return StatusCode(201, _chinookSupervisor.AddArtist(input));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ArtistApiModel>> Put(int id, [FromBody] ArtistApiModel input)
        {
            try
            {
                if (input == null)
                    return BadRequest();
                if (!_chinookSupervisor.ArtistExists(id))
                {
                    return NotFound();
                }

                var errors = JsonConvert.SerializeObject(ModelState.Values
                    .SelectMany(state => state.Errors)
                    .Select(error => error.ErrorMessage));
                Debug.WriteLine(errors);

                if (await _chinookSupervisor.UpdateArtist(input))
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
                if (!_chinookSupervisor.ArtistExists(id))
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