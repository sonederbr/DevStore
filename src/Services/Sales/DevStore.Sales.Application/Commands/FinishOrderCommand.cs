using System;

using DevStore.Core.Messages;

using FluentValidation;

namespace DevStore.Sales.Application.Commands
{
    public class FinishOrderCommand : Command
    {

        public Guid OrderId { get; private set; }
        public Guid ClientId { get; private set; }

        public FinishOrderCommand(Guid orderId, Guid clientId)
        {
            AggregateId = orderId;
            OrderId = orderId;
            ClientId = clientId;
        }

        public override bool IsValid()
        {
            ValidationResult = new FinishOrderValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class FinishOrderValidation : AbstractValidator<FinishOrderCommand>
    {
        public FinishOrderValidation()
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
