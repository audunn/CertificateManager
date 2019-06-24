using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using CertificateManager.Models;
using Microsoft.Extensions.Logging;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Operators;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Crypto.Prng;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Pkcs;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.X509;
using Org.BouncyCastle.X509.Extension;

namespace CertificateManager.BusinessLogic
{
    /// <inheritdoc/>
    public class CertificateManagerBL : ICertificateManager
    {
        private readonly ILogger<CertificateManagerBL> _logger;
        /// <summary>
        /// Load logger
        /// </summary>
        /// <param name="logger">mapper</param>        
        public CertificateManagerBL(ILogger<CertificateManagerBL> logger)
        {
            _logger = logger;
        }

        /// <inheritdoc />        
        public string GenerateSelfSignedCertificate(APICertificateRequest request)
        {
            _logger.LogDebug("Create Certificate for Certificate request");
            var cert = CreateCertificate(request);
            return GetCertificateAsString(cert);
        }

        /// <inheritdoc />
        public CertificateResponse GenerateEIDASSelfSignedCertificate(APICertificateRequest request)
        {
            //var certificate = CreateCertificate(request, true);            
            AsymmetricKeyParameter myCAprivateKey = null;
            _logger.LogDebug("Create CA Certificate for EIDAS Certificate request");
            X509Certificate2 certificateAuthorityCertificate = CreateCertificateAuthorityCertificate($"CN={request.CommonName}CA", ref myCAprivateKey);
            _logger.LogDebug("Creating certificate based on CA");
            //X509Certificate2 certificate = CreateSelfSignedCertificateBasedOnCertificateAuthorityPrivateKey("CN=" + certSubjectName, "CN=" + certSubjectName + "CA", myCAprivateKey);
            X509Certificate2 certificate = CreateSelfSignedCertificateBasedOnCertificateAuthorityPrivateKey($"CN={request.CommonName}", $"CN={request.CommonName}CA", myCAprivateKey);
            TextWriter textWriter = new StringWriter();
            PemWriter pemWriter = new PemWriter(textWriter);
            pemWriter.WriteObject(myCAprivateKey);
            pemWriter.Writer.Flush();

            string privateKey = textWriter.ToString();
            CertificateResponse response = new CertificateResponse() { EncodedCert = GetCertificateAsString(certificate), PrivateKey = privateKey };
            return response;
        }

        private static string GetCertificateAsString(X509Certificate2 certificate)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("-----BEGIN CERTIFICATE-----");
            builder.AppendLine(Convert.ToBase64String(certificate.RawData));
            builder.AppendLine("-----END CERTIFICATE-----");
            return builder.ToString();
        }

        private X509Certificate2 CreateCertificate(APICertificateRequest request, bool eidas = false)
        {
            using (RSA parent = RSA.Create(4096))
            using (RSA rsa = RSA.Create(2048))
            {
                CertificateRequest parentReq = new CertificateRequest(
                    "CN=Experimental Issuing Authority",
                    parent,
                    HashAlgorithmName.SHA256,
                    RSASignaturePadding.Pkcs1);

                parentReq.CertificateExtensions.Add(
                    new X509BasicConstraintsExtension(true, false, 0, true));

                parentReq.CertificateExtensions.Add(
                    new X509SubjectKeyIdentifierExtension(parentReq.PublicKey, false));

                using (X509Certificate2 parentCert = parentReq.CreateSelfSigned(
                    DateTimeOffset.UtcNow.AddDays(-45),
                    DateTimeOffset.UtcNow.AddDays(365)))
                {
                    CertificateRequest req = new CertificateRequest(
                        "CN=Valid-Looking Timestamp Authority",
                        rsa,
                        HashAlgorithmName.SHA256,
                        RSASignaturePadding.Pkcs1);

                    req.CertificateExtensions.Add(
                        new X509BasicConstraintsExtension(false, false, 0, false));

                    req.CertificateExtensions.Add(
                        new X509KeyUsageExtension(
                            X509KeyUsageFlags.DigitalSignature | X509KeyUsageFlags.NonRepudiation,
                            false));

                    req.CertificateExtensions.Add(
                        new X509EnhancedKeyUsageExtension(
                            new OidCollection
                            {
                    new Oid("1.3.6.1.5.5.7.3.8")
                            },
                            true));

                    req.CertificateExtensions.Add(
                        new X509SubjectKeyIdentifierExtension(req.PublicKey, false));

                    using (X509Certificate2 cert = req.Create(
                        parentCert,
                        DateTimeOffset.UtcNow.AddDays(-1),
                        DateTimeOffset.UtcNow.AddDays(90),
                        new byte[] { 1, 2, 3, 4 }))
                    {
                        return new X509Certificate2(cert.Export(X509ContentType.Pfx, "WeNeedASaf3rPassword"), "WeNeedASaf3rPassword", X509KeyStorageFlags.MachineKeySet);
                                                
                    }
                }
            }
        }

