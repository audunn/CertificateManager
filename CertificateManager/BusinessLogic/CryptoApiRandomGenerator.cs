using Org.BouncyCastle.Crypto.Prng;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace CertificateManager.BusinessLogic
{
    /// <summary>
    ///  Random number generator for Bouncy Castle. Uses Microsoft's RNGCryptoServiceProvider
    /// </summary>
    public class CryptoApiRandomGenerator: IRandomGenerator
    {
        private readonly RNGCryptoServiceProvider rndProv;

        /// <summary>
        /// Constructs the CryptoApiRandomGenerator
        /// </summary>
        public CryptoApiRandomGenerator()
        {
            rndProv = new RNGCryptoServiceProvider();
        }

        #region IRandomGenerator Members

        /// <inheritdoc/>
        public virtual void AddSeedMaterial(byte[] seed)
        {
        }

        /// <inheritdoc/>
        public virtual void AddSeedMaterial(long seed)
        {
        }

        /// <inheritdoc/>
        public virtual void NextBytes(byte[] bytes)
        {
            rndProv.GetBytes(bytes);
        }

        /// <inheritdoc/>
        public virtual void NextBytes(byte[] bytes, int start, int len)
        {
            if (start < 0)
                throw new ArgumentException("Start offset cannot be negative", nameof(start));
            if (bytes.Length < (start + len))
                throw new ArgumentException("Byte array too small for requested offset and length");

            if (bytes.Length == len && start == 0)
            {
                NextBytes(bytes);
            }
            else
            {
                byte[] tmpBuf = new byte[len];
                rndProv.GetBytes(tmpBuf);
                Array.Copy(tmpBuf, 0, bytes, start, len);
            }
        }
        #endregion

    }
}
