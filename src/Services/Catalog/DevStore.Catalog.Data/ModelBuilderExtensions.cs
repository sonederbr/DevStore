using System;
using System.Collections.Generic;

using DevStore.Catalog.Domain;

using Microsoft.EntityFrameworkCore;

namespace DevStore.Catalog.Data
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            var c1 = new Category("Category 1", 1);
            var c2 = new Category("Category 2", 2);
            modelBuilder.Entity<Category>().HasData(c1, c2);

            var produtcts = new List<Course> {
                new Course("Course 1", "Description 1", true, 10, c1.Id, "image1.jpg", "video1.mp4",
                10, new Period(DateTime.Now, DateTime.Now.AddMonths(1)), new Specification(100, 10)),

                new Course("Course 2", "Description 2", true, 20, c2.Id, "image2.jpg", "video1.mp4",
                20, new Period(DateTime.Now, DateTime.Now.AddMonths(2)), new Specification(200, 20))
            };
            modelBuilder.Entity<Course>(prd =>
            {
                foreach (var product in produtcts)
                {
                    prd.HasData(product.MapCourseToAnonymousObject());
                    prd.OwnsOne(x => x.Specification)
                        .HasData(product.MapSpecificationOfCourseToAnonymousObject());
                    prd.OwnsOne(x => x.Period)
                        .HasData(product.MapPeriodOfCourseToAnonymousObject());
                }
            });
        }

        /// <summary>
        /// Issue https://github.com/dotnet/efcore/issues/10000
        /// https://medium.com/@sacchitiellogiovanni/ef-core-e-ddd-mapeando-e-criando-seeds-com-objetos-de-valor-f26fc5d73a94
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private static object MapCourseToAnonymousObject(this Course obj)
        {
            return new
            {
                obj.Id,
                obj.CategoryId,
                obj.Name,
                obj.Description,
                obj.Enable,
                obj.Price,
                obj.Image,
                obj.Video,
                obj.EnrollimentLimit,
                obj.TotalOfEnrolled,
                CreatedDate = DateTime.Now
            };
        }

        /// <summary>
        /// Issue https://github.com/dotnet/efcore/issues/10000
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private static object MapSpecificationOfCourseToAnonymousObject(this Course obj)
        {
            return new
            {
                CourseId = obj.Id,
                obj.Specification.TotalTime,
                obj.Specification.NumberOfClasses
            };
        }

        /// <summary>
        /// Issue https://github.com/dotnet/efcore/issues/10000
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private static object MapPeriodOfCourseToAnonymousObject(this Course obj)
        {
            return new
            {
                CourseId = obj.Id,
                obj.Period.StartDate,
                obj.Period.EndDate
            };
        }
    }
}