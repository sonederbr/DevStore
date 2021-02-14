using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DevStore.Catalog.Application.Dtos;

namespace DevStore.Catalog.Application.Services
{
    public interface ICourseAppService : IDisposable
    {
        Task<IEnumerable<CourseDto>> GetByCategory(int code);
        Task<CourseDto> GetById(Guid id);
        Task<IEnumerable<CourseDto>> GetAll();
        Task<IEnumerable<CategoryDto>> GetCategories();

        Task CreateCourse(CourseDto courseDto);
        Task UpdateCourse(CourseDto courseDto);

        Task<CourseDto> EnrolCourse(Guid id);
        Task<CourseDto> DisenrollCourse(Guid id);
    }
}