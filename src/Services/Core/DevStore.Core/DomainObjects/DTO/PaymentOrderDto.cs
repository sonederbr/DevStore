using System;

namespace DevStore.Core.DomainObjects.DTO
{
    public class PaymentOrderDto
    {
        public Guid OrderId { get; set; }
        public Guid ClientId { get; set; }
        public decimal Total { get; set; }
        public string NameCard { get; set; }
        public string NumberCard { get; set; }
        public string ExpirationDateCard { get; set; }
        public string CvvCard { get; set; }
    }
}