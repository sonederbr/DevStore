using System;

using DevStore.Core.Messages;

using FluentValidation;

namespace DevStore.Sales.Application.Commands
{
    public class StartOrderCommand : Command
    {
        public Guid ClientId { get; private set; }
        public Guid CourseId { get; private set; }
        public decimal Total { get; private set; }
        public string NameCard { get; private set; }
        public string NumberCard { get; private set; }
        public string ExpirationDateCard { get; private set; }
        public string CvvCard { get; private set; }

        public StartOrderCommand(Guid clientId, Guid courseId, decimal total, string nameCard, string numberCard, string expirationDateCard, string cvvCard)
        {
            ClientId = clientId;
            CourseId = courseId;
            Total = total;
            NameCard = nameCard;
            NumberCard = numberCard;
            ExpirationDateCard = expirationDateCard;
            CvvCard = cvvCard;
        }

        public override bool IsValid()
        {
            ValidationResult = new StartOrderValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class StartOrderValidation : AbstractValidator<StartOrderCommand>
    {
        public StartOrderValidation()
        {
            RuleFor(c => c.ClientId)
                .NotEqual(Guid.Empty)
                .WithMessage("Id do cliente inválido");

            RuleFor(c => c.CourseId)
                .NotEqual(Guid.Empty)
                .WithMessage("Id do curso inválido");

            RuleFor(c => c.NameCard)
                .NotEmpty()
                .WithMessage("O nome no cartão não foi informado");

            RuleFor(c => c.NumberCard)
                .CreditCard()
                .WithMessage("Número de cartão de crédito inválido");

            RuleFor(c => c.ExpirationDateCard)
                .NotEmpty()
                .WithMessage("Data de expiração não informada");

            RuleFor(c => c.CvvCard)
                .Length(3, 4)
                .WithMessage("O CVV não foi preenchido corretamente");
        }
    }
}
