using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;
using HCMApi.Modal;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.Azure.ActiveDirectory.GraphClient;
using Microsoft.Graph;

namespace HCMApi.Controllers
{

    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private AzureAd AzureAdSettings { get; set; }
        
        public ValuesController(IOptions<AzureAd> settings)
        {
            AzureAdSettings = settings.Value;
        }

        // GET api/values
        [HttpGet]
        public async Task<ActionResult<IEnumerable<string>>> Get()
        {
            if (User.Identity.IsAuthenticated)
            {
                
            }
            else
            {

            }
            return new string[] { "value1", "value2" };
        }

        private static string EnsureTrailingSlash(string value)
        {
            if (value == null)
            {
                value = string.Empty;
            }

            if (!value.EndsWith("/", StringComparison.Ordinal))
            {
                return value + "/";
            }

            return value;

        }
        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
            // For more information on protecting this API from Cross Site Request Forgery (CSRF) attacks, see https://go.microsoft.com/fwlink/?LinkID=717803
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
            // For more information on protecting this API from Cross Site Request Forgery (CSRF) attacks, see https://go.microsoft.com/fwlink/?LinkID=717803
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            // For more information on protecting this API from Cross Site Request Forgery (CSRF) attacks, see https://go.microsoft.com/fwlink/?LinkID=717803
        }
    }
}
