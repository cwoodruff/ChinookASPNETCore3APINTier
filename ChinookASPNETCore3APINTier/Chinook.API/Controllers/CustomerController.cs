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
    public class CustomerController : ControllerBase
    {
        private readonly IChinookSupervisor _chinookSupervisor;

        public CustomerController(IChinookSupervisor chinookSupervisor)
        {
            _chinookSupervisor = chinookSupervisor;
        }

        [HttpGet]
        [Produces(typeof(List<CustomerApiModel>))]
        public ActionResult<List<CustomerApiModel>> Get()
        {
            try
            {
                return new ObjectResult(_chinookSupervisor.GetAllCustomer());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpGet("{id}")]
        [Produces(typeof(CustomerApiModel))]
        public ActionResult<CustomerApiModel> Get(int id)
        {
            try
            {
                var customer = _chinookSupervisor.GetCustomerById(id);
                if ( customer == null)
                {
                    return NotFound();
                }

                return Ok(customer);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpGet("supportrep/{id}")]
        [Produces(typeof(List<CustomerApiModel>))]
        public ActionResult<CustomerApiModel> GetBySupportRepId(int id)
        {
            try
            {
                var rep = _chinookSupervisor.GetEmployeeById(id);
                if ( rep == null)
                {
                    return NotFound();
                }

                return Ok(rep);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPost]
        public ActionResult<CustomerApiModel> Post([FromBody] CustomerApiModel input)
        {
            try
            {
                if (input == null)
                    return BadRequest();

                return StatusCode(201, _chinookSupervisor.AddCustomer(input));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CustomerApiModel>> Put(int id, [FromBody] CustomerApiModel input)
        {
            try
            {
                if (input == null)
                    return BadRequest();
                if (!_chinookSupervisor.CustomerExists(id))
                {
                    return NotFound();
                }

                var errors = JsonConvert.SerializeObject(ModelState.Values
                    .SelectMany(state => state.Errors)
                    .Select(error => error.ErrorMessage));
                Debug.WriteLine(errors);

                if (await _chinookSupervisor.UpdateCustomer(input))
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
                if (!_chinookSupervisor.CustomerExists(id))
                {
                    return NotFound();
                }

                if (await _chinookSupervisor.DeleteCustomer(id))
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