namespace tienda_project_backend.Dtos.Marca
{
    public class CreateMarcaDTO
    {
        public string Nombre { get; set; } = null!;

        public IFormFile? Imagen { get; set; }  // Archivo de imagen
    }
}
