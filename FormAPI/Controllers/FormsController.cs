using AutoMapper;
using FormAPI.Context;
using FormAPI.DTOs;
using FormAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FormAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class FormsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public FormsController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost("Form")]
        public async Task<ActionResult<FormDto>> CreateForm(FormDto formDto)
        {
            if (formDto == null)
            {
                return BadRequest("Form DTO is null");
            }

            var form = _mapper.Map<Form>(formDto);

            _context.forms.Add(form);
            await _context.SaveChangesAsync();

            var createdFormDto = _mapper.Map<FormDto>(form);

            return CreatedAtAction(nameof(GetFormById), new { id = form.Id }, createdFormDto); //{ id = createdFormDto.Id }
        }

        [HttpGet("Forms")]
        public async Task<ActionResult<IEnumerable<FormDto>>> GetForms()
        {
            var forms = await _context.forms.ToListAsync();
            var formDtos = _mapper.Map<List<FormDto>>(forms);

            return Ok(formDtos);
        }

        [HttpGet("Form/{id}", Name = "GetFormById")]
        public async Task<ActionResult<FormDto>> GetFormById(int id)
        {
            var form = await _context.forms.FindAsync(id);

            if (form == null)
            {
                return NotFound("Form not found.");
            }

            var formDto = _mapper.Map<FormDto>(form);

            return Ok(formDto);
        }

        [HttpPut("Form/{id}")]
        public async Task<ActionResult<FormDto>> UpdateForm(int id, FormDto formDto)
        {
            if (formDto == null)
            {
                return BadRequest("Form DTO is null");
            }

            var form = await _context.forms.FindAsync(id);

            if (form == null)
            {
                return NotFound("Form not found.");
            }

            _mapper.Map(formDto, form);

            _context.forms.Update(form);
            await _context.SaveChangesAsync();

            var updatedFormDto = _mapper.Map<FormDto>(form);

            return Ok(updatedFormDto);
        }

        [HttpDelete("Form/{id}")]
        public async Task<ActionResult> DeleteForm(int id)
        {
            var form = await _context.forms.FindAsync(id);

            if (form == null)
            {
                return NotFound("Form not found.");
            }

            _context.forms.Remove(form);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost("FormField")]
        public async Task<ActionResult<FormFieldDto>> CreateFormField(FormFieldDto formFieldDto)
        {
            if (formFieldDto == null)
            {
                return BadRequest("FormField DTO is null");
            }

            var formField = _mapper.Map<FormField>(formFieldDto);

            _context.formfields.Add(formField);
            await _context.SaveChangesAsync();

            //formFieldDto.Id = formField.Id;

            return CreatedAtAction(nameof(GetFormFieldById), new { id = formField.Id }, formFieldDto); //{ id = formFieldDto.Id }
        }

        [HttpGet("FormFields")]
        public async Task<ActionResult<IEnumerable<FormFieldDto>>> GetFormFields()
        {
            var formFields = await _context.formfields.ToListAsync();
            var formFieldDtos = _mapper.Map<List<FormFieldDto>>(formFields);

            return Ok(formFieldDtos);
        }

        [HttpGet("FormField/{id}", Name = "GetFormFieldById")]
        public async Task<ActionResult<FormFieldDto>> GetFormFieldById(int id)
        {
            var formField = await _context.formfields.FindAsync(id);

            if (formField == null)
            {
                return NotFound("FormField not found.");
            }

            var formFieldDto = _mapper.Map<FormFieldDto>(formField);

            return Ok(formFieldDto);
        }

        [HttpPut("FormField/{id}")]
        public async Task<ActionResult<FormFieldDto>> UpdateFormField(int id, FormFieldDto formFieldDto)
        {
            if (formFieldDto == null)
            {
                return BadRequest("FormField DTO is null");
            }

            var formField = await _context.formfields.FindAsync(id);

            if (formField == null)
            {
                return NotFound("FormField not found.");
            }

            _mapper.Map(formFieldDto, formField);

            _context.formfields.Update(formField);
            await _context.SaveChangesAsync();

            return Ok(formFieldDto);
        }

        [HttpDelete("FormField/{id}")]
        public async Task<ActionResult> DeleteFormField(int id)
        {
            var formField = await _context.formfields.FindAsync(id);

            if (formField == null)
            {
                return NotFound("FormField not found.");
            }

            _context.formfields.Remove(formField);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("FormRecords")]
        public async Task<ActionResult<IEnumerable<FormRecordDto>>> GetFormRecords()
        {
            var formRecords = await _context.formrecords.ToListAsync();
            var formRecordDtos = _mapper.Map<List<FormRecordDto>>(formRecords);

            return Ok(formRecordDtos);
        }

        //[HttpGet("FormRecord/{id}", Name = "GetFormRecordById")]
        [HttpGet("FormRecord/{id}", Name = "GetFormRecordByFormRecordId")]
        public async Task<ActionResult<FormRecordDto>> GetFormRecordById(int id)
        {
            var formRecord = await _context.formrecords.FindAsync(id);

            if (formRecord == null)
            {
                return NotFound("Form record not found.");
            }

            var formRecordDto = _mapper.Map<FormRecordDto>(formRecord);

            return Ok(formRecordDto);
        }

        [HttpPost("FormRecord")]
        public async Task<ActionResult<FormRecordDto>> CreateFormRecord(FormRecordDto formRecordDto)
        {
            if (formRecordDto == null)
            {
                return BadRequest("Form record DTO is null");
            }

            var formRecord = _mapper.Map<FormRecord>(formRecordDto);

            _context.formrecords.Add(formRecord);
            await _context.SaveChangesAsync();

            var createdFormRecordDto = _mapper.Map<FormRecordDto>(formRecord);

            return CreatedAtAction(nameof(GetFormRecordById), new { id = formRecord.Id }, createdFormRecordDto); //{ id = createdFormRecordDto.Id }
        }

        [HttpPut("FormRecord/{id}")]
        public async Task<ActionResult<FormRecordDto>> UpdateFormRecord(int id, FormRecordDto formRecordDto)
        {
            if (formRecordDto == null)
            {
                return BadRequest("Form record DTO is null");
            }

            var formRecord = await _context.formrecords.FindAsync(id);

            if (formRecord == null)
            {
                return NotFound("Form record not found.");
            }

            _mapper.Map(formRecordDto, formRecord);

            _context.formrecords.Update(formRecord);
            await _context.SaveChangesAsync();

            var updatedFormRecordDto = _mapper.Map<FormRecordDto>(formRecord);

            return Ok(updatedFormRecordDto);
        }

        [HttpDelete("FormRecord/{id}")]
        public async Task<ActionResult> DeleteFormRecord(int id)
        {
            var formRecord = await _context.formrecords.FindAsync(id);

            if (formRecord == null)
            {
                return NotFound("Form record not found.");
            }

            _context.formrecords.Remove(formRecord);
            await _context.SaveChangesAsync();

            return NoContent();
        }




        [HttpGet("{formId}/records")]
    public async Task<ActionResult<IEnumerable<FormRecordDto>>> GetFormRecords(int formId)
    {
        var formRecords = await _context.formrecords.Where(fr => fr.FormId == formId).ToListAsync();
        var formRecordDtos = _mapper.Map<List<FormRecordDto>>(formRecords);

        return Ok(formRecordDtos);
    }

        //[HttpGet("Forms/{formId}/records/{id}", Name = "GetFormRecordByFormIdAndId")]
        [HttpGet("Forms/{formId}/records/{id}", Name = "GetFormRecordByFormIdAndRecordId")]
        public async Task<ActionResult<FormRecordDto>> GetFormRecordById(int formId, int id)
    {
        var formRecord = await _context.formrecords.FirstOrDefaultAsync(fr => fr.FormId == formId && fr.Id == id);

        if (formRecord == null)
        {
            return NotFound("Form record not found.");
        }

        var formRecordDto = _mapper.Map<FormRecordDto>(formRecord);

        return Ok(formRecordDto);
    }

    [HttpPost("{formId}/records")]
    public async Task<ActionResult<FormRecordDto>> CreateFormRecord(int formId, FormRecordDto formRecordDto)
    {
        if (formRecordDto == null)
        {
            return BadRequest("Form record DTO is null");
        }

        var form = await _context.forms.FindAsync(formId);
        if (form == null)
        {
            return NotFound("Form not found.");
        }

        var formRecord = _mapper.Map<FormRecord>(formRecordDto);
        formRecord.FormId = formId;

        _context.formrecords.Add(formRecord);
        await _context.SaveChangesAsync();

        var createdFormRecordDto = _mapper.Map<FormRecordDto>(formRecord);

        return CreatedAtAction(nameof(GetFormRecordById), new { formId, id = formRecord.Id }, createdFormRecordDto); //{ formId, id = createdFormRecordDto.Id }
        }

    [HttpPut("{formId}/records/{id}")]
    public async Task<ActionResult<FormRecordDto>> UpdateFormRecord(int formId, int id, FormRecordDto formRecordDto)
    {
        if (formRecordDto == null)
        {
            return BadRequest("Form record DTO is null");
        }

        var formRecord = await _context.formrecords.FirstOrDefaultAsync(fr => fr.FormId == formId && fr.Id == id);
        if (formRecord == null)
        {
            return NotFound("Form record not found.");
        }

        _mapper.Map(formRecordDto, formRecord);

        _context.formrecords.Update(formRecord);
        await _context.SaveChangesAsync();

        var updatedFormRecordDto = _mapper.Map<FormRecordDto>(formRecord);

        return Ok(updatedFormRecordDto);
    }

    [HttpDelete("{formId}/records/{id}")]
    public async Task<ActionResult> DeleteFormRecord(int formId, int id)
    {
        var formRecord = await _context.formrecords.FirstOrDefaultAsync(fr => fr.FormId == formId && fr.Id == id);

        if (formRecord == null)
        {
            return NotFound("Form record not found.");
        }

        _context.formrecords.Remove(formRecord);
        await _context.SaveChangesAsync();

        return NoContent();
    }
    }
}







