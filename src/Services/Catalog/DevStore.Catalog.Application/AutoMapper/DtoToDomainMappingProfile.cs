using AutoMapper;

using DevStore.Catalog.Application.Dtos;
using DevStore.Catalog.Domain;

namespace DevStore.Catalog.Application.AutoMapper
{
    public class DtoToDomainMappingProfile : Profile
    {
        public DtoToDomainMappingProfile()
        {
            CreateMap<CourseDto, Course>()
                .ConstructUsing(p =>
                    new Course(p.Name, p.Description, p.Enable, p.Price, p.CategoryId,
                        p.Image, p.Video,p.ClassSize, new Period(p.StartDate, p.EndDate), new Specification(p.TotalTime, p.NumberOfClasses)));

            CreateMap<CategoryDto, Category>()
                .ConstructUsing(c => new Category(c.Name, c.Code));
        }
    }
}