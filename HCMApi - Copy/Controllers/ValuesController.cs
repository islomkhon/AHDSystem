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
using System.IO;

namespace HCMApi.Controllers
{

    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private AzureAd AzureAdSettings { get; set; }
        private readonly IHostingEnvironment _hostingEnvironment;

        public ValuesController(IOptions<AzureAd> settings, IHostingEnvironment hostingEnvironment)
        {
            AzureAdSettings = settings.Value;
            _hostingEnvironment = hostingEnvironment;
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

        private static string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        [HttpGet]
        [Route("WeatherForecasts")]
        public IEnumerable<WeatherForecast> WeatherForecasts(int startDateIndex)
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                DateFormatted = DateTime.Now.AddDays(index + startDateIndex).ToString("d"),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            });
        }

        [HttpGet]
        [Route("HCMRequestTemplate")]
        public JsonResult HCMRequestTemplate(string templateType)
        {
            //string webRootPath = _hostingEnvironment.WebRootPath;
            string contentRootPath = _hostingEnvironment.ContentRootPath;
            //var path = Path.Combine(contentRootPath, @"\MyStaticFiles\hcmtemplate.json");
            var path = contentRootPath + "\\MyStaticFiles\\hcmtemplate.json";
            var requestTemplate = System.IO.File.ReadAllText(path);
            return new JsonResult(new JsonResponse("Ok", "Request withdrawn successfully.", requestTemplate));
        }

        public class WeatherForecast
        {
            public string DateFormatted { get; set; }
            public int TemperatureC { get; set; }
            public string Summary { get; set; }

            public int TemperatureF
            {
                get
                {
                    return 32 + (int)(TemperatureC / 0.5556);
                }
            }
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
