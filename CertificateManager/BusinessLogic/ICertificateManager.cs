using CertificateManager.Models;

namespace CertificateManager.BusinessLogic
{
    /// <summary>
    /// CertificateManager logic
    /// </summary>
    public interface ICertificateManager
    {
        /// <summary>
        /// Generates a self signed EIDAS certificate
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        CertificateResponse GenerateSelfSignedCertificate(APICertificateRequest request);
    }
}
