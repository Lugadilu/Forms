using AutoMapper;
using FormAPI.DTOs;
using FormAPI.Models;
using FormAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FormAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class FormRecordsController : ControllerBase
    {
        private readonly IFormRepository _formRepository;
        private readonly IMapper _mapper;
        

        public FormRecordsController(IFormRepository formRepository, IMapper mapper)
        {
            _formRepository = formRepository;
            _mapper = mapper;
            
       
        }
        [HttpGet("{formId}/records")]
        public async Task<ActionResult> ListFormRecords(Guid formId)
        {
            try
            {
                var formRecords = await _formRepository.GetAllFormRecordsAsync();

                var response = new
                {
                    data = formRecords
                        .Where(r => r.FormId == formId)
                        .Select(r =>
                        {
                            // Deserialize FormFieldValues into a dynamic object
                            var formFieldValues = JsonConvert.DeserializeObject<Dictionary<string, object>>(r.FormFieldValues);

                            // Construct the response object
                            var record = new
                            {
                                type = "formRecord",
                                id = r.Id,
                                formId = r.FormId
                            };

                            // Merge the record with formFieldValues
                            return new
                            {
                                data = record
                                    .GetType()
                                    .GetProperties()
                                    .ToDictionary(prop => prop.Name, prop => prop.GetValue(record))
                                    .Concat(formFieldValues)
                                    .ToDictionary(kv => kv.Key, kv => kv.Value),
                                links = new { self = "../dictionary" }
                            };
                        }),
                    links = new { self = "/form/formRecords" }
                };

                return Content(JsonConvert.SerializeObject(response), "application/vnd.api+json");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    error = "An unexpected error occurred while retrieving form records.",
                    details = ex.Message
                });
            }
        }



        // GET: forms/{formId}/records/{recordId}
        [HttpGet("{formId}/records/{recordId}")]
        public async Task<ActionResult> GetFormRecord(Guid formId, Guid recordId)
        {
            try
            {
                var formRecord = await _formRepository.GetFormRecordByIdAsync(formId, recordId);

                if (formRecord == null)
                {
                    return NotFound(new { error = "Form record not found." });
                }

                var response = new
                {
                    data = new
                    {
                        formRecord.Id,
                        formRecord.FormId,
                        formRecord.FormFieldValues
                    },
                    type = "formRecord"
                };

                return Content(JsonConvert.SerializeObject(response), "application/vnd.api+json");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    error = "An unexpected error occurred while retrieving the form record.",
                    details = ex.Message
                });
            }
        }


        
        [HttpPost("{formId}/records")]
        public async Task<ActionResult> CreateFormRecord(Guid formId, [FromBody] Dictionary<string, string> formFieldValues)
        {
            try
            {
                
                if (formFieldValues == null || !formFieldValues.Any())
                {
                    return BadRequest(new
                    {
                        error = "Form field values are required."
                    });
                }
                
                // Retrieve the form definition using the repository
                var form = await _formRepository.GetFormWithFieldsAsync(formId);

                if (form == null)
                {
                    return NotFound(new
                    {
                        error = "Form not found."
                    });
                }

                // Extract the allowed fields from the form definition
                var allowedFields = form.Pages
                    .SelectMany(p => p.FormFields)
                    .Select(ff => ff.Name)
                    .ToList();

                // Validate the provided fields against the allowed fields
                var invalidFields = formFieldValues.Keys.Except(allowedFields).ToList();
                if (invalidFields.Any())
                {
                    return BadRequest(new
                    {
                        error = "Invalid fields provided.",
                        invalidFields
                    });
                }

                // Create a new FormRecord entity using AutoMapper
                var formRecord = new FormRecord
                {
                    Id = Guid.NewGuid(),
                    FormId = formId,
                    FormFieldValues = JsonConvert.SerializeObject(formFieldValues),
                    CreatedAt = DateTime.UtcNow
                };

                // Use the repository to add the form record
                await _formRepository.AddFormRecordAsync(formRecord);

                // Return the response
                var response = new
                {
                    data = new
                    {
                        type = "formRecord",
                        id = formRecord.Id,
                        formId = formRecord.FormId,
                        formFieldValues = formFieldValues,
                        createdAt = formRecord.CreatedAt
                    }
                };

                return StatusCode(201, response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    error = "An unexpected error occurred while creating the form record.",
                    details = ex.Message
                });
            }
        }
        

        // DELETE: forms/{formId}/records/{recordId}
        [HttpDelete("{formId}/records/{recordId}")]
        public async Task<ActionResult> DeleteFormRecord(Guid formId, Guid recordId)
        {
            try
            {
                var formRecord = await _formRepository.GetFormRecordByIdAsync(formId, recordId);

                if (formRecord == null)
                {
                    return NotFound(new { error = "Form record not found." });
                }

                await _formRepository.DeleteFormRecordAsync(formRecord);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    error = "An unexpected error occurred while deleting the form record.",
                    details = ex.Message
                });
            }
        }

        [HttpPut("FormRecords/{formId}/records/{recordId}")]
        //[HttpPut("{formId}/records/{recordId}")]
        public async Task<ActionResult> UpdateFormRecord(Guid formId, Guid recordId, [FromBody] Dictionary<string, string> formFieldValues)
        {
            try
            {
                // Validate the input
                if (formFieldValues == null || !formFieldValues.Any())
                {
                    return BadRequest(new { error = "Form field values are required." });
                }

                // Retrieve the form record by recordId and formId
                var formRecord = await _formRepository.GetFormRecordByIdAsync(formId, recordId); //recordId, formId
                if (formRecord == null)
                {
                    return NotFound(new { error = "Form record not found." });
                }

                // Retrieve the form definition
                var form = await _formRepository.GetFormWithFieldsAsync(formId);
                if (form == null)
                {
                    return NotFound(new { error = "Form not found." });
                }

                // Validate the provided fields against the allowed fields
                var allowedFields = form.Pages.SelectMany(p => p.FormFields).Select(ff => ff.Name).ToList();
                var invalidFields = formFieldValues.Keys.Except(allowedFields).ToList();
                if (invalidFields.Any())
                {
                    return BadRequest(new { error = "Invalid fields provided.", invalidFields });
                }

                // Update the form record's field values
                formRecord.FormFieldValues = JsonConvert.SerializeObject(formFieldValues);

                // Save the updated form record using the repository
                await _formRepository.UpdateFormRecordAsync(formRecord);

                // Return the updated form record as a response
                var response = new
                {
                    data = new
                    {
                        type = "formRecord",
                        id = formRecord.Id,
                        formId = formRecord.FormId,
                        formFieldValues = formFieldValues,
                    }
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "An unexpected error occurred while updating the form record.", details = ex.Message });
            }
        }

       
    }
}







