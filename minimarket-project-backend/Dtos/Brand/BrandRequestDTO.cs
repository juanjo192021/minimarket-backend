namespace minimarket_project_backend.Dtos.Brand
{
    public class BrandRequestDTO
    {
        public string? name { get; set; }
        public IFormFile? fileImage { get; set; }
        public bool? status { get; set; }
    }
}
