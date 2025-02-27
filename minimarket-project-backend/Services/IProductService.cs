using minimarket_project_backend.Common.Responses;
using minimarket_project_backend.Dtos.Product;

namespace minimarket_project_backend.Services
{
    public interface IProductService
    {
        public Task<DataResponse<List<ProductDTO>>> getAll(int page, int limit);

        //public Task<DataResponse<List<MarcaDTO>>> search(string name, int page, int limit);
        //public Task<DataResponse<MarcaDTO>> searchById(int id);
        //public Task<ApiResponse> create(CreateMarcaDTO createMarcaDTO);
        //public Task<ApiResponse> update(UpdateMarcaDTO updateMarcaDTO);
        //public Task<ApiResponse> delete(int id)
    }
}
