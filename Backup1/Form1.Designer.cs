namespace AccountManagementGUI
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tcMain = new System.Windows.Forms.TabControl();
            this.tpGroups = new System.Windows.Forms.TabPage();
            this.tpUsers = new System.Windows.Forms.TabPage();
            this.lbUsers = new System.Windows.Forms.ListBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnDeleteUser = new System.Windows.Forms.Button();
            this.btnEditUser = new System.Windows.Forms.Button();
            this.btnNewUser = new System.Windows.Forms.Button();
            this.btnClearFilterUsers = new System.Windows.Forms.Button();
            this.btnFilterUsers = new System.Windows.Forms.Button();
            this.lbGroups = new System.Windows.Forms.ListBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnDeleteGroup = new System.Windows.Forms.Button();
            this.btnEditGroup = new System.Windows.Forms.Button();
            this.btnNewGroup = new System.Windows.Forms.Button();
            this.btnClearFilterGroups = new System.Windows.Forms.Button();
            this.btnFilterGroups = new System.Windows.Forms.Button();
            this.btnChangePassword = new System.Windows.Forms.Button();
            this.btnListGroupUser = new System.Windows.Forms.Button();
            this.btnListUsersGroups = new System.Windows.Forms.Button();
            this.btnAddUserToAGroup = new System.Windows.Forms.Button();
            this.btnRemoveUserFromAGroup = new System.Windows.Forms.Button();
            this.tcMain.SuspendLayout();
            this.tpGroups.SuspendLayout();
            this.tpUsers.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tcMain
            // 
            this.tcMain.Controls.Add(this.tpGroups);
            this.tcMain.Controls.Add(this.tpUsers);
            this.tcMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcMain.Location = new System.Drawing.Point(0, 0);
            this.tcMain.Name = "tcMain";
            this.tcMain.SelectedIndex = 0;
            this.tcMain.Size = new System.Drawing.Size(962, 512);
            this.tcMain.TabIndex = 2;
            // 
            // tpGroups
            // 
            this.tpGroups.Controls.Add(this.lbGroups);
            this.tpGroups.Controls.Add(this.panel2);
            this.tpGroups.Location = new System.Drawing.Point(4, 22);
            this.tpGroups.Name = "tpGroups";
            this.tpGroups.Padding = new System.Windows.Forms.Padding(3);
            this.tpGroups.Size = new System.Drawing.Size(954, 486);
            this.tpGroups.TabIndex = 0;
            this.tpGroups.Text = "Groups";
            this.tpGroups.UseVisualStyleBackColor = true;
            // 
            // tpUsers
            // 
            this.tpUsers.Controls.Add(this.lbUsers);
            this.tpUsers.Controls.Add(this.panel1);
            this.tpUsers.Location = new System.Drawing.Point(4, 22);
            this.tpUsers.Name = "tpUsers";
            this.tpUsers.Padding = new System.Windows.Forms.Padding(3);
            this.tpUsers.Size = new System.Drawing.Size(954, 486);
            this.tpUsers.TabIndex = 1;
            this.tpUsers.Text = "Users";
            this.tpUsers.UseVisualStyleBackColor = true;
            // 
            // lbUsers
            // 
            this.lbUsers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbUsers.FormattingEnabled = true;
            this.lbUsers.Location = new System.Drawing.Point(3, 32);
            this.lbUsers.Name = "lbUsers";
            this.lbUsers.Size = new System.Drawing.Size(948, 446);
            this.lbUsers.TabIndex = 6;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnRemoveUserFromAGroup);
            this.panel1.Controls.Add(this.btnAddUserToAGroup);
            this.panel1.Controls.Add(this.btnListUsersGroups);
            this.panel1.Controls.Add(this.btnChangePassword);
            this.panel1.Controls.Add(this.btnDeleteUser);
            this.panel1.Controls.Add(this.btnEditUser);
            this.panel1.Controls.Add(this.btnNewUser);
            this.panel1.Controls.Add(this.btnClearFilterUsers);
            this.panel1.Controls.Add(this.btnFilterUsers);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(948, 29);
            this.panel1.TabIndex = 5;
            // 
            // btnDeleteUser
            // 
            this.btnDeleteUser.Location = new System.Drawing.Point(327, 3);
            this.btnDeleteUser.Name = "btnDeleteUser";
            this.btnDeleteUser.Size = new System.Drawing.Size(75, 23);
            this.btnDeleteUser.TabIndex = 4;
            this.btnDeleteUser.Text = "Delete User";
            this.btnDeleteUser.UseVisualStyleBackColor = true;
            this.btnDeleteUser.Click += new System.EventHandler(this.btnDeleteUser_Click);
            // 
            // btnEditUser
            // 
            this.btnEditUser.Location = new System.Drawing.Point(246, 3);
            this.btnEditUser.Name = "btnEditUser";
            this.btnEditUser.Size = new System.Drawing.Size(75, 23);
            this.btnEditUser.TabIndex = 3;
            this.btnEditUser.Text = "Edit User";
            this.btnEditUser.UseVisualStyleBackColor = true;
            this.btnEditUser.Click += new System.EventHandler(this.btnEditUser_Click);
            // 
            // btnNewUser
            // 
            this.btnNewUser.Location = new System.Drawing.Point(165, 3);
            this.btnNewUser.Name = "btnNewUser";
            this.btnNewUser.Size = new System.Drawing.Size(75, 23);
            this.btnNewUser.TabIndex = 2;
            this.btnNewUser.Text = "New User";
            this.btnNewUser.UseVisualStyleBackColor = true;
            this.btnNewUser.Click += new System.EventHandler(this.btnNewUser_Click);
            // 
            // btnClearFilterUsers
            // 
            this.btnClearFilterUsers.Location = new System.Drawing.Point(84, 3);
            this.btnClearFilterUsers.Name = "btnClearFilterUsers";
            this.btnClearFilterUsers.Size = new System.Drawing.Size(75, 23);
            this.btnClearFilterUsers.TabIndex = 1;
            this.btnClearFilterUsers.Text = "Clear Filter";
            this.btnClearFilterUsers.UseVisualStyleBackColor = true;
            this.btnClearFilterUsers.Click += new System.EventHandler(this.btnClearFilterUsers_Click);
            // 
            // btnFilterUsers
            // 
            this.btnFilterUsers.Location = new System.Drawing.Point(3, 3);
            this.btnFilterUsers.Name = "btnFilterUsers";
            this.btnFilterUsers.Size = new System.Drawing.Size(75, 23);
            this.btnFilterUsers.TabIndex = 0;
            this.btnFilterUsers.Text = "Filter";
            this.btnFilterUsers.UseVisualStyleBackColor = true;
            this.btnFilterUsers.Click += new System.EventHandler(this.btnFilterUsers_Click);
            // 
            // lbGroups
            // 
            this.lbGroups.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbGroups.FormattingEnabled = true;
            this.lbGroups.Location = new System.Drawing.Point(3, 32);
            this.lbGroups.Name = "lbGroups";
            this.lbGroups.Size = new System.Drawing.Size(948, 446);
            this.lbGroups.TabIndex = 8;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnListGroupUser);
            this.panel2.Controls.Add(this.btnDeleteGroup);
            this.panel2.Controls.Add(this.btnEditGroup);
            this.panel2.Controls.Add(this.btnNewGroup);
            this.panel2.Controls.Add(this.btnClearFilterGroups);
            this.panel2.Controls.Add(this.btnFilterGroups);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(948, 29);
            this.panel2.TabIndex = 7;
            // 
            // btnDeleteGroup
            // 
            this.btnDeleteGroup.Location = new System.Drawing.Point(327, 3);
            this.btnDeleteGroup.Name = "btnDeleteGroup";
            this.btnDeleteGroup.Size = new System.Drawing.Size(85, 23);
            this.btnDeleteGroup.TabIndex = 4;
            this.btnDeleteGroup.Text = "Delete Group";
            this.btnDeleteGroup.UseVisualStyleBackColor = true;
            this.btnDeleteGroup.Click += new System.EventHandler(this.btnDeleteGroup_Click);
            // 
            // btnEditGroup
            // 
            this.btnEditGroup.Location = new System.Drawing.Point(246, 3);
            this.btnEditGroup.Name = "btnEditGroup";
            this.btnEditGroup.Size = new System.Drawing.Size(75, 23);
            this.btnEditGroup.TabIndex = 3;
            this.btnEditGroup.Text = "Edit Group";
            this.btnEditGroup.UseVisualStyleBackColor = true;
            this.btnEditGroup.Click += new System.EventHandler(this.btnEditGroup_Click);
            // 
            // btnNewGroup
            // 
            this.btnNewGroup.Location = new System.Drawing.Point(165, 3);
            this.btnNewGroup.Name = "btnNewGroup";
            this.btnNewGroup.Size = new System.Drawing.Size(75, 23);
            this.btnNewGroup.TabIndex = 2;
            this.btnNewGroup.Text = "New Group";
            this.btnNewGroup.UseVisualStyleBackColor = true;
            this.btnNewGroup.Click += new System.EventHandler(this.btnNewGroup_Click);
            // 
            // btnClearFilterGroups
            // 
            this.btnClearFilterGroups.Location = new System.Drawing.Point(84, 3);
            this.btnClearFilterGroups.Name = "btnClearFilterGroups";
            this.btnClearFilterGroups.Size = new System.Drawing.Size(75, 23);
            this.btnClearFilterGroups.TabIndex = 1;
            this.btnClearFilterGroups.Text = "Clear Filter";
            this.btnClearFilterGroups.UseVisualStyleBackColor = true;
            this.btnClearFilterGroups.Click += new System.EventHandler(this.btnClearFilterGroups_Click);
            // 
            // btnFilterGroups
            // 
            this.btnFilterGroups.Location = new System.Drawing.Point(3, 3);
            this.btnFilterGroups.Name = "btnFilterGroups";
            this.btnFilterGroups.Size = new System.Drawing.Size(75, 23);
            this.btnFilterGroups.TabIndex = 0;
            this.btnFilterGroups.Text = "Filter";
            this.btnFilterGroups.UseVisualStyleBackColor = true;
            this.btnFilterGroups.Click += new System.EventHandler(this.btnFilterGroups_Click);
            // 
            // btnChangePassword
            // 
            this.btnChangePassword.Location = new System.Drawing.Point(408, 3);
            this.btnChangePassword.Name = "btnChangePassword";
            this.btnChangePassword.Size = new System.Drawing.Size(111, 23);
            this.btnChangePassword.TabIndex = 5;
            this.btnChangePassword.Text = "Change Password";
            this.btnChangePassword.UseVisualStyleBackColor = true;
            this.btnChangePassword.Click += new System.EventHandler(this.btnChangePassword_Click);
            // 
            // btnListGroupUser
            // 
            this.btnListGroupUser.Location = new System.Drawing.Point(418, 3);
            this.btnListGroupUser.Name = "btnListGroupUser";
            this.btnListGroupUser.Size = new System.Drawing.Size(103, 23);
            this.btnListGroupUser.TabIndex = 5;
            this.btnListGroupUser.Text = "List Group Users";
            this.btnListGroupUser.UseVisualStyleBackColor = true;
            this.btnListGroupUser.Click += new System.EventHandler(this.btnListGroupUser_Click);
            // 
            // btnListUsersGroups
            // 
            this.btnListUsersGroups.Location = new System.Drawing.Point(525, 3);
            this.btnListUsersGroups.Name = "btnListUsersGroups";
            this.btnListUsersGroups.Size = new System.Drawing.Size(103, 23);
            this.btnListUsersGroups.TabIndex = 6;
            this.btnListUsersGroups.Text = "List Users Groups";
            this.btnListUsersGroups.UseVisualStyleBackColor = true;
            this.btnListUsersGroups.Click += new System.EventHandler(this.btnListUsersGroups_Click);
            // 
            // btnAddUserToAGroup
            // 
            this.btnAddUserToAGroup.Location = new System.Drawing.Point(635, 3);
            this.btnAddUserToAGroup.Name = "btnAddUserToAGroup";
            this.btnAddUserToAGroup.Size = new System.Drawing.Size(132, 23);
            this.btnAddUserToAGroup.TabIndex = 7;
            this.btnAddUserToAGroup.Text = "Add User To A Group";
            this.btnAddUserToAGroup.UseVisualStyleBackColor = true;
            this.btnAddUserToAGroup.Click += new System.EventHandler(this.btnAddUserToAGroup_Click);
            // 
            // btnRemoveUserFromAGroup
            // 
            this.btnRemoveUserFromAGroup.Location = new System.Drawing.Point(773, 3);
            this.btnRemoveUserFromAGroup.Name = "btnRemoveUserFromAGroup";
            this.btnRemoveUserFromAGroup.Size = new System.Drawing.Size(167, 23);
            this.btnRemoveUserFromAGroup.TabIndex = 8;
            this.btnRemoveUserFromAGroup.Text = "Remove User From A Group";
            this.btnRemoveUserFromAGroup.UseVisualStyleBackColor = true;
            this.btnRemoveUserFromAGroup.Click += new System.EventHandler(this.btnRemoveUserFromAGroup_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(962, 512);
            this.Controls.Add(this.tcMain);
            this.Name = "frmMain";
            this.Text = "Account Management";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tcMain.ResumeLayout(false);
            this.tpGroups.ResumeLayout(false);
            this.tpUsers.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tcMain;
        private System.Windows.Forms.TabPage tpGroups;
        private System.Windows.Forms.TabPage tpUsers;
        private System.Windows.Forms.ListBox lbUsers;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnClearFilterUsers;
        private System.Windows.Forms.Button btnFilterUsers;
        private System.Windows.Forms.Button btnDeleteUser;
        private System.Windows.Forms.Button btnEditUser;
        private System.Windows.Forms.Button btnNewUser;
        private System.Windows.Forms.ListBox lbGroups;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnDeleteGroup;
        private System.Windows.Forms.Button btnEditGroup;
        private System.Windows.Forms.Button btnNewGroup;
        private System.Windows.Forms.Button btnClearFilterGroups;
        private System.Windows.Forms.Button btnFilterGroups;
        private System.Windows.Forms.Button btnChangePassword;
        private System.Windows.Forms.Button btnListGroupUser;
        private System.Windows.Forms.Button btnListUsersGroups;
        private System.Windows.Forms.Button btnRemoveUserFromAGroup;
        private System.Windows.Forms.Button btnAddUserToAGroup;
    }
}

