namespace DevStore.Catalog.Application.Dtos
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class CourseDto
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public Guid CategoryId { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Name { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Description { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public bool Enable { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Image { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Video { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "O campo {0} precisa ter o valor mínimo de {1}")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public int ClassSize { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "O campo {0} precisa ter o valor mínimo de {1}")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public int PlacesAvailable { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "O campo {0} precisa ter o valor mínimo de {1}")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public int TotalTime { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "O campo {0} precisa ter o valor mínimo de {1}")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public int NumberOfClasses { get; set; }

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public IEnumerable<CategoryDto> Categories { get; set; }
    }
}