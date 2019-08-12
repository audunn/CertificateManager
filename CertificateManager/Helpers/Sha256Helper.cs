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
                var bytes = Encoding.UTF8.GetBytes(input);
                var hash = Convert.ToBase64String(hashEngine.ComputeHash(bytes, 0, bytes.Length));
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
            var key = CreateRSACryptoServiceProvider(privateKey);
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
        /// Verifies data using public key from certificate (SHA256) against given  value
        /// </summary>
        /// <param name="publicCert"></param>
        /// <param name="signature"></param>
        /// <param name="hash"></param>
        /// <returns></returns>
        public static bool VerifyHash(X509Certificate2 publicCert, byte[] signature, byte[] hash)
        {
            using (var rsaCSP = (RSACryptoServiceProvider)publicCert.PublicKey.Key)
            {                
                if (!rsaCSP.VerifyHash(hash,CryptoConfig.MapNameToOID("SHA256"), signature))
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
        public static bool VerifyData(string publicCert, byte[] data, byte[] signature)
        {
            //buffer for encoding
            publicCert = publicCert.Replace("-----BEGIN CERTIFICATE-----", "");
            publicCert = publicCert.Replace("-----END CERTIFICATE-----", "");
            byte[] base64EncodedBytes = new byte[GetBase64BufferLength(publicCert)];
            if (Convert.TryFromBase64String(publicCert, base64EncodedBytes, out int bytesWritten) && bytesWritten > 0)
            {
                var clientCertificate = new X509Certificate2(base64EncodedBytes);
                return VerifyData(clientCertificate, data, signature);
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// Verifies that a digital signature is valid (SHA256),  
        /// </summary>
        /// <param name="publicCert"></param>
        /// <param name="data"></param>
        /// <param name="signature"></param>
        /// <returns></returns>
        public static bool VerifyData(X509Certificate2 publicCert, byte[] data, byte[] signature)
        {
            using (var key = publicCert.GetRSAPublicKey())
            {
                if (key != null)
                {
                    return key.VerifyData(data, signature, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
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
        internal static int GetBase64BufferLength(string base64String)
        {
            //x = (n * (3/4)) - y; y will be 2 if Base64 ends with '==' and 1 if Base64 ends with '='.
            var base64Length = ((base64String.Length * 3) + 3) / 4 - GetBase64BufferLengthY(base64String);

            return base64Length;
        }
        internal static int GetBase64BufferLengthY(string base64String)
        {
            // y will be 2 if Base64 ends with '==' and 1 if Base64 ends with '='.
            if (base64String.Length > 0 && base64String[base64String.Length - 1] == '=')
            {
                if (base64String.Length > 1 && base64String[base64String.Length - 2] == '=')
                {
                    return 2;
                }

                return 1;
            }

            return 0;
        }
    }
}
