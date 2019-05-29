using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.DirectoryServices;
using System.DirectoryServices.Protocols;
using System.Net;
using System.DirectoryServices.AccountManagement;
using System.Data.Entity.Validation;

namespace ConsoleApp1
{
    class Program
    {

        public static bool fnValidateUser()
        {
            bool validation;
            try
            {
                LdapConnection lcon = new LdapConnection
                        (new LdapDirectoryIdentifier((string)null, false, false));
                NetworkCredential nc = new NetworkCredential(Environment.UserName,
                                       "Password.2", Environment.UserDomainName);
                lcon.Credential = nc;
                lcon.AuthType = AuthType.Negotiate;
                
                // user has authenticated at this point,
                // as the credentials were used to login to the dc.
                lcon.Bind(nc);

                String Name = "Monin";
                




                validation = true;
            }
            catch (LdapException e)
            {
                validation = false;
            }
            return validation;
        }

        public static void fnListAllUser()
        {
            DirectoryEntry directoryEntry = new DirectoryEntry
                    ("WinNT://" + Environment.UserDomainName);
            string userNames = "";
            string authenticationType = "";
            foreach (DirectoryEntry child in directoryEntry.Children)
            {
                if (child.SchemaClassName == "User")
                {
                    userNames += child.Name +
                        Environment.NewLine; //Iterates and binds all user using a newline
                    authenticationType += child.Username + Environment.NewLine;
                }
            }
            Console.WriteLine("************************Users************************");
            Console.WriteLine(userNames);
            Console.WriteLine("*****************Authentication Type*****************");
            Console.WriteLine(authenticationType);
        }

        public static void fnGetListOfUsers()
        {
            // set up domain context
            PrincipalContext ctx = new PrincipalContext(ContextType.Domain);
            // find the group in question
            GroupPrincipal group = GroupPrincipal.FindByIdentity(ctx, "USERS");
            // if found....
            if (group != null)
            {
                // iterate over members
                foreach (Principal p in group.GetMembers())
                {
                    Console.WriteLine("{0}: {1}",
                        p.StructuralObjectClass, p.DisplayName);
                    // do whatever you need to do to those members
                }
            }
        }

        public static void fnImp()
        {
                    using (var context = new PrincipalContext(ContextType.Domain, Environment.UserDomainName))
                    {
                        using (var searcher = new PrincipalSearcher(new UserPrincipal(context)))
                        {

                             /*var result = searcher.FindAll();
                                foreach (var item in result)
                                {
                                    DirectoryEntry de = item.GetUnderlyingObject() as DirectoryEntry;
                                    var d = de.Properties["manager"].Value;
                                    
                                    Console.WriteLine(d);
                                }*/
                            

                    foreach (var result in searcher.FindAll())
                    {
                        DirectoryEntry de = result.GetUnderlyingObject() as DirectoryEntry;
                        if ((string)de.Properties["givenName"].Value == Environment.UserName)
                        {
                            Console.WriteLine("First Name: " +
                            de.Properties["givenName"].Value);
                            Console.WriteLine("Last Name : " +
                            de.Properties["sn"].Value);
                            Console.WriteLine("SAM account name   : " +
                            de.Properties["samAccountName"].Value);
                            Console.WriteLine("User principal name: " +
                            de.Properties["userPrincipalName"].Value);
                            Console.WriteLine();
                            PropertyCollection pc = de.Properties;
                            foreach (PropertyValueCollection col in pc)
                            {
                                Console.WriteLine(col.PropertyName + " : " + col.Value);
                                Console.WriteLine();
                            }
                        }
                    }
                }
                    }
        }

        static DirectoryEntry _directoryEntry = null;

        private static DirectoryEntry SearchRoot
        {
            get
            {
                 _directoryEntry = null;
                if (_directoryEntry == null)
                {
                    _directoryEntry = new DirectoryEntry("LDAP://inad05.corp.neudesic.net", "monin.jose", "Password.1", AuthenticationTypes.Secure);
                }
                return _directoryEntry;
            }
        }