//mannual instansation of dtos instead of using mapper 

//using AutoMapper;
//using FormAPI.Context;
//using FormAPI.DTOs;
//using FormAPI.Models;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace FormAPI.Controllers
//{
//    [Route("[controller]")]
//    [ApiController]
//    public class FormsController : ControllerBase
//    {
//        private readonly ApplicationDbContext _context;



//        public FormsController(ApplicationDbContext context)
//        {
//            _context = context;

//        }

//        [HttpPost("Form")]
//        public async Task<ActionResult<FormDto>> CreateForm(FormDto formDto)
//        {
//            if (formDto == null)
//            {
//                return BadRequest("Form DTO is null");
//            }

//            var form = new Form
//            {
//                Name = formDto.Name,
//                Description = formDto.Description,
//                Fields = formDto.Fields
//            };

//            _context.forms.Add(form);
//            await _context.SaveChangesAsync();

//            var createdFormDto = new FormDto
//            {
//                Id = form.Id,
//                Name = form.Name,
//                Description = form.Description,
//                Fields = form.Fields  // Map the Fields property
//            };

//            return CreatedAtAction(nameof(GetFormById), new { id = createdFormDto.Id }, createdFormDto);
//        }

//        [HttpGet("Forms")]
//        public async Task<ActionResult<IEnumerable<FormDto>>> GetForms()
//        {
//            var forms = await _context.forms.ToListAsync();
//            var formDtos = forms.Select(form => new FormDto
//            {
//                Id = form.Id,
//                Name = form.Name,
//                Description = form.Description
//            }).ToList();

//            return Ok(formDtos);
//        }

//        [HttpGet("Form/{id}", Name = "GetFormById")]
//        public async Task<ActionResult<FormDto>> GetFormById(int id)
//        {
//            var form = await _context.forms.FindAsync(id);

//            if (form == null)
//            {
//                return NotFound("Form not found.");
//            }

//            var formDto = new FormDto
//            {
//                Id = form.Id,
//                Name = form.Name,
//                Description = form.Description
//            };

//            return Ok(formDto);
//        }

//        [HttpPut("Form/{id}")]
//        public async Task<ActionResult<FormDto>> UpdateForm(int id, FormDto formDto)
//        {
//            if (formDto == null)
//            {
//                return BadRequest("Form DTO is null");
//            }

//            var form = await _context.forms.FindAsync(id);

//            if (form == null)
//            {
//                return NotFound("Form not found.");
//            }

//            form.Name = formDto.Name;
//            form.Description = formDto.Description;

//            _context.forms.Update(form);
//            await _context.SaveChangesAsync();

//            var updatedFormDto = new FormDto
//            {
//                Id = form.Id,
//                Name = form.Name,
//                Description = form.Description
//            };

//            return Ok(updatedFormDto);
//        }

//        [HttpDelete("Form/{id}")]
//        public async Task<ActionResult> DeleteForm(int id)
//        {
//            var form = await _context.forms.FindAsync(id);

//            if (form == null)
//            {
//                return NotFound("Form not found.");
//            }

//            _context.forms.Remove(form);
//            await _context.SaveChangesAsync();

//            return NoContent();
//        }

//        [HttpPost("FormField")]
//        public async Task<ActionResult<FormFieldDto>> CreateFormField(FormFieldDto formFieldDto)
//        {
//            if (formFieldDto == null)
//            {
//                return BadRequest("FormField DTO is null");
//            }

//            var formField = new FormField
//            {
//                Name = formFieldDto.Name,
//                Required = formFieldDto.Required,
//                Attributes = formFieldDto.Attributes,
//                Kind = formFieldDto.Kind,
//                FieldType = formFieldDto.FieldType,
//                Rules = formFieldDto.Rules
//            };

//            _context.formfields.Add(formField);
//            await _context.SaveChangesAsync();

//            formFieldDto.Id = formField.Id;

//            return CreatedAtAction(nameof(GetFormFieldById), new { id = formFieldDto.Id }, formFieldDto);
//        }

//        [HttpGet("FormFields")]
//        public async Task<ActionResult<IEnumerable<FormFieldDto>>> GetFormFields()
//        {
//            var formFields = await _context.formfields.ToListAsync();
//            var formFieldDtos = formFields.Select(field => new FormFieldDto
//            {
//                Id = field.Id,
//                Name = field.Name,
//                Required = field.Required,
//                Attributes = field.Attributes,
//                Kind = field.Kind,
//                FieldType = field.FieldType,
//                Rules = field.Rules
//            }).ToList();

//            return Ok(formFieldDtos);
//        }

//        [HttpGet("FormField/{id}", Name = "GetFormFieldById")]
//        public async Task<ActionResult<FormFieldDto>> GetFormFieldById(int id)
//        {
//            var formField = await _context.formfields.FindAsync(id);

//            if (formField == null)
//            {
//                return NotFound("FormField not found.");
//            }

