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
    public class EmployeeController : ControllerBase
    {
        private readonly IChinookSupervisor _chinookSupervisor;

        public EmployeeController(IChinookSupervisor chinookSupervisor)
        {
            _chinookSupervisor = chinookSupervisor;
        }

        [HttpGet]
        [Produces(typeof(List<EmployeeApiModel>))]
        public async Task<ActionResult<List<EmployeeApiModel>>> Get()
        {
            try
            {
                return new ObjectResult(await _chinookSupervisor.GetAllEmployee());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpGet("{id}")]
        [Produces(typeof(EmployeeApiModel))]
        public async Task<ActionResult<EmployeeApiModel>> Get(int id)
        {
            try
            {
                var employee = await _chinookSupervisor.GetEmployeeById(id);
                if ( employee == null)
                {
                    return NotFound();
                }

                return Ok(employee);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpGet("reportsto/{id}")]
        [Produces(typeof(List<EmployeeApiModel>))]
        public async Task<ActionResult<List<EmployeeApiModel>>> GetReportsTo(int id)
        {
            try
            {
                var employee = await _chinookSupervisor.GetEmployeeById(id);
                if ( employee == null)
                {
                    return NotFound();
                }

                return Ok(employee);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpGet("directreports/{id}")]
        [Produces(typeof(EmployeeApiModel))]
        public async Task<ActionResult<EmployeeApiModel>> GetDirectReports(int id)
        {
            try
            {
                var employee = await _chinookSupervisor.GetEmployeeById(id);
                if ( employee == null)
                {
                    return NotFound();
                }

                return Ok(employee);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPost]
        public async Task<ActionResult<EmployeeApiModel>> Post([FromBody] EmployeeApiModel input)
        {
            try
            {
                if (input == null)
                    return BadRequest();

                return StatusCode(201, await _chinookSupervisor.AddEmployee(input));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<EmployeeApiModel>> Put(int id, [FromBody] EmployeeApiModel input)
        {
            try
            {
                if (input == null)
                    return BadRequest();
                if (!_chinookSupervisor.EmployeeExists(id))
                {
                    return NotFound();
                }

                var errors = JsonConvert.SerializeObject(ModelState.Values
                    .SelectMany(state => state.Errors)
                    .Select(error => error.ErrorMessage));
                Debug.WriteLine(errors);

                if (await _chinookSupervisor.UpdateEmployee(input))
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
                if (!_chinookSupervisor.EmployeeExists(id))
                {
                    return NotFound();
                }

                if (await _chinookSupervisor.DeleteEmployee(id))
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