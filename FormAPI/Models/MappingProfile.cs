using AutoMapper;
using FormAPI.DTOs;
using FormAPI.Models;
using Newtonsoft.Json;

namespace FormAPI.Models
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Map Form to FormDto and exclude unwanted properties
            CreateMap<Form, FormDto>()
                .ForMember(dest => dest.FormRecords, opt => opt.MapFrom(src => src.FormRecords))
                .ForMember(dest => dest.Pages, opt => opt.MapFrom(src => src.Pages))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => "form"));

            // Map FormField to FormFieldDto and exclude unwanted properties
            CreateMap<FormField, FormFieldDto>();

            // Map FormRecord to FormRecordDto
            CreateMap<FormRecord, FormRecordDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.FormId, opt => opt.MapFrom(src => src.FormId))
                .ForMember(dest => dest.FormFieldValues, opt => opt.MapFrom(src => JsonToDictionary(src.FormFieldValues))) // Convert JSON string to dictionary
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt));

            // Map Page to PageDto and exclude unwanted properties
            CreateMap<Page, PageDto>()
                .ForMember(dest => dest.Fields, opt => opt.MapFrom(src => src.FormFields));

            // Reverse mappings (for incoming data)
            CreateMap<FormDto, Form>()
                .ForMember(dest => dest.Pages, opt => opt.MapFrom(src => src.Pages))
                .ForMember(dest => dest.FormRecords, opt => opt.MapFrom(src => src.FormRecords));

            CreateMap<FormFieldDto, FormField>();

            CreateMap<FormRecordDto, FormRecord>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.FormId, opt => opt.MapFrom(src => src.FormId))
                .ForMember(dest => dest.FormFieldValues, opt => opt.MapFrom(src => JsonConvert.SerializeObject(src.FormFieldValues))) // Convert dictionary to JSON string
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt));

            CreateMap<PageDto, Page>()
                .ForMember(dest => dest.FormFields, opt => opt.MapFrom(src => src.Fields));
        }

        // Custom conversion method for JSON string to Dictionary
        private Dictionary<string, string> JsonToDictionary(string json)
        {
            return string.IsNullOrEmpty(json)
                ? new Dictionary<string, string>()
                : JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
        }
    }
}



/*
using AutoMapper;
using FormAPI.DTOs;
using FormAPI.Models;
using Newtonsoft.Json;

namespace FormAPI.Models
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Map Form to FormDto and exclude unwanted properties
            CreateMap<Form, FormDto>()
                .ForMember(dest => dest.FormRecords, opt => opt.MapFrom(src => src.FormRecords))
                .ForMember(dest => dest.Pages, opt => opt.MapFrom(src => src.Pages))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => "form"));

            // Map FormField to FormFieldDto and exclude unwanted properties
            CreateMap<FormField, FormFieldDto>();

            // Map FormRecord to FormRecordDto
            CreateMap<FormRecord, FormRecordDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.FormId, opt => opt.MapFrom(src => src.FormId))
               .ForMember(dest => dest.FormFieldValues, opt => opt.MapFrom(src => src.FormFieldValues))
               
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt));

            // Map Page to PageDto and exclude unwanted properties
            CreateMap<Page, PageDto>()
                .ForMember(dest => dest.Fields, opt => opt.MapFrom(src => src.FormFields));

            // Reverse mappings (for incoming data)
            CreateMap<FormDto, Form>()
                .ForMember(dest => dest.Pages, opt => opt.MapFrom(src => src.Pages))
                .ForMember(dest => dest.FormRecords, opt => opt.MapFrom(src => src.FormRecords));

            CreateMap<FormFieldDto, FormField>();

            CreateMap<FormRecordDto, FormRecord>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.FormId, opt => opt.MapFrom(src => src.FormId))
                .ForMember(dest => dest.FormFieldValues, opt => opt.MapFrom(src => src.FormFieldValues))
                
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt));



            CreateMap<PageDto, Page>()
                .ForMember(dest => dest.FormFields, opt => opt.MapFrom(src => src.Fields));
        }
    }
}
*/