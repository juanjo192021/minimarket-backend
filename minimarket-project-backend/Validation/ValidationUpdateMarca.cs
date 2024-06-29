using FluentValidation;
using tienda_project_backend.Dtos.Marca;

namespace tienda_project_backend.Validation
{
    public class ValidationUpdateMarca: AbstractValidator<UpdateMarcaDTO>
    {
        public ValidationUpdateMarca() 
        {
            RuleFor(m => m.Id)
            .GreaterThan(0).WithMessage("El caompo {PropertyName} debe ser mayor que 0")
            .NotEmpty().WithMessage("El campo {PropertyName} es requerido")
            .Must(id => int.TryParse(id.ToString(), out _)).WithMessage("El {PropertyName} debe ser un número válido");

            RuleFor(m => m.Nombre).NotEmpty().WithMessage("El campo {PropertyName}  es requerido")
                      .MinimumLength(5).WithMessage("El campo {PropertyName} debe tener por lo menos 5 caracteres.")
                      .MaximumLength(100).WithMessage("El campo {PropertyName} debe tener menos de 100 caracteres.");

            RuleFor(m => m.RutaImagen).MaximumLength(200).WithMessage("El campo {PropertyName} debe tener menos de 100 caracteres.");

            RuleFor(m => m.Estado).Must(value => value == true || value == false).WithMessage("El estado debe ser true o false."); ;

        }
    }
}
