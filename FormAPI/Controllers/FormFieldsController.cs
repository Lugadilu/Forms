
/*
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FormAPI.Context;
using FormAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FormAPI.Repositories
{
    public class FormRepository : IFormRepository
    {
        private readonly ApplicationDbContext _context;

        public FormRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Form>> GetAllFormsAsync()
        {
            try
            {
                return await _context.Forms.Include(f => f.Pages)
                                           .ThenInclude(p => p.FormFields)
                                           .ToListAsync();
            }
            catch (Exception ex)
            {
                // Error handling
                throw; // Re-throw the exception
            }
        }

        public async Task<Form> GetFormByIdAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("Invalid form ID.", nameof(id));
            }

            try
            {
                return await _context.Forms.Include(f => f.Pages)
                                           .ThenInclude(p => p.FormFields)
                                           .FirstOrDefaultAsync(f => f.Id == id);
            }
            catch (Exception ex)
            {
                // Error handling
                throw; // Re-throw the exception
            }
        }

        public async Task<Form> CreateFormAsync(Form form)
        {
            if (form == null)
            {
                throw new ArgumentNullException(nameof(form), "Form cannot be null.");
            }

            try
            {
                _context.Forms.Add(form);
                await _context.SaveChangesAsync();
                return form;
            }
            catch (Exception ex)
            {
                // Error handling
                throw; // Re-throw the exception
            }
        }

        public async Task<Form> UpdateFormAsync(Form form)
        {
            if (form.Id == Guid.Empty)
            {
                throw new ArgumentException("Form Id must be provided.", nameof(form.Id));
            }

            try
            {
                _context.Entry(form).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return form;
            }
            catch (Exception ex)
            {
                // Error handling
                throw;
            }
        }

        public async Task DeleteFormAsync(Guid id)
        {
            try
            {
                var form = await _context.Forms.FindAsync(id);
                if (form != null)
                {
                    _context.Forms.Remove(form);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                // Error handling
                throw;
            }
        }

        public async Task<IEnumerable<FormField>> GetAllFormFieldsAsync()
        {
            try
            {
                return await _context.FormFields.ToListAsync();
            }
            catch (Exception ex)
            {
                // Error handling
                throw; // Re-throw the exception
            }
        }

        public async Task<FormField> CreateFormFieldAsync(FormField formField)
        {
            if (formField == null)
            {
                throw new ArgumentNullException(nameof(formField), "Form field cannot be null.");
            }

            try
            {
                _context.FormFields.Add(formField);
                await _context.SaveChangesAsync();
                return formField;
            }
            catch (Exception ex)
            {
                // Error handling
                throw;
            }
        }

        public async Task<bool> DeleteFormFieldAsync(Guid id)
        {
            try
            {
                var formField = await _context.FormFields.FindAsync(id);
                if (formField != null)
                {
                    _context.FormFields.Remove(formField);
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                // Error handling
                throw;
            }
        }

        public async Task<IEnumerable<FormRecord>> GetAllFormRecordsAsync()
        {
            try
            {
                return await _context.FormRecords.ToListAsync();
            }
            catch (Exception ex)
            {
                // Error handling
                throw; // Re-throw the exception
            }
        }

        public async Task<FormRecord> GetFormRecordByIdAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("Invalid form record ID.", nameof(id));
            }

            try
            {
                return await _context.FormRecords.FindAsync(id);
            }
            catch (Exception ex)
            {
                // Error handling
                throw; // Re-throw the exception
            }
        }

        public async Task<FormRecord> CreateFormRecordAsync(FormRecord formRecord)
        {
            if (formRecord == null)
            {
                throw new ArgumentNullException(nameof(formRecord), "Form record cannot be null.");
            }

            try
            {
                _context.FormRecords.Add(formRecord);
                await _context.SaveChangesAsync();
                return formRecord;
            }
            catch (Exception ex)
            {
                // Error handling
                throw; // Re-throw the exception
            }
        }

        public async Task<FormRecord> UpdateFormRecordAsync(FormRecord formRecord)
        {
            if (formRecord.Id == Guid.Empty)
            {
                throw new ArgumentException("Form record Id must be provided.", nameof(formRecord.Id));
            }

            try
            {
                _context.Entry(formRecord).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return formRecord;
            }
            catch (Exception ex)
            {
                // Error handling
                throw; // Re-throw the exception
            }
        }

        public async Task<bool> DeleteFormRecordAsync(Guid id)
        {
            try
            {
                var formRecord = await _context.FormRecords.FindAsync(id);
                if (formRecord != null)
                {
                    _context.FormRecords.Remove(formRecord);
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                // Error handling
                throw; // Re-throw the exception
            }
        }

        public async Task<IEnumerable<FormRecord>> GetFormRecordsByFormIdAsync(Guid formId)
        {
            if (formId == Guid.Empty)
            {
                throw new ArgumentException("Invalid form ID.", nameof(formId));
            }

            try
            {
                return await _context.FormRecords.Where(fr => fr.FormId == formId).ToListAsync();
            }
            catch (Exception ex)
            {
                // Error handling
                throw;
            }
        }

        public async Task<FormRecord> CreateFormRecordForFormAsync(Guid formId, FormRecord formRecord)
        {
            if (formRecord == null)
            {
                throw new ArgumentNullException(nameof(formRecord), "Form record cannot be null.");
            }

            formRecord.FormId = formId;

            try
            {
                _context.FormRecords.Add(formRecord);
                await _context.SaveChangesAsync();
                return formRecord;
            }
            catch (Exception ex)
            {
                // Error handling
                throw;
            }
        }

        public async Task<FormRecord> UpdateFormRecordForFormAsync(Guid formId, FormRecord formRecord)
        {
            if (formRecord == null)
            {
                throw new ArgumentNullException(nameof(formRecord), "Form record cannot be null.");
            }

            if (formRecord.Id == Guid.Empty)
            {
                throw new ArgumentException("Form record Id must be provided.", nameof(formRecord.Id));
            }

            formRecord.FormId = formId;

            try
            {
                _context.Entry(formRecord).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return formRecord;
            }
            catch (Exception ex)
            {
                // Error handling
                throw;
            }
        }

        public async Task<bool> DeleteFormRecordForFormAsync(Guid formId, Guid recordId)
        {
            if (formId == Guid.Empty || recordId == Guid.Empty)
            {
                throw new ArgumentException("Invalid form or record ID.");
            }

            try
            {
                var formRecord = await _context.FormRecords.FirstOrDefaultAsync(fr => fr.Id == recordId && fr.FormId == formId);
                if (formRecord != null)
                {
                    _context.FormRecords.Remove(formRecord);
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                // Error handling
                throw;
            }
        }
    }
}
*/






