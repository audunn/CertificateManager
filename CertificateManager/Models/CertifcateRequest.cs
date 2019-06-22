using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CertificateManager.Models
{
    /// <summary>
    /// Certificate Request
    /// </summary>
    public class APICertificateRequest
    {
        /// <summary>
        /// Common name of the TPP company (e.g. "Brinklyfy.io").
        /// </summary>
        public string CommonName { get; set; }

        /// <summary>
        /// Country as ISO 3166-1 Alpha 2 (e.g. FI).
        /// </summary>
        public string Country{ get; set; }
    }
}
