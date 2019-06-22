using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CertificateManager.BusinessLogic;
using CertificateManager.Models;
using Microsoft.AspNetCore.Mvc;

namespace CertificateService.Controllers
{
    /// <summary>
    /// Certificate Manger API 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CertificateController : ControllerBase
    {
        // GET api/values
        /// <summary>
        /// Creates and resturns a self signed test certificate
        /// </summary>
        [HttpPost]
        public ActionResult<string> Get(APICertificateRequest request)
        {
            ICertificateManager manager = new CertificateManagerBL();
            return manager.GenerateSelfSignedCertificate(request);
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