        public static X509Certificate2 CreateSelfSignedCertificateBasedOnCertificateAuthorityPrivateKey(string subjectName, string issuerName, AsymmetricKeyParameter issuerPrivKey)
        {
            const int keyStrength = 4096;

            // Generating Random Numbers            
            CryptoApiRandomGenerator randomGenerator = new CryptoApiRandomGenerator();
            SecureRandom random = new SecureRandom(randomGenerator);
            ISignatureFactory signatureFactory = new Asn1SignatureFactory("SHA512WITHRSA", issuerPrivKey, random);
            // The Certificate Generator
            X509V3CertificateGenerator certificateGenerator = new X509V3CertificateGenerator();
            certificateGenerator.AddExtension(X509Extensions.ExtendedKeyUsage, true, new ExtendedKeyUsage((new List<DerObjectIdentifier>() { new DerObjectIdentifier("1.3.6.1.5.5.7.3.1"), new DerObjectIdentifier("1.3.6.1.5.5.7.3.2") })));

            var lExtensions = new List<DerObjectIdentifier>();

            lExtensions.Add(new DerObjectIdentifier("0.4.0.19495.1.1"));//, PSD2Roles.ASPSP);
            lExtensions.Add(new DerObjectIdentifier("0.4.0.19495.1.2"));//, PSD2Roles.PISP);
            lExtensions.Add(new DerObjectIdentifier("0.4.0.19495.1.3"));//, PSD2Roles.AISP);
            lExtensions.Add(new DerObjectIdentifier("0.4.0.19495.1.4"));//, PSD2Roles.PIISP);

            certificateGenerator.AddExtension(X509Extensions.QCStatements, true, new ExtendedKeyUsage(lExtensions));
            

            // Serial Number
            BigInteger serialNumber = BigIntegers.CreateRandomInRange(BigInteger.One, BigInteger.ValueOf(Int64.MaxValue), random);
            certificateGenerator.SetSerialNumber(serialNumber);

            // Issuer and Subject Name
            X509Name subjectDN = new X509Name($"CN={subjectName}");
            X509Name issuerDN = new X509Name(issuerName);
            certificateGenerator.SetIssuerDN(issuerDN);
            certificateGenerator.SetSubjectDN(subjectDN);

            // Valid For
            DateTime notBefore = DateTime.UtcNow.Date;
            DateTime notAfter = notBefore.AddYears(2);

            certificateGenerator.SetNotBefore(notBefore);
            certificateGenerator.SetNotAfter(notAfter);

            // Subject Public Key
            AsymmetricCipherKeyPair subjectKeyPair;
            var keyGenerationParameters = new KeyGenerationParameters(random, keyStrength);
            var keyPairGenerator = new RsaKeyPairGenerator();
            keyPairGenerator.Init(keyGenerationParameters);
            subjectKeyPair = keyPairGenerator.GenerateKeyPair();

            certificateGenerator.SetPublicKey(subjectKeyPair.Public);

            //GeneralNames subjectAltName = new GeneralNames(new GeneralName(GeneralName.OtherName, subjectName));
            //certificateGenerator.AddExtension(X509Extensions.SubjectAlternativeName, false, subjectAltName);


            // self sign certificate
            Org.BouncyCastle.X509.X509Certificate certificate = certificateGenerator.Generate(signatureFactory);

            X509Certificate2 certificate2 = new X509Certificate2(certificate.GetEncoded());
            //certificate2.Import(certificate.GetEncoded());
            return certificate2;
        }

        public static X509Certificate2 CreateCertificateAuthorityCertificate(string subjectName, ref AsymmetricKeyParameter CaPrivateKey)
        {
            const int keyStrength = 2048;

            // Generating Random Numbers
            CryptoApiRandomGenerator randomGenerator = new CryptoApiRandomGenerator();
            SecureRandom random = new SecureRandom(randomGenerator);

            // The Certificate Generator
            X509V3CertificateGenerator certificateGenerator = new X509V3CertificateGenerator();

            // Serial Number
            BigInteger serialNumber = BigIntegers.CreateRandomInRange(BigInteger.One, BigInteger.ValueOf(Int64.MaxValue), random);
            certificateGenerator.SetSerialNumber(serialNumber);

            // Signature Algorithm
            //const string signatureAlgorithm = "SHA256WithRSA";
            //certificateGenerator.SetSignatureAlgorithm(signatureAlgorithm);

            // Issuer and Subject Name
            X509Name subjectDN = new X509Name(subjectName);
            X509Name issuerDN = subjectDN;
            certificateGenerator.SetIssuerDN(issuerDN);
            certificateGenerator.SetSubjectDN(subjectDN);

            // Valid For
            DateTime notBefore = DateTime.UtcNow.Date;
            DateTime notAfter = notBefore.AddYears(2);

            certificateGenerator.SetNotBefore(notBefore);
            certificateGenerator.SetNotAfter(notAfter);

            // Subject Public Key
            AsymmetricCipherKeyPair subjectKeyPair;
            KeyGenerationParameters keyGenerationParameters = new KeyGenerationParameters(random, keyStrength);
            RsaKeyPairGenerator keyPairGenerator = new RsaKeyPairGenerator();
            keyPairGenerator.Init(keyGenerationParameters);
            subjectKeyPair = keyPairGenerator.GenerateKeyPair();

            certificateGenerator.SetPublicKey(subjectKeyPair.Public);

            // Generating the Certificate
            AsymmetricCipherKeyPair issuerKeyPair = subjectKeyPair;
            ISignatureFactory signatureFactory = new Asn1SignatureFactory("SHA512WITHRSA", issuerKeyPair.Private, random);
            // selfsign certificate
            Org.BouncyCastle.X509.X509Certificate certificate = certificateGenerator.Generate(signatureFactory);
            X509Certificate2 x509 = new X509Certificate2(certificate.GetEncoded());

            CaPrivateKey = issuerKeyPair.Private;

            return x509;
            //return issuerKeyPair.Private;

        }
    }
}
