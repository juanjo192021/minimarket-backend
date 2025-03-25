using minimarket_project_backend.Helpers;
using System.ComponentModel.DataAnnotations;

namespace minimarket_project_backend.Dtos.Category
{
    public class CategoryRequestDTO
    {
        [Required]
        [MinLength(5)]
        [MaxLength(100)]
        public required string Name { get; set; }

        public string? Description { get; set; }

        [CustomFileExtensionsAttribute(Extensions = "jpg,jpeg,png")]
        [FileSizeLimitAttribute(2 * 1024 * 1024)]
        public IFormFile? FileImage { get; set; }

        public int? ParentCategoryId { get; set; }

        [Required]
        public int CategoryLevel { get; set; }

        [Required]
        public bool status { get; set; }

    }
}
