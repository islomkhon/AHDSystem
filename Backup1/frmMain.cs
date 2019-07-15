using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.DirectoryServices.AccountManagement;

namespace AccountManagementGUI
{

    public partial class frmMain : Form
    {
        internal class UserPassword
        {
            private string _Password;

            public string Password
            {
                get { return _Password; }
                set { _Password = value; }
            }
        }
        internal class UserGroup
        {
            private string _GroupName;

            public string GroupName
            {
                get { return _GroupName; }
                set { _GroupName = value; }
            }
        }
        public enum ActionTypes
        {
            Filter,
            Save,
            None,
        }
        public frmMain()
        {
            InitializeComponent();
        }
        PrincipalContext insPrincipalContext = new PrincipalContext(ContextType.Machine);
        //PrincipalContext insPrincipalContext = new PrincipalContext(ContextType.Domain, "MyDomain", "DC=MyDomain,DC=com"); //domaine bağlanma
        //PrincipalContext insPrincipalContext = new PrincipalContext(ContextType.Machine,"TAMERO","administrator","password");//lokal bilgisayara bir kullanıcı ile bağlanma
        private void Form1_Load(object sender, EventArgs e)
        {
            ListGroups();
            ListUsers();
            
        }
        private void SearchGroups(GroupPrincipal parGroupPrincipal)
        {
            lbGroups.Items.Clear();
            PrincipalSearcher insPrincipalSearcher = new PrincipalSearcher();
            insPrincipalSearcher.QueryFilter = parGroupPrincipal;
            PrincipalSearchResult<Principal> results = insPrincipalSearcher.FindAll();
            foreach (Principal p in results)
            {
                lbGroups.Items.Add(p);
            }
        }
        private void SearchUsers(UserPrincipal parUserPrincipal)
        {

            lbUsers.Items.Clear();
            PrincipalSearcher insPrincipalSearcher = new PrincipalSearcher();
            insPrincipalSearcher.QueryFilter = parUserPrincipal;
            PrincipalSearchResult<Principal> results = insPrincipalSearcher.FindAll();
            foreach (Principal p in results)
            {
                lbUsers.Items.Add(p);
            }
        }
        private void ListGroups()
        {
            GroupPrincipal insGroupPrincipal = new GroupPrincipal(insPrincipalContext);
            insGroupPrincipal.Name = "*";
            SearchGroups(insGroupPrincipal);
        }
        private void ListUsers()
        {
            UserPrincipal insUserPrincipal = new UserPrincipal(insPrincipalContext);
            insUserPrincipal.Name = "*";
            SearchUsers(insUserPrincipal);
        }

        private void btnFilterUsers_Click(object sender, EventArgs e)
        {
            UserPrincipal insUserPrincipal = new UserPrincipal(insPrincipalContext);
            frmProperties insFrmProperties = new frmProperties(insUserPrincipal, ActionTypes.Filter);
            insFrmProperties.ShowDialog();
            if (insFrmProperties.DialogResult == DialogResult.OK)
            {
                SearchUsers(insUserPrincipal);
            }
        }
        private void btnFilterGroups_Click(object sender, EventArgs e)
        {
            GroupPrincipal insGroupPrincipal = new GroupPrincipal(insPrincipalContext);
            frmProperties insFrmProperties = new frmProperties(insGroupPrincipal, ActionTypes.Filter);
            insFrmProperties.ShowDialog();
            if (insFrmProperties.DialogResult == DialogResult.OK)
            {
                SearchGroups(insGroupPrincipal);
            }
        }

        private void btnClearFilterUsers_Click(object sender, EventArgs e)
        {
            ListUsers();
        }
        private void btnClearFilterGroups_Click(object sender, EventArgs e)
        {
            ListGroups();
        }

