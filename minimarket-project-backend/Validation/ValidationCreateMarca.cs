using FluentValidation;
using tienda_project_backend.Dtos.Marca;

namespace tienda_project_backend.Validation
{
    public class ValidationCreateMarca : AbstractValidator<CreateMarcaDTO>
    {
        public ValidationCreateMarca()
        {
            RuleFor(m => m.Nombre)
                .NotEmpty().WithMessage("El campo {PropertyName}  es requerido")
                .MinimumLength(5).WithMessage("El campo {PropertyName} debe tener por lo menos 5 caracteres.")
                .MaximumLength(100).WithMessage("El campo {PropertyName} debe tener menos de 100 caracteres.");

            //RuleFor(m => m.RutaImagen).MaximumLength(200).WithMessage("El campo {PropertyName} debe tener menos de 100 caracteres.");

            RuleFor(m => m.Imagen)
                .Must(EsImagenValida).WithMessage("El archivo debe ser una imagen válida (png, jpg, jpeg).")
                .When(m => m.Imagen != null); // Solo valida si el archivo no es nulo

            RuleFor(m => m.Imagen)
                .Must(imagen => imagen == null || imagen.Length <= 2 * 1024 * 1024) // Máximo 2MB
                .WithMessage("El tamaño máximo de la imagen es de 2MB.");
        }

        private bool EsImagenValida(IFormFile? file)
        {
            if (file == null) return true;

            var formatosPermitidos = new[] { ".jpg", ".jpeg", ".png" };
            var extension = System.IO.Path.GetExtension(file.FileName).ToLower();

            return formatosPermitidos.Contains(extension);
        }
    }
}
