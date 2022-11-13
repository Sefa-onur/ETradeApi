using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using System.Text.Json;
using System.Net;
using System.Net.Mime;

namespace ETradeApi.API.Extensions
{
    static public class ConfigureExeptionHandlerExtensions
    {
        public static void ConfigureExeptionHandler<T>(this WebApplication application,ILogger<T> logger)
        {
            application.UseExceptionHandler(builder =>
            {
                builder.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = MediaTypeNames.Application.Json;

                    var contentFeature = context.Features.Get<IExceptionHandlerFeature>();

                    if(contentFeature != null)
                    {
                        await context.Response.WriteAsync(JsonSerializer.Serialize(new
                        {
                            StatusCode = context.Response.StatusCode,
                            Message = contentFeature.Error.Message,
                            Title = "Hata Alındı"
                        }));
                    }
                });
            });
        }
    }
}
