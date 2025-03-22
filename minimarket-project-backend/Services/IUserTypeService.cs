using minimarket_project_backend.Common.Responses;
using minimarket_project_backend.Dtos.UserType;
using minimarket_project_backend.Models;

namespace minimarket_project_backend.Services
{
    public interface IUserTypeService
    {
        Task<PaginationResponse<List<UserType>>> GetAll(string name, int page, int limit);
        Task<UserType?> SearchById(int id);
        Task<UserType?> SearchByName(string name);
        Task<UserType?> Create(UserTypeRequestDTO userTypeRequestDTO);
        Task<UserType?> Update(UserType userType, UserTypeRequestDTO userTypeRequestDTO);
        Task<bool> Deactivated(UserType userType);
        Task<bool> Delete(UserType userType);
    }
}
