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
    public partial class frmProperties : Form
    {
        public frmProperties(object propertyItem, frmMain.ActionTypes actionType)
        {
            InitializeComponent();
            if (propertyItem is List<Principal>)
            {
                pgMain.SelectedObject = ((List<Principal>)propertyItem).ToArray();
            }
            else
            {
                pgMain.SelectedObject = propertyItem;
            }
            btnAction.Text = actionType.ToString();
            btnAction.Visible = (actionType != frmMain.ActionTypes.None);

        }

        private void btnAction_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
