using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mime;
using System.Threading.Tasks;
using Common.Exceptions;
using Common.Extensions;
using Microsoft.AspNetCore.Http;
using Model;

namespace API.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        public RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (InnerException ex)
            {
                context.Response.ContentType = MediaTypeNames.Application.Json;
                var status = (int)HttpStatusCode.BadRequest;
                context.Response.StatusCode = status;
                var model = new BadRequestModel
                {
                    Status = status,
                    Title = "Model data error",
                    ExceptionCode = ex.ExceptionCode,
                    Errors = new Dictionary<string, IEnumerable<string>>
                    {
                        { ex.PropertyName ?? "Alert", new string[]{ ex.Message } }
                    }
                };

                await context.Response.WriteAsync(model.ToJson());
            }
            catch (Exception ex)
            {
                context.Response.ContentType = MediaTypeNames.Application.Json;
                var status = (int)HttpStatusCode.InternalServerError;
                context.Response.StatusCode = status;
                var model = new BadRequestModel
                {
                    Status = status,
                    Title = "Internal Server Error",
                    Errors = new Dictionary<string, IEnumerable<string>>
                    {
                        { "Alert", new string[]{ ex.Message } }
                    }
                };

                await context.Response.WriteAsync(model.ToJson());
            }
        }
    }
}