using CertificateManager.SwaggerSchemaFilters;
using Swashbuckle.AspNetCore.Annotations;
using System.Runtime.Serialization;

namespace CertificateManager.Models
{
    /// <summary>
    /// Certificate Request, Generates a self-signed certifiacte.
    /// </summary>
    /// <remarks>If you don't need a EIDAS certifcate set EIDAS specific flags to false.</remarks>
    [SwaggerSchemaFilter(typeof(CertificateRequestSchemaFilter))]
    public class APICertificateRequest
    {
        /// <summary>
        /// Common name of the TPP company 
        /// </summary>        
        [DataMember(Name = "commonName")]
        public string CommonName { get; set; }
        /// <summary>
        /// City
        /// </summary>        
        [DataMember(Name = "city")]
        public string City { get; set; }

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
        /// aisp
        /// </summary>        
        [DataMember(Name = "aisp")]
        public bool Aisp { get; set; }

        /// <summary>
        /// aspsp - EIDAS specific 
        /// </summary>   
        /// <remarks>
        /// EIDAS specific 
        /// </remarks>
        [DataMember(Name = "aspsp")]
        public bool Aspsp { get; set; }


        /// <summary>
        /// piisp - EIDAS specific 
        /// </summary>        
        /// <remarks>
        /// EIDAS specific 
        /// </remarks>
        [DataMember(Name = "piisp")]
        public bool Piisp { get; set; }

        /// <summary>
        /// pisp - EIDAS specific 
        /// </summary>        
        /// <remarks>
        /// EIDAS specific 
        /// </remarks>
        [DataMember(Name = "pisp")]
        public bool Pisp { get; set; }

        /// <summary>
        /// pspAuthorityId  - EIDAS specific 
        /// </summary>        
        /// <remarks>
        /// EIDAS specific 
        /// </remarks>
        [DataMember(Name = "pspAuthorityId")]
        public string PspAuthorityId { get; set; }

        /// <summary>
        /// pspAuthorityName  - EIDAS specific 
        /// </summary>        
        /// <remarks>
        /// EIDAS specific 
        /// </remarks>
        [DataMember(Name = "pspAuthorityName")]
        public string PspAuthorityName { get; set; }

        /// <summary>
        /// pspAuthorzationNumber  - EIDAS specific 
        /// </summary>        
        /// <remarks>
        /// EIDAS specific 
        /// </remarks>
        [DataMember(Name = "pspAuthorzationNumber")]
        public string PspAuthorzationNumber { get; set; }

        
        /// <summary>
        /// state - EIDAS specific 
        /// </summary>        
        /// <remarks>
        /// EIDAS specific 
        /// </remarks>
        [DataMember(Name = "state")]
        public string State { get; set; }

        /// <summary>
        /// validity in days
        /// </summary>        
        /// <remarks>
        /// How many days the certificate should be valid
        /// </remarks>
        [DataMember(Name = "validity")]
        public int Validity { get; set; }
    }
}
