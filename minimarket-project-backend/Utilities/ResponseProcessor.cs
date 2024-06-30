using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using minimarket_project_backend.Models.Responses;

namespace minimarket_project_backend.Utilities
{
    public class ResponseProcessor
    {
        public IActionResult ProcessDataResponse<T>(DataResponse<List<T>> response)
        {
            if (response.Data == null) return new ObjectResult
                                                              (new
                                                              {
                                                                  response.StatusCode,
                                                                  response.Message,
                                                                  response.Error,
                                                                  response.Success
                                                              }){ StatusCode = 500 };
                                                                           
            if (response.Data.Count == 0) return new NotFoundObjectResult
                                                                     (new
                                                                     {
                                                                         response.StatusCode,
                                                                         response.Message,
                                                                         response.Success,
                                                                         response.Data
                                                                     });

            return new OkObjectResult
                      (new
                      {
                          response.StatusCode,
                          response.Message,
                          response.Success,
                          response.Data
                      });
        }

        public IActionResult ProcessSingleDataResponse<T>(DataResponse<T> response)
        {

            if (!response.Success) return new ObjectResult
                                                         (new
                                                         {
                                                             response.StatusCode,
                                                             response.Message,
                                                             response.Error,
                                                             response.Success
                                                         }){ StatusCode = 500 };

            return new OkObjectResult
                     (new
                     {
                         response.StatusCode,
                         response.Message,
                         response.Success,
                         response.Data
                     });
        }

        public IActionResult ProcessApiResponse(ApiResponse response)
        {
            if (!response.Success) return new ObjectResult
                                                         (new
                                                         {
                                                             response.StatusCode,
                                                             response.Message,
                                                             response.Error,
                                                             response.Success
                                                         }){ StatusCode = 500 };

            return new OkObjectResult
                      (new 
                      {
                          response.StatusCode,
                          response.Message,
                          response.Success
                      });
        }
    }
}