//            var formFieldDto = new FormFieldDto
//            {
//                Id = formField.Id,
//                Name = formField.Name,
//                Required = formField.Required,
//                Attributes = formField.Attributes,
//                Kind = formField.Kind,
//                FieldType = formField.FieldType,
//                Rules = formField.Rules
//            };

//            return Ok(formFieldDto);
//        }

//        [HttpPut("FormField/{id}")]
//        public async Task<ActionResult<FormFieldDto>> UpdateFormField(int id, FormFieldDto formFieldDto)
//        {
//            if (formFieldDto == null)
//            {
//                return BadRequest("FormField DTO is null");
//            }

//            var formField = await _context.formfields.FindAsync(id);

//            if (formField == null)
//            {
//                return NotFound("FormField not found.");
//            }

//            formField.Name = formFieldDto.Name;
//            formField.Required = formFieldDto.Required;
//            formField.Attributes = formFieldDto.Attributes;
//            formField.Kind = formFieldDto.Kind;
//            formField.FieldType = formFieldDto.FieldType;
//            formField.Rules = formFieldDto.Rules;

//            _context.formfields.Update(formField);
//            await _context.SaveChangesAsync();

//            return Ok(formFieldDto);
//        }

//        [HttpDelete("FormField/{id}")]
//        public async Task<ActionResult> DeleteFormField(int id)
//        {
//            var formField = await _context.formfields.FindAsync(id);

//            if (formField == null)
//            {
//                return NotFound("FormField not found.");
//            }

//            _context.formfields.Remove(formField);
//            await _context.SaveChangesAsync();

//            return NoContent();
//        }

//        [HttpGet("FormRecords")]
//        public async Task<ActionResult<IEnumerable<FormRecordDto>>> GetFormRecords()
//        {
//            var formRecords = await _context.formrecords.ToListAsync();
//            var formRecordDtos = formRecords.Select(record => new FormRecordDto
//            {
//                Id = record.Id,
//                FirstName = record.FirstName,
//                SecondName = record.SecondName,
//                LastName = record.LastName,
//                Birthdate = record.Birthdate,
//                Gender = record.Gender,
//                LanguageCode = record.LanguageCode,
//                Nationality = record.Nationality,
//                PhoneNumber = record.PhoneNumber,
//                Email = record.Email,
//                Arrival = record.Arrival,
//                Departure = record.Departure,
//                Address = record.Address,
//                Zip = record.Zip,
//                City = record.City,
//                Country = record.Country,
//                //Kind = record.Kind,
//                //FieldType = record.FieldType,
//                //Attributes = record.Attributes
//            }).ToList();

//            return Ok(formRecordDtos);
//        }

//        [HttpGet("FormRecord/{id}", Name = "GetFormRecordById")]
//        public async Task<ActionResult<FormRecordDto>> GetFormRecordById(int id)
//        {
//            var formRecord = await _context.formrecords.FindAsync(id);

//            if (formRecord == null)
//            {
//                return NotFound("Form record not found.");
//            }

//            var formRecordDto = new FormRecordDto
//            {
//                Id = formRecord.Id,
//                FirstName = formRecord.FirstName,
//                SecondName = formRecord.SecondName,
//                LastName = formRecord.LastName,
//                Birthdate = formRecord.Birthdate,
//                Gender = formRecord.Gender,
//                LanguageCode = formRecord.LanguageCode,
//                Nationality = formRecord.Nationality,
//                PhoneNumber = formRecord.PhoneNumber,
//                Email = formRecord.Email,
//                Arrival = formRecord.Arrival,
//                Departure = formRecord.Departure,
//                Address = formRecord.Address,
//                Zip = formRecord.Zip,
//                City = formRecord.City,
//                Country = formRecord.Country,
//                //Kind = formRecord.Kind,
//                //FieldType = formRecord.FieldType,
//                //Attributes = formRecord.Attributes
//            };

//            return Ok(formRecordDto);
//        }

//        [HttpPost("FormRecord")]
//        public async Task<ActionResult<FormRecordDto>> CreateFormRecord(FormRecordDto formRecordDto)
//        {
//            if (formRecordDto == null)
//            {
//                return BadRequest("Form record DTO is null");
//            }

//            var formRecord = new FormRecord
//            {
//                FirstName = formRecordDto.FirstName,
//                SecondName = formRecordDto.SecondName,
//                LastName = formRecordDto.LastName,
//                Birthdate = formRecordDto.Birthdate,
//                Gender = formRecordDto.Gender,
//                LanguageCode = formRecordDto.LanguageCode,
//                Nationality = formRecordDto.Nationality,
//                PhoneNumber = formRecordDto.PhoneNumber,
//                Email = formRecordDto.Email,
//                Arrival = formRecordDto.Arrival,
//                Departure = formRecordDto.Departure,
//                Address = formRecordDto.Address,
//                Zip = formRecordDto.Zip,
//                City = formRecordDto.City,
//                Country = formRecordDto.Country,
//                //Kind = formRecordDto.Kind,
//                //FieldType = formRecordDto.FieldType,
//                //Attributes = formRecordDto.Attributes
//            };

//            _context.formrecords.Add(formRecord);
//            await _context.SaveChangesAsync();

//            var createdFormRecordDto = new FormRecordDto
//            {
//                Id = formRecord.Id,
//                FirstName = formRecord.FirstName,
//                SecondName = formRecord.SecondName,
//                LastName = formRecord.LastName,
//                Birthdate = formRecord.Birthdate,
//                Gender = formRecord.Gender,
//                LanguageCode = formRecord.LanguageCode,
//                Nationality = formRecord.Nationality,
//                PhoneNumber = formRecord.PhoneNumber,
//                Email = formRecord.Email,
//                Arrival = formRecord.Arrival,
//                Departure = formRecord.Departure,
//                Address = formRecord.Address,
//                Zip = formRecord.Zip,
//                City = formRecord.City,
//                Country = formRecord.Country,
//                //Kind = formRecord.Kind,
//                //FieldType = formRecord.FieldType,
//                //Attributes = formRecord.Attributes
//            };

//            return CreatedAtAction(nameof(GetFormRecordById), new { id = createdFormRecordDto.Id }, createdFormRecordDto);
//        }

//        [HttpPut("FormRecord/{id}")]
//        public async Task<ActionResult<FormRecordDto>> UpdateFormRecord(int id, FormRecordDto formRecordDto)
//        {
//            if (formRecordDto == null)
//            {
//                return BadRequest("Form record DTO is null");
//            }

//            var formRecord = await _context.formrecords.FindAsync(id);

//            if (formRecord == null)
//            {
//                return NotFound("Form record not found.");
//            }

//            formRecord.FirstName = formRecordDto.FirstName;
//            formRecord.SecondName = formRecordDto.SecondName;
//            formRecord.LastName = formRecordDto.LastName;
//            formRecord.Birthdate = formRecordDto.Birthdate;
//            formRecord.Gender = formRecordDto.Gender;
//            formRecord.LanguageCode = formRecordDto.LanguageCode;
//            formRecord.Nationality = formRecordDto.Nationality;
//            formRecord.PhoneNumber = formRecordDto.PhoneNumber;
//            formRecord.Email = formRecordDto.Email;
//            formRecord.Arrival = formRecordDto.Arrival;
//            formRecord.Departure = formRecordDto.Departure;
//            formRecord.Address = formRecordDto.Address;
//            formRecord.Zip = formRecordDto.Zip;
//            formRecord.City = formRecordDto.City;
//            formRecord.Country = formRecordDto.Country;
//            //formRecord.Kind = formRecordDto.Kind;
//            //formRecord.FieldType = formRecordDto.FieldType;
//            //formRecord.Attributes = formRecordDto.Attributes;

