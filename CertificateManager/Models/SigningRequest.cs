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
        /// Signing data, String base64 encoded to survive endcoding and transport through transport layers
        /// </summary>
        [DataMember(Name = "dataToSign")]
        public string DataToSign { get; set; }

        /// <summary>
        /// keyID, key id that the server can use to look up the component they need to validate the signature.
        /// Will be returned in the singin header string 
        /// This is the Serial Number of the TPP's certificate included in the "TPP-Signature-Certificate" header in requests to PSD2 API'S 
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

        /// <summary>
        /// The full Distinguished Name of the Certification Authority having produced the certificate to be used. 
        /// </summary>
        public string CertificateAuthority { get; set; }
    }
}
