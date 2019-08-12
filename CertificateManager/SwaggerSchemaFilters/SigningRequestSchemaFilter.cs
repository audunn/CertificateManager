using CertificateManager.Models;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace CertificateManager.SwaggerSchemaFilters
{

    /// <summary>
    /// Defines the sample SigningRequestSchemaFilter schema 
    /// </summary>
    public class SigningRequestSchemaFilter : ISchemaFilter
    {
        /// <summary>
        /// Applies the sample SigningRequestSchemaFilter schema 
        /// </summary>
        /// <param name="schema"></param>
        /// <param name="context"></param>
        public void Apply(Schema schema, SchemaFilterContext context)
        {
            //For some reason the SchemaFilter annotation on the schema type does not work so we add in startup
            //this would make it run for all actions, but we check the type here so they are applied only
            //to the right actions
            if (context.SystemType == typeof(APICertificateRequest))
            {
                schema.Example = new APICertificateRequest
                {
                    Aisp = true,
                    Aspsp = true,
                    City = "Nuremberg",
                    CommonName = "MatrixAPI.PSD2_Client",
                    Country = "Germany",
                    Organization = "org",
                    OrganizationUnit = "ou",
                    Piisp = true,
                    Pisp = true,
                    PspAuthorityId = "Germany",
                    PspAuthorityName = "Auth",
                    PspAuthorzationNumber = "12345987",
                    State = "Bayern",
                    Validity = 365
                };
            }
            else if (context.SystemType == typeof(SigningRequest))
            {
                schema.Example = new SigningRequest
                {
                    Algorithm = SigningAlgoritm.SHA256WITHRSA,
                    Digest = "SHA-256=xStG1qWQGpXRza7nysZeXJB3UOndEZN2BC4T9b781sE=",
                    DataToSign = "dGVzdHN0cmluZw==",
                    KeyID = "1735065854",
                    PrivateKey = "-----BEGIN RSA PRIVATE KEY-----\r\n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\r\n-----END RSA PRIVATE KEY-----\r\n",
                    PsuID = "psuID",
                    Timestamp = "2019-12-01T08:28:13.215Z",
                    TppRequestID = "tppRequestID",
                    TppTransactionID = "tppTransactionID",
                    Certificate = "-----BEGIN CERTIFICATE-----MIIEbzCCA1egAwIBAgIIbgqgplVhOQowDQYJKoZIhvcNAQENBQAwIDEeMBwGA1UEAwwVTWF0cml4QVBJLlBTRDJfQ2xpZW50MB4XDTE5MDgxMjAwMDAwMFoXDTIwMDgxMTAwMDAwMFowcjEeMBwGA1UEAwwVTWF0cml4QVBJLlBTRDJfQ2xpZW50MQwwCgYDVQQKDANvcmcxCzAJBgNVBAsMAm91MRAwDgYDVQQGEwdHZXJtYW55MRIwEAYDVQQHDAlOdXJlbWJlcmcxDzANBgNVBAgMBkJheWVybjCCAiIwDQYJKoZIhvcNAQEBBQADggIPADCCAgoCggIBAMU4SBhngDDYpYWEj5Pie4STB/HPP08fQ0xsa97W8TJILY3wWtVdZRz6VFMrOm2jbqNtHRqUggrknPeLLoOEYdi0V3ATa2Cpsuci1zrkdlBU1a+1/E7vXY9Ji7+yOjTEMygJESKjoeiJVdAUrE6YjuE7SlvtnQsnUCoeqGKyzhJvduIxmxCzKXWvIxiKQgvXVhMtDbSX1FL/Ut2m4qzpuco1+721FNwm7K8a5wGpCtHwagJIqKMtsEwVzDxPYo7/bZdYqbrgbE4Fp1j0G7n5ElKaE+Hc+XPNWuQUHKQkFTuuMc2e9i9kz/MYb3TDJ/8mw8bhgMrg4KwomW8sv0l2ppzz1JuscFg8Hbx4sVwWoLCr3jxgGoUqqNjuphIhiKb2zYZ4rejceUoQ9JNAGXosEsp6jeO83I2LmqcmX6VvKcPigesrr7dmgHefx5sJR1HwVxYeZEHXDt5TBP8nbzUvszBSWrC/CU38T24VO0a25IjBr4pB59GmMAIa0/BUjFnWjeSEo0umGvr+0xusLoJcYsfbLYH9NXTbg/rgZs3zgAzkm8F0TXVLN6OC0t2/HF5TDowz5BPKMMkRtwPBvSvGyHpA6exZNjQ3rwaOYavHIphaCVHCl9p76Bzws1UBZjhHooJXP74wyDHvRKGIlKGV+8pc27jZ8WiA4bGI1eJJII75AgMBAAGjWzBZMCAGA1UdJQEB/wQWMBQGCCsGAQUFBwMBBggrBgEFBQcDAjA1BggrBgEFBQcBAwEB/wQmMCQGBwQAgZgnAQEGBwQAgZgnAQIGBwQAgZgnAQMGBwQAgZgnAQQwDQYJKoZIhvcNAQENBQADggEBAHPHZOhkInhO6yVhz1V/6s+vZaSOlIlvNBJUZdPpPi7TZUfCdPpQg4o+lNMVDQbBK0xlpVZOxnQbgSkHL4x1IIdJEXqm4UI25krHVf1dlpF5eJEFpmrhsnglYheh88R3y++Y7avjcxEaANu4MaccJofdYxdplwT4TINn4eYmHxEENTj1lZ9cgE9QeNdSZYxIB7WM59n8arbItZJWgg8eRB3MjWnwzXy9YJd2ThPqo1OW0GIZmiAsdKZJAbB9dS9A+AXF2+W5izQOuAcb5kYyxLvNJYrFITfNHyg2WzhqrmD5wHfTIID+hZjgtUncuczZ2kNZqjcLFe+qtxPgU2nKdgc=-----END CERTIFICATE-----"
                };
            }
        }
    }
}
