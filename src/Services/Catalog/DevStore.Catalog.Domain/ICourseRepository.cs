using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DevStore.Core.Data;

namespace DevStore.Catalog.Domain
{
    public interface ICourseRepository : IRepository<Course>
    {
        Task<IEnumerable<Course>> GetAll();
        Task<Course> GetById(Guid id);
        Task<IEnumerable<Course>> GetByCategory(int code);
        Task<IEnumerable<Category>> GetCategories();

        void Create(Course course);
        void Update(Course course);

        void Create(Category category);
        void Update(Category category);
    }
}