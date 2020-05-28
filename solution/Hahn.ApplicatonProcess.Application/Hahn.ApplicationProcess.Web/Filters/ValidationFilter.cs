using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hahn.ApplicationProcess.Web.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Hahn.ApplicationProcess.Web.Filters
{
    public class ValidationFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            //vorher
            if(!context.ModelState.IsValid)
            {
                var errorModelStates = context.ModelState.Where(x => x.Value.Errors.Count > 0).ToDictionary(k => k.Key, k => k.Value.Errors.Select(x => x.ErrorMessage)).ToArray();

                var errorResponse = new ErrorResponse();
                foreach(var errr in errorModelStates)
                {
                    foreach(var suberr in errr.Value)
                    {
                        var errModel = new ErrorModel
                        { FieldName = errr.Key,
                            Message = suberr                           
                        };

                        errorResponse.Errors.Add(errModel);

                    }
                }
                context.Result = new BadRequestObjectResult(errorResponse);
                return;
            }


            await next();

            //nacher
        }
    }
}