        private void btnNewUser_Click(object sender, EventArgs e)
        {
            UserPrincipal insUserPrincipal = new UserPrincipal(insPrincipalContext);
            frmProperties insFrmProperties = new frmProperties(insUserPrincipal, ActionTypes.Save);
            insFrmProperties.ShowDialog();
            if (insFrmProperties.DialogResult == DialogResult.OK)
            {
                try
                {
                    insUserPrincipal.Save();
                    insUserPrincipal.Dispose();
                    MessageBox.Show("User created.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                ListUsers();
            }
        }
        private void btnNewGroup_Click(object sender, EventArgs e)
        {
            GroupPrincipal insGroupPrincipal = new GroupPrincipal(insPrincipalContext);
            frmProperties insFrmProperties = new frmProperties(insGroupPrincipal, ActionTypes.Save);
            insFrmProperties.ShowDialog();
            if (insFrmProperties.DialogResult == DialogResult.OK)
            {
                try
                {
                    insGroupPrincipal.Save();
                    insGroupPrincipal.Dispose();
                    MessageBox.Show("Group created.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                ListGroups();
            }
        }

        private void btnEditUser_Click(object sender, EventArgs e)
        {
            if (lbUsers.SelectedItem == null)
            {
                MessageBox.Show("Please select a user");
                return;
            }
            UserPrincipal insUserPrincipal = (UserPrincipal)lbUsers.SelectedItem;
            frmProperties insFrmProperties = new frmProperties(insUserPrincipal, ActionTypes.Save);
            insFrmProperties.ShowDialog();
            if (insFrmProperties.DialogResult == DialogResult.OK)
            {
                try
                {
                    insUserPrincipal.Save();
                    insUserPrincipal.Dispose();
                    MessageBox.Show("User updated.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                ListUsers();
            }
        }
        private void btnEditGroup_Click(object sender, EventArgs e)
        {
            if (lbGroups.SelectedItem == null)
            {
                MessageBox.Show("Please select a group");
                return;
            }
            GroupPrincipal insGroupPrincipal = (GroupPrincipal)lbGroups.SelectedItem;
            frmProperties insFrmProperties = new frmProperties(insGroupPrincipal, ActionTypes.Save);
            insFrmProperties.ShowDialog();
            if (insFrmProperties.DialogResult == DialogResult.OK)
            {
                try
                {
                    insGroupPrincipal.Save();
                    insGroupPrincipal.Dispose();
                    MessageBox.Show("Group updated.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                ListGroups();
            }
        }

        private void btnDeleteUser_Click(object sender, EventArgs e)
        {
            if (lbUsers.SelectedItem == null)
            {
                MessageBox.Show("Please select a user");
                return;
            }
            UserPrincipal insUserPrincipal = (UserPrincipal)lbUsers.SelectedItem;
            try
            {
                insUserPrincipal.Delete();
                insUserPrincipal.Dispose();
                MessageBox.Show("User deleted.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            ListUsers();
        }
        private void btnDeleteGroup_Click(object sender, EventArgs e)
        {
            if (lbGroups.SelectedItem == null)
            {
                MessageBox.Show("Please select a group");
                return;
            }
            GroupPrincipal insGroupPrincipal = (GroupPrincipal)lbGroups.SelectedItem;
            try
            {
                insGroupPrincipal.Delete();
                insGroupPrincipal.Dispose();
                MessageBox.Show("Group deleted.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            ListUsers();
        }

        private void btnChangePassword_Click(object sender, EventArgs e)
        {
            if (lbUsers.SelectedItem == null)
            {
                MessageBox.Show("Please select a user");
                return;
            }
            UserPrincipal insUserPrincipal = (UserPrincipal)lbUsers.SelectedItem;

            UserPassword password = new UserPassword();
            frmProperties insFrmProperties = new frmProperties(password, ActionTypes.Save);
            insFrmProperties.ShowDialog();
            if (insFrmProperties.DialogResult == DialogResult.OK)
            {
                try
                {
                    insUserPrincipal.SetPassword(password.Password);
                    MessageBox.Show("Password changed.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                ListUsers();
            }
        }

        private void btnListGroupUser_Click(object sender, EventArgs e)
        {
            if (lbGroups.SelectedItem == null)
            {
                MessageBox.Show("Please select a group");
                return;
            }
            GroupPrincipal insGroupPrincipal = (GroupPrincipal)lbGroups.SelectedItem;
            List<Principal> insListPrincipal = new List<Principal>();
            foreach (Principal p in insGroupPrincipal.Members)
            {
                insListPrincipal.Add(p);
            }
            frmProperties insFrmProperties = new frmProperties(insListPrincipal, ActionTypes.None);
            insFrmProperties.ShowDialog();
        }

        private void btnListUsersGroups_Click(object sender, EventArgs e)
        {
            if (lbUsers.SelectedItem == null)
            {
                MessageBox.Show("Please select a user");
                return;
            }
            UserPrincipal insUserPrincipal = (UserPrincipal)lbUsers.SelectedItem;
            List<Principal> insListPrincipal = new List<Principal>();
            foreach (Principal p in insUserPrincipal.GetGroups())
            {
                insListPrincipal.Add(p);
            }
            frmProperties insFrmProperties = new frmProperties(insListPrincipal, ActionTypes.None);
            insFrmProperties.ShowDialog();
        }

        private void btnAddUserToAGroup_Click(object sender, EventArgs e)
        {
            if (lbUsers.SelectedItem == null)
            {
                MessageBox.Show("Please select a user");
                return;
            }
            UserPrincipal insUserPrincipal = (UserPrincipal)lbUsers.SelectedItem;

            UserGroup group = new UserGroup();
            frmProperties insFrmProperties = new frmProperties(group, ActionTypes.Save);
            insFrmProperties.ShowDialog();
            if (insFrmProperties.DialogResult == DialogResult.OK)
            {
                try
                {
                    GroupPrincipal groupPrincipal = GroupPrincipal.FindByIdentity(insPrincipalContext, group.GroupName);
                    if (groupPrincipal == null)
                    {
                        MessageBox.Show("Group not found.");
                        return;
                    }
                    if (groupPrincipal.Members.Contains(insPrincipalContext, IdentityType.SamAccountName, insUserPrincipal.SamAccountName))
                    {
                        MessageBox.Show(insUserPrincipal.Name + " is already a member of group " + group.GroupName);
                        return;
                    }
                    groupPrincipal.Members.Add(insUserPrincipal);
                    groupPrincipal.Save();

                    MessageBox.Show("User added to group.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                ListUsers();
            }
        }

        private void btnRemoveUserFromAGroup_Click(object sender, EventArgs e)
        {
            if (lbUsers.SelectedItem == null)
            {
                MessageBox.Show("Please select a user");
                return;
            }
            UserPrincipal insUserPrincipal = (UserPrincipal)lbUsers.SelectedItem;

            UserGroup group = new UserGroup();
            frmProperties insFrmProperties = new frmProperties(group, ActionTypes.Save);
            insFrmProperties.ShowDialog();
            if (insFrmProperties.DialogResult == DialogResult.OK)
            {
                try
                {
                    GroupPrincipal groupPrincipal = GroupPrincipal.FindByIdentity(insPrincipalContext, group.GroupName);
                    if (groupPrincipal == null)
                    {
                        MessageBox.Show("Group not found.");
                        return;
                    }
                    if (!groupPrincipal.Members.Contains(insPrincipalContext, IdentityType.SamAccountName, insUserPrincipal.SamAccountName))
                    {
                        MessageBox.Show(insUserPrincipal.Name + " is not a member of group " + group.GroupName);
                        return;
                    }
                    groupPrincipal.Members.Remove(insUserPrincipal);
                    groupPrincipal.Save();

                    MessageBox.Show("User removed from group.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                ListUsers();
            }
        }
    }
}
