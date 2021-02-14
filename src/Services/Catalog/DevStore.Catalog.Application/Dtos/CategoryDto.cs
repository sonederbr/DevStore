namespace DevStore.Catalog.Application.Dtos
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class CategoryDto
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Name { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public int Code { get; set; }
    }
}