using Newtonsoft.Json;

namespace CertificateManager.Models
{
    /// <summary>
    /// Hodlds details of error messages.
    /// </summary>
    public class ErrorDetails
    {
        /// <summary>
        /// The status code to set on the response.
        /// </summary>
        public int StatusCode { get; set; }

        /// <summary>
        /// The message set on the response.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Returns a string that represents the current object in json.
        /// </summary>
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