//            _context.formrecords.Update(formRecord);
//            await _context.SaveChangesAsync();

//            var updatedFormRecordDto = new FormRecordDto
//            {
//                Id = formRecord.Id,
//                FirstName = formRecord.FirstName,
//                SecondName = formRecord.SecondName,
//                LastName = formRecord.LastName,
//                Birthdate = formRecord.Birthdate,
//                Gender = formRecord.Gender,
//                LanguageCode = formRecord.LanguageCode,
//                Nationality = formRecord.Nationality,
//                PhoneNumber = formRecord.PhoneNumber,
//                Email = formRecord.Email,
//                Arrival = formRecord.Arrival,
//                Departure = formRecord.Departure,
//                Address = formRecord.Address,
//                Zip = formRecord.Zip,
//                City = formRecord.City,
//                Country = formRecord.Country,
//                //Kind = formRecord.Kind,
//                //FieldType = formRecord.FieldType,
//                //Attributes = formRecord.Attributes
//            };

//            return Ok(updatedFormRecordDto);
//        }

//        [HttpDelete("FormRecord/{id}")]
//        public async Task<ActionResult> DeleteFormRecord(int id)
//        {
//            var formRecord = await _context.formrecords.FindAsync(id);

//            if (formRecord == null)
//            {
//                return NotFound("Form record not found.");
//            }

//            _context.formrecords.Remove(formRecord);
//            await _context.SaveChangesAsync();

//            return NoContent();
//        }
//    }
//}








//using FormAPI.Context;
//using FormAPI.DTOs;
//using FormAPI.Models;
//using FormAPI.Repositories;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.Configuration;
//using AutoMapper;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace FormAPI.Controllers
//{
//    [Route("[controller]")]
//    [ApiController]
//    public class FormsController : ControllerBase
//    {
//        private readonly IFormRepository _formRepository;
//        private readonly IMapper _mapper;

//        public FormsController(IFormRepository formRepository, IMapper mapper)
//        {
//            _formRepository = formRepository;
//            _mapper = mapper;
//        }

//        [HttpPost("Form")]
//        public async Task<ActionResult<FormDto>> CreateForm(FormDto formDto)
//        {
//            if (formDto == null)
//            {
//                return BadRequest("Form DTO is null");
//            }

//            if (!ModelState.IsValid)
//            {
//                return BadRequest(ModelState);
//            }

//            try
//            {
//                var form = _mapper.Map<Form>(formDto); // Map from DTO to entity
//                var createdForm = await _formRepository.CreateFormAsync(form);

//                var createdFormDto = _mapper.Map<FormDto>(createdForm); // Map from entity to DTO
//                return CreatedAtAction(nameof(GetFormById), new { id = createdForm.Id }, createdFormDto); // Return the location of the created resource
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(500, $"An error occurred: {ex.Message}");
//            }
//        }

//        [HttpGet("Forms")]
//        public async Task<ActionResult<IEnumerable<FormDto>>> GetForms()
//        {
//            try
//            {
//                var forms = await _formRepository.GetAllFormsAsync();
//                var formDtos = forms.Select(form => _mapper.Map<FormDto>(form)).ToList();
//                return Ok(formDtos);
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(500, $"An error occurred: {ex.Message}");
//            }
//        }

//        [HttpGet("Form/{id}")]
//        public async Task<ActionResult<FormDto>> GetFormById(int id)
//        {
//            try
//            {
//                var form = await _formRepository.GetFormByIdAsync(id);

//                if (form == null)
//                {
//                    return NotFound("Form not found.");
//                }

//                var formDto = _mapper.Map<FormDto>(form);
//                return Ok(formDto);
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(500, $"An error occurred: {ex.Message}");
//            }
//        }

//        [HttpPut("Form/{id}")]
//        public async Task<ActionResult<FormDto>> UpdateForm(int id, FormDto formDto)
//        {
//            if (formDto == null)
//            {
//                return BadRequest("Form DTO is null");
//            }

//            if (!ModelState.IsValid)
//            {
//                return BadRequest(ModelState);
//            }

//            try
//            {
//                var form = _mapper.Map<Form>(formDto); // Map from DTO to entity
//                form.Id = id; // Ensure the ID is set for the entity
//                var updatedForm = await _formRepository.UpdateFormAsync(form);

//                if (updatedForm == null)
//                {
//                    return NotFound("Form not found.");
//                }

//                var updatedFormDto = _mapper.Map<FormDto>(updatedForm); // Map from entity to DTO
//                return Ok(updatedFormDto);
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(500, $"An error occurred: {ex.Message}");
//            }
//        }

//        [HttpDelete("{id}")]
//        public async Task<ActionResult> DeleteForm(int id)
//        {
//            try
//            {
//                await _formRepository.DeleteFormAsync(id);
//                return NoContent();
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(500, $"An error occurred: {ex.Message}");
//            }
//        }

//        [HttpGet("FormFields")]
//        public async Task<ActionResult<IEnumerable<FormFieldDto>>> GetFormFields()
//        {
//            try
//            {
//                var formFields = await _formRepository.GetAllFormFieldsAsync();
//                var formFieldDtos = formFields.Select(field => _mapper.Map<FormFieldDto>(field)).ToList();
//                return Ok(formFieldDtos);
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(500, $"An error occurred: {ex.Message}");
//            }
//        }

//        [HttpPost("FormField")]
//        public async Task<ActionResult<FormFieldDto>> CreateFormField(FormFieldDto formFieldDto)
//        {
//            if (formFieldDto == null)
//            {
//                return BadRequest("Form field DTO is null");
//            }

//            if (!ModelState.IsValid)
//            {
//                return BadRequest(ModelState);
//            }

//            try
//            {
//                var formField = _mapper.Map<FormField>(formFieldDto);
//                var createdFormField = await _formRepository.CreateFormFieldAsync(formField);

//                var createdFormFieldDto = _mapper.Map<FormFieldDto>(createdFormField);
//                return CreatedAtAction(nameof(GetFormFields), new { id = createdFormField.Id }, createdFormFieldDto);
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(500, $"An error occurred: {ex.Message}");
//            }
//        }

//        [HttpDelete("formfield/{id}")]
//        public async Task<ActionResult> DeleteFormField(int id)
//        {
//            try
//            {
//                var success = await _formRepository.DeleteFormFieldAsync(id);

//                if (!success)
//                {
//                    return NotFound("Form field not found.");
//                }

//                return NoContent();
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(500, $"An error occurred: {ex.Message}");
//            }
//        }

//        [HttpGet("FormRecords")]
//        public async Task<ActionResult<IEnumerable<FormRecordDto>>> GetFormRecords()
//        {
//            try
//            {
//                var formRecords = await _formRepository.GetAllFormRecordsAsync();
//                var formRecordDtos = formRecords.Select(record => _mapper.Map<FormRecordDto>(record)).ToList();
//                return Ok(formRecordDtos);
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(500, $"An error occurred: {ex.Message}");
//            }
//        }

//        [HttpGet("FormRecord/{id}")]
//        public async Task<ActionResult<FormRecordDto>> GetFormRecordById(int id)
//        {
//            try
//            {
//                var formRecord = await _formRepository.GetFormRecordByIdAsync(id);

//                if (formRecord == null)
//                {
//                    return NotFound("Form record not found.");
//                }

//                var formRecordDto = _mapper.Map<FormRecordDto>(formRecord);
//                return Ok(formRecordDto);
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(500, $"An error occurred: {ex.Message}");
//            }
//        }

