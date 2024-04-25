using FormAPI.Models;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FormAPI.Repositories;
using Newtonsoft.Json;
using FormAPI.Service;


namespace FormAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FormController : ControllerBase
    {
        private readonly IFormRepository _formRepository;
        private readonly FormService _formService;

        public FormController(IFormRepository formRepository, FormService formService)
        {
            _formRepository = formRepository;
            _formService = formService;
        }

        // GET: api/Form
        [HttpGet]
        //[HttpGet("{id}")]for all
        public async Task<ActionResult<IEnumerable<FormField>>> GetFormFields()
        {
            var formFields = await _formRepository.GetAll();
            return Ok(formFields);
        }
        //[HttpGet("{id}")]
        //public async Task<ActionResult<IEnumerable<FormField>>> GetFormField(int Id)
        //{
        //    var formFields = await _formRepository.FindAsync(Id);
        //if(FormField is null)
            //return NotFound("FormField not found.");
        //    return Ok(formFields);
        //}

        // POST: api/Form
        [HttpPost]
        
        public async Task<ActionResult<FormField>> CreateFormField(FormField formField)
        {
            var createdFormField = await _formRepository.Create(formField);
            return CreatedAtAction(nameof(GetFormFields), new { id = createdFormField.Id }, createdFormField);
        }

        // POST: api/Form/GenerateFormRecord
        [HttpPost("GenerateFormRecord")]
        public ActionResult<FormRecord> GenerateFormRecord(List<FormField> formFields)
        {
            var formRecord = _formService.CreateFormRecord(formFields);
            return Ok(formRecord);
        }
    }
}