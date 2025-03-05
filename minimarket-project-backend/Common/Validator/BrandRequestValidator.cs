
namespace minimarket_project_backend.Common.Validator
{
    // : AbstractValidator<BrandRequestDTO>
    public class BrandRequestValidator
    {
        public BrandRequestValidator() 
        {

            //RuleFor(m => m.name)
            //    .NotNull().WithMessage("El campo {PropertyName} es requerido y no puede ser nulo.")
            //    .MinimumLength(5).WithMessage("El campo {PropertyName} debe tener por lo menos 5 caracteres.")
            //    .MaximumLength(100).WithMessage("El campo {PropertyName} debe tener menos de 100 caracteres.");

            //RuleFor(m => m.fileImage)
            //    .Must(EsImagenValida).WithMessage("El archivo debe ser una imagen válida (png, jpg, jpeg).")
            //    .When(m => m.fileImage != null); // Solo valida si el archivo no es nulo

            //RuleFor(m => m.fileImage)
            //    .Must(imagen => imagen == null || imagen.Length <= 2 * 1024 * 1024) // Máximo 2MB
            //    .WithMessage("El tamaño máximo de la imagen es de 2MB.");

            //RuleFor(m => m.status)
            //    .NotNull().WithMessage("El campo {PropertyName} es requerido y no puede ser nulo.")
            //    .Must(value => value == true || value == false)
            //    .WithMessage("El estado debe ser true o false.");
                //.When(m => m.status.HasValue); // Solo se valida si status tiene un valor (no es null);

            //RuleFor(m => m.Estado).Must(value => value == true || value == false).WithMessage("El estado debe ser true o false."); ;
        }

        private bool EsImagenValida(IFormFile? file)
        {
            if (file == null) return true;

            var formatosPermitidos = new[] { ".jpg", ".jpeg", ".png" };
            var extension = Path.GetExtension(file.FileName).ToLower();

            return formatosPermitidos.Contains(extension);
        }
    }
}
