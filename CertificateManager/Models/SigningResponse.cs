using System.Runtime.Serialization;
using System.Text;

namespace CertificateManager.Models
{
    /// <summary>
    /// Signing Response
    /// <para>
    ///  This class provides a HTTP Signature Header that can be used by a client to authenticate the sender of a message and ensure that 
    ///  particular data have not been modified in transit. as described in NextGenPSD2 XS2A Framework Implementation Guidelines (based on https://tools.ietf.org/pdf/draft-cavage-http-signatures-10.pdf#8)    
    /// </para>
    /// <para>
    ///  example
    ///  Signature: keyId="9FA1,CA=CA",algorithm="rsa-sha512",    headers="Digest X-Request - ID PSU - ID TPP - Redirect - URI Date",    signature="M39kC9uIRN4cx/6XJ866cdbTR9gEB18jq/eCqpkR+RDO58CaXUcHP5p625PBXL/KWl0HPvSOZBE1VLDvhPaBe5mFbG7geP338r5XnwP+AFYQaehpC/OloJtbQNUR2xfzDcYi9ocUlh5dYVy2B2w5TnsG1CsA2rF6Afdu23CKz2aRgV1t9JSLAY13wViwe7sRT+oXKl/OR+v9iyD0ZAZs15b89dqiV5zcjf1owgyAeF9DS1agHdzFpeM35Fz0JBJbs+tGjnMJ0I7/2/acu7Z7zooDPcefr4Og+julXqSbbiV0IZxqFjSF+sLkb2ZuH6fg4k4PjIaCUTHgRKsPjPRgzg==" 
    /// </para>
    ///</summary>    
    public class SigningResponse
    {
        /// <summary>
        /// The ‘keyId‘ field is an opaque string that the server can use to look up the component they need to validate the signature
        /// </summary>
        /// <remarks>
        ///  It shall be formatted as follows: keyId="SN=XXX,CA=YYYYYY YYYYYYYYYY" 
        ///  where “XXX" is the serial number of the certificate in hexadecimal coding given in the TPP-Signature-CertificateHeader and "YYYYYYYYYYYYYYYY" is the full Distinguished Name of 
        ///  the Certification Authority having produced this certificate. 
        /// </remarks>
        [DataMember(Name = "keyId")]
        public string KeyId { get; set; }

        /// <summary>
        ///  The ‘algorithm‘ parameter is used to specify the digital signature algorithm to use when generating the signature.  Valid   values for 
        ///  this parameter can be found in the Signature Algorithms registry located at http://www.iana.org/assignments/signature-algorithms [6] and MUST NOT be marked "deprecated".
        ///  It must identify SHA-256 or SHA-512 as Hash algorithm
        /// </summary>
        /// <remarks>
        ///   It is preferred that the algorithm used by an implementation be derived from the key metadata identified by the 'keyId' rather than from this field. […] The 'algorithm' parameter [..] will most likely be deprecated in the future.        
        /// </remarks>
        [DataMember(Name = "algorithm")]
        public string Algorithm { get; set; }

        /// <summary>
        ///  The "Headers" parameter is used to specify the list of HTTP headers included when generating the signature for the message.If specified, it should be a lowercased, quoted list of HTTP header fields
        /// </summary>
        /// <remarks>
        ///  Mandatory. 
        ///  Must include  
        ///      * "Digest",  
        ///      * "X-Request-ID",  
        ///      * "PSU-ID" (if and only if "PSU-ID" is included as a header of the HTTP-Request). 
        ///      * "PSU-Corporate-ID" (if and only if "PSUCorporate-ID" is included as a header of the HTTP-Request). 
        ///      * "Date" 
        ///      * "TPP-Redirect-URI"(if and only if "TPPRedirect-URI" is included as a header of the HTTP-Request). 
        ///  No other entries may be included.
        /// </remarks>
        [DataMember(Name = "headers")]
        public string Headers { get; set; }

        /// <summary>
        ///  The ‘signature‘ parameter is a base 64 encoded digital   signature, as described in RFC 4648[RFC4648], Section 4 [5]
        /// </summary>
        /// <remarks>
        ///  The client uses the `algorithm` and `headers` signature parameters to form a canonicalised `signing string`. This `signing string` is then signed with 
        ///  the key associated with `keyId` and the algorithm corresponding to `algorithm`.  The `signature` parameter is then set to the base 64 encoding of the signature.         
        /// </remarks>
        [DataMember(Name = "signature")]
        public string Signature { get; set; }

        /// <summary>
        ///  The full Distinguished Name of the Certification Authority having produced the certificate to be used. 
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
