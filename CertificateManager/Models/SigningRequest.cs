using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using CertificateManager.SwaggerSchemaFilters;
using Swashbuckle.AspNetCore.Annotations;

namespace CertificateManager.Models
{
    /// <summary>
    /// SigningRequest
    /// </summary>    
    public class SigningRequest
    {
        /// <summary>
        /// Algorithm 
        /// </summary>
        [DataMember(Name = "algorithm")]

        public string Algorithm { get; set; }

        /// <summary>
        /// Algorithm 
        /// </summary>
        [DataMember(Name = "digest")]
        public string Digest { get; set; }

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
