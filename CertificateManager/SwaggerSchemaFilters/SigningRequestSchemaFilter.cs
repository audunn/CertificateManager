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
            if (context.SystemType == typeof(SigningRequest))
            {
                schema.Example = new SigningRequest
                {
                    Algorithm = "SHA256WITHRSA",
                    Digest = "SHA-256=xStG1qWQGpXRza7nysZeXJB3UOndEZN2BC4T9b781sE=",
                    KeyID = "1735065854",
                    PrivateKey = @"-----BEGIN RSA PRIVATE KEY-----
MIIEogIBAAKCAQEAyX0S/zO08x+0dMaReKQmasK1Fw8pZvQFTNxFj9VBveFq84U9oUN7diLugGkcvzg8w9IyZb7C64dgBXiA3Vd5bPiNRNM1LSpVv5yxl1HxFJLUoim2sWZybwt2JiMI51FDr8MV6J2W33kxlE8ZFEKKe19oX6hAR3lzxqgvBGxe6cMZgiRtDATMe1CpBrx7ClRNEKEV0f0PR/kTyBQq7tLUmZOgJQ76I3GDDsVfgSGaUshHgcuq9Wu8jjWyBU9DPQixAPrk9BmMdZaKkdhhksreHb9xeMPI9W+bf+mLuIzOshlowOtMVm1qSKLTcJ0Yy8qGDexcBA1aMigxQROyA304KwIDAQABAoIBACo+Grl+cbV3NDI3X1BXueYwJB9NgnSfPG080SiyoTHaDL45dQ5WQ5AlCrPJVcmRVMwJSZ+jOuJ+Y1dCSGIfcMmz4opHbaKmeYvOoj0DZPPXRNUqwa63t84oLQEZ30f/9Zzn3bQDNhpsIkThwHRK7xWmjw9fCvrxL6C6qgDXZ57CdPANt3ksTsuvPicNXpmac2UY2B2G/IwT5kHY6g5fLnHI5lVxYjRluowNln1eoRie32WIBY7K3h2JDNdb7OTvpKgMSC3SnQrxlRVXfFlOlFJ2K/uuPL4+qY1Es3KypnyZ1cxnDr4+InfSYkpQmApyoFE/JNfYZkzudq7h1XfYhEECgYEA+d7pBZU0hp2ygpGrS59CNw+4LHbNlj5ubCYCM5XNYYUsHc0uRLHsV5ujOu9ogznrBhITHPdobCSkkS+7i9o61RRUuVO78FqZI0LszMvy5shUf1etQIoi5ACIclCY0rp2uTbYbbvJA/IAxPyqwq3G6zxIO6OHDa8R95GAHwXSzRkCgYEAzm5Xzsd0rg3yoSkO/j/mZQKxqhz44WR0xmFSW/ju2kgQT3jmAbnQA7GVnLnfRs7vJEnyVHrDWmQ3bZbitc4J0tZ/Q1x+f08cG675j4uKR1svNEZPY5gG44tWDlAQusUsvUj+6YkJmBYAl36zBOaXQ/9WDs66Dhu0J7Mk5Smsk+MCgYATQve7knDmH6nvHsORpMk57WBJLS9T8aQZeiSZTWbzqYxpD7QNX5nUdw7yMBpiY0iWwzXt+bR3Aawd9QwED+KRImwVjTrjoFhFu5b9gotK9w5scpnMa9mcsd3S0hu1wuH8DRpJ+zvXrgQZK1EOiM7Yu2DmGAkFKQW9i93QhmZloQKBgDvP8vdCCYfJGM0jZWm7wBjyb+H3ZRGBluhIGKH3fdWXXcp5IEPNv/zh/pSbspsEqHveRS2/KE1PjlPdjTDaUzRY3CT7bteaZ22KBFtYNie4vvOpj6UuzogtvjCFeGN1cCIkPWATSclcTq2Wk25PFfMoz5mYwoYF9uwt1vnjWlK7AoGAKeA1q2MqXPt+EXiazI204k6E9dM38l/q4YgGiKobPIW+SsnJDGa9cDl2UQwtpHAtzFBYTKH/j7IAyOW0WP4va47wvT9bK7Qs+jOcn4l9XcFPYFEdugoUcyg6ooAoDM/S70v0/YNeMymb1IyJQqfhNfm6XFYBOaBubU4Eh/8SMrg=
-----END RSA PRIVATE KEY-----",
                    PsuID = "psuID",
                    Timestamp = "2019-12-01T08:28:13.215Z",
                    TppRequestID = "tppRequestID",
                    TppTransactionID = "tppTransactionID"
                };
            }
        }
    }
}