//        [HttpPost("FormRecord")]
//        public async Task<ActionResult<FormRecordDto>> CreateFormRecord(FormRecordDto formRecordDto)
//        {
//            if (formRecordDto == null)
//            {
//                return BadRequest("Form record DTO is null");
//            }

//            if (!ModelState.IsValid)
//            {
//                return BadRequest(ModelState);
//            }

//            try
//            {
//                var formRecord = _mapper.Map<FormRecord>(formRecordDto);
//                var createdFormRecord = await _formRepository.CreateFormRecordAsync(formRecord);

//                var createdFormRecordDto = _mapper.Map<FormRecordDto>(createdFormRecord);
//                return CreatedAtAction(nameof(GetFormRecordById), new { id = createdFormRecord.Id }, createdFormRecordDto);
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(500, $"An error occurred: {ex.Message}");
//            }
//        }

//        [HttpPut("FormRecord/{id}")]
//        public async Task<ActionResult<FormRecordDto>> UpdateFormRecord(int id, FormRecordDto formRecordDto)
//        {
//            if (formRecordDto == null)
//            {
//                return BadRequest("Form record DTO is null");
//            }

//            if (!ModelState.IsValid)
//            {
//                return BadRequest(ModelState);
//            }

//            try
//            {
//                var formRecord = _mapper.Map<FormRecord>(formRecordDto);
//                formRecord.Id = id;
//                var updatedFormRecord = await _formRepository.UpdateFormRecordAsync(formRecord);

//                if (updatedFormRecord == null)
//                {
//                    return NotFound("Form record not found.");
//                }

//                var updatedFormRecordDto = _mapper.Map<FormRecordDto>(updatedFormRecord);
//                return Ok(updatedFormRecordDto);
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(500, $"An error occurred: {ex.Message}");
//            }
//        }

//        [HttpDelete("FormRecord/{id}")]
//        public async Task<ActionResult> DeleteFormRecord(int id)
//        {
//            try
//            {
//                var success = await _formRepository.DeleteFormRecordAsync(id);

//                if (!success)
//                {
//                    return NotFound("Form record not found.");
//                }

//                return NoContent();
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(500, $"An error occurred: {ex.Message}");
//            }
//        }
//    }
//}













//using AutoMapper;
//using FormAPI.Context;
//using FormAPI.DTOs;
//using FormAPI.Models;
//using FormAPI.Repositories;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.Configuration;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace FormAPI.Controllers
//{
//    [Route("[controller]")]
//    [ApiController]
//    public class FormsController : ControllerBase
//    {
//        private readonly string _connectionString;
//        private readonly IMapper _mapper;
//        private readonly ApplicationDbContext _dbContext;

//        public FormsController(IConfiguration configuration, ApplicationDbContext dbContext)
//        {
//            _connectionString = configuration.GetConnectionString("DefaultConnection");
//            _dbContext = dbContext;

//            // Manually configure AutoMapper
//            var mapperConfig = new MapperConfiguration(cfg =>
//            {
//                cfg.CreateMap<FormDto, Form>();
//                cfg.CreateMap<Form, FormDto>();
//                cfg.CreateMap<FormFieldDto, FormField>();
//                cfg.CreateMap<FormField, FormFieldDto>();
//                cfg.CreateMap<FormRecordDto, FormRecord>();
//                cfg.CreateMap<FormRecord, FormRecordDto>();
//            });
//            _mapper = new Mapper(mapperConfig);
//        }

//        [HttpPost("Form")]
//        public async Task<ActionResult<FormDto>> CreateForm(FormDto formDto)
//        {
//            if (formDto == null)
//            {
//                return BadRequest("Form DTO is null");
//            }

//            if (!ModelState.IsValid)
//            {
//                return BadRequest(ModelState);
//            }

//            try
//            {
//                var form = _mapper.Map<Form>(formDto); // Map from DTO to entity
//                var formRepository = new FormRepository(_connectionString); // Manually instantiate FormRepository
//                var createdForm = await formRepository.CreateFormAsync(form);

//                var createdFormDto = _mapper.Map<FormDto>(createdForm); // Map from entity to DTO
//                return CreatedAtAction(nameof(GetFormById), new { id = createdForm.Id }, createdFormDto); // Return the location of the created resource
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(500, $"An error occurred: {ex.Message}");
//            }
//        }

//        [HttpGet("Forms")]
//        public async Task<ActionResult<IEnumerable<FormDto>>> GetForms()
//        {
//            var formRepository = new FormRepository(_connectionString); // Manually instantiate FormRepository

//            try
//            {
//                var forms = await formRepository.GetAllFormsAsync();
//                var formDtos = forms.Select(form => _mapper.Map<FormDto>(form)).ToList();
//                return Ok(formDtos);
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(500, $"An error occurred: {ex.Message}");
//            }
//        }

//        [HttpGet("Form/{id}")]
//        public async Task<ActionResult<FormDto>> GetFormById(int id)
//        {
//            var formRepository = new FormRepository(_connectionString); // Manually instantiate FormRepository

//            try
//            {
//                var form = await formRepository.GetFormByIdAsync(id);

//                if (form == null)
//                {
//                    return NotFound("Form not found.");
//                }

//                var formDto = _mapper.Map<FormDto>(form);
//                return Ok(formDto);
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(500, $"An error occurred: {ex.Message}");
//            }
//        }

//        [HttpPut("Form/{id}")]
//        public async Task<ActionResult<FormDto>> UpdateForm(int id, FormDto formDto)
//        {
//            if (formDto == null)
//            {
//                return BadRequest("Form DTO is null");
//            }

//            if (!ModelState.IsValid)
//            {
//                return BadRequest(ModelState);
//            }

//            try
//            {
//                var form = _mapper.Map<Form>(formDto); // Map from DTO to entity
//                form.Id = id; // Ensure the ID is set for the entity
//                var formRepository = new FormRepository(_connectionString); // Manually instantiate FormRepository
//                var updatedForm = await formRepository.UpdateFormAsync(form);

//                if (updatedForm == null)
//                {
//                    return NotFound("Form not found.");
//                }

//                var updatedFormDto = _mapper.Map<FormDto>(updatedForm); // Map from entity to DTO
//                return Ok(updatedFormDto);
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(500, $"An error occurred: {ex.Message}");
//            }
//        }


//        // DELETE: api/forms/{id}
//        [HttpDelete("{id}")]
//        public async Task<ActionResult> DeleteForm(int id)
//        {
//            var formRepository = new FormRepository(_connectionString);

//            try
//            {
//                await formRepository.DeleteFormAsync(id);
//                return NoContent();
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(500, $"An error occurred: {ex.Message}");
//            }
//        }

//        [HttpGet("FormFields")]
//        public async Task<ActionResult<IEnumerable<FormFieldDto>>> GetFormFields()
//        {
//            var formRepository = new FormRepository(_connectionString);

//            try
//            {
//                var formFields = await formRepository.GetAllFormFieldsAsync();
//                var formFieldDtos = formFields.Select(field => _mapper.Map<FormFieldDto>(field)).ToList();
//                return Ok(formFieldDtos);
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(500, $"An error occurred: {ex.Message}");
//            }
//        }
//        [HttpPost("FormField")]
//        public async Task<ActionResult<FormFieldDto>> CreateFormField(FormFieldDto formFieldDto)
//        {
//            if (formFieldDto == null)
//            {
//                return BadRequest("Form field DTO is null");
//            }

//            if (!ModelState.IsValid)
//            {
//                return BadRequest(ModelState);
//            }

