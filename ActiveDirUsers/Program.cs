using System;
using System.Collections;
using System.Collections.Generic;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActiveDirUsers
{
    class Program
    {
        public static List<object> deps = new List<object>();
        static void Main(string[] args)
        {

            SomeUserModelClass someUserModelClass = new SomeUserModelClass();
            Task<IEnumerable<SomeUserModelClass>> usersEnum = GetUsers();
            Task continuation = usersEnum.ContinueWith(x => someUserModelClass = Print(x.Result));
            continuation.Wait();

            Console.ReadLine();

        }

        private static SomeUserModelClass Print(IEnumerable<SomeUserModelClass> result)
        {
            foreach (var item in result)
            {
                Console.WriteLine(item.userName + " > " + item.email);
            }
            return null;
        }

        public static Task<IEnumerable<SomeUserModelClass>> GetUsers()
        {
            return Task.Run(() =>
            {
                PrincipalContext context = new PrincipalContext(ContextType.Domain, "corp.neudesic.net");
                UserPrincipal principal = new UserPrincipal(context);
                principal.Enabled = true;
                PrincipalSearcher searcher = new PrincipalSearcher(principal);

                var users = searcher.FindAll().Cast<UserPrincipal>()
                    //.Where(x => x.SomeProperty... // Perform queries)

                    .Select(x => new SomeUserModelClass
                    {
                        userName = x.SamAccountName,
                        email = x.UserPrincipalName,
                        guid = x.Guid.Value
                    }).OrderBy(x => x.userName).AsEnumerable();
                return users;
            });
        }

        public static void getGlbList()
        {
            using (var context = new PrincipalContext(ContextType.Domain, "corp.neudesic.net"))
            {
                var principal = new UserPrincipal(context);

                using (var searcher = new PrincipalSearcher(principal))
                {
                    //searcher.QueryFilter();
                    var dirContext = searcher.FindAll();
                    foreach (var result in dirContext)
                    {
                        DirectoryEntry de = result.GetUnderlyingObject() as DirectoryEntry;
                        try
                        {
                            Console.WriteLine(de.Properties["mail"].Value.ToString());
                        }
                        catch (Exception e)
                        {

                        }

                        /*try
                        {
                            foreach (var name in de.Properties.PropertyNames)
                            {
                                Console.WriteLine(name.ToString() + " > " +de.Properties[name.ToString()].Value);
                            }

                        }
                        catch (Exception ex)
                        {
                            
                        }*/
                        //Console.WriteLine(de.Properties[""].Value);
                    }
                }
            }
            List<object> depsdis = deps.Distinct().ToList();

            foreach (var item in depsdis)
            {
                Console.WriteLine(item);
            }
        }

        

        //public static Task GetUsers()
        //{
        //    return Task.Run(() =>
        //    {
        //        PrincipalContext context = new PrincipalContext(ContextType.Domain);
        //        UserPrincipal principal = new UserPrincipal(context);
        //        principal.Enabled = true;
        //        PrincipalSearcher searcher = new PrincipalSearcher(principal);

        //        var users = searcher.FindAll().Cast<UserPrincipal>()
        //            //.Where(x => x.SomeProperty... // Perform queries)

        //            .Select(x => new SomeUserModelClass
        //            {
        //                userName = x.SamAccountName,
        //                email = x.UserPrincipalName,
        //                guid = x.Guid.Value
        //            }).OrderBy(x => x.userName).AsEnumerable();

        //        return users;
        //    });
        //}
        public static void getList()
        {
            using (var context = new PrincipalContext(ContextType.Domain, "corp.neudesic.net"))
            {
                using (var searcher = new PrincipalSearcher(new UserPrincipal(context)))
                {
                    foreach (var result in searcher.FindAll())
                    {
                        DirectoryEntry entry = result.GetUnderlyingObject() as DirectoryEntry;

                        foreach (string key in entry.Properties.PropertyNames)
                        {

                            string sPropertyValues = String.Empty;

                            foreach (object pc in entry.Properties[key])
                            {

                                sPropertyValues += Convert.ToString(pc) + ";";

                            }

                            sPropertyValues = sPropertyValues.Substring(0, sPropertyValues.Length - 1);

                            Console.WriteLine(key + "=" + sPropertyValues);
                        }

                        Console.WriteLine("=======================================");
                    }

                    Console.ReadKey();
                }
            }
        }

    }

    public class SomeUserModelClass
    {
        public string userName { get;set;}
        public string email { get; set; }
        public Guid guid { get; set; }
    }
}
