namespace DevStore.Catalog.Domain
{
    using System;

    using DevStore.Core.DomainObjects;

    public class Course : Entity, IAggregateRoot
    {
        public Guid CategoryId { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public int ClassSize { get; private set; }
        public int PlacesAvailable { get; private set; }
        public decimal Price { get; private set; }
        public string Image { get; private set; }
        public string Video { get; private set; }
        public Specification Specification { get;  private set; }
        public Period Period { get; private set; }
        public Category Category { get; private set; }
        public bool Enable { get; private set; }
        public DateTime CreatedDate { get; private set; }
        public DateTime? UpdatedDate { get; private set; }

        protected Course() { }

        public Course(string name, 
                       string description, 
                       bool enable, 
                       decimal price, 
                       Guid categoryId, 
                       string image,
                       string video,
                       int classSize,
                       Period period,
                       Specification specification)
        {
            CategoryId = categoryId;
            Name = name;
            Description = description;
            Enable = enable;
            Price = price;
            Image = image;
            Video = video;
            ClassSize = classSize;
            Period = period;
            Specification = specification;

            Validate();
        }

        public void Active() => Enable = true;

        public void Desactive() => Enable = false;

        public void ChangeCategory(Category category)
        {
            Category = category;
            CategoryId = category.Id;
        }

        public void ChangeDescription(string descricao)
        {
            AssertionConcern.ValidarSeVazio(descricao, "O campo Descricao do produto não pode estar vazio");
            Description = descricao;
        }

        public void WithdrawStock(int quantidade)
        {
            if (quantidade < 0) quantidade *= -1;
            if (!HasStock(quantidade)) throw new DomainException("Estoque insuficiente");
            PlacesAvailable -= quantidade;
        }

        public void ChargeStock(int quantidade)
        {
            PlacesAvailable += quantidade;
        }

        public bool HasStock(int quantidade)
        {
            return PlacesAvailable >= quantidade;
        }

        public void Validate()
        {
            AssertionConcern.ValidarSeVazio(Name, "O campo Nome do produto não pode estar vazio");
            AssertionConcern.ValidarSeVazio(Description, "O campo Descricao do produto não pode estar vazio");
            AssertionConcern.ValidarSeIgual(CategoryId, Guid.Empty, "O campo CategoriaId do produto não pode estar vazio");
            AssertionConcern.ValidarSeMenorQue(Price, 1, "O campo Valor do produto não pode se menor igual a 0");
            AssertionConcern.ValidarSeVazio(Image, "O campo Imagem do produto não pode estar vazio");
        }
    }
}