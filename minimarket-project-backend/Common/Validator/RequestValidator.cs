namespace minimarket_project_backend.Common.Validator
{
    public class RequestValidator
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
