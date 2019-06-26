using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CertificateManager.BusinessLogic;
using CertificateManager.Helpers;
using CertificateManager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

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
        [ProducesResponseType(typeof(CertificateResponse), 200)]
        [ProducesResponseType(typeof(void), 400)]
        [ProducesResponseType(typeof(void), 500)]
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
        /// Creates and returns a sha256 digest, use plain/test (to be compatible with API that this replaces)
        /// </summary>        
        /// <param name="request">Create digest request</param>        
        /// <returns>The generated digest</returns>        
        /// <response code="200">The generated digest.</response>
        /// <response code="400">Bad Request</response>
        /// <response code="500">Internal Server Error</response>        
        [HttpPost("generateDigest", Name = "GenerateDigest")]
        [Produces("application/json")]
        [Consumes("text/plain")]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(ErrorDetails), 400)]
        [ProducesResponseType(typeof(ErrorDetails), 500)]        
        // [SwaggerOperation(OperationId = nameof(GenerateDigest))]
        public ActionResult<string> GenerateDigest([FromBody] string request)
        {
            _logger.LogDebug($"Calling POST /generateDigest endpoint ");
            if (request == null)
            {
                return BadRequest();
            }
            
            _logger.LogDebug($"Calling POST /generateDigest endpoint ");
            return $"SHA-256={Sha256Helper.GenerateHash(request)}";
        }


        //// GET api/values/5
        //[HttpGet("{id}")]
        //public ActionResult<string> Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/values
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/values/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/values/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
