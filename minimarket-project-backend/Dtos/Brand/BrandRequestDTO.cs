using minimarket_project_backend.Helpers;
using System.ComponentModel.DataAnnotations;

namespace minimarket_project_backend.Dtos.Brand
{
    public class BrandRequestDTO
    {
        [Required]
        [MinLength(5)]
        [MaxLength(100)]
        public string name { get; set; }

        [CustomFileExtensionsAttribute(Extensions = "jpg,jpeg,png")]
        [FileSizeLimitAttribute(2 * 1024 * 1024)]
        public IFormFile? fileImage { get; set; }

        [Required]
        public bool? status { get; set; }
    }
}
