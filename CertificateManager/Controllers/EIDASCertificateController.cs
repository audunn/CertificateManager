﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CertificateManager.BusinessLogic;
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
        /// Creates and resturns a self signed test certificate
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(CertificateResponse), 200)]
        public ActionResult<CertificateResponse> Post(APICertificateRequest request)
        {
            _logger.LogDebug($"Calling POST / endpoint ");
            return _manager.GenerateEIDASSelfSignedCertificate(request);
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
