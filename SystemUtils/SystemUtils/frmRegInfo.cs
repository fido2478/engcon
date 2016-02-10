using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SystemUtils
{
    public partial class frmRegInfo : Form
    {
        public frmRegInfo()
        {
            InitializeComponent();
        }

        private void btnGetInfo_Click(object sender, EventArgs e)
        {
            // grab the registry key information
            string RegKey = txtRegKey.Text;
            // get the last subkey from the string
            // example: System\\Drivers\\Active returns Active
            string[] RegSubKeys = RegKey.Split('\\');
            string lastSubKey = (string)RegSubKeys.GetValue(RegSubKeys.GetUpperBound(0));

            // Retrieve the registry information
            Cursor.Current = Cursors.WaitCursor;
            TreeNode RootNode = new TreeNode(lastSubKey);
            PopulateTreeFromReg(RegKey, "", ref RootNode);
            treRegInfo.Nodes.Add(RootNode);
            Cursor.Current = Cursors.Default;
        }

        // This function takes a registry key and recursively enumerates its values
        // and subkeys while adding each to the parent TreeNode for listing
        // in the TreeView control.
        private void PopulateTreeFromReg(string rootKey, string keyName, ref TreeNode parent)
        {
            // make sure rootKey and keyName aren't empty
            if (!rootKey.Equals(""))
            {
                // if keyName is not empty create new node for this key
                // otherwise its the root key so just use parent
                TreeNode newNode;
                bool Named;     // true if it's the root node
                if (!keyName.Equals(""))
                {
                    newNode = new TreeNode(keyName);
                    Named = false;
                }
                else
                {
                    // current key is the root node, so use parent
                    // (the passed in root node) as the current node
                    newNode = parent;
                    Named = true;
                }

                // list the values first
                string Values = Registry.EnumValues(Registry.RootKey.LocalMachine, rootKey, false);

                // make sure Values is not null
                if (Values != null)
                {
                    // split the coma-delimited return string up into the individual values
                    string[] ValList = Values.Split(',');
                    // iterate through each value and add to the tree
                    foreach (string curVal in ValList)
                    {
                        if (!curVal.Equals(""))
                        {
                            TreeNode newVal = new TreeNode(curVal);
                            newNode.Nodes.Add(newVal);
                        }
                    }
                }

                // now list the keys
                string Keys = Registry.EnumValues(Registry.RootKey.LocalMachine, rootKey, true);
                if (Keys != null)
                {
                    // split up the coma-delimited key list
                    string[] KeyList = Keys.Split(',');
                    // iterate through each subkey and add to the tree
                    foreach (string curKey in KeyList)
                    {
                        if (!curKey.Equals(""))
                        {
                            string subKey = rootKey + "\\" + curKey;
                            PopulateTreeFromReg(subKey, curKey, ref newNode);
                        }
                    }
                }
                // now add the node to the parent
                if (!Named)
                    parent.Nodes.Add(newNode);
            }
        }

        #region Form Selection Code
        private void ShowPowerInfo(object sender, EventArgs e)
        {
            // create a new PowerInfo form if necessary
            if (frmMain.PIForm == null)
                frmMain.PIForm = new frmPowerInfo();
            frmMain.PIForm.Show();
        }

        private void ShowPowerTrans(object sender, EventArgs e)
        {
            // create a new PowerTrans form if necessary
            if (frmMain.PTForm == null)
                frmMain.PTForm = new frmPowerTrans();
            frmMain.PTForm.Show();
        }

        private void ShowSelector(object sender, EventArgs e)
        {
            frmMain.MainForm.Show();
        }

        private void ShowPowerDrain(object sender, EventArgs e)
        {
            if (frmMain.PDForm == null)
                frmMain.PDForm = new frmPowerDrain();
            frmMain.PDForm.Show();
        }

        private void ShowTimerInfo(object sender, EventArgs e)
        {
            if (frmMain.TIForm == null)
                frmMain.TIForm = new frmTimerInfo();
            frmMain.TIForm.Show();
        }

        private void ShowBatteryInfo(object sender, EventArgs e)
        {
            if (frmMain.BIForm == null)
                frmMain.BIForm = new frmBatteryInfo();
            frmMain.BIForm.Show();
        }
        #endregion

        private void Exit(object sender, EventArgs e)
        {
            frmMain.MainForm.Close();
        }
    }
}