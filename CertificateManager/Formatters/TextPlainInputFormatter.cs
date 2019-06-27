using Microsoft.AspNetCore.Mvc.Formatters;
using System;
using System.Text;
using System.Threading.Tasks;

namespace CertificateManager.Formatters
{
    /// <summary>
    /// Formatter that allows content of type text/plain        
    /// 
    /// </summary>
    public class TextPlainInputFormatter : TextInputFormatter
    {
        /// <summary>
        /// Constructs TextPlainInputFormatter. Adds SupportedEncodings
        /// </summary>
        public TextPlainInputFormatter()
        {
            SupportedMediaTypes.Add("text/plain");
            //SupportedMediaTypes.Add("text");
            SupportedEncodings.Add(UTF8EncodingWithoutBOM);
            SupportedEncodings.Add(UTF16EncodingLittleEndian);
        }

        /// <summary>
        /// Allow text/plain and no content type to
        /// be processed
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        protected override bool CanReadType(Type type)
        {
            return type == typeof(string);
        }

        /// <summary>
        /// Handle text/plain or no content type for string results
        /// Handle application/octet-stream for byte[] results
        /// </summary>
        /// <param name="context"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>    
        public override async Task<InputFormatterResult> ReadRequestBodyAsync(InputFormatterContext context, Encoding encoding)
        {
            string data = null;
            using (var streamReader = context.ReaderFactory(context.HttpContext.Request.Body, encoding))
            {
                data = await streamReader.ReadToEndAsync();
            }
            return InputFormatterResult.Success(data);
        }

        //public override Task<InputFormatterResult> ReadRequestBodyAsync(InputFormatterContext context, Encoding encoding)
        //{
        //    throw new NotImplementedException();
        //}
    }
}

