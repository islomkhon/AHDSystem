using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HCMApi.Modal
{
    public class Utils
    {

        public ListUserId generateUserDropdownList(List<DAL.NueUserProfile> userProfiles)
        {
            ListUserId listUserId = new ListUserId();
            List<int> userIds = new List<int>();
            List<string> emails = new List<string>();

            if(userProfiles != null && userProfiles.Count > 0)
            {
                for (int i = 0; i < userProfiles.Count; i++)
                {
                    try
                    {
                        userIds.Add(userProfiles[i].Id);
                        emails.Add(userProfiles[i].Email);
                    }
                    catch (Exception e)
                    {
                    }
                }
            }
            listUserId.userIds = userIds;
            listUserId.emails = emails;
            return listUserId;
        }

    }
}
