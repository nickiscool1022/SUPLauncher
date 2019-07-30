using System;
using System.ComponentModel;
using System.Linq;
using System.IO;
using System.Windows.Forms;

namespace SUPLauncher
{
    partial class DupeManager : Form
    {
        public DupeManager()
        {
            InitializeComponent();
        }
        private void DupeManager_Load(object sender, EventArgs e) // Get nodes for the treeview
        {
            DirectoryInfo dInfo = new DirectoryInfo(frmLauncher.dupePath); // Directory info to see what directories we have to work with
            var root = new TreeNode(dInfo.Name); // Decalre new root treenode
            root.Tag = dInfo; // Assign tag of advdupe2 to it
            GetDirectories(dInfo.GetDirectories(), root); // Get Directories
            treeView1.Nodes.Add(root); // Add the node to the treeview after processing
            
        }
        private void GetDirectories(DirectoryInfo[] subDirs, TreeNode nodeToAddTo) // Stolen because too lazy to code it (if it aint broke why fix it)
        {

            TreeNode aNode;
            DirectoryInfo[] subSubDirs;

            foreach (DirectoryInfo subDir in subDirs)
            {
                aNode = new TreeNode(subDir.Name, 0, 0);

                aNode.Tag = subDir;
                aNode.ImageKey = "folder";
                subSubDirs = subDir.GetDirectories();
                if (subSubDirs.Length != 0)
                {

                    GetDirectories(subSubDirs, aNode);

                }
                nodeToAddTo.Nodes.Add(aNode);
            }
        }
        private void TreeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            Dupes.Items.Clear();
            DirectoryInfo nodeDirInfo = (DirectoryInfo)e.Node.Tag;
            foreach (FileInfo file in nodeDirInfo.GetFiles())
            {
                Dupes.Items.Add(file.Name);
            }
        }

        private void ContextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            if (Dupes.SelectedIndex == -1)
                e.Cancel = true;
        }

        private void DeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete " + Dupes.SelectedItem.ToString() + "?", "Are you sure?", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
            {
                File.Delete(frmLauncher.dupePath + @"\" + Dupes.SelectedItem);
                Dupes.Items.RemoveAt(Dupes.SelectedIndex);
            }
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
