using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Identity.Client;
using System.Net.Http.Headers;

namespace ActiveDirect
{
    public class Users
    {
        public string Email { get; set; }
        public string UserName { get; set; }
        public string DisplayName { get; set; }
        public bool isMapped { get; set; }
    }

    class Program
    {

        //public List<Users> GetADUsers()
        //{
        //    try
        //    {
        //        List<Users> lstADUsers = new List<Users>();
        //        string DomainPath = "LDAP://DC=neuad09.corp.neudesic.net,DC=com";
        //        DirectoryEntry searchRoot = new DirectoryEntry(DomainPath);
        //        DirectorySearcher search = new DirectorySearcher(searchRoot);
        //        search.Filter = "(&(objectClass=user)(objectCategory=person))";
        //        search.PropertiesToLoad.Add("samaccountname");
        //        search.PropertiesToLoad.Add("mail");
        //        search.PropertiesToLoad.Add("usergroup");
        //        search.PropertiesToLoad.Add("displayname");//first name
        //        SearchResult result;
        //        SearchResultCollection resultCol = search.FindAll();
        //        if (resultCol != null)
        //        {
        //            for (int counter = 0; counter < resultCol.Count; counter++)
        //            {
        //                string UserNameEmailString = string.Empty;
        //                result = resultCol[counter];
        //                if (result.Properties.Contains("samaccountname") &&
        //                         result.Properties.Contains("mail") &&
        //                    result.Properties.Contains("displayname"))
        //                {
        //                    Users objSurveyUsers = new Users();
        //                    objSurveyUsers.Email = (String)result.Properties["mail"][0] +
        //                      "^" + (String)result.Properties["displayname"][0];
        //                    objSurveyUsers.UserName = (String)result.Properties["samaccountname"][0];
        //                    objSurveyUsers.DisplayName = (String)result.Properties["displayname"][0];
        //                    lstADUsers.Add(objSurveyUsers);
        //                }
        //            }
        //        }
        //        return lstADUsers;
        //    }
        //    catch (Exception ex)
        //    {

        //    }

        //    return null;
        //}

        //public async Task getUserActiveAsync()
        //{
        //    try
        //    {
        //        string AuthString = "https://login.microsoftonline.com/";
        //        string ResourceUrl = "https://graph.windows.net";
        //        string ClientId = "bc189a35-aefc-4d79-b116-f92ab2c7a717";
        //        var redirectUri = new Uri("https://localhost");
        //        string TenantId = "687f51c3-0c5d-4905-84f8-97c683a5b9d1";

        //        AuthenticationContext authenticationContext = new AuthenticationContext(AuthString + TenantId, false);
        //        AuthenticationResult userAuthnResult = await authenticationContext.AcquireTokenAsync(ResourceUrl,
        //            ClientId, redirectUri, new PlatformParameters(PromptBehavior.RefreshSession));
        //        var TokenForUser = userAuthnResult.AccessToken;
        //        var client = new HttpClient();

        //        var uri = $"https://graph.windows.net/{TenantId}/users?api-version=1.6";
        //        client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", TokenForUser);
        //        var response = await client.GetAsync(uri);
        //        if (response.Content != null)
        //        {
        //            var responseString = await response.Content.ReadAsStringAsync();
        //            Console.WriteLine(responseString);
        //        }
        //    }
        //    catch (Exception e1)
        //    {

        //        throw;
        //    }
            
        //}

        public static void Main(string[] args)
        {
            Main1();
        }

        static async Task Main1()
        {
            //new Program().getUsAsync();
            //GraphServiceClient client = new GraphServiceClient(new AzureAuthenticationProvider());
            //QueryOption option = new QueryOption("$top","10");
            //IGraphServiceUsersCollectionPage users = await client.Users.Request(new Option[] { option }).GetAsync();

            var tenantId = "687f51c3-0c5d-4905-84f8-97c683a5b9d1";
            var clientId = "389cb4c3-4467-40a9-8a64-de68d06906f2";
            var clientSecret = "j8HmL7.GSuEQhA*9XWqAvhCYoKee2W+/";

            // Configure app builder
            var authority = $"https://login.microsoftonline.com/{tenantId}";
            var app = ConfidentialClientApplicationBuilder
                .Create(clientId)
                .WithClientSecret(clientSecret)
                .WithAuthority(new Uri(authority))
                .Build();

            // Acquire tokens for Graph API
            var scopes = new[] { "https://graph.microsoft.com/.default" };
            var authenticationResult = await app.AcquireTokenForClient(scopes).ExecuteAsync();
            
            // Create GraphClient and attach auth header to all request (acquired on previous step)
            var graphClient = new GraphServiceClient(
                new DelegateAuthenticationProvider(requestMessage => {
                    requestMessage.Headers.Authorization =
                        new AuthenticationHeaderValue("bearer", authenticationResult.AccessToken);

                    return Task.FromResult(0);
                }));

            // Call Graph API
            var user = await graphClient.Users["monin.jose@neudesic.com"].Request().GetAsync();

        }



        //static async Task<string> GetTokenAsync(PublicClientApplication clientApp)

        //{

        //    //need to pass scope of activity to get token

        //    string[] Scopes = {"User.Read"};

        //    string token = null;

        //    AuthenticationResult authResult = await clientApp.AcquireTokenAsync(Scopes);

        //    token = authResult.AccessToken;

        //    return token;

        //}

        //public async Task<IGraphServiceUsersCollectionPage> getUsAsync()
        //{
        //    try
        //    {
        //        GraphServiceClient client = new GraphServiceClient(new AzureAuthenticationProvider());
        //        QueryOption option = new QueryOption("$top", "10");
        //        IGraphServiceUsersCollectionPage users = await client.Users.Request(new Option[] { option }).GetAsync();
        //        return users;
        //    }
        //    catch (Exception e)
        //    {


        //    }
        //    return null;
        //}

        //public async Task<User> GetUser(string userPrincipalName)
        //{
        //    GraphServiceClient graphClient = new GraphServiceClient(new AzureAuthenticationProvider());
        //    User user = await graphClient.Users[userPrincipalName].Request().GetAsync();
        //    return user;
        //}
    }

    //class AzureAuthenticationProvider : IAuthenticationProvider
    //{
    //    public async Task AuthenticateRequestAsync(HttpRequestMessage request)
    //    {
    //        string clientId = "bc189a35-aefc-4d79-b116-f92ab2c7a717";
    //        string clientSecret = "*.CgYO+ilvX.uhvk0eZSOjKW3NvTet18";

    //        AuthenticationContext authContext = new AuthenticationContext("https://neudesic.onmicrosoft.com/HCMApi/oauth2/token");

    //        ClientCredential creds = new ClientCredential(clientId, clientSecret);

    //        AuthenticationResult authResult = await authContext.AcquireTokenAsync("https://graph.microsoft.com/", creds);

    //        request.Headers.Add("Authorization", "Bearer " + authResult.AccessToken);
    //    }
    //}
}
