using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace CertificateManager.Models
{
    /// <summary>
    /// Signing algoritm
    /// </summary>
    public enum SigningAlgoritm
    {
        /// <summary>
        /// SHA256 WITH RSA
        /// </summary>
        [Description("rsa-sha256")]
        SHA256WITHRSA,

        /// <summary>
        /// SHA512 WITH RSA
        /// </summary>
        [Description("rsa-sha512")]
        SHA512WITHRSA,
    }
}
