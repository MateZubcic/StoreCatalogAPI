using System.ComponentModel.DataAnnotations;

namespace StoreCatalog.Dtos
{
    public class CreateItemDto
    {
        [Required]
        public string Name { get; init; }

        [Required]
        public decimal Price { get; init; }
    }
}