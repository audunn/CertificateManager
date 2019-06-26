using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CertificateManager.Helpers
{
    /// <summary>
    /// Calcuate sha256 hash
    /// </summary>
    public static class Sha256Helper
    {
        /// <summary>
        /// Computes the hash value for the input data.
        /// </summary>
        /// <param name="input"></param>
        /// <returns>SHA256 Hash string </returns>
        public static string GenerateHash(string input)
        {
            // Use a new engine every time
            using (var hashEngine = SHA256.Create())
            {                
                var bytes = Encoding.Unicode.GetBytes(input);
                var hash = HexStringFromBytes(hashEngine.ComputeHash(bytes, 0, bytes.Length));
                return hash;
            }
        }

        private static string HexStringFromBytes(IEnumerable<byte> bytes)
        {
            var sb = new StringBuilder();

            foreach (var b in bytes)
            {
                var hex = b.ToString("x2");
                sb.Append(hex);
            }

            return sb.ToString();
        }
    }
}
