using System;
using System.Collections.Generic;

using DevStore.Core.Messages.CommonMessages.DomainEvents;

using FluentValidation;
using FluentValidation.Results;

namespace DevStore.Sales.Domain
{
    public class Voucher : Entity
    {
        public string Code { get; private set; }
        public decimal? Percentage { get; private set; }
        public decimal? ValueOfDiscount { get; private set; }
        public int Quantity { get; private set; }
        public TypeOfDiscount TypeOfDiscount { get; private set; }
        public DateTime CreatedDate { get; private set; }
        public DateTime? AppliedDate { get; private set; }
        public DateTime DateOfExpiration { get; private set; }
        public bool Enable { get; private set; }
        public bool HasUsed { get; private set; }

        // EF Rel.
        public ICollection<Order> Orders { get; set; }

        internal ValidationResult ValidIfIsAnable()
        {
            return new VoucherAplicavelValidation().Validate(this);
        }
    }

    public class VoucherAplicavelValidation : AbstractValidator<Voucher>
    {

        public VoucherAplicavelValidation()
        {
            RuleFor(c => c.DateOfExpiration)
                .Must(VoucherExpired)
                .WithMessage("Este voucher está expirado.");

            RuleFor(c => c.Enable)
                .Equal(true)
                .WithMessage("Este voucher não é mais válido.");

            RuleFor(c => c.HasUsed)
                .Equal(false)
                .WithMessage("Este voucher já foi utilizado.");

            RuleFor(c => c.Quantity)
                .GreaterThan(0)
                .WithMessage("Este voucher não está mais disponível");
        }

        protected static bool VoucherExpired(DateTime dateOfExpiration)
        {
            return dateOfExpiration >= DateTime.Now;
        }
    }
}