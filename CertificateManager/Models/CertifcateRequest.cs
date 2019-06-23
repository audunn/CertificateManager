using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace CertificateManager.Models
{
    /// <summary>
    /// Certificate Request
    /// </summary>
    public class APICertificateRequest
    {
        /// <summary>
        /// aisp
        /// </summary>        
        [DataMember(Name = "aisp")]
        public bool Aisp { get; set; }

        /// <summary>
        /// aspsp
        /// </summary>        
        [DataMember(Name = "aspsp")]
        public bool Aspsp { get; set; }

        /// <summary>
        /// City
        /// </summary>        
        [DataMember(Name = "city")]
        public string City { get; set; }

        /// <summary>
        /// Common name of the TPP company 
        /// </summary>        
        [DataMember(Name = "commonName")]
        public string CommonName { get; set; }

        /// <summary>
        /// Country as ISO 3166-1 Alpha 2 (e.g. FI).
        /// </summary>
        [DataMember(Name = "Country")]
        public string Country { get; set; }

        /// <summary>
        /// organization 
        /// </summary>
        [DataMember(Name = "organization")]
        public string Organization { get; set; }

        /// <summary>
        /// organizationUnit 
        /// </summary>
        [DataMember(Name = "organizationUnit")]
        public string OrganizationUnit { get; set; }

        /// <summary>
        /// piisp
        /// </summary>        
        [DataMember(Name = "piisp")]
        public bool Piisp { get; set; }

        /// <summary>
        /// pisp
        /// </summary>        
        [DataMember(Name = "pisp")]
        public bool Pisp { get; set; }

        /// <summary>
        /// pspAuthorityId
        /// </summary>        
        [DataMember(Name = "pspAuthorityId")]
        public string PspAuthorityId { get; set; }

        /// <summary>
        /// pspAuthorityName
        /// </summary>        
        [DataMember(Name = "pspAuthorityName")]
        public string pspAuthorityName { get; set; }

        /// <summary>
        /// state
        /// </summary>        
        [DataMember(Name = "state")]
        public string State { get; set; }

        /// <summary>
        /// validity
        /// </summary>        
        [DataMember(Name = "validity")]
        public string Validity { get; set; }
    }
}
