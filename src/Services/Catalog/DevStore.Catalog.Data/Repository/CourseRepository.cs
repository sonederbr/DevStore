using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DevStore.Catalog.Domain;
using DevStore.Core.Data;

namespace DevStore.Catalog.Data.Repository
{
    public class CourseRepository : ICourseRepository
    {
        private readonly CatalogContext _context;

        public CourseRepository(CatalogContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task<IEnumerable<Course>> GetAll()
        {
            return await _context.Courses.AsNoTracking().ToListAsync();
        }

        public async Task<Course> GetById(Guid id)
        {
            return await _context.Courses.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Course>> GetByCategory(int code)
        {
            return await _context.Courses.AsNoTracking().Include(p => p.Category).Where(c => c.Category.Code == code).ToListAsync();
        }

        public async Task<IEnumerable<Category>> GetCategories()
        {
            return await _context.Categories.AsNoTracking().ToListAsync();
        }

        public void Create(Course course)
        {
            _context.Courses.Add(course);
        }

        public void Update(Course course)
        {
            _context.Courses.Update(course);
        }

        public void Create(Category category)
        {
            _context.Categories.Add(category);
        }

        public void Update(Category category)
        {
            _context.Categories.Update(category);
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}