        public ADUserDetail GetUserByFullName(String userName)
        {
            try
            {
                _directoryEntry = null;
                DirectorySearcher directorySearch = new DirectorySearcher(SearchRoot);
                directorySearch.Filter = "(&(objectClass=user)(cn=" + userName + "))";
                SearchResult results = directorySearch.FindOne();
                if (results != null)
                {
                    DirectoryEntry user = new DirectoryEntry(results.Path, "monin.jose", "Password.1");
                    return ADUserDetail.GetUser(user);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static ADUserDetail GetUserByLoginName(String userName)
        {
            try
            {
                _directoryEntry = null;
                DirectorySearcher directorySearch = new DirectorySearcher(SearchRoot);
                directorySearch.Filter = "(&(objectClass=user)(SAMAccountName=" + userName + "))";
                SearchResult results = directorySearch.FindOne();
                if (results != null)
                {
                    DirectoryEntry user = new DirectoryEntry(results.Path, "monin.jose", "Password.1");
                    return ADUserDetail.GetUser(user);
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static void getAllUser()
        {

            _directoryEntry = null;
            DirectorySearcher directorySearch = new DirectorySearcher(SearchRoot);
            directorySearch.Filter = "(&(objectCategory=person)(objectClass=user))";
            var results = directorySearch.FindAll().Cast<SearchResult>();
            if (results != null)
            {
                List<ADUserDetail> users = new List<ADUserDetail>();
                List<UserDetail> usersList = new List<UserDetail>();

                System.Threading.Tasks.Parallel.ForEach(results, sr =>
                {
                    DirectoryEntry user = new DirectoryEntry(sr.Path);
                    ADUserDetail userAd = ADUserDetail.GetUser(user);
                    if (userAd != null  && userAd.EmailAddress != null && userAd.EmailAddress.Trim() != "")
                    {
                        if (userAd.FirstName.ToLower().Contains("monin"))
                        {

                        }
                        Console.WriteLine(userAd.EmailAddress);
                        UserDetail userDetail = userAd.copyToUserDetails();
                        if (userAd.Manager != null)
                        {
                            userDetail.ManagerEmail = userAd.Manager.EmailAddress.ToLower();
                        }
                        usersList.Add(userDetail);
                        users.Add(userAd);
                    }
                    
                });

                /*foreach (SearchResult sr in results)
                {
                    DirectoryEntry user = new DirectoryEntry(sr.Path);
                    ADUserDetail userAd = ADUserDetail.GetUser(user);
                    if (userAd != null && userAd.FirstName != null && userAd.EmailAddress != null && userAd.EmailAddress.Trim() != "" && userAd.FirstName.Trim() != "")
                    {
                        if (userAd.FirstName.ToLower().Contains("monin"))
                        {

                        }
                        Console.WriteLine(userAd.EmailAddress);
                        UserDetail userDetail = userAd.copyToUserDetails();
                        if (userAd.Manager != null)
                        {
                            //userDetail.ManagerEmail = userAd.Manager.EmailAddress.ToLower();
                        }
                        usersList.Add(userDetail);
                        users.Add(userAd);
                    }
                }*/

                using (var ctx = new UserDb())
                {
                    //var stud = new UserDetail() { StudentName = "Bill" };
                    for (int i = 0; i < usersList.Count; i++)
                    {
                        ctx.UserDetails.Add(usersList[i]);
                    }
                    
                    ctx.SaveChanges();
                }

                //DirectoryEntry user = new DirectoryEntry(results.Path, "monin.jose", "Password.1");
                //return ADUserDetail.GetUser(user);
            }

            //DirectoryEntry entry = new DirectoryEntry("LDAP://domainName");
            //DirectorySearcher dsearch = new DirectorySearcher(entry);


            //dsearch.Filter = "(&(objectClass=user))";
            //SearchResult result = dsearch.FindOne();
        }

        static void Main(string[] args)
        {
            try
            {
                getAllUser();

            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }

                }
            }

        }

        static void Main1(string[] args)
        {

            try
            {
                ADUserDetail user = GetUserByLoginName("monin.jose");
                
                Console.WriteLine();

                //Console.WriteLine(fnValidateUser());
                //fnImp();
                //fnListAllUser();

                //LdapConnection ldapConnection;
                //string ldapServer = "ldap://localhost:389/dc=example,dc=com";
                //NetworkCredential credential = new NetworkCredential("monin.jose", "Password", "inad05.corp.neudesic.net");

                // Create the new LDAP connection
                //ldapConnection = new LdapConnection(ldapServer);
                //ldapConnection.Credential = credential;
                //Console.WriteLine("LdapConnection is created successfully.");



                //DirectoryEntry directoryEntry = new DirectoryEntry("LDAP://inad05.corp.neudesic.net", "monin.jose", "Password");
                //DirectoryEntry directoryEntry = new DirectoryEntry();
                //DirectorySearcher mySearcher = new DirectorySearcher(directoryEntry);
                //mySearcher.Filter = "(&(objectClass=user)(anr=monin))";
                //foreach (SearchResult resEnt in mySearcher.FindAll())
                //{

                //}


                //DC is your domain. If you want to connect to the domain example.com than your dc's are: DC=example,DC=com

                //You actually don't need any hostname or ip address of your domain controller (There could be plenty of them).

                //Just imagine that you're connecting to the domain itself. So for connecting to the domain example.com you can simply write

                DirectoryEntry directoryEntry = new DirectoryEntry("ldap://localhost:389/dc=corp.neudesic,dc=net");
                DirectorySearcher searcher = new DirectorySearcher(directoryEntry)
                {
                    PageSize = int.MaxValue,
                    Filter = "(&(objectCategory=person)(objectClass=user)(sAMAccountName=AnAccountName))"
                };
                searcher.PropertiesToLoad.Add("sn");

                var result = searcher.FindOne();

                if (result == null)
                {
                    return; // Or whatever you need to do in this case
                }

                string surname;

                if (result.Properties.Contains("sn"))
                {
                    surname = result.Properties["sn"][0].ToString();
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("\r\nUnexpected exception occurred:\r\n\t" + e.GetType() + ":" + e.Message);
            }

            //try
            //{
            //    string domainControllerName = "PDC";
            //    string domainName = "inad05.corp.neudesic.net"; // leave out the .Local, this is just to use as the prefix for the username if the user left it off or didn't use the principal address notation
            //    string username = "monin.jose";
            //    string password = "Password.1";

            //    using (var ldap = new PrincipalContext(ContextType.Domain, domainControllerName))
            //    {
            //        var usernameToValidate = username;
            //        if (!usernameToValidate.Any(c => c == '@' || c == '\\'))
            //            usernameToValidate = $"{domainName}\\{username}";

            //        if (!ldap.ValidateCredentials(username, password, ContextOptions.SimpleBind))
            //            throw new Exception();
            //    }
            //}
            //catch (Exception e)
            //{
            //    var m = e.Message;
            //    throw;
            //}

            //try
            //{

            //    PrincipalContext insPrincipalContext = new PrincipalContext(ContextType.Domain, "ldap://localhost:389/dc=maxcrc,dc=com", "monin.jose", "password");

            //    LdapConnection connection = new LdapConnection("corp.neudesic.net");
            //    NetworkCredential credential = new NetworkCredential("monin.jose", "Password.1");
            //    connection.Credential = credential;
            //    connection.Bind();
            //    Console.WriteLine("logged in");
            //}
            //catch (Exception e)
            //{
            //    var m = e.Message;
            //    throw;
            //}

            //DirectoryEntry enTry = new DirectoryEntry("LDAP://OU=MyUsers,DC=Steve,DC=Schofield,DC=com");
            //DirectorySearcher mySearcher = new DirectorySearcher(enTry);

            //mySearcher.Filter = "(&(objectClass=user)(anr=monin))";

            //try
            //{

            //    foreach (SearchResult resEnt in mySearcher.FindAll())
            //    {

            //        /*Console.WriteLine(resEnt.GetDirectoryEntry.Properties.Item("cn").Value);

            //        Console.WriteLine(resEnt.GetDirectoryEntry.Properties.Item("distinguishedName").Value);

            //        Console.WriteLine(resEnt.GetDirectoryEntry.Properties.Item("name").Value);

            //        Console.WriteLine(resEnt.GetDirectoryEntry.Properties.Item("givenName").Value);

            //        Console.WriteLine(resEnt.GetDirectoryEntry.Properties.Item("displayName").Value);*/

            //    }

            //}
            //catch (Exception f)
            //{

            //    Console.WriteLine(f.Message);

            //}
        }
    }
}
