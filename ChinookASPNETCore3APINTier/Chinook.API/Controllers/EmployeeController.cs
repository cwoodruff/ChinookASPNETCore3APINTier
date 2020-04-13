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
    public class EmployeeController : ControllerBase
    {
        private readonly IChinookSupervisor _chinookSupervisor;

        public EmployeeController(IChinookSupervisor chinookSupervisor)
        {
            _chinookSupervisor = chinookSupervisor;
        }

        [HttpGet]
        [Produces(typeof(List<EmployeeApiModel>))]
        public ActionResult<List<EmployeeApiModel>> Get()
        {
            try
            {
                return new ObjectResult(_chinookSupervisor.GetAllEmployee());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpGet("{id}")]
        [Produces(typeof(EmployeeApiModel))]
        public ActionResult<EmployeeApiModel> Get(int id)
        {
            try
            {
                var employee = _chinookSupervisor.GetEmployeeById(id);
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
        public ActionResult<List<EmployeeApiModel>> GetReportsTo(int id)
        {
            try
            {
                var employee = _chinookSupervisor.GetEmployeeById(id);
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
        public ActionResult<EmployeeApiModel> GetDirectReports(int id)
        {
            try
            {
                var employee = _chinookSupervisor.GetEmployeeById(id);
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
        public ActionResult<EmployeeApiModel> Post([FromBody] EmployeeApiModel input)
        {
            try
            {
                if (input == null)
                    return BadRequest();

                return StatusCode(201, _chinookSupervisor.AddEmployee(input));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPut("{id}")]
        public ActionResult<EmployeeApiModel> Put(int id, [FromBody] EmployeeApiModel input)
        {
            try
            {
                if (input == null)
                    return BadRequest();
                if (_chinookSupervisor.GetEmployeeById(id) == null)
                {
                    return NotFound();
                }

                // var errors = JsonConvert.SerializeObject(ModelState.Values
                //     .SelectMany(state => state.Errors)
                //     .Select(error => error.ErrorMessage));
                // Debug.WriteLine(errors);

                if (_chinookSupervisor.UpdateEmployee(input))
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
                if (_chinookSupervisor.GetEmployeeById(id) == null)
                {
                    return NotFound();
                }

                if (_chinookSupervisor.DeleteEmployee(id))
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