/*
using FormAPI.Context;
using FormAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FormAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FormFieldsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public FormFieldsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<FormField>> CreateFormField(FormField formField)
        {
            if (formField == null)
            {
                return BadRequest(new { errors = new[] { new { status = "400", title = "Bad Request", detail = "FormField is null" } } });
            }

            try
            {
                _context.formfields.Add(formField);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetFormFieldById), new { id = formField.Id }, formField);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating form field: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, new { errors = new[] { new { status = "500", title = "Internal Server Error", detail = "Unexpected error occurred." } } });
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FormField>>> GetFormFields()
        {
            var formFields = await _context.formfields.ToListAsync();
            return Ok(formFields);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FormField>> GetFormFieldById(Guid id)
        {
            var formField = await _context.formfields.FindAsync(id);
            if (formField == null)
            {
                return NotFound(new { errors = new[] { new { status = "404", title = "Not Found", detail = "FormField not found." } } });
            }

            return Ok(formField);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<FormField>> UpdateFormField(Guid id, FormField formField)
        {
            if (formField == null)
            {
                return BadRequest(new { errors = new[] { new { status = "400", title = "Bad Request", detail = "FormField is null" } } });
            }

            var existingFormField = await _context.formfields.FindAsync(id);
            if (existingFormField == null)
            {
                return NotFound(new { errors = new[] { new { status = "404", title = "Not Found", detail = "FormField not found." } } });
            }

            existingFormField.Name = formField.Name; // Update properties as needed


            _context.formfields.Update(existingFormField);
            await _context.SaveChangesAsync();

            return Ok(existingFormField);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteFormField(Guid id)
        {
            var formField = await _context.formfields.FindAsync(id);
            if (formField == null)
            {
                return NotFound(new { errors = new[] { new { status = "404", title = "Not Found", detail = "FormField not found." } } });
            }

            try
            {
                _context.formfields.Remove(formField);
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting form field: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, new { errors = new[] { new { status = "500", title = "Internal Server Error", detail = "Unexpected error occurred." } } });
            }
        }
    }
}

*/








/*
using FormAPI.Context;
using FormAPI.DTOs;
using FormAPI.Models;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace FormAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FormFieldsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public FormFieldsController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<FormFieldDto>> CreateFormField(FormFieldDto formFieldDto)
        {
            if (formFieldDto == null)
            {
                return BadRequest(new { errors = new[] { new { status = "400", title = "Bad Request", detail = "FormField DTO is null" } } });
            }

            try
            {
                var formField = _mapper.Map<FormField>(formFieldDto);
                _context.formfields.Add(formField);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetFormFieldById), new { id = formField.Id }, formFieldDto);
            }
            catch (Exception ex)
            {
                // Log the exception for debugging (optional)
                Console.WriteLine($"Error creating form field: {ex.Message}");

                return StatusCode(StatusCodes.Status500InternalServerError, new { errors = new[] { new { status = "500", title = "Internal Server Error", detail = "Unexpected error occurred." } } });
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FormFieldDto>>> GetFormFields()
        {
            var formFields = await _context.formfields.ToListAsync();
            var formFieldDtos = _mapper.Map<List<FormFieldDto>>(formFields);

            return Ok(formFieldDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FormFieldDto>> GetFormFieldById(int id)
        {
            var formField = await _context.formfields.FindAsync(id);
            if (formField == null)
            {
                return NotFound(new { errors = new[] { new { status = "404", title = "Not Found", detail = "FormField not found." } } });
            }

            var formFieldDto = _mapper.Map<FormFieldDto>(formField);
            return Ok(formFieldDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<FormFieldDto>> UpdateFormField(int id, FormFieldDto formFieldDto)
        {
            if (formFieldDto == null)
            {
                return BadRequest(new { errors = new[] { new { status = "400", title = "Bad Request", detail = "FormField DTO is null" } } });
            }

            var formField = await _context.formfields.FindAsync(id);
            if (formField == null)
            {
                return NotFound(new { errors = new[] { new { status = "404", title = "Not Found", detail = "FormField not found." } } });
            }

            try
            {
                _mapper.Map(formFieldDto, formField);
                _context.formfields.Update(formField);
                await _context.SaveChangesAsync();

                return Ok(formFieldDto);
            }
            catch (Exception ex)
            {
                // Log the exception for debugging (optional)
                Console.WriteLine($"Error updating form field: {ex.Message}");

                return StatusCode(StatusCodes.Status500InternalServerError, new { errors = new[] { new { status = "500", title = "Internal Server Error", detail = "Unexpected error occurred." } } });
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteFormField(int id)
        {
            var formField = await _context.formfields.FindAsync(id);
            if (formField == null)
            {
                return NotFound(new { errors = new[] { new { status = "404", title = "Not Found", detail = "FormField not found." } } });
            }

            try
            {
                _context.formfields.Remove(formField);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                // Log the exception for debugging (optional)
                Console.WriteLine($"Error deleting form field: {ex.Message}");

                return StatusCode(StatusCodes.Status500InternalServerError, new { errors = new[] { new { status = "500", title = "Internal Server Error", detail = "Unexpected error occurred." } } });
            }
        }
    }
}
*/






/*
using FormAPI.Context;
using FormAPI.DTOs;
using FormAPI.Models;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FormAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FormFieldsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public FormFieldsController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<FormFieldDto>> CreateFormField(FormFieldDto formFieldDto)
        {
            if (formFieldDto == null)
            {
                return BadRequest("FormField DTO is null");
            }

            var formField = _mapper.Map<FormField>(formFieldDto);
            _context.formfields.Add(formField);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetFormFieldById), new { id = formField.Id }, formFieldDto);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FormFieldDto>>> GetFormFields()
        {
            var formFields = await _context.formfields.ToListAsync();
            var formFieldDtos = _mapper.Map<List<FormFieldDto>>(formFields);

            return Ok(formFieldDtos);
        }

        [HttpGet("{id}")]
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

        [HttpPut("{id}")]
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

        [HttpDelete("{id}")]
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
    }
}
*/