using AutoMapper;
using Microsoft.EntityFrameworkCore;
using minimarket_project_backend.Common.Responses;
using minimarket_project_backend.Dtos.Brand;
using minimarket_project_backend.Dtos.UserType;
using minimarket_project_backend.Helpers;
using minimarket_project_backend.Models;
using minimarket_project_backend.Utilities;

namespace minimarket_project_backend.Services.Implementation
{
    public class UserTypeService : IUserTypeService
    {
        private readonly DbMinimarketContext _dbcontext;
        private readonly IMapper _mapper;
        private readonly QueryHelper _queryHelper = new();
        private readonly ResponseHelper _responseHelper = new();

        public UserTypeService(DbMinimarketContext dbcontext, IMapper mapper)
        {
            _dbcontext = dbcontext;
            _mapper = mapper;
        }

        public async Task<PaginationResponse<List<UserType>>> GetAll(string name, int page, int limit)
        {
            IQueryable<UserType> query = _dbcontext.UserTypes;

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(m => m.Name.Contains(name))
                             .OrderBy(m => m.Id);
            }

            int totalRecords = await query.CountAsync();

            List<UserType> data = await _queryHelper.GetPaginatedList(query, page, limit);

            var paginationResponse = new PaginationResponse<List<UserType>>
            {
                Page = page,
                PageSize = limit,
                TotalRecords = totalRecords,
                TotalPages = (int)Math.Ceiling((double)totalRecords / limit),
                HasPreviousPage = page > 1,
                HasNextPage = page < (int)Math.Ceiling((double)totalRecords / limit),
                Data = data
            };

            return _responseHelper.CreatePaginationResponse(paginationResponse);
        }

        public async Task<UserType?> SearchById(int id)
        {
            try
            {
                var userType = await _dbcontext.UserTypes.FindAsync(id);

                if (userType == null) return null;

                return userType;

            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<UserType?> SearchByName(string name)
        {
            try
            {
                var userType = await _dbcontext.UserTypes.FirstOrDefaultAsync(x => x.Name == name);

                if (userType == null) return null;

                return userType;

            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<UserType?> Create(UserTypeRequestDTO userTypeRequestDTO)
        {
            try
            {
                var userType = _mapper.Map<UserType>(userTypeRequestDTO);

                _dbcontext.UserTypes.Add(userType);

                int filasAfectadas = await _dbcontext.SaveChangesAsync();

                if (userType.Id > 0 && filasAfectadas > 0)
                {
                    return userType;
                }

                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<UserType?> Update(UserType userType, UserTypeRequestDTO userTypeRequestDTO)
        {
            try
            {
                userType.Name = userTypeRequestDTO.name ?? userType.Name;
                userType.Description = userTypeRequestDTO.description ?? userType.Description;
                userType.Status = userTypeRequestDTO.status;

                _dbcontext.UserTypes.Update(userType);

                int filasAfectadas = await _dbcontext.SaveChangesAsync();

                if (userType.Id > 0 && filasAfectadas > 0)
                {
                    return userType;
                }

                return null;

            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool> Deactivated(UserType userType)
        {
            try
            {
                userType.Status = false;

                _dbcontext.UserTypes.Update(userType);

                int filasAfectadas = await _dbcontext.SaveChangesAsync();

                if (userType.Id > 0 && filasAfectadas > 0)
                {
                    return true;
                }

                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }


        public async Task<bool> Delete(UserType userType)
        {
            try
            {
                _dbcontext.UserTypes.Remove(userType);

                int filasAfectadas = await _dbcontext.SaveChangesAsync();

                if (userType.Id > 0 && filasAfectadas > 0)
                {
                    return true;
                }

                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}