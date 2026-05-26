using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using MultiTenantEmployeeApi.Domain.Exceptions;

namespace MultiTenantEmployeeApi.Api.Filters
{
    public class ResponseWrapperFilter : IResultFilter
    {
        public void OnResultExecuting(ResultExecutingContext context)
        {
            if (context.Result is ObjectResult objectResult)
            {
                var wrapped = new ApiResponseWrapper<object>
                {
                    Data = objectResult.Value,
                    Success = objectResult.StatusCode < 400
                };

                context.Result = new ObjectResult(wrapped)
                {
                    StatusCode = objectResult.StatusCode
                };
            }
        }

        public void OnResultExecuted(ResultExecutedContext context)
        {
           
        }
    }
}
