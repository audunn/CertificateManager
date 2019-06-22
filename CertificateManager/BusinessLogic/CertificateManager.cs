﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using CertificateManager.Models;

namespace CertificateManager.BusinessLogic
{
    /// <inheritdoc/>
    public class CertificateManagerBL : ICertificateManager
    {
        /// <inheritdoc />        
        public string GenerateSelfSignedCertificate(APICertificateRequest request)
        {            
            return GetCertificateAsString(CreateCertificate(request));
        }

        /// <inheritdoc />
        public string GenerateEIDASSelfSignedCertificate(APICertificateRequest request)
        {
            var certificate = CreateCertificate(request, true);
            return GetCertificateAsString(certificate);
        }

        private static string GetCertificateAsString(X509Certificate2 certificate)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("-----BEGIN CERTIFICATE-----");
            builder.AppendLine(Convert.ToBase64String(certificate.RawData));
            builder.AppendLine("-----END CERTIFICATE-----");
            return builder.ToString();
        }

        private X509Certificate2 CreateCertificate(APICertificateRequest request, bool eidas = true)
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
                        $"CN={request.CommonName}",
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
                            true)
                        );

                    req.CertificateExtensions.Add(
                        new X509SubjectKeyIdentifierExtension(req.PublicKey, false));
                    
                    if (eidas)
                    {
                        req.CertificateExtensions.Add(
                            new X509EnhancedKeyUsageExtension(
                                new OidCollection{
                                new Oid("1.3.6.1.5.5.7.3.8")
                                },
                                true)
                            );
                    }
                    using (X509Certificate2 cert = req.Create(
                        parentCert,
                        DateTimeOffset.UtcNow.AddDays(-1),
                        DateTimeOffset.UtcNow.AddDays(90),
                        new byte[] { 1, 2, 3, 4 }))
                    {
                        return cert;
                    }
                }
            }
        }
    }
}
