using System.Runtime.Serialization;
using System.Text;

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

        /// <summary>
        /// The full Distinguished Name of the Certification Authority having produced the certificate to be used. 
        /// </summary>
        public string CertificateAuthority { get; set; }

        /// <summary>Returns a string that represents the current object.</summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return $"keyId=\"{KeyId},CA={CertificateAuthority}\",algorithm=\"{Algorithm}\",    headers=\"Digest X-Request - ID PSU - ID TPP - Redirect - URI Date\",    signature=\"{Signature}\" ";
        }
    }

}
