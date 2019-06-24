using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace CertificateManager.Models
{
    /// <summary>
    /// Certificate Response
    /// </summary>
    public class CertificateResponse
    {
        /// <summary>
        /// Certificate base64 encoded
        /// </summary>
        [DataMember(Name = "encodedCert")]
        public string EncodedCert { get; set; }

        /// <summary>
        /// Certificate private key
        /// </summary>
        [DataMember(Name = "privateKey")]
        public string PrivateKey { get; set; }
    }
}
