using Microsoft.AspNetCore.Builder;

namespace CertificateManager.Middleware
{
    /// <summary>
    /// Exteions to configure exceptionHandler
    /// </summary>
    public static class ExceptionMiddlewareExtensions
    {
        /// <summary>
        /// ConfigureExceptionHandler
        /// </summary>
        /// <param name="app"></param>
        public static void ConfigureCustomExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
