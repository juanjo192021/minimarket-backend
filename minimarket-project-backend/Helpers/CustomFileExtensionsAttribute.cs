using System.ComponentModel.DataAnnotations;

namespace minimarket_project_backend.Helpers
{
    public class CustomFileExtensionsAttribute: ValidationAttribute
    {
        public string Extensions { get; set; } = "jpg,jpeg,png";

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is IFormFile file)
            {
                var allowedExtensions = Extensions.Split(',').Select(e => e.Trim().ToLower());
                var fileExtension = Path.GetExtension(file.FileName).ToLower().TrimStart('.');

                if (!allowedExtensions.Contains(fileExtension))
                {
                    return new ValidationResult($"The file must be a valid image ({Extensions}).");
                }
            }
            return ValidationResult.Success;
        }
    }
}