/*
using FormAPI.Context;
using FormAPI.DTOs;
using FormAPI.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FormAPI.Controllers
{
    [Route("forms/{formId}/records")]
    [ApiController]
    public class FormRecordsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public FormRecordsController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        // GET: formrecords
        [HttpGet("/formrecords")]
        public async Task<ActionResult> GetAllFormRecords()
        {
            try
            {
                var formRecords = await _context.formrecords.ToListAsync();

                var formRecordDtos = formRecords.Select(fr => _mapper.Map<FormRecordDto>(fr)).ToList();

                var response = new
                {
                    data = formRecordDtos.Select(fr => new
                    {
                        type = "formRecord",
                        id = fr.Id,
                        formId = fr.FormId,
                        formFieldValues = fr.FormFieldValues,
                        createdAt = fr.CreatedAt
                    })
                };

                return Content(JsonConvert.SerializeObject(response), "application/vnd.api+json");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching form records: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, new { errors = new[] { new { status = "500", title = "Internal Server Error", detail = "Unexpected error occurred." } } });
            }
        }



        // GET: forms/{formId}/records
        [HttpGet]
        //[HttpGet("{formId}/records")]
        public async Task<ActionResult> GetFormRecords(Guid formId)
        {
            try
            {
                var formRecords = await _context.formrecords
                    .Where(fr => fr.FormId == formId)
                    .ToListAsync();

                var formRecordDtos = formRecords.Select(fr => _mapper.Map<FormRecordDto>(fr)).ToList();

                var response = new
                {
                    data = formRecordDtos.Select(fr => new
                    {
                        type = "formRecord",
                        id = fr.Id,
                        formId = fr.FormId,

                        formFieldValues = fr.FormFieldValues,
                        createdAt = fr.CreatedAt
                    })
                };

                return Content(JsonConvert.SerializeObject(response), "application/vnd.api+json");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching form records: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, new { errors = new[] { new { status = "500", title = "Internal Server Error", detail = "Unexpected error occurred." } } });
            }
        }



        // POST: forms/{formId}/records
        [HttpPost]
        public async Task<ActionResult> CreateFormRecord(Guid formId, [FromBody] Dictionary<string, string> formFieldValues)
        {
            try
            {
                if (formFieldValues == null || !formFieldValues.Any())
                {
                    return BadRequest(new
                    {
                        error = "Form field values are required."
                    });
                }

                // Retrieve the form definition
                var form = await _context.forms
                    .Include(f => f.Pages)
                        .ThenInclude(p => p.FormFields)
                    .FirstOrDefaultAsync(f => f.Id == formId);

                if (form == null)
                {
                    return NotFound(new
                    {
                        error = "Form not found."
                    });
                }

                // Extract the allowed fields from the form definition
                var allowedFields = form.Pages
                    .SelectMany(p => p.FormFields)
                    .Select(ff => ff.Name)
                    .ToList();

                // Validate the provided fields against the allowed fields
                var invalidFields = formFieldValues.Keys.Except(allowedFields).ToList();
                if (invalidFields.Any())
                {
                    return BadRequest(new
                    {
                        error = "Invalid fields provided.",
                        invalidFields
                    });
                }

                // Create a new FormRecord entity
                var formRecord = new FormRecord
                {
                    Id = Guid.NewGuid(),
                    FormId = formId,
                    FormFieldValues = JsonConvert.SerializeObject(formFieldValues),
                    CreatedAt = DateTime.UtcNow
                };

                // Add the form record to the context
                _context.formrecords.Add(formRecord);
                await _context.SaveChangesAsync();

                // Return the response
                var response = new
                {
                    data = new
                    {
                        type = "formRecord",
                        id = formRecord.Id,
                        formId = formRecord.FormId,
                        formFieldValues = formFieldValues,
                        createdAt = formRecord.CreatedAt
                    }
                };

                return StatusCode(201, response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    error = "An unexpected error occurred while creating the form record.",
                    details = ex.Message
                });
            }
        }


        

        // GET: forms/{formId}/records/{recordId}
        [HttpGet("{recordId}")]
        public async Task<ActionResult> GetFormRecordById(Guid formId, Guid recordId)
        {
            try
            {
                var formRecord = await _context.formrecords
                    .FirstOrDefaultAsync(fr => fr.FormId == formId && fr.Id == recordId);

                if (formRecord == null)
                {
                    return NotFound(new { errors = new[] { new { status = "404", title = "Not Found", detail = "Form record not found." } } });
                }

                var formRecordDto = _mapper.Map<FormRecordDto>(formRecord);

                var response = new
                {
                    type = "formRecord",
                    id = formRecordDto.Id,
                    formId = formRecordDto.FormId,
                    formFieldValues = formRecordDto.FormFieldValues,
                    createdAt = formRecordDto.CreatedAt
                };

                return Content(JsonConvert.SerializeObject(response), "application/vnd.api+json");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching form record: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, new { errors = new[] { new { status = "500", title = "Internal Server Error", detail = "Unexpected error occurred." } } });
            }
        }

        // [HttpPut("{formId}/records/{recordId}")]
        [HttpPut("{recordId}")]
        public async Task<ActionResult> UpdateFormRecord(Guid formId, Guid recordId, [FromBody] FormRecordDto formRecordDto)
        {
            try
            {
                var existingFormRecord = await _context.formrecords
                    .FirstOrDefaultAsync(fr => fr.Id == recordId && fr.FormId == formId);

                if (existingFormRecord == null)
                {
                    return NotFound(new { error = "Form record not found." });
                }

                // Update form record properties
                existingFormRecord.FormFieldValues = formRecordDto.FormFieldValues;

                await _context.SaveChangesAsync();

                var response = new
                {
                    data = new
                    {
                        type = "formRecord",
                        id = existingFormRecord.Id,
                        attributes = new
                        {
                            existingFormRecord.FormFieldValues
                        }
                    }
                };

                return Ok(response);
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, new
                {
                    error = "A database error occurred while updating the form record.",
                    details = ex.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    error = "An unexpected error occurred while updating the form record.",
                    details = ex.Message
                });
            }
        }




        // DELETE: forms/{formId}/records/{recordId}
        [HttpDelete("{recordId}")]
        public async Task<ActionResult> DeleteFormRecord(Guid formId, Guid recordId)
        {
            try
            {
                var formRecord = await _context.formrecords
                    .FirstOrDefaultAsync(fr => fr.FormId == formId && fr.Id == recordId);

                if (formRecord == null)
                {
                    return NotFound(new { errors = new[] { new { status = "404", title = "Not Found", detail = "Form record not found." } } });
                }

                _context.formrecords.Remove(formRecord);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting form record: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, new { errors = new[] { new { status = "500", title = "Internal Server Error", detail = "Unexpected error occurred." } } });
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
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FormAPI.Controllers
{
    //[Route("api/forms/{formId}/records")]
    [Route("forms/{formId}/records")]
    [ApiController]
    public class FormRecordsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public FormRecordsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/forms/{formId}/records
        [HttpGet]
        public async Task<ActionResult> GetFormRecords(Guid formId)
        {
            try
            {
                var formRecords = await _context.formrecords
                    .Where(fr => fr.FormId == formId)
                    .ToListAsync();

                var response = new
                {
                    data = formRecords.Select(fr => new
                    {
                        type = "formRecord",
                        id = fr.Id,
                        firstName = fr.FirstName,
                        secondName = fr.SecondName,
                        lastName = fr.LastName,
                        birthdate = fr.Birthdate,
                        gender = fr.Gender,
                        languageCode = fr.LanguageCode,
                        nationality = fr.Nationality,
                        phoneNumber = fr.PhoneNumber,
                        email = fr.Email,
                        arrival = fr.Arrival,
                        departure = fr.Departure,
                        address = fr.Address,
                        zip = fr.Zip,
                        city = fr.City,
                        country = fr.Country,
                        rating = fr.Rating,
                        formId = fr.FormId
                    })
                };

                //return Ok(response);
                return Content(JsonConvert.SerializeObject(response), "application/vnd.api+json");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching form records: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, new { errors = new[] { new { status = "500", title = "Internal Server Error", detail = "Unexpected error occurred." } } });
            }
        }

        // POST: api/forms/{formId}/records
        [HttpPost]
        public async Task<ActionResult> CreateFormRecord(Guid formId, FormRecord formRecord)
        {
            if (formRecord == null)
            {
                return BadRequest(new { errors = new[] { new { status = "400", title = "Bad Request", detail = "Form record is null" } } });
            }

            try
            {
                formRecord.Id = Guid.NewGuid();
                formRecord.FormId = formId;
                _context.formrecords.Add(formRecord);
                await _context.SaveChangesAsync();

                var response = new
                {
                    type = "formRecord",
                    id = formRecord.Id,
                    firstName = formRecord.FirstName,
                    secondName = formRecord.SecondName,
                    lastName = formRecord.LastName,
                    birthdate = formRecord.Birthdate,
                    gender = formRecord.Gender,
                    languageCode = formRecord.LanguageCode,
                    nationality = formRecord.Nationality,
                    phoneNumber = formRecord.PhoneNumber,
                    email = formRecord.Email,
                    arrival = formRecord.Arrival,
                    departure = formRecord.Departure,
                    address = formRecord.Address,
                    zip = formRecord.Zip,
                    city = formRecord.City,
                    country = formRecord.Country,
                    rating = formRecord.Rating,
                    formId = formRecord.FormId
                };

                return CreatedAtAction(nameof(GetFormRecordById), new { formId = formId, recordId = formRecord.Id }, response);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating form record: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, new { errors = new[] { new { status = "500", title = "Internal Server Error", detail = "Unexpected error occurred." } } });
            }
        }

        // GET: api/forms/{formId}/records/{recordId}
        [HttpGet("{recordId}")]
        public async Task<ActionResult> GetFormRecordById(Guid formId, Guid recordId)
        {
            try
            {
                var formRecord = await _context.formrecords
                    .FirstOrDefaultAsync(fr => fr.FormId == formId && fr.Id == recordId);

                if (formRecord == null)
                {
                    return NotFound(new { errors = new[] { new { status = "404", title = "Not Found", detail = "Form record not found." } } });
                }

                var response = new
                {
                    type = "formRecord",
                    id = formRecord.Id,
                    firstName = formRecord.FirstName,
                    secondName = formRecord.SecondName,
                    lastName = formRecord.LastName,
                    birthdate = formRecord.Birthdate,
                    gender = formRecord.Gender,
                    languageCode = formRecord.LanguageCode,
                    nationality = formRecord.Nationality,
                    phoneNumber = formRecord.PhoneNumber,
                    email = formRecord.Email,
                    arrival = formRecord.Arrival,
                    departure = formRecord.Departure,
                    address = formRecord.Address,
                    zip = formRecord.Zip,
                    city = formRecord.City,
                    country = formRecord.Country,
                    rating = formRecord.Rating,
                    formId = formRecord.FormId
                };

                //return Ok(response);
                return Content(JsonConvert.SerializeObject(response), "application/vnd.api+json");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching form record: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, new { errors = new[] { new { status = "500", title = "Internal Server Error", detail = "Unexpected error occurred." } } });
            }
        }

        // PUT: api/forms/{formId}/records/{recordId}
        [HttpPut("{recordId}")]
        public async Task<ActionResult> UpdateFormRecord(Guid formId, Guid recordId, FormRecord formRecord)
        {
            if (formRecord == null)
            {
                return BadRequest(new { errors = new[] { new { status = "400", title = "Bad Request", detail = "Form record is null" } } });
            }

            try
            {
                var existingFormRecord = await _context.formrecords
                    .FirstOrDefaultAsync(fr => fr.FormId == formId && fr.Id == recordId);

                if (existingFormRecord == null)
                {
                    return NotFound(new { errors = new[] { new { status = "404", title = "Not Found", detail = "Form record not found." } } });
                }

                existingFormRecord.FirstName = formRecord.FirstName;
                existingFormRecord.SecondName = formRecord.SecondName;
                existingFormRecord.LastName = formRecord.LastName;
                existingFormRecord.Birthdate = formRecord.Birthdate;
                existingFormRecord.Gender = formRecord.Gender;
                existingFormRecord.LanguageCode = formRecord.LanguageCode;
                existingFormRecord.Nationality = formRecord.Nationality;
                existingFormRecord.PhoneNumber = formRecord.PhoneNumber;
                existingFormRecord.Email = formRecord.Email;
                existingFormRecord.Arrival = formRecord.Arrival;
                existingFormRecord.Departure = formRecord.Departure;
                existingFormRecord.Address = formRecord.Address;
                existingFormRecord.Zip = formRecord.Zip;
                existingFormRecord.City = formRecord.City;
                existingFormRecord.Country = formRecord.Country;
                existingFormRecord.Rating = formRecord.Rating;

                _context.formrecords.Update(existingFormRecord);
                await _context.SaveChangesAsync();

                var response = new
                {
                    type = "formRecord",
                    id = existingFormRecord.Id,
                    firstName = existingFormRecord.FirstName,
                    secondName = existingFormRecord.SecondName,
                    lastName = existingFormRecord.LastName,
                    birthdate = existingFormRecord.Birthdate,
                    gender = existingFormRecord.Gender,
                    languageCode = existingFormRecord.LanguageCode,
                    nationality = existingFormRecord.Nationality,
                    phoneNumber = existingFormRecord.PhoneNumber,
                    email = existingFormRecord.Email,
                    arrival = existingFormRecord.Arrival,
                    departure = existingFormRecord.Departure,
                    address = existingFormRecord.Address,
                    zip = existingFormRecord.Zip,
                    city = existingFormRecord.City,
                    country = existingFormRecord.Country,
                    rating = existingFormRecord.Rating,
                    formId = existingFormRecord.FormId
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating form record: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, new { errors = new[] { new { status = "500", title = "Internal Server Error", detail = "Unexpected error occurred." } } });
            }
        }

        // DELETE: api/forms/{formId}/records/{recordId}
        [HttpDelete("{recordId}")]
        public async Task<ActionResult> DeleteFormRecord(Guid formId, Guid recordId)
        {
            try
            {
                var formRecord = await _context.formrecords
                    .FirstOrDefaultAsync(fr => fr.FormId == formId && fr.Id == recordId);

                if (formRecord == null)
                {
                    return NotFound(new { errors = new[] { new { status = "404", title = "Not Found", detail = "Form record not found." } } });
                }

                _context.formrecords.Remove(formRecord);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting form record: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, new { errors = new[] { new { status = "500", title = "Internal Server Error", detail = "Unexpected error occurred." } } });
            }
        }
    }
}
*/













