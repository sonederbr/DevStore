using System;

using DevStore.Core.Messages;

using FluentValidation;

namespace DevStore.Sales.Application.Commands
{
    public class AddOrderItemCommand : Command
    {
        public Guid ClientId { get; private set; }
        public Guid CourseId { get; private set; }
        public string CourseName { get; private set; }
        public int Quantity { get; private set; }
        public decimal ItemValue { get; private set; }

        public AddOrderItemCommand(Guid clientId, Guid courseId, string courseName, int quantity, decimal itemValue)
        {
            ClientId = clientId;
            CourseId = courseId;
            CourseName = courseName;
            Quantity = quantity;
            ItemValue = itemValue;
        }

        public override bool IsValid()
        {
            ValidationResult = new AddOrderItemValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class AddOrderItemValidation : AbstractValidator<AddOrderItemCommand>
    {
        public AddOrderItemValidation()
        {
            RuleFor(c => c.ClientId)
               .NotEqual(Guid.Empty)
               .WithMessage("Id do cliente inválido");

            RuleFor(c => c.CourseId)
                .NotEqual(Guid.Empty)
                .WithMessage("Id do produto inválido");

            RuleFor(c => c.CourseName)
                .NotEmpty()
                .WithMessage("O nome do produto não foi informado");

            RuleFor(c => c.Quantity)
                .GreaterThan(0)
                .WithMessage("A quantidade miníma de um item é 1");

            RuleFor(c => c.Quantity)
                .LessThan(15)
                .WithMessage("A quantidade máxima de um item é 15");

            RuleFor(c => c.ItemValue)
                .GreaterThan(0)
                .WithMessage("O valor do item precisa ser maior que 0");
        }
    }
}
