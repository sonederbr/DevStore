using AutoMapper;

using DevStore.Catalog.Application.Dtos;
using DevStore.Catalog.Domain;

namespace DevStore.Catalog.Application.AutoMapper
{
    public class DomainToDtoMappingProfile : Profile
    {
        public DomainToDtoMappingProfile()
        {
            CreateMap<Course, CourseDto>()
                .ForMember(d => d.PlacesAvailable, o => o.MapFrom(s => s.EnrollimentLimit - s.TotalOfEnrolled))
                .ForMember(d => d.TotalTime, o => o.MapFrom(s => s.Specification.TotalTime))
                .ForMember(d => d.NumberOfClasses, o => o.MapFrom(s => s.Specification.NumberOfClasses))
                .ForMember(d => d.StartDate, o => o.MapFrom(s => s.Period.StartDate))
                .ForMember(d => d.EndDate, o => o.MapFrom(s => s.Period.EndDate));

            CreateMap<Category, CategoryDto>();
        }
    }
}
