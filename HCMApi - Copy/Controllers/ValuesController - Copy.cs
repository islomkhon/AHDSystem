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

    //class AzureAuthenticationProvider : IAuthenticationProvider
    //{
    //    public async Task AuthenticateRequestAsync(HttpRequestMessage request)
    //    {
    //        string clientId = "bc189a35-aefc-4d79-b116-f92ab2c7a717";
    //        string clientSecret = "W2@*o]kbA-5n2MCSX8oZ./xFS*412QuI";

    //        AuthenticationContext authContext = new AuthenticationContext("https://login.windows.net/neudesic.onmicrosoft.com/oauth2/token");

    //        ClientCredential creds = new ClientCredential(clientId, clientSecret);

    //        AuthenticationResult authResult = await authContext.AcquireTokenAsync("https://graph.microsoft.com/", creds);

    //        request.Headers.Add("Authorization", "Bearer " + authResult.AccessToken);
    //    }
    //}

    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private AzureAd AzureAdSettings { get; set; }
        private string graphResourceID = "https://graph.windows.net";
        
        public ValuesController(IOptions<AzureAd> settings)
        {
            AzureAdSettings = settings.Value;
        }

        // GET api/values
        [HttpGet]
        public async Task<ActionResult<IEnumerable<string>>> Get()
        {
            string email = "monin.jose@neudesic.com";
            if (User.Identity.IsAuthenticated)
            {
                
            }
            else
            {

            }
            return new string[] { "value1", "value2" };
        }

        public async Task<IList<Microsoft.Graph.Contact>> GetMyEmailContacts(GraphServiceClient graphClient)
        {
            Microsoft.Graph.User me = await graphClient.Me.Request().Select("mail,userPrincipalName").GetAsync();
            var contacts = me.Contacts;
            return contacts;
        }

        public async Task<string> GetAccessToken()
        {
            string authorityUri = "https://login.microsoftonline.com/"+ AzureAdSettings.TenantId;
            AuthenticationContext authContext = new AuthenticationContext(authorityUri);

            string resourceUrl = "https://graph.microsoft.com";

            ClientCredential creds = new ClientCredential(AzureAdSettings.ClientId, AzureAdSettings.ClientSecret);
            AuthenticationResult authResult = await authContext.AcquireTokenAsync(resourceUrl, creds);

            return authResult.AccessToken;
        }

        public async Task<GraphServiceClient> GetGraphClient()
        {
            GraphServiceClient graphServiceClient = new GraphServiceClient(new DelegateAuthenticationProvider(
                                                        async (requestMessage) =>
                                                        {
                                                            string accessToken = await GetAccessToken();
                                                            if (!string.IsNullOrEmpty(accessToken))
                                                            {
                                                                requestMessage.Headers.Authorization = new AuthenticationHeaderValue("bearer", accessToken);
                                                            }
                                                        }));

            return graphServiceClient;
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
