using HCMApi.DB;
using HCMApi.Modal;
using System;
using System.Collections;
using System.Collections.Generic;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace HCMApi.Shedules
{
    public class SyncUsersAd
    {
        public static List<UserAdModelClass> userAdModelClasses = new List<UserAdModelClass>();
        public static void SyncUsers(AzureAd AzureAdSettings)
        {
            try
            {
                userAdModelClasses = new List<UserAdModelClass>();
                UserAdModelClass someUserModelClass = new UserAdModelClass();
                Task<IEnumerable<UserAdModelClass>> usersEnum = GetUsers();
                Task continuation = usersEnum.ContinueWith(x => someUserModelClass = LoadUserAdData(x.Result));
                continuation.Wait();

                List<DAL.NueUserProfile> nueUserProfilesMaster = new DataAccess(AzureAdSettings).getAllUserProfilesDinamic();

                List<UserAdModelClass> toBeAdded = new List<UserAdModelClass>();
                List<UserAdModelClass> toBeRemoved = new List<UserAdModelClass>();

                if(userAdModelClasses.Count > 0)
                {

                    var abc = userAdModelClasses.Where(x => x.email == "monin.jose@neudesic.com");

                    foreach (var item in userAdModelClasses)
                    {
                        try
                        {   
                            if (!IsEmailValid(item.email))
                            {

                            }
                            if (nueUserProfilesMaster.Where(x => x.Email.ToLower() == item.email.ToLower()).Count() <= 0)
                            {
                                toBeAdded.Add(item);
                            }
                        }
                        catch(Exception e1)
                        {

                        }
                        
                    }

                    //string emailToRE = "";
                    foreach (var item in nueUserProfilesMaster)
                    {
                        try
                        {
                            if (!IsEmailValid(item.Email))
                            {

                            }

                            if (userAdModelClasses.Where(x => x.email.ToLower() == item.Email.ToLower()).Count() <= 0)
                            {
                                toBeRemoved.Add(new UserAdModelClass(item.Email, item.FullName));
                                //emailToRE += item.Email+", ";
                            }
                        }
                        catch (Exception e1)
                        {

                        }
                    }

                    new DataAccess(AzureAdSettings).sincUsersAd(toBeAdded, toBeRemoved);
                }

               

            }
            catch (Exception e1)
            {
                
            }
        }

        private static UserAdModelClass LoadUserAdData(IEnumerable<UserAdModelClass> result)
        {
            foreach (var item in result)
            {
                try
                {
                    UserAdModelClass userAdModelClass = item;

                    if(userAdModelClass.email != null && userAdModelClass.userName != null)
                    {
                        var email = userAdModelClass.email;
                        var userName = userAdModelClass.userName;

                        if (!IsEmailValid(email) && IsEmailValid(userName))
                        {
                            var temp = userName;
                            userName = email;
                            email = temp;
                        }
                        userAdModelClass.userName = userName;
                        userAdModelClass.email = email;
                    }
                    userAdModelClasses.Add(userAdModelClass);
                }
                catch (Exception e1)
                {
                    
                }
            }
            return null;
        }

        public static bool IsEmailValid(string emailaddress)
        {
            try
            {
                MailAddress m = new MailAddress(emailaddress);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        public static Task<IEnumerable<UserAdModelClass>> GetUsers()
        {
            return Task.Run(() =>
            {
                PrincipalContext context = new PrincipalContext(ContextType.Domain, "corp.neudesic.net");
                UserPrincipal principal = new UserPrincipal(context);
                principal.Enabled = true;
                principal.Name = "*";
                PrincipalSearcher searcher = new PrincipalSearcher(principal);
                //searcher.QueryFilter = principal;
                //searcher.QueryFilter = "(&(objectClass = user)(objectCategory = person))";
                var users = searcher.FindAll().Cast<UserPrincipal>()
                    .Where(x => x.EmailAddress != null)
                    .Select(x => new UserAdModelClass
                    {
                        firstName = x.GivenName,
                        lastName = x.Surname,
                        empId = x.EmployeeId,
                        userName = x.DisplayName,
                        email = x.EmailAddress.ToLower(),
                        guid = x.Guid.Value,
                        adObjectType = x.StructuralObjectClass
                    }).OrderBy(x => x.userName).AsEnumerable();

                //var abc = searcher.FindAll().Cast<UserPrincipal>().First();

                return users;
            });
        }

        //var users = searcher.FindAll().Cast<UserPrincipal>()
        //            //.Where(x => x.SomeProperty... // Perform queries)

        //            .Select(x => new UserAdModelClass
        //            {
        //                empId = x.EmployeeId,
        //                userName = x.SamAccountName,
        //                email = x.UserPrincipalName,
        //                guid = x.Guid.Value
        //            }).OrderBy(x => x.userName).AsEnumerable();
    }

}
