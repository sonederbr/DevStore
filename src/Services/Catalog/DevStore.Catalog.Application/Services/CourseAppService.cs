using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using AutoMapper;

using DevStore.Catalog.Application.Dtos;
using DevStore.Catalog.Domain;
using DevStore.Core.DomainObjects;

namespace DevStore.Catalog.Application.Services
{
    public class CourseAppService : ICourseAppService
    {
        private readonly ICourseRepository _courseRepository;
        private readonly ICourseService _courseService;
        private readonly IMapper _mapper;

        public CourseAppService(ICourseRepository courseRepository, 
                                 ICourseService courseService,
                                 IMapper mapper)
        {
            _courseRepository = courseRepository;
            _courseService = courseService;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CourseDto>> GetByCategory(int code)
        {
            return _mapper.Map<IEnumerable<CourseDto>>(await _courseRepository.GetByCategory(code));
        }

        public async Task<CourseDto> GetById(Guid id)
        {
            return _mapper.Map<CourseDto>(await _courseRepository.GetById(id));
        }

        public async Task<IEnumerable<CourseDto>> GetAll()
        {
            return _mapper.Map<IEnumerable<CourseDto>>(await _courseRepository.GetAll());
        }

        public async Task<IEnumerable<CategoryDto>> GetCategories()
        {
            return _mapper.Map<IEnumerable<CategoryDto>>(await _courseRepository.GetCategories());
        }

        public async Task CreateCourse(CourseDto courseDto)
        {
            var course = _mapper.Map<Course>(courseDto);
            _courseRepository.Create(course);

            await _courseRepository.UnitOfWork.Commit();
        }

        public async Task UpdateCourse(CourseDto courseDto)
        {
            var course = _mapper.Map<Course>(courseDto);
            _courseRepository.Update(course);

            await _courseRepository.UnitOfWork.Commit();
        }

        public async Task<CourseDto> EnrolCourse(Guid id)
        {
            if (!_courseService.EnrolCourse(id).Result)
            {
                throw new DomainException("Falha ao entrar na vaga do curso.");
            }

            return _mapper.Map<CourseDto>(await _courseRepository.GetById(id));
        }

        public async Task<CourseDto> DisenrollCourse(Guid id)
        {
            if (!_courseService.DisenrollCourse(id).Result)
            {
                throw new DomainException("Falha ao repor vaga do curso.");
            }

            return _mapper.Map<CourseDto>(await _courseRepository.GetById(id));
        }

        public void Dispose()
        {
            _courseRepository?.Dispose();
            _courseService?.Dispose();
        }
    }
}