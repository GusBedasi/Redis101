
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Redis101.Handlers
{
    public class ErrorHandlerAttribute: ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            const int statusCode = (int)HttpStatusCode.InternalServerError;

            var response = context.HttpContext.Response;

            response.StatusCode = statusCode;
            response.ContentType = "application/json";

            //Log Exception message
            //Log Exception StackTrace

            context.Result = new JsonResult("Oops! Something wrong happened!");
        }
    }
}