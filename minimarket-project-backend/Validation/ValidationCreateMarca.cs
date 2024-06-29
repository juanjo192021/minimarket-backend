using FluentValidation;
using tienda_project_backend.Dtos.Marca;

namespace tienda_project_backend.Validation
{
    public class ValidationCreateMarca : AbstractValidator<CreateMarcaDTO>
    {
        public ValidationCreateMarca()
        {
            RuleFor(m => m.Nombre).NotEmpty().WithMessage("El campo {PropertyName}  es requerido")
                                  .MinimumLength(5).WithMessage("El campo {PropertyName} debe tener por lo menos 5 caracteres.")
                                  .MaximumLength(100).WithMessage("El campo {PropertyName} debe tener menos de 100 caracteres.");

            RuleFor(m => m.RutaImagen).MaximumLength(200).WithMessage("El campo {PropertyName} debe tener menos de 100 caracteres.");

            RuleFor(m => m.Estado).Must(value => value == true || value == false).WithMessage("El estado debe ser true o false."); ;
        }
    }    
}
