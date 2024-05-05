//using FormAPI.Models;
//using Microsoft.AspNetCore.Cors.Infrastructure;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using FormAPI.Repositories;
//using Newtonsoft.Json;
//using FormAPI.Service;


//namespace FormAPI.Controllers
//{
//    [Route("api/[controller]")]
//    //[Route("api/form/formrecord")]
//    [ApiController]
//    public class FormsController : ControllerBase
//    {
//        private readonly IFormRepository _formRepository;
//        private readonly FormService _formService;

//        public FormsController(IFormRepository formRepository, FormService formService)
//        {
//            _formRepository = formRepository;
//            _formService = formService;
//        }



//        // GET: api/Form
//        [HttpGet("Fields")]
//        //[HttpGet("{id}")]for all
//        public async Task<ActionResult<IEnumerable<FormField>>> GetFormFields()
//        {
//            var formFields = await _formRepository.GetAllFormFields();
//            return Ok(formFields);
//        }

//        // POST: api/Form
//        [HttpPost]
        
//        public async Task<ActionResult<FormField>> CreateFormField(FormField formField)
//        {
//            var createdFormField = await _formRepository.CreateFormField(formField);
//            return CreatedAtAction(nameof(GetFormFields), new { id = createdFormField.Id }, createdFormField);
//        }

//        [HttpGet("Records")]
//        public async Task<ActionResult<IEnumerable<FormRecord>>> GetFormRecords()
//        {
//            var formRecords = await _formRepository.GetAllFormRecordsAsync();
//            return Ok(formRecords);
//        }
//        [HttpGet("{id}")]

//        public async Task<ActionResult<FormRecord>> GetFormRecord(int id)
//        {
//            var formRecord = await _formRepository.GetByIdAsync(id);

//            if (formRecord == null)
//            {
//                return NotFound("FormRecord not found.");
//            }

//            return Ok(formRecord);
//        }


//        // POST: api/Form/Record
//        [HttpPost("Record")]
//        public async Task<ActionResult<FormRecord>> CreateFormRecord(FormRecord formRecord)
//        {
//            if (formRecord == null)
//            {
//                return BadRequest("FormRecord object is null");
//            }

//            // Perform additional validation if needed
//            if (!ModelState.IsValid)
//            {
//                return BadRequest(ModelState);
//            }

//            // Create the form record
//            var createdFormRecord = await _formRepository.CreateFormRecordAsync(formRecord);

//            // Return the created form record
//            return CreatedAtRoute("GetFormRecord", new { id = createdFormRecord.Id }, createdFormRecord);
//        }

//    }
//}





using FormAPI.Models;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FormAPI.Repositories;
using Newtonsoft.Json;
using FormAPI.Service;
using FormAPI.Repositories;


namespace FormAPI.Controllers
{
    [Route("api/[controller]")]
    //[Route("api/form/formrecord")]
    [ApiController]
    public class FormsController : ControllerBase
    {
        private readonly string _connectionString;

        public FormsController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        [HttpGet("Fields")]
        public async Task<ActionResult<IEnumerable<FormField>>> GetFormFields()
        {
            // Manually instantiate FormRepository
            var formRepository = new FormRepository(_connectionString);

            try
            {
                var formFields = await formRepository.GetAllFormFields();
                return Ok(formFields);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpGet("Records")]
        public async Task<ActionResult<IEnumerable<FormRecord>>> GetFormRecords()
        {
            // Manually instantiate FormRepository
            var formRepository = new FormRepository(_connectionString);

            try
            {
                var formRecords = await formRepository.GetAllFormRecordsAsync();
                return Ok(formRecords);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<FormRecord>> GetFormRecord(int id)
        {
            // Manually instantiate FormRepository
            var formRepository = new FormRepository(_connectionString);

            try
            {
                var formRecord = await formRepository.GetByIdAsync(id);
                if (formRecord == null)
                {
                    return NotFound("FormRecord not found.");
                }
                return Ok(formRecord);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
        [HttpPost("Record")]
        public async Task<ActionResult<FormRecord>> CreateFormRecord(FormRecord formRecord)
        {
            // Manually instantiate FormRepository
            var formRepository = new FormRepository(_connectionString);

            try
            {
                if (formRecord == null)
                {
                    return BadRequest("FormRecord object is null");
                }
                // Perform additional validation if needed
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                // Create the form record
                var createdFormRecord = await formRepository.CreateFormRecordAsync(formRecord);
                // Return the created form record
                return CreatedAtRoute("GetFormRecord", new { id = createdFormRecord.Id }, createdFormRecord);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}

