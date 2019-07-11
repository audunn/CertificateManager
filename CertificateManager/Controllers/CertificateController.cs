using System;
using System.ComponentModel;
using System.Reflection;
using System.Text;
using CertificateManager.BusinessLogic;
using CertificateManager.Helpers;
using CertificateManager.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;

namespace CertificateService.Controllers
{

    /// <summary>
    /// Certificate Manger API 
    /// </summary>
    [ApiVersion("1")]
    [Route("api/v{api-version:apiVersion}/[controller]")]
    [ApiController]
    public class CertificateController : ControllerBase
    {
        private readonly ICertificateManager _manager;
        private readonly IConfiguration _configuration;
        private readonly ILogger<CertificateController> _logger;

        /// <summary>
        /// Load accounts, logger, app settings and url helper
        /// </summary>
        /// <param name="manager">accounts</param>
        /// <param name="logger">logger</param>        
        /// <param name="config">logger</param>        
        public CertificateController(ICertificateManager manager, ILogger<CertificateController> logger, IConfiguration config)
        {
            _manager = manager;
            _logger = logger;
            _configuration = config;
        }
        //// POST api/values
        /// <summary>
        /// Creates and returns a self signed test certificate.        
        /// </summary>        
        /// <param name="request">Create certificate request</param>        
        /// <returns>The generated self signed certficate.</returns>
        /// <remarks>If you don't need a EIDAS certifcate set EIDAS specific flags to false.</remarks>
        /// <response code="200">The generated self signed <paramref name="request"/>.</response>
        /// <response code="400">Bad Request</response>
        /// <response code="500">Internal Server Error</response>        
        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(typeof(CertificateResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<CertificateResponse> Post(APICertificateRequest request)
        {
            if (request == null)
            {
                return BadRequest();
            }

            _logger.LogDebug($"Calling POST / endpoint ");
            return _manager.GenerateSelfSignedCertificate(request);
        }

        //// POST api/generateDigest
        /// <summary>
        /// Creates and returns a sha256/sha512 digest, use text/plain (to be compatible with API that this replaces) and a Base64 encoded string
        /// </summary>
        /// <param name="request">Create digest request. String base64 encoded to survive endcoding and transport through transport layers</param>
        /// <param name="algoritm">Digest algoritm to use</param>
        /// <remarks>
        /// ### Base64 encoding
        ///  Base64 encoded string is needed since string data when transported over the wire via different systems is sometimes encoded in different fashion depending on layer or system. 
        ///  That can mean that while the string may look the same there may be very slight difference in the binary data so when we calculate digest to compare and verify the results may not match.
        ///  Base64 encoding ensures the string survive encoding and transport through transport layers
        /// </remarks>
        /// <returns>The generated digest as a base64 endocded string</returns>
        /// <response code="200">The generated digest as a base64 endocded string.</response>
        /// <response code="400">Bad Request</response>
        /// <response code="500">Internal Server Error</response>                
        [HttpPost("generateDigest", Name = "GenerateDigest")]
        //[Produces("application/json")]
        [Produces("text/plain")]
        [Consumes("text/plain", new[] { "application/json"})]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(OperationId = nameof(GenerateDigest))]
        public ActionResult<string> GenerateDigest([FromBody] string request, SigningAlgoritm algoritm = SigningAlgoritm.SHA256WITHRSA)
        {
            _logger.LogDebug($"Calling POST /generateDigest{algoritm} endpoint ");
            if (request == null)
            {
                return BadRequest();
            }
            try
            {
                request = DecodeBase64(request);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error decoding request data", ex);
                return BadRequest("Error decoding request data, check base64 encoding");
            }
            if (algoritm == SigningAlgoritm.SHA256WITHRSA)
            {
                return $"SHA-256={Sha256Helper.GenerateHash(request)}";
            }
            else if (algoritm == SigningAlgoritm.SHA512WITHRSA)
            {
                return $"SHA-512={Sha512Helper.GenerateHash(request)}";
            }
            else
            {
                return BadRequest($"Unsupported digest algoritm {algoritm}");
            }

        }

        //// POST api/generateDigest
        /// <summary>
        /// Generates Signature using given algoritm (Experimental)
        /// </summary>        
        /// <remarks>
        ///  Supports SHA256WITHRSA and SHA512WITHRSA
        ///  Note clients can use accept header to request either json object or accept: text/plain which will return a string formatted as signature header, 
        ///  as described in RFC 4648[RFC4648], Section 4 [5]
        /// </remarks>
        /// <param name="request">Signature request</param>        
        /// <returns>The generated digest</returns>        
        /// <response code="200">The generated digest.</response>
        /// <response code="400">Bad Request</response>
        /// <response code="500">Internal Server Error</response>        
        [HttpPost("generateSignature", Name = "SignData")]
        //[Produces("text/plain", "application/json")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(OperationId = nameof(SignData))]                
        public IActionResult SignData([FromBody] SigningRequest request)
        {
            _logger.LogDebug($"Calling POST /generateDigest endpoint ");
            if (request == null)
            {
                return BadRequest();
            }

            _logger.LogDebug($"Calling POST /generateDigest endpoint ");
            try
            {
                request.DataToSign = DecodeBase64(request.DataToSign);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error decoding data to sign", ex);
                return BadRequest("Error decoding data to sign, check base64 encoding");
            }
            if (request.Algorithm == SigningAlgoritm.SHA256WITHRSA)
            {
                var bytes = Encoding.UTF8.GetBytes(request.DataToSign ?? request.Digest.Replace("SHA-256=", ""));
                var signature = Convert.ToBase64String(Sha256Helper.SignData(request.PrivateKey, bytes));
                var response = new SigningResponse()
                {
                    Algorithm = Enum.GetName(typeof(SigningAlgoritm),
                    request.Algorithm),
                    Headers = "digest tpp-transaction - id tpp - request - id timestamp psu-id",
                    KeyId = request.KeyID,
                    Signature = signature,
                    CertificateAuthority = "CA"
                };


                return Ok(response);
            }
            else if (request.Algorithm == SigningAlgoritm.SHA512WITHRSA)
            {
                var bytes = Encoding.UTF8.GetBytes(request.DataToSign ?? request.Digest.Replace("SHA-512=", ""));
                var signature = Convert.ToBase64String(Sha512Helper.SignData(request.PrivateKey, bytes));
                var response = new SigningResponse()
                {
                    Algorithm = GetEnumDescription(request.Algorithm),
                    Headers = "digest tpp-transaction - id tpp - request - id timestamp psu-id",
                    KeyId = request.KeyID,
                    Signature = signature,
                    CertificateAuthority = "CA"
                };
                return Ok(response);
            }
            else
            {
                return BadRequest($"Unsupported signing algoritm {request.Algorithm}");
            }
        }
        private static string GetEnumDescription(Enum enumVal)
        {
            System.Reflection.MemberInfo[] memInfo = enumVal.GetType().GetMember(enumVal.ToString());
            DescriptionAttribute attribute = CustomAttributeExtensions.GetCustomAttribute<DescriptionAttribute>(memInfo[0]);
            return attribute.Description;
        }
        private string DecodeBase64(string inputString)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(inputString);
            return Encoding.UTF8.GetString(base64EncodedBytes);
        }
    }
}
