using System;
using System.Collections.Generic;

namespace DevStore.Core.DomainObjects.DTO
{
    public class CoursesOrderDto
    {
        public Guid OrderId { get; set; }
        public ICollection<ItemDto> Items { get; set; }
    }

    public class ItemDto
    {
        public Guid CourseId { get; set; }
    }
}