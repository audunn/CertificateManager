using CertificateManager.Models;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CertificateManager.SwaggerSchemaFilters
{

    /// <summary>
    /// Defines the sample CertificateRequestSchemaFilter schema 
    /// </summary>
    public class CertificateRequestSchemaFilter : ISchemaFilter
    {
        /// <summary>
        /// Applies the sample CertificateRequestSchemaFilter schema 
        /// </summary>
        /// <param name="schema"></param>
        /// <param name="context"></param>
        public void Apply(Schema schema, SchemaFilterContext context)
        {
            //For some reason the SchemaFilter annotation on the schema type does not work so we add in startup
            //this would make it run for all actions, but we check the type here so they are applied only
            //to the right actions
            if (context.SystemType == typeof(APICertificateRequest))
            {
                schema.Example = new APICertificateRequest
                {
                    Aisp = true,
                    Aspsp = true,
                    City = "Nuremberg",
                    CommonName = "MatrixAPI.PSD2_Client",
                    Country = "Germany",
                    Organization = "org",
                    OrganizationUnit = "ou",
                    Piisp = true,
                    Pisp = true,
                    PspAuthorityId = "Germany",
                    PspAuthorityName = "Auth",
                    PspAuthorzationNumber = "12345987",
                    State = "Bayern",
                    Validity = 365
                };
            }
        }
    }
}
