using CertificateManager.SwaggerSchemaFilters;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace CertificateManager.Models
{
    /// <summary>
    /// SigningRequest
    /// </summary>    
    [SwaggerSchemaFilter(typeof(SigningRequestSchemaFilter))]
    public class SigningRequest
    {
        /// <summary>
        /// Algorithm, Available values : SHA256WITHRSA, SHA512WITHRSA
        /// </summary>
        [DataMember(Name = "algorithm")]
        public SigningAlgoritm Algorithm { get; set; }

        /// <summary>
        /// Algorithm 
        /// </summary>
        [DataMember(Name = "digest")]
        public string Digest { get; set; }

        /// <summary>
        /// Signing data
        /// </summary>
        [DataMember(Name = "dataToSign")]
        public string DataToSign { get; set; }

        /// <summary>
        /// keyID 
        /// </summary>
        [DataMember(Name = "keyID")]
        public string KeyID { get; set; }

        /// <summary>
        /// privateKey 
        /// </summary>
        [DataMember(Name = "privateKey")]
        public string PrivateKey { get; set; }

        /// <summary>
        /// psuID 
        /// </summary>
        [DataMember(Name = "psuID")]
        public string PsuID { get; set; }

        /// <summary>
        /// timestamp 
        /// </summary>
        [DataMember(Name = "timestamp")]
        public string Timestamp { get; set; }

        /// <summary>
        /// tppRequestID 
        /// </summary>
        [DataMember(Name = "tppRequestID")]
        public string TppRequestID { get; set; }

        /// <summary>
        /// tppTransactionID 
        /// </summary>
        [DataMember(Name = "tppTransactionID")]
        public string TppTransactionID { get; set; }
    }
}