//            try
//            {
//                var formField = _mapper.Map<FormField>(formFieldDto);
//                var formRepository = new FormRepository(_connectionString);
//                var createdFormField = await formRepository.CreateFormFieldAsync(formField);

//                var createdFormFieldDto = _mapper.Map<FormFieldDto>(createdFormField);
//                return CreatedAtAction(nameof(GetFormFields), new { id = createdFormField.Id }, createdFormFieldDto);
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(500, $"An error occurred: {ex.Message}");
//            }
//        }
//        // DELETE: api/forms/formfield/{id}
//        [HttpDelete("formfield/{id}")]
//        public async Task<ActionResult> DeleteFormField(int id)
//        {
//            var formRepository = new FormRepository(_connectionString);

//            try
//            {
//                var success = await formRepository.DeleteFormFieldAsync(id);

//                if (!success)
//                {
//                    return NotFound("Form field not found.");
//                }

//                return NoContent();
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(500, $"An error occurred: {ex.Message}");
//            }
//        }

//        [HttpGet("FormRecords")]
//        public async Task<ActionResult<IEnumerable<FormRecordDto>>> GetFormRecords()
//        {
//            var formRepository = new FormRepository(_connectionString);

//            try
//            {
//                var formRecords = await formRepository.GetAllFormRecordsAsync();
//                var formRecordDtos = formRecords.Select(record => _mapper.Map<FormRecordDto>(record)).ToList();
//                return Ok(formRecordDtos);
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(500, $"An error occurred: {ex.Message}");
//            }
//        }

//        [HttpGet("FormRecord/{id}")]
//        public async Task<ActionResult<FormRecordDto>> GetFormRecordById(int id)
//        {
//            var formRepository = new FormRepository(_connectionString);

//            try
//            {
//                var formRecord = await formRepository.GetFormRecordByIdAsync(id);

//                if (formRecord == null)
//                {
//                    return NotFound("Form record not found.");
//                }

//                var formRecordDto = _mapper.Map<FormRecordDto>(formRecord);
//                return Ok(formRecordDto);
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(500, $"An error occurred: {ex.Message}");
//            }
//        }

//        [HttpPost("FormRecord")]
//        public async Task<ActionResult<FormRecordDto>> CreateFormRecord(FormRecordDto formRecordDto)
//        {
//            if (formRecordDto == null)
//            {
//                return BadRequest("Form record DTO is null");
//            }

//            if (!ModelState.IsValid)
//            {
//                return BadRequest(ModelState);
//            }

//            try
//            {
//                var formRecord = _mapper.Map<FormRecord>(formRecordDto);
//                var formRepository = new FormRepository(_connectionString);
//                var createdFormRecord = await formRepository.CreateFormRecordAsync(formRecord);

//                var createdFormRecordDto = _mapper.Map<FormRecordDto>(createdFormRecord);
//                return CreatedAtAction(nameof(GetFormRecordById), new { id = createdFormRecord.Id }, createdFormRecordDto);
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(500, $"An error occurred: {ex.Message}");
//            }
//        }

//        [HttpPut("FormRecord/{id}")]
//        public async Task<ActionResult<FormRecordDto>> UpdateFormRecord(int id, FormRecordDto formRecordDto)
//        {
//            if (formRecordDto == null)
//            {
//                return BadRequest("Form record DTO is null");
//            }

//            if (!ModelState.IsValid)
//            {
//                return BadRequest(ModelState);
//            }

//            try
//            {
//                var formRecord = _mapper.Map<FormRecord>(formRecordDto);
//                formRecord.Id = id;
//                var formRepository = new FormRepository(_connectionString);
//                var updatedFormRecord = await formRepository.UpdateFormRecordAsync(formRecord);

//                if (updatedFormRecord == null)
//                {
//                    return NotFound("Form record not found.");
//                }

//                var updatedFormRecordDto = _mapper.Map<FormRecordDto>(updatedFormRecord);
//                return Ok(updatedFormRecordDto);
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(500, $"An error occurred: {ex.Message}");
//            }
//        }

//        [HttpDelete("FormRecord/{id}")]
//        public async Task<ActionResult> DeleteFormRecord(int id)
//        {
//            var formRepository = new FormRepository(_connectionString);

//            try
//            {
//                var success = await formRepository.DeleteFormRecordAsync(id);

//                if (!success)
//                {
//                    return NotFound("Form record not found.");
//                }

//                return NoContent();
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(500, $"An error occurred: {ex.Message}");
//            }
//        }
//    }
//}
















//using FormAPI.Models;
//using FormAPI.DTOs;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.Configuration;
//using System;
//using System.Collections.Generic;
//using System.Threading.Tasks;
//using FormAPI.Repositories;
//using FormAPI.Context;
//using Microsoft.EntityFrameworkCore;
//using AutoMapper;

//namespace FormAPI.Controllers
//{
//    [Route("[controller]")]
//    [ApiController]
//    public class FormsController : ControllerBase
//    {
//        private readonly string _connectionString;
//        private readonly IMapper _mapper;
//        private readonly ApplicationDbContext _dbContext;

//        public FormsController(IConfiguration configuration, ApplicationDbContext dbContext)
//        {
//            _connectionString = configuration.GetConnectionString("DefaultConnection");
//            _dbContext = dbContext;

//            // Create MapperConfiguration
//            var config = new MapperConfiguration(cfg =>
//            {
//                cfg.CreateMap<Form, FormDto>().ForMember(dest => dest.Id, opt => opt.Ignore()); // Ignore mapping Id
//                cfg.CreateMap<FormField, FormFieldDto>(); // Map FormField to FormFieldDto
//                cfg.CreateMap<FormRecord, FormRecordDto>();
//            });

//            // Create IMapper
//            _mapper = config.CreateMapper();
//        }
//        [HttpPost("Form")]
//        public async Task<ActionResult<FormDto>> CreateForm(FormDto formDto)
//        {
//            if (formDto == null)
//            {
//                return BadRequest("Form DTO is null");
//            }

//            if (!ModelState.IsValid)
//            {
//                return BadRequest(ModelState);
//            }

//            try
//            {
//                var form = _mapper.Map<Form>(formDto); // Map from DTO to entity
//                var formRepository = new FormRepository(_connectionString);
//                var createdForm = await formRepository.CreateFormAsync(form);

//                var createdFormDto = _mapper.Map<FormDto>(createdForm); // Map from entity to DTO
//                createdFormDto.Id = null; // Remove Id from response
//                return CreatedAtAction(nameof(GetFormById), new { id = createdForm.Id }, createdFormDto); // Return the location of the created resource
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(500, $"An error occurred: {ex.Message}");
//            }
//        }




//        [HttpGet("Forms")]
//        public async Task<ActionResult<IEnumerable<FormDto>>> GetForms()
//        {
//            var formRepository = new FormRepository(_connectionString);

//            try
//            {
//                var forms = await formRepository.GetAllFormsAsync();
//                var formDtos = new List<FormDto>();
//                foreach (var form in forms)
//                {
//                    var formDto = _mapper.Map<FormDto>(form);
//                    formDto.Id = null; // Remove Id from response
//                    formDtos.Add(formDto);
//                }
//                return Ok(formDtos);
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(500, $"An error occurred: {ex.Message}");
//            }
//        }




//        [HttpGet("Form/{id}")]
//        public async Task<ActionResult<FormDto>> GetFormById(int id)
//        {
//            var formRepository = new FormRepository(_connectionString);

//            try
//            {
//                var form = await formRepository.GetFormByIdAsync(id);

