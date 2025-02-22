using Microsoft.AspNetCore.Mvc;
using minimarket_project_backend.Models.Responses;

namespace minimarket_project_backend.Validation
{
    public class MethodsHTTPValidator
    {
        public bool ValidatePagination(int page, int limit)
        {
            if (page < 0 || limit < 0) return false;

            return true;
        }        
        
        public bool ValidateId(int id)
        {
            if (id <= 0)
            {
                return false;
            }

            return true;
        }
    }
}
