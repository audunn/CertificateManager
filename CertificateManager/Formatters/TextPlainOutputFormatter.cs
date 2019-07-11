using CertificateManager.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CertificateManager.Formatters
{
    /// <summary>
    /// TextPlainOutputFormatter
    /// </summary>
    public class TextPlainOutputFormatter : TextOutputFormatter
    {
        /// <summary>
        /// TextPlainOutputFormatter Formatter
        /// </summary>
        public TextPlainOutputFormatter()
        {
            SupportedMediaTypes.Add(Microsoft.Net.Http.Headers.MediaTypeHeaderValue.Parse("text/plain"));
            SupportedEncodings.Add(Encoding.UTF8);
            SupportedEncodings.Add(Encoding.Unicode);
        }

        /// <summary>
        /// Returns a value indicating whether or not the given type can be written by this serializer.
        /// </summary>
        /// <param name="type">The object type.</param>
        /// <returns><c>true</c> if the type can be written, otherwise <c>false</c>.</returns>
        protected override bool CanWriteType(Type type)
        {
            if (typeof(SigningResponse).IsAssignableFrom(type) || typeof(IEnumerable<SigningResponse>).IsAssignableFrom(type))
            {
                return base.CanWriteType(type);
            }
            return false;
        }
        /// <summary>
        /// Writes the response body.
        /// </summary>
        /// <param name="context">The formatter context associated with the call.</param>
        /// <param name="selectedEncoding">The <see cref="Encoding" /> that should be used to write the response.</param>
        /// <returns>A task which can write the response body.</returns>
        public override Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
        {
            IServiceProvider serviceProvider = context.HttpContext.RequestServices;
            var logger = serviceProvider.GetService(typeof(ILogger<TextPlainOutputFormatter>)) as ILogger;
            var response = context.HttpContext.Response;
            var signingResponse = context.Object as SigningResponse;
            logger.LogInformation($"Writing {signingResponse.ToString()} ");
            return response.WriteAsync(signingResponse.ToString(), selectedEncoding);
        }
    }
}
