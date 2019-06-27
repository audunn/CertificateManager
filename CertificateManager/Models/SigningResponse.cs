using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace CertificateManager.Models
{
    /// <summary>
    /// Signing Response
    /// </summary>
    public class SigningResponse
    {
        /// <summary>
        /// keyId 
        /// </summary>
        [DataMember(Name = "keyId")]
        public string KeyId { get; set; }

        /// <summary>
        /// Algorithm 
        /// </summary>
        [DataMember(Name = "algorithm")]
        public string Algorithm { get; set; }

        /// <summary>
        /// headers 
        /// </summary>
        [DataMember(Name = "headers")]
        public string Headers { get; set; }

        /// <summary>
        /// signature 
        /// </summary>
        [DataMember(Name = "ignature")]
        public string Signature { get; set; }
    }
}