//                if (form == null)
//                {
//                    return NotFound("Form not found.");
//                }

//                var formDto = _mapper.Map<FormDto>(form);
//                formDto.Id = null; // Remove Id from response
//                return Ok(formDto);
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(500, $"An error occurred: {ex.Message}");
//            }
//        }



//        [HttpPut("Form/{id}")]
//        public async Task<ActionResult<FormDto>> UpdateForm(int id, FormDto formDto)
//        {
//            if (formDto == null)
//            {
//                return BadRequest("Form DTO is null");
//            }

//            if (!ModelState.IsValid)
//            {
//                return BadRequest(ModelState);
//            }

//            try
//            {
//                var form = _mapper.Map<Form>(formDto); // Map from DTO to entity
//                form.Id = id; // Ensure the ID is set for the entity
//                var formRepository = new FormRepository(_connectionString);
//                var updatedForm = await formRepository.UpdateFormAsync(form);

//                if (updatedForm == null)
//                {
//                    return NotFound("Form not found.");
//                }

//                var updatedFormDto = _mapper.Map<FormDto>(updatedForm); // Map from entity to DTO
//                updatedFormDto.Id = null; // Remove Id from response
//                return Ok(updatedFormDto);
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(500, $"An error occurred: {ex.Message}");
//            }
//        }

//        [HttpDelete("api/Forms/{id}")]
//        public async Task<ActionResult> DeleteForm(int id)
//        {
//            // Validate the ID if needed

//            try
//            {
//                string connectionString = "your_connection_string_here";
//                var formRepository = new FormRepository(connectionString);

//                await formRepository.DeleteFormAsync(id);
//                return Ok("Form deleted successfully.");
//            }
//            catch (Exception ex)
//            {
//                // Error handling
//                return StatusCode(500, $"An error occurred: {ex.Message}");
//            }
//        }



//        [HttpGet("Fields")]
//        public async Task<ActionResult<IEnumerable<FormFieldDto>>> GetFormFields()
//        {
//            // Manually instantiate FormRepository
//            var formRepository = new FormRepository(_connectionString);

//            try
//            {
//                var formFields = await formRepository.GetAllFormFieldsAsync();

//                // Map the formFields to FormFieldDto
//                var formFieldDtos = _mapper.Map<IEnumerable<FormFieldDto>>(formFields);

//                return Ok(formFieldDtos);
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(500, $"An error occurred: {ex.Message}");
//            }
//        }
//        [HttpPost("Field")]
//        public async Task<ActionResult<FormFieldDto>> CreateFormField(FormFieldDto formFieldDto)
//        {
//            if (formFieldDto == null)
//            {
//                return BadRequest("FormField DTO is null");
//            }

//            if (!ModelState.IsValid)
//            {
//                return BadRequest(ModelState);
//            }

//            try
//            {
//                var formField = _mapper.Map<FormField>(formFieldDto); // Map from DTO to entity
//                var formRepository = new FormRepository(_connectionString);
//                var createdFormField = await formRepository.CreateFormField(formField);

//                var createdFormFieldDto = _mapper.Map<FormFieldDto>(createdFormField); // Map from entity to DTO
//                return CreatedAtAction(nameof(GetFormFields), new { id = createdFormField.Id }, createdFormFieldDto); // Return the location of the created resource
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(500, $"An error occurred: {ex.Message}");
//            }
//        }




//        [HttpGet("Records")]
//        public async Task<ActionResult<IEnumerable<FormRecordDto>>> GetFormRecords()
//        {
//            var formRepository = new FormRepository(_connectionString);

//            try
//            {
//                var formRecords = await formRepository.GetAllFormRecordsAsync();
//                var formRecordDtos = formRecords.Select(record => _mapper.Map<FormRecordDto>(record)).ToList();
//                return Ok(formRecordDtos);
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(500, $"An error occurred: {ex.Message}");
//            }
//        }

//        [HttpPost("Record")]
//        public async Task<ActionResult<FormRecordDto>> CreateFormRecord(FormRecordDto formRecordDto)
//        {
//            if (formRecordDto == null)
//            {
//                return BadRequest("FormRecord DTO is null");
//            }

//            if (!ModelState.IsValid)
//            {
//                return BadRequest(ModelState);
//            }

//            try
//            {
//                var formRecord = _mapper.Map<FormRecord>(formRecordDto); // Map from DTO to entity
//                var formRepository = new FormRepository(_connectionString);
//                var createdFormRecord = await formRepository.CreateFormRecordAsync(formRecord);

//                var createdFormRecordDto = _mapper.Map<FormRecordDto>(createdFormRecord); // Map from entity to DTO
//                return CreatedAtAction(nameof(GetFormRecords), new { id = createdFormRecord.Id }, createdFormRecordDto); // Return the location of the created resource
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(500, $"An error occurred: {ex.Message}");
//            }
//        }

//        //[HttpGet("Fields")]
//        //public async Task<ActionResult<IEnumerable<FormFieldDto>>> GetFormFields()
//        //{
//        //    var formRepository = new FormRepository(_connectionString);

//        //    try
//        //    {
//        //        var formFields = await formRepository.GetAllFormFields();
//        //        var formFieldDtos = formFields.Select(field => _mapper.Map<FormFieldDto>(field)).ToList();
//        //        return Ok(formFieldDtos);
//        //    }
//        //    catch (Exception ex)
//        //    {
//        //        return StatusCode(500, $"An error occurred: {ex.Message}");
//        //    }
//        //}
//        [HttpGet("Record/{id}")]
//        public async Task<ActionResult<FormRecord>> GetFormRecord(int id)
//        {
//            // Manually instantiate FormRepository
//            var formRepository = new FormRepository(_connectionString);

//            try
//            {
//                var formRecord = await formRepository.GetByIdAsync(id);
//                if (formRecord == null)
//                {
//                    return NotFound("FormRecord not found.");
//                }
//                return Ok(formRecord);
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(500, $"An error occurred: {ex.Message}");
//            }
//        }
//        //[HttpPost("Record")]
//        //public async Task<ActionResult<FormRecord>> CreateFormRecord(FormRecord formRecord)
//        //{
//        //    // Manually instantiate FormRepository
//        //    var formRepository = new FormRepository(_connectionString);

//        //    try
//        //    {
//        //        if (formRecord == null)
//        //        {
//        //            return BadRequest("FormRecord object is null");
//        //        }

//        //        // Perform additional validation if needed
//        //        if (!ModelState.IsValid)
//        //        {
//        //            return BadRequest(ModelState);
//        //        }

//        //        // Create the form record
//        //        var createdFormRecord = await formRepository.CreateFormRecordAsync(formRecord);

//        //        // Check if the record was created successfully
//        //        if (createdFormRecord == null)
//        //        {
//        //            return StatusCode(500, "Failed to create the form record");
//        //        }

//        //        // Return the created form record along with a route to retrieve it
//        //        return CreatedAtAction(nameof(GetFormRecord), new { id = createdFormRecord.Id }, createdFormRecord);
//        //    }
//        //    catch (Exception ex)
//        //    {
//        //        return StatusCode(500, $"An error occurred: {ex.Message}");
//        //    }
//        //}
//        [HttpPut("Record/{id}")]
//        public async Task<ActionResult<FormRecord>> UpdateFormRecord(int id, FormRecord formRecord)
//        {
//            var formRepository = new FormRepository(_connectionString);

//            try
//            {
//                if (formRecord == null || formRecord.Id != id)
//                {
//                    return BadRequest("Invalid request");
//                }

