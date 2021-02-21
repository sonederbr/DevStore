using System;

using DevStore.Core.Messages;

using FluentValidation;

namespace DevStore.Sales.Application.Commands
{
    public class CancelOrderAndDisrollFromCourseCommand : Command
    {
        public Guid OrderId { get; private set; }
        public Guid ClientId { get; private set; }

        public CancelOrderAndDisrollFromCourseCommand(Guid orderId, Guid clientId)
        {
            AggregateId = orderId;
            OrderId = orderId;
            ClientId = clientId;
        }

        public override bool IsValid()
        {
            ValidationResult = new CancelOrderAndDisrollFromCourseValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class CancelOrderAndDisrollFromCourseValidation : AbstractValidator<CancelOrderAndDisrollFromCourseCommand>
    {
        public CancelOrderAndDisrollFromCourseValidation()
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
