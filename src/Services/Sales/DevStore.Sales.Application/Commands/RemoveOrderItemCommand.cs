using System;

using DevStore.Core.Messages;

using FluentValidation;

namespace DevStore.Sales.Application.Commands
{
    public class RemoveOrderItemCommand : Command
    {
        public Guid ClientId { get; private set; }
        public Guid CourseId { get; private set; }

        public RemoveOrderItemCommand(Guid clientId, Guid courseId)
        {
            AggregateId = courseId;
            ClientId = clientId;
            CourseId = courseId;
        }

        public override bool IsValid()
        {
            ValidationResult = new RemoveOrderItemValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class RemoveOrderItemValidation : AbstractValidator<RemoveOrderItemCommand>
    {
        public RemoveOrderItemValidation()
        {
            RuleFor(c => c.ClientId)
                .NotEqual(Guid.Empty)
                .WithMessage("Id do cliente inválido");

            RuleFor(c => c.CourseId)
                .NotEqual(Guid.Empty)
                .WithMessage("Id do curso inválido");
        }
    }
}
