using LS.Domain.Core.DomainObjects;
using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using System.Threading.Tasks;

namespace LS.WebApi.Extensions
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch(DomainException domainException) 
            {
                HandleDomainExceptionAsync(httpContext, domainException);
            }
            catch (Exception ex)
            {
                HandleExceptionAsync(httpContext, ex);
            }
        }

        private static void HandleExceptionAsync(HttpContext context, Exception exception)
        {   
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            context.Response.WriteAsync(new ErroDetalhes()
            {
                Status = context.Response.StatusCode,
                Message = "Ateção! Erro não esperado."
            }.ToString());
        }

        private static void HandleDomainExceptionAsync(HttpContext context, DomainException domainException)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            context.Response.WriteAsync(new ErroDetalhes()
            {
                Status = context.Response.StatusCode,
                Message = domainException.Message,
                Exception = domainException.InnerException
            }.ToString());
        }
    }
}
