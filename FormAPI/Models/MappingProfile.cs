using AutoMapper;
using FormAPI.Models;
using FormAPI.DTOs;

namespace FormAPI.Models
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Form, FormDto>();
                //.ForMember(dest => dest.Fields, opt => opt.MapFrom(src => src.Fields));

            CreateMap<FormField, FormFieldDto>();
            CreateMap<FormRecord, FormRecordDto>();

            // Reverse mappings
            CreateMap<FormDto, Form>();
            CreateMap<FormFieldDto, FormField>();
            CreateMap<FormRecordDto, FormRecord>();
        }
    }
}
