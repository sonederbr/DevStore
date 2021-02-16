using System;

using DevStore.Core.Messages;

using FluentValidation;

namespace DevStore.Sales.Application.Commands
{
    public class ApplyVoucherOrderCommand : Command
    {
        public Guid ClientId { get; private set; }
        public string VoucherCode { get; private set; }

        public ApplyVoucherOrderCommand(Guid clientId, string voucherCode)
        {
            ClientId = clientId;
            VoucherCode = voucherCode;
        }

        public override bool IsValid()
        {
            ValidationResult = new ApplyVoucherOrderValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class ApplyVoucherOrderValidation : AbstractValidator<ApplyVoucherOrderCommand>
    {
        public ApplyVoucherOrderValidation()
        {
            RuleFor(c => c.ClientId)
               .NotEqual(Guid.Empty)
               .WithMessage("Id do cliente inválido");

            RuleFor(c => c.VoucherCode)
                .NotEmpty()
                .WithMessage("O voucher não foi informado");
        }
    }
}
