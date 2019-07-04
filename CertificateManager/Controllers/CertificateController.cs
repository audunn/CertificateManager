using System;
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
        /// Creates and returns a sha256/sha512 digest, use text/plain (to be compatible with API that this replaces)
        /// 
        /// </summary>
        /// <param name="request">Create digest request</param>        
        /// <param name="algoritm">Digest algoritm to use</param>
        /// <returns>The generated digest as a base64 endocded string</returns>
        /// <response code="200">The generated digest as a base64 endocded string.</response>
        /// <response code="400">Bad Request</response>
        /// <response code="500">Internal Server Error</response>                
        [HttpPost("generateDigest", Name = "GenerateDigest")]
        [Produces("application/json")]
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
        /// </remarks>
        /// <param name="request">Signature request</param>        
        /// <returns>The generated digest</returns>        
        /// <response code="200">The generated digest.</response>
        /// <response code="400">Bad Request</response>
        /// <response code="500">Internal Server Error</response>        
        [HttpPost("generateSignature", Name = "SignData")]
        [Produces("application/json")]        
        [ProducesResponseType(typeof(SigningResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(OperationId = nameof(SignData))]                
        public ActionResult<SigningResponse> SignData([FromBody] SigningRequest request)
        {
            _logger.LogDebug($"Calling POST /generateDigest endpoint ");
            if (request == null)
            {
                return BadRequest();
            }

            _logger.LogDebug($"Calling POST /generateDigest endpoint ");
            if (request.Algorithm == SigningAlgoritm.SHA256WITHRSA)
            {
                var bytes = Encoding.UTF8.GetBytes(request.DataToSign ?? request.Digest.Replace("SHA-256=", ""));
                var signature = Convert.ToBase64String(Sha256Helper.SignData(request.PrivateKey, bytes));
                return new SigningResponse() { Algorithm = request.Algorithm, Headers = "digest tpp-transaction - id tpp - request - id timestamp psu-id", KeyId = request.KeyID, Signature = signature };
            }
            else if (request.Algorithm == SigningAlgoritm.SHA512WITHRSA)
            {
                var bytes = Encoding.UTF8.GetBytes(request.DataToSign ?? request.Digest.Replace("SHA-512=", ""));
                var signature = Convert.ToBase64String(Sha512Helper.SignData(request.PrivateKey, bytes));
                return new SigningResponse() { Algorithm = request.Algorithm, Headers = "digest tpp-transaction - id tpp - request - id timestamp psu-id", KeyId = request.KeyID, Signature = signature };
            }
            else
            {
                return BadRequest($"Unsupported signing algoritm {request.Algorithm}");
            }
        }
    }
}
