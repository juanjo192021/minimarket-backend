using System.ComponentModel.DataAnnotations;

namespace minimarket_project_backend.Helpers
{
    public class FileSizeLimitAttribute : ValidationAttribute
    {
        private readonly int _maxSize;

        public FileSizeLimitAttribute(int maxSize)
        {
            _maxSize = maxSize;
            ErrorMessage = $"The maximum image size is {_maxSize / (1024 * 1024)}MB.";
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is IFormFile file && file.Length > _maxSize)
            {
                return new ValidationResult(ErrorMessage);
            }

            return ValidationResult.Success;
        }
    }
}