/*
using FormAPI.Context;
using FormAPI.Models;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System;

namespace FormAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FormRecordsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public FormRecordsController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<FormRecord>> CreateFormRecord(FormRecord formRecord)
        {
            if (formRecord == null)
            {
                return BadRequest(new { errors = new[] { new { status = "400", title = "Bad Request", detail = "Form record is null" } } });
            }

            try
            {
                formRecord.Id = Guid.NewGuid();
                _context.formrecords.Add(formRecord);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetFormRecordById), new { id = formRecord.Id }, formRecord);
            }
            catch (Exception ex)
            {
                // Log the exception for debugging (optional)
                Console.WriteLine($"Error creating form record: {ex.Message}");

                return StatusCode(StatusCodes.Status500InternalServerError, new { errors = new[] { new { status = "500", title = "Internal Server Error", detail = "Unexpected error occurred." } } });
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FormRecord>>> GetFormRecords()
        {
            var formRecords = await _context.formrecords.ToListAsync();
            return Ok(formRecords);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FormRecord>> GetFormRecordById(Guid id)
        {
            var formRecord = await _context.formrecords.FindAsync(id);
            if (formRecord == null)
            {
                return NotFound(new { errors = new[] { new { status = "404", title = "Not Found", detail = "Form record not found." } } });
            }

            return Ok(formRecord);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<FormRecord>> UpdateFormRecord(Guid id, FormRecord formRecord)
        {
            if (formRecord == null)
            {
                return BadRequest(new { errors = new[] { new { status = "400", title = "Bad Request", detail = "Form record is null" } } });
            }

            var existingFormRecord = await _context.formrecords.FindAsync(id);
            if (existingFormRecord == null)
            {
                return NotFound(new { errors = new[] { new { status = "404", title = "Not Found", detail = "Form record not found." } } });
            }

            try
            {
                _mapper.Map(formRecord, existingFormRecord);
                _context.formrecords.Update(existingFormRecord);
                await _context.SaveChangesAsync();

                return Ok(existingFormRecord);
            }
            catch (Exception ex)
            {
                // Log the exception for debugging (optional)
                Console.WriteLine($"Error updating form record: {ex.Message}");

                return StatusCode(StatusCodes.Status500InternalServerError, new { errors = new[] { new { status = "500", title = "Internal Server Error", detail = "Unexpected error occurred." } } });
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteFormRecord(Guid id)
        {
            var formRecord = await _context.formrecords.FindAsync(id);
            if (formRecord == null)
            {
                return NotFound(new { errors = new[] { new { status = "404", title = "Not Found", detail = "Form record not found." } } });
            }

            try
            {
                _context.formrecords.Remove(formRecord);
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                // Log the exception for debugging (optional)
                Console.WriteLine($"Error deleting form record: {ex.Message}");

                return StatusCode(StatusCodes.Status500InternalServerError, new { errors = new[] { new { status = "500", title = "Internal Server Error", detail = "Unexpected error occurred." } } });
            }
        }

        [HttpGet("form/{formId}")]
        public async Task<ActionResult<IEnumerable<FormRecord>>> GetFormRecordsByFormId(Guid formId)
        {
            var formRecords = await _context.formrecords.Where(fr => fr.FormId == formId).ToListAsync();
            return Ok(formRecords);
        }

        [HttpGet("form/{formId}/{id}")]
        public async Task<ActionResult<FormRecord>> GetFormRecordByFormIdAndId(Guid formId, Guid id)
        {
            var formRecord = await _context.formrecords.FirstOrDefaultAsync(fr => fr.FormId == formId && fr.Id == id);
            if (formRecord == null)
            {
                return NotFound(new { errors = new[] { new { status = "404", title = "Not Found", detail = "Form record not found." } } });
            }

            return Ok(formRecord);
        }

        [HttpPost("form/{formId}")]
        public async Task<ActionResult<FormRecord>> CreateFormRecordForForm(Guid formId, FormRecord formRecord)
        {
            if (formRecord == null)
            {
                return BadRequest(new { errors = new[] { new { status = "400", title = "Bad Request", detail = "Form record is null" } } });
            }

            var form = await _context.forms.FindAsync(formId);
            if (form == null)
            {
                return NotFound(new { errors = new[] { new { status = "404", title = "Not Found", detail = "Form not found." } } });
            }

            try
            {
                formRecord.Id = Guid.NewGuid();
                formRecord.FormId = formId;

                _context.formrecords.Add(formRecord);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetFormRecordByFormIdAndId), new { formId, id = formRecord.Id }, formRecord);
            }
            catch (Exception ex)
            {
                // Log the exception for debugging (optional)
                Console.WriteLine($"Error creating form record: {ex.Message}");

                return StatusCode(StatusCodes.Status500InternalServerError, new { errors = new[] { new { status = "500", title = "Internal Server Error", detail = "Unexpected error occurred." } } });
            }
        }

        [HttpPut("form/{formId}/{id}")]
        public async Task<ActionResult<FormRecord>> UpdateFormRecordForForm(Guid formId, Guid id, FormRecord formRecord)
        {
            if (formRecord == null)
            {
                return BadRequest(new { errors = new[] { new { status = "400", title = "Bad Request", detail = "Form record is null" } } });
            }

            var existingFormRecord = await _context.formrecords.FirstOrDefaultAsync(fr => fr.FormId == formId && fr.Id == id);
            if (existingFormRecord == null)
            {
                return NotFound(new { errors = new[] { new { status = "404", title = "Not Found", detail = "Form record not found." } } });
            }

            try
            {
                _mapper.Map(formRecord, existingFormRecord);
                _context.formrecords.Update(existingFormRecord);
                await _context.SaveChangesAsync();

                return Ok(existingFormRecord);
            }
            catch (Exception ex)
            {
                // Log the exception for debugging (optional)
                Console.WriteLine($"Error updating form record: {ex.Message}");

                return StatusCode(StatusCodes.Status500InternalServerError, new { errors = new[] { new { status = "500", title = "Internal Server Error", detail = "Unexpected error occurred." } } });
            }
        }

        [HttpDelete("form/{formId}/{id}")]
        public async Task<ActionResult> DeleteFormRecordForForm(Guid formId, Guid id)
        {
            var formRecord = await _context.formrecords.FirstOrDefaultAsync(fr => fr.FormId == formId && fr.Id == id);
            if (formRecord == null)
            {
                return NotFound(new { errors = new[] { new { status = "404", title = "Not Found", detail = "Form record not found." } } });
            }

            try
            {
                _context.formrecords.Remove(formRecord);
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                // Log the exception for debugging (optional)
                Console.WriteLine($"Error deleting form record: {ex.Message}");

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
using System.Linq;
using System;

namespace FormAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FormRecordsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public FormRecordsController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<FormRecordDto>> CreateFormRecord(FormRecordDto formRecordDto)
        {
            if (formRecordDto == null)
            {
                return BadRequest(new { errors = new[] { new { status = "400", title = "Bad Request", detail = "Form record DTO is null" } } });
            }

            try
            {
                var formRecord = _mapper.Map<FormRecord>(formRecordDto);
                _context.formrecords.Add(formRecord);
                await _context.SaveChangesAsync();

                var createdFormRecordDto = _mapper.Map<FormRecordDto>(formRecord);
                return CreatedAtAction(nameof(GetFormRecordById), new { id = formRecord.Id }, createdFormRecordDto);
            }
            catch (Exception ex)
            {
                // Log the exception for debugging (optional)
                Console.WriteLine($"Error creating form record: {ex.Message}");

                return StatusCode(StatusCodes.Status500InternalServerError, new { errors = new[] { new { status = "500", title = "Internal Server Error", detail = "Unexpected error occurred." } } });
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FormRecordDto>>> GetFormRecords()
        {
            var formRecords = await _context.formrecords.ToListAsync();
            var formRecordDtos = _mapper.Map<List<FormRecordDto>>(formRecords);

            return Ok(formRecordDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FormRecordDto>> GetFormRecordById(int id)
        {
            var formRecord = await _context.formrecords.FindAsync(id);
            if (formRecord == null)
            {
                return NotFound(new { errors = new[] { new { status = "404", title = "Not Found", detail = "Form record not found." } } });
            }

            var formRecordDto = _mapper.Map<FormRecordDto>(formRecord);
            return Ok(formRecordDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<FormRecordDto>> UpdateFormRecord(int id, FormRecordDto formRecordDto)
        {
            if (formRecordDto == null)
            {
                return BadRequest(new { errors = new[] { new { status = "400", title = "Bad Request", detail = "Form record DTO is null" } } });
            }

            var formRecord = await _context.formrecords.FindAsync(id);
            if (formRecord == null)
            {
                return NotFound(new { errors = new[] { new { status = "404", title = "Not Found", detail = "Form record not found." } } });
            }

            try
            {
                _mapper.Map(formRecordDto, formRecord);
                _context.formrecords.Update(formRecord);
                await _context.SaveChangesAsync();

                var updatedFormRecordDto = _mapper.Map<FormRecordDto>(formRecord);
                return Ok(updatedFormRecordDto);
            }
            catch (Exception ex)
            {
                // Log the exception for debugging (optional)
                Console.WriteLine($"Error updating form record: {ex.Message}");

                return StatusCode(StatusCodes.Status500InternalServerError, new { errors = new[] { new { status = "500", title = "Internal Server Error", detail = "Unexpected error occurred." } } });
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteFormRecord(int id)
        {
            var formRecord = await _context.formrecords.FindAsync(id);
            if (formRecord == null)
            {
                return NotFound(new { errors = new[] { new { status = "404", title = "Not Found", detail = "Form record not found." } } });
            }

            try
            {
                _context.formrecords.Remove(formRecord);
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                // Log the exception for debugging (optional)
                Console.WriteLine($"Error deleting form record: {ex.Message}");

                return StatusCode(StatusCodes.Status500InternalServerError, new { errors = new[] { new { status = "500", title = "Internal Server Error", detail = "Unexpected error occurred." } } });
            }
        }

        [HttpGet("form/{formId}")]
        public async Task<ActionResult<IEnumerable<FormRecordDto>>> GetFormRecordsByFormId(int formId)
        {
            var formRecords = await _context.formrecords.Where(fr => fr.FormId == formId).ToListAsync();
            var formRecordDtos = _mapper.Map<List<FormRecordDto>>(formRecords);

            return Ok(formRecordDtos);
        }

        [HttpGet("form/{formId}/{id}")]
        public async Task<ActionResult<FormRecordDto>> GetFormRecordByFormIdAndId(int formId, int id)
        {
            var formRecord = await _context.formrecords.FirstOrDefaultAsync(fr => fr.FormId == formId && fr.Id == id);
            if (formRecord == null)
            {
                return NotFound(new { errors = new[] { new { status = "404", title = "Not Found", detail = "Form record not found." } } });
            }

            var formRecordDto = _mapper.Map<FormRecordDto>(formRecord);
            return Ok(formRecordDto);
        }

        [HttpPost("form/{formId}")]
        public async Task<ActionResult<FormRecordDto>> CreateFormRecordForForm(int formId, FormRecordDto formRecordDto)
        {
            if (formRecordDto == null)
            {
                return BadRequest(new { errors = new[] { new { status = "400", title = "Bad Request", detail = "Form record DTO is null" } } });
            }

            var form = await _context.forms.FindAsync(formId);
            if (form == null)
            {
                return NotFound(new { errors = new[] { new { status = "404", title = "Not Found", detail = "Form not found." } } });
            }

            try
            {
                var formRecord = _mapper.Map<FormRecord>(formRecordDto);
                formRecord.FormId = formId;

                _context.formrecords.Add(formRecord);
                await _context.SaveChangesAsync();

                var createdFormRecordDto = _mapper.Map<FormRecordDto>(formRecord);
                return CreatedAtAction(nameof(GetFormRecordByFormIdAndId), new { formId, id = formRecord.Id }, createdFormRecordDto);
            }
            catch (Exception ex)
            {
                // Log the exception for debugging (optional)
                Console.WriteLine($"Error creating form record: {ex.Message}");

                return StatusCode(StatusCodes.Status500InternalServerError, new { errors = new[] { new { status = "500", title = "Internal Server Error", detail = "Unexpected error occurred." } } });
            }
        }

        [HttpPut("form/{formId}/{id}")]
        public async Task<ActionResult<FormRecordDto>> UpdateFormRecordForForm(int formId, int id, FormRecordDto formRecordDto)
        {
            if (formRecordDto == null)
            {
                return BadRequest(new { errors = new[] { new { status = "400", title = "Bad Request", detail = "Form record DTO is null" } } });
            }

            var formRecord = await _context.formrecords.FirstOrDefaultAsync(fr => fr.FormId == formId && fr.Id == id);
            if (formRecord == null)
            {
                return NotFound(new { errors = new[] { new { status = "404", title = "Not Found", detail = "Form record not found." } } });
            }

            try
            {
                _mapper.Map(formRecordDto, formRecord);
                _context.formrecords.Update(formRecord);
                await _context.SaveChangesAsync();

                var updatedFormRecordDto = _mapper.Map<FormRecordDto>(formRecord);
                return Ok(updatedFormRecordDto);
            }
            catch (Exception ex)
            {
                // Log the exception for debugging (optional)
                Console.WriteLine($"Error updating form record: {ex.Message}");

                return StatusCode(StatusCodes.Status500InternalServerError, new { errors = new[] { new { status = "500", title = "Internal Server Error", detail = "Unexpected error occurred." } } });
            }
        }

        [HttpDelete("form/{formId}/{id}")]
        public async Task<ActionResult> DeleteFormRecordForForm(int formId, int id)
        {
            var formRecord = await _context.formrecords.FirstOrDefaultAsync(fr => fr.FormId == formId && fr.Id == id);
            if (formRecord == null)
            {
                return NotFound(new { errors = new[] { new { status = "404", title = "Not Found", detail = "Form record not found." } } });
            }

            try
            {
                _context.formrecords.Remove(formRecord);
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                // Log the exception for debugging (optional)
                Console.WriteLine($"Error deleting form record: {ex.Message}");

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
using System.Linq;

namespace FormAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FormRecordsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public FormRecordsController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
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
            return CreatedAtAction(nameof(GetFormRecordById), new { id = formRecord.Id }, createdFormRecordDto);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FormRecordDto>>> GetFormRecords()
        {
            var formRecords = await _context.formrecords.ToListAsync();
            var formRecordDtos = _mapper.Map<List<FormRecordDto>>(formRecords);

            return Ok(formRecordDtos);
        }

        [HttpGet("{id}")]
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

        [HttpPut("{id}")]
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

        [HttpDelete("{id}")]
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

        [HttpGet("form/{formId}")]
        public async Task<ActionResult<IEnumerable<FormRecordDto>>> GetFormRecordsByFormId(int formId)
        {
            var formRecords = await _context.formrecords.Where(fr => fr.FormId == formId).ToListAsync();
            var formRecordDtos = _mapper.Map<List<FormRecordDto>>(formRecords);

            return Ok(formRecordDtos);
        }

        [HttpGet("form/{formId}/{id}")]
        public async Task<ActionResult<FormRecordDto>> GetFormRecordByFormIdAndId(int formId, int id)
        {
            var formRecord = await _context.formrecords.FirstOrDefaultAsync(fr => fr.FormId == formId && fr.Id == id);
            if (formRecord == null)
            {
                return NotFound("Form record not found.");
            }

            var formRecordDto = _mapper.Map<FormRecordDto>(formRecord);
            return Ok(formRecordDto);
        }

        [HttpPost("form/{formId}")]
        public async Task<ActionResult<FormRecordDto>> CreateFormRecordForForm(int formId, FormRecordDto formRecordDto)
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
            return CreatedAtAction(nameof(GetFormRecordByFormIdAndId), new { formId, id = formRecord.Id }, createdFormRecordDto);
        }

        [HttpPut("form/{formId}/{id}")]
        public async Task<ActionResult<FormRecordDto>> UpdateFormRecordForForm(int formId, int id, FormRecordDto formRecordDto)
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

        [HttpDelete("form/{formId}/{id}")]
        public async Task<ActionResult> DeleteFormRecordForForm(int formId, int id)
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
*/