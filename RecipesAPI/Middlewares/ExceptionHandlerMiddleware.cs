using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PoS.Core.Exceptions;
using System.Net;
using System.Text.Json;

namespace RecipesAPI.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionsAsync(context, ex);
            }
        }

        public async Task HandleExceptionsAsync(HttpContext context, Exception ex)
        {
            HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
            string message = "Unexpected internal server error";

            if(ex is DbUpdateConcurrencyException)
            {
                message = "Entity was not updated, because the version is outdated";
            }

            if (ex is ApiException)
            {
                statusCode = ((ApiException)ex).StatusCode;
                message = ex.Message;
            }

            context.Response.StatusCode = (int)statusCode;
            context.Response.ContentType = "application/json";

            var problemDetails = new ProblemDetails();

            problemDetails.Status = (int)statusCode;
            problemDetails.Title = message;

            var result = JsonSerializer.Serialize(problemDetails);

            await context.Response.WriteAsync(result);

        }
    }
}
