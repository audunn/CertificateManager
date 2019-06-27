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

        /// <summary>
        /// Signs data with private key using SHA256
        /// </summary>
        /// <param name="privateKey"></param>
        /// <param name="dataToSign"></param>
        /// <returns></returns>
        public static byte[] SignData(string privateKey, byte[] dataToSign)
        {
            var key = PrivateKeyFromPem(privateKey);
            //bouncy
            byte[] sig = key.SignData(dataToSign, CryptoConfig.MapNameToOID("SHA256"));
            return sig;

            //.net
            //RSA.Create(new RSAParameters().)
            //using (RSA rsa = certificate.GetRSAPrivateKey())
            //{
            //    return rsa.SignData(dataToSign, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
            //}
        }

        /// <summary>
        /// Verifies signature using public key from certificate (SHA256)
        /// </summary>
        /// <param name="publicCert"></param>
        /// <param name="data"></param>
        /// <param name="sig"></param>
        /// <returns></returns>
        public static bool VerifySignature(X509Certificate2 publicCert, byte[] data, byte[] sig)
        {
            var key = (RSACryptoServiceProvider)publicCert.PublicKey.Key;
            if (!key.VerifyData(data, CryptoConfig.MapNameToOID("SHA256"), sig))
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Read Key from pem encoded string 
        /// </summary>
        /// <param name="pemString"></param>
        /// <returns></returns>
        private  static RSACryptoServiceProvider PrivateKeyFromPem(String pemString)
        {
            using (TextReader privateKeyTextReader = new StringReader(pemString))
            {
                AsymmetricCipherKeyPair readKeyPair = (AsymmetricCipherKeyPair)new PemReader(privateKeyTextReader).ReadObject();


                RsaPrivateCrtKeyParameters privateKeyParams = ((RsaPrivateCrtKeyParameters)readKeyPair.Private);
                RSACryptoServiceProvider cryptoServiceProvider = new RSACryptoServiceProvider();
                RSAParameters parms = new RSAParameters();

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