//                var updatedFormRecord = await formRepository.UpdateFormRecordAsync(formRecord);

//                if (updatedFormRecord == null)
//                {
//                    return NotFound("FormRecord not found.");
//                }

//                return Ok(updatedFormRecord);
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(500, $"An error occurred: {ex.Message}");
//            }
//        }

//        [HttpDelete("Record/{id}")]
//        public async Task<ActionResult<bool>> DeleteFormRecord(int id)
//        {
//            var formRepository = new FormRepository(_connectionString);

//            try
//            {
//                var isDeleted = await formRepository.DeleteFormRecordAsync(id);

//                if (!isDeleted)
//                {
//                    return NotFound("FormRecord not found.");
//                }

//                return Ok(true);
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(500, $"An error occurred: {ex.Message}");
//            }
//        }
//    }
//}























//correct code for manual but returns id



//using FormAPI.Models;
//using Microsoft.AspNetCore.Cors.Infrastructure;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using FormAPI.Repositories;
//using Newtonsoft.Json;
//using FormAPI.Service;
//using FormAPI.Repositories;


//namespace FormAPI.Controllers
//{
//    [Route("api/[controller]")]
//    //[Route("api/form/formrecord")]
//    [ApiController]
//    public class FormsController : ControllerBase
//    {
//        private readonly string _connectionString;

//        public FormsController(IConfiguration configuration)
//        {
//            _connectionString = configuration.GetConnectionString("DefaultConnection");
//        }

//        [HttpGet]
//        public async Task<ActionResult<IEnumerable<Form>>> GetForms()
//        {
//            var formRepository = new FormRepository(_connectionString);

//            try
//            {
//                var forms = await formRepository.GetAllFormsAsync();
//                return Ok(forms);
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(500, $"An error occurred: {ex.Message}");
//            }
//        }

//        [HttpPost]
//        public async Task<ActionResult<Form>> CreateForm(Form form)
//        {
//            var formRepository = new FormRepository(_connectionString);

//            try
//            {
//                if (form == null)
//                {
//                    return BadRequest("Form object is null");
//                }

//                if (!ModelState.IsValid)
//                {
//                    return BadRequest(ModelState);
//                }

//                var createdForm = await formRepository.CreateFormAsync(form);

//                if (createdForm == null)
//                {
//                    return StatusCode(500, "Failed to create the form");
//                }

//                return Ok(createdForm); // Return the created form details
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(500, $"An error occurred: {ex.Message}");
//            }
//        }


//[HttpDelete("api/Forms/{id}")]
//public async Task<ActionResult<bool>> DeleteForm(int id)
//{
//    var formRepository = new FormRepository(_connectionString);

//    try
//    {
//        var isDeleted = await formRepository.DeleteFormAsync(id);

//        if (!isDeleted)
//        {
//            return NotFound("Form not found.");
//        }

//        return Ok(true);
//    }
//    catch (Exception ex)
//    {
//        return StatusCode(500, $"An error occurred: {ex.Message}");
//    }
//}

//        [HttpPut("{id}")]
//        public async Task<ActionResult<Form>> UpdateForm(int id, Form form)
//        {
//            var formRepository = new FormRepository(_connectionString);

//            try
//            {
//                if (form == null || form.Id != id)
//                {
//                    return BadRequest("Invalid request");
//                }

//                var updatedForm = await formRepository.UpdateFormAsync(form);

//                if (updatedForm == null)
//                {
//                    return NotFound("Form not found.");
//                }

//                return Ok(updatedForm);
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(500, $"An error occurred: {ex.Message}");
//            }
//        }

//[HttpGet("Fields")]
//public async Task<ActionResult<IEnumerable<FormField>>> GetFormFields()
//{
//    // Manually instantiate FormRepository
//    var formRepository = new FormRepository(_connectionString);

//    try
//    {
//        var formFields = await formRepository.GetAllFormFields();
//        return Ok(formFields);
//    }
//    catch (Exception ex)
//    {
//        return StatusCode(500, $"An error occurred: {ex.Message}");
//    }
//}

//[HttpGet("Records")]
//public async Task<ActionResult<IEnumerable<FormRecord>>> GetFormRecords()
//{
//    // Manually instantiate FormRepository
//    var formRepository = new FormRepository(_connectionString);

//    try
//    {
//        var formRecords = await formRepository.GetAllFormRecordsAsync();
//        return Ok(formRecords);
//    }
//    catch (Exception ex)
//    {
//        return StatusCode(500, $"An error occurred: {ex.Message}");
//    }
//}
//[HttpGet("{id}")]
//public async Task<ActionResult<FormRecord>> GetFormRecord(int id)
//{
//    // Manually instantiate FormRepository
//    var formRepository = new FormRepository(_connectionString);

//    try
//    {
//        var formRecord = await formRepository.GetByIdAsync(id);
//        if (formRecord == null)
//        {
//            return NotFound("FormRecord not found.");
//        }
//        return Ok(formRecord);
//    }
//    catch (Exception ex)
//    {
//        return StatusCode(500, $"An error occurred: {ex.Message}");
//    }
//}
//[HttpPost("Record")]
//public async Task<ActionResult<FormRecord>> CreateFormRecord(FormRecord formRecord)
//{
//    // Manually instantiate FormRepository
//    var formRepository = new FormRepository(_connectionString);

//    try
//    {
//        if (formRecord == null)
//        {
//            return BadRequest("FormRecord object is null");
//        }

//        // Perform additional validation if needed
//        if (!ModelState.IsValid)
//        {
//            return BadRequest(ModelState);
//        }

//        // Create the form record
//        var createdFormRecord = await formRepository.CreateFormRecordAsync(formRecord);

//        // Check if the record was created successfully
//        if (createdFormRecord == null)
//        {
//            return StatusCode(500, "Failed to create the form record");
//        }

//        // Return the created form record along with a route to retrieve it
//        return CreatedAtAction(nameof(GetFormRecord), new { id = createdFormRecord.Id }, createdFormRecord);
//    }
//    catch (Exception ex)
//    {
//        return StatusCode(500, $"An error occurred: {ex.Message}");
//    }
//}

//[HttpDelete("api/Forms/Record/{id}")]
//public async Task<ActionResult<FormRecord>> UpdateFormRecord(int id, FormRecord formRecord)
//{
//    var formRepository = new FormRepository(_connectionString);

//    try
//    {
//        if (formRecord == null || formRecord.Id != id)
//        {
//            return BadRequest("Invalid request");
//        }

//        var updatedFormRecord = await formRepository.UpdateFormRecordAsync(formRecord);

//        if (updatedFormRecord == null)
//        {
//            return NotFound("FormRecord not found.");
//        }

//        return Ok(updatedFormRecord);
//    }
//    catch (Exception ex)
//    {
//        return StatusCode(500, $"An error occurred: {ex.Message}");
//    }
//}

//[HttpDelete("{id}")]
//public async Task<ActionResult<bool>> DeleteFormRecord(int id)
//{
//    var formRepository = new FormRepository(_connectionString);

//    try
//    {
//        var isDeleted = await formRepository.DeleteFormRecordAsync(id);

//        if (!isDeleted)
//        {
//            return NotFound("FormRecord not found.");
//        }

//        return Ok(true);
//    }
//    catch (Exception ex)
//    {
//        return StatusCode(500, $"An error occurred: {ex.Message}");
//    }
//}
//    }
//}












//code for dependancy injection but has the dependency error

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


//        public FormsController(IFormRepository formRepository, FormService formService)
//        {
//            _formRepository = formRepository;

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

