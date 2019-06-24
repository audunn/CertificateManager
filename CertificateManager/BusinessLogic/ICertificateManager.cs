using CertificateManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CertificateManager.BusinessLogic
{
    /// <summary>
    /// CertificateManager logic
    /// </summary>
    public interface ICertificateManager
    {
        /// <summary>
        /// Generates a self signed certificate
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        string GenerateSelfSignedCertificate(APICertificateRequest request);

        /// <summary>
        /// Generates a self signed EIDAS certificate
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        CertificateResponse GenerateEIDASSelfSignedCertificate(APICertificateRequest request);
    }
}
