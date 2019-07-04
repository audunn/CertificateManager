using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.OpenSsl;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace CertificateManager.Helpers
{
    /// <summary>
    /// Calcuate Sha512 hash
    /// </summary>
    public static class Sha512Helper
    {
        /// <summary>
        /// Computes the hash value for the input data.
        /// </summary>
        /// <param name="input"></param>
        /// <returns>SHA512 Hash string </returns>
        public static string GenerateHash(string input)
        {
            // Use a new engine every time
            //input = TrimAllWithInplaceCharArray(input);
            using (var hashEngine = SHA512.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(input);
                var hash = Convert.ToBase64String(hashEngine.ComputeHash(bytes, 0, bytes.Length));
                return hash;
            }
        }

        /// <summary>
        /// Signs data with private key using SHA512
        /// </summary>
        /// <param name="privateKey"></param>
        /// <param name="dataToSign"></param>
        /// <returns></returns>
        public static byte[] SignData(string privateKey, byte[] dataToSign)
        {
            using (var key = CreateRSACryptoServiceProvider(privateKey))
            {
                byte[] sig = key.SignData(dataToSign, CryptoConfig.MapNameToOID("SHA512"));
                return sig;
            }
        }

        /// <summary>
        /// Verifies data using public key from certificate (SHA512) against given  value
        /// </summary>
        /// <param name="publicCert"></param>
        /// <param name="signature"></param>
        /// <param name="hash"></param>
        /// <returns></returns>
        public static bool VerifyHash(byte[] hash, X509Certificate2 publicCert, byte[] signature )
        {
            using (var rsaCSP = (RSACryptoServiceProvider)publicCert.PublicKey.Key)
            {
                if (!rsaCSP.VerifyHash(hash,CryptoConfig.MapNameToOID("SHA512"), signature))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Verifies that a digital signature is valid (SHA512),  
        /// </summary>
        /// <param name="publicCert"></param>
        /// <param name="data"></param>
        /// <param name="signature"></param>
        /// <returns></returns>
        public static bool VerifyData(X509Certificate2 publicCert, byte[] data, byte[] signature)
        {
            using (var key = (RSACryptoServiceProvider)publicCert.PublicKey.Key)
            {
                if (!key.VerifyData(data, CryptoConfig.MapNameToOID("SHA512"), signature))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Read Key from pem encoded string 
        /// </summary>
        /// <param name="pemString"></param>
        /// <returns></returns>
        private  static RSACryptoServiceProvider CreateRSACryptoServiceProvider (String pemString)
        {
            using (TextReader privateKeyTextReader = new StringReader(pemString))
            {
                AsymmetricCipherKeyPair readKeyPair = (AsymmetricCipherKeyPair)new PemReader(privateKeyTextReader).ReadObject();

                
                RsaPrivateCrtKeyParameters privateKeyParams = ((RsaPrivateCrtKeyParameters)readKeyPair.Private);
                RSACryptoServiceProvider cryptoServiceProvider = new RSACryptoServiceProvider();
                RSAParameters parms = new RSAParameters();
                var keyid = privateKeyParams.Modulus;

                parms.Modulus = privateKeyParams.Modulus.ToByteArrayUnsigned();
                parms.P = privateKeyParams.P.ToByteArrayUnsigned();
                parms.Q = privateKeyParams.Q.ToByteArrayUnsigned();
                parms.DP = privateKeyParams.DP.ToByteArrayUnsigned();
                parms.DQ = privateKeyParams.DQ.ToByteArrayUnsigned();
                parms.InverseQ = privateKeyParams.QInv.ToByteArrayUnsigned();
                parms.D = privateKeyParams.Exponent.ToByteArrayUnsigned();
                parms.Exponent = privateKeyParams.PublicExponent.ToByteArrayUnsigned();

                cryptoServiceProvider.ImportParameters(parms);

                return cryptoServiceProvider;
            }
        }
    }
}
