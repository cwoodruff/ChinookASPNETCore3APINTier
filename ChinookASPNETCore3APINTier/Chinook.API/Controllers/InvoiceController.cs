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
    public class InvoiceController : ControllerBase
    {
        private readonly IChinookSupervisor _chinookSupervisor;

        public InvoiceController(IChinookSupervisor chinookSupervisor)
        {
            _chinookSupervisor = chinookSupervisor;
        }

        [HttpGet]
        [Produces(typeof(List<InvoiceApiModel>))]
        public ActionResult<List<InvoiceApiModel>> Get()
        {
            try
            {
                return new ObjectResult(_chinookSupervisor.GetAllInvoice());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpGet("{id}")]
        [Produces(typeof(InvoiceApiModel))]
        public ActionResult<InvoiceApiModel> Get(int id)
        {
            try
            {
                var invoice = _chinookSupervisor.GetInvoiceById(id);
                if ( invoice == null)
                {
                    return NotFound();
                }

                return Ok(invoice);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpGet("customer/{id}")]
        [Produces(typeof(List<InvoiceApiModel>))]
        public ActionResult<InvoiceApiModel> GetByCustomerId(int id)
        {
            try
            {
                if (_chinookSupervisor.GetCustomerById(id) == null)
                {
                    return NotFound();
                }

                return Ok(_chinookSupervisor.GetInvoiceByCustomerId(id));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPost]
        public ActionResult<InvoiceApiModel> Post([FromBody] InvoiceApiModel input)
        {
            try
            {
                if (input == null)
                    return BadRequest();

                return StatusCode(201, _chinookSupervisor.AddInvoice(input));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<InvoiceApiModel>> Put(int id, [FromBody] InvoiceApiModel input)
        {
            try
            {
                if (input == null)
                    return BadRequest();
                if (!_chinookSupervisor.InvoiceExists(id))
                {
                    return NotFound();
                }

                var errors = JsonConvert.SerializeObject(ModelState.Values
                    .SelectMany(state => state.Errors)
                    .Select(error => error.ErrorMessage));
                Debug.WriteLine(errors);

                if (await _chinookSupervisor.UpdateInvoice(input))
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
                if (!_chinookSupervisor.InvoiceExists(id))
                {
                    return NotFound();
                }

                if (await _chinookSupervisor.DeleteInvoice(id))
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