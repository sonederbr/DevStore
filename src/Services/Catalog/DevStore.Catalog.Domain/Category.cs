﻿namespace DevStore.Catalog.Domain
{
    using System.Collections.Generic;

    using DevStore.Core.Messages.CommonMessages.DomainEvents;


    public class Category : Entity
    {
        public string Name { get; private set; }
        public int Code { get; private set; }

        // EF Relation
        public ICollection<Course> Products { get; set; }

        protected Category() { }

        public Category(string name, int code)
        {
            Name = name;
            Code = code;

            Validate();
        }

        public override string ToString()
        {
            return $"{Name} - {Code}";
        }

        public void Validate()
        {
            AssertionConcern.ValidarSeVazio(Name, "O campo Nome da categoria não pode estar vazio");
            AssertionConcern.ValidarSeIgual(Code, 0, "O campo Codigo não pode ser 0");
        }
    }
}