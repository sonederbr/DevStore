using System;

using DevStore.Core.Messages;

using FluentValidation;

namespace DevStore.Sales.Application.Commands
{
    public class CancelOrderCommand : Command
    {
        public Guid OrderId { get; private set; }
        public Guid ClientId { get; private set; }

        public CancelOrderCommand(Guid orderId, Guid clientId)
        {
            AggregateId = orderId;
            OrderId = orderId;
            ClientId = clientId;
        }

        public override bool IsValid()
        {
            ValidationResult = new CancelOrderValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class CancelOrderValidation : AbstractValidator<CancelOrderCommand>
    {
        public CancelOrderValidation()
        {
            RuleFor(c => c.OrderId)
                .NotEqual(Guid.Empty)
                .WithMessage("Id da order inválido");

            RuleFor(c => c.ClientId)
                .NotEqual(Guid.Empty)
                .WithMessage("Id do cliente inválido");
        }
    }
}
