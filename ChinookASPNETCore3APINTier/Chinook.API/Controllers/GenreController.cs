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
    [ResponseCache(Duration = 604800)]
    public class GenreController : ControllerBase
    {
        private readonly IChinookSupervisor _chinookSupervisor;

        public GenreController(IChinookSupervisor chinookSupervisor)
        {
            _chinookSupervisor = chinookSupervisor;
        }

        [HttpGet]
        [Produces(typeof(List<GenreApiModel>))]
        public async Task<ActionResult<List<GenreApiModel>>> Get()
        {
            try
            {
                return new ObjectResult(await _chinookSupervisor.GetAllGenre());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpGet("{id}")]
        [Produces(typeof(GenreApiModel))]
        public async Task<ActionResult<GenreApiModel>> Get(int id)
        {
            try
            {
                var genre = await _chinookSupervisor.GetGenreById(id);
                if ( genre == null)
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
        public async Task<ActionResult<GenreApiModel>> Post([FromBody] GenreApiModel input)
        {
            try
            {
                if (input == null)
                    return BadRequest();

                return StatusCode(201, await _chinookSupervisor.AddGenre(input));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<GenreApiModel>> Put(int id, [FromBody] GenreApiModel input)
        {
            try
            {
                if (input == null)
                    return BadRequest();
                if (!_chinookSupervisor.GenreExists(id))
                {
                    return NotFound();
                }

                var errors = JsonConvert.SerializeObject(ModelState.Values
                    .SelectMany(state => state.Errors)
                    .Select(error => error.ErrorMessage));
                Debug.WriteLine(errors);

                if (await _chinookSupervisor.UpdateGenre(input))
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
                if (!_chinookSupervisor.GenreExists(id))
                {
                    return NotFound();
                }

                if (await _chinookSupervisor.DeleteGenre(id))
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