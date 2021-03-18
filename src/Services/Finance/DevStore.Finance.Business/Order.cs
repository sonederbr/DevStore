using System;
using System.Collections.Generic;

namespace DevStore.Finance.Business
{
    public class Order
    {
        public Guid Id { get; set; }
        public Guid ClientId { get; set; }
        public decimal Total { get; set; }
        public List<Course> Courses { get; set; }
    }
}