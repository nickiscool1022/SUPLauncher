using Microsoft.VisualBasic;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace SUPLauncher
{
    partial class DupeManager : Form
    {
        public DupeManager()
        {
            InitializeComponent();
            this.AllowDrop = true;
        }

        private TreeNode lastNode;
        private DirectoryInfo[] lastSubDirs;
        string copiedNode;
        string copiedNodeName;
        object selectedPath;
        bool isMouseOver = false;
        private Image refresh_img;
        private Image original_refreshimg;

        private void rotateInThread(Bitmap bm, float angle)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<Bitmap, float>(rotateInThread), new object[] { bm, angle });
            }
            refresh_img = RotateBitmap(bm, angle);
        }
        private void GetPointBounds(PointF[] points,
    out float xmin, out float xmax,
    out float ymin, out float ymax)
        {
            xmin = points[0].X;
            xmax = xmin;
            ymin = points[0].Y;
            ymax = ymin;
            foreach (PointF point in points)
            {
                if (xmin > point.X) xmin = point.X;
                if (xmax < point.X) xmax = point.X;
                if (ymin > point.Y) ymin = point.Y;
                if (ymax < point.Y) ymax = point.Y;
            }
        }

        private Bitmap RotateBitmap(Bitmap bm, float angle)
        {
            // Make a Matrix to represent rotation
            // by this angle.
            Matrix rotate_at_origin = new Matrix();
            rotate_at_origin.Rotate(angle);

            // Rotate the image's corners to see how big
            // it will be after rotation.
            PointF[] points =
            {
        new PointF(0, 0),
        new PointF(bm.Width, 0),
        new PointF(bm.Width, bm.Height),
        new PointF(0, bm.Height),
    };
            rotate_at_origin.TransformPoints(points);
            float xmin, xmax, ymin, ymax;
            GetPointBounds(points, out xmin, out xmax,
                out ymin, out ymax);

            // Make a bitmap to hold the rotated result.
            int wid = (int)Math.Round(xmax - xmin);
            int hgt = (int)Math.Round(ymax - ymin);
            Bitmap result = new Bitmap(wid, hgt);

            // Create the real rotation transformation.
            Matrix rotate_at_center = new Matrix();
            rotate_at_center.RotateAt(angle,
                new PointF(wid / 2f, hgt / 2f));

            // Draw the image onto the new bitmap rotated.
            using (Graphics gr = Graphics.FromImage(result))
            {
                // Use smooth image interpolation.
                gr.InterpolationMode = InterpolationMode.High;

                // Clear with the color in the image's upper left corner.
                gr.Clear(bm.GetPixel(0, 0));

                //// For debugging. (It's easier to see the background.)
                //gr.Clear(Color.LightBlue);

                // Set up the transformation to rotate.
                gr.Transform = rotate_at_center;

                // Draw the image centered on the bitmap.
                int x = (wid - bm.Width) / 2;
                int y = (hgt - bm.Height) / 2;
                gr.DrawImage(bm, x, y);
            }

            // Return the result bitmap.
            return result;
        }

        private void DupeManager_Load(object sender, EventArgs e) // Get nodes for the treeview
        {

            refresh_img = imgrefresh.Image;
            original_refreshimg = imgrefresh.Image;
            DirectoryInfo dInfo = new DirectoryInfo(frmLauncher.dupePath); // Directory info to see what directories we have to work with
            var root = new TreeNode(dInfo.Name); // Decalre new root treenode
            root.Tag = dInfo; // Assign tag of advdupe2 to it
            GetDirectories(dInfo.GetDirectories(), root); // Get Directories
            treeView1.Nodes.Add(root); // Add the node to the treeview after processing

        }
        private void reloadDupes()
        {
            Dupes.Items.Clear();
            DirectoryInfo nodeDirInfo = (DirectoryInfo)selectedPath;
            foreach (FileInfo file in nodeDirInfo.GetFiles())
            {
                Dupes.Items.Add(file.Name);
            }
        }

        private void reloadFolders()
        {
            treeView1.Nodes.Clear();
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

            lastSubDirs = subDirs;
            lastNode = nodeToAddTo;

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
            selectedPath = e.Node.Tag;
            foreach (FileInfo file in nodeDirInfo.GetFiles())
            {
                Dupes.Items.Add(file.Name);
            }
        }

        private void ContextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

            if (copiedNode == null)
            {
                pasteToolStripMenuItem.Enabled = false;
            }
            else
            {
                pasteToolStripMenuItem.Enabled = true;
            }

            if (isMouseOver & Dupes.SelectedIndex == -1)
            {
                copyToolStripMenuItem.Visible = false;

                deleteToolStripMenuItem.Visible = false;

            }
            else
            {

                if (Dupes.SelectedIndex == -1)
                {
                    e.Cancel = true;
                }
                else
                {
                    copyToolStripMenuItem.Visible = true;

                    deleteToolStripMenuItem.Visible = true;
                }
            }
        }

        private void DeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete " + Dupes.SelectedItem.ToString() + "?", "Are you sure?", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
            {

                File.Delete(frmLauncher.dupePath + treeView1.SelectedNode.FullPath.Substring(8, treeView1.SelectedNode.FullPath.Length - 8) + @"\" + Dupes.SelectedItem);
                Dupes.Items.RemoveAt(Dupes.SelectedIndex);
            }
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            copiedNode = frmLauncher.dupePath + treeView1.SelectedNode.FullPath.Substring(8, treeView1.SelectedNode.FullPath.Length - 8) + @"\" + Dupes.SelectedItem;
            copiedNodeName = Dupes.Text;
        }

        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (File.Exists(copiedNode.ToString()))
            {
                int i = 1;
                while (File.Exists(frmLauncher.dupePath + treeView1.SelectedNode.FullPath.Substring(8, treeView1.SelectedNode.FullPath.Length - 8) + @"\" + copiedNodeName + "(" + i + ")"))
                {
                    i = i + 1;

                }
                File.Copy(copiedNode.ToString(), frmLauncher.dupePath + treeView1.SelectedNode.FullPath.Substring(8, treeView1.SelectedNode.FullPath.Length - 8) + @"\" + copiedNodeName + "(" + i + ")");

            }
            else
            {
                try
                {
                    File.Copy(copiedNode.ToString(), frmLauncher.dupePath + treeView1.SelectedNode.FullPath.Substring(8, treeView1.SelectedNode.FullPath.Length - 8) + @"\" + copiedNode);
                }
                catch (Exception) // Stops errors popping up if the file does not exist anymore
                {

                }
            }
            reloadDupes();
        }

        private void CreateNewFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            String name = Interaction.InputBox("Folder name:");
            Directory.CreateDirectory(frmLauncher.dupePath + treeView1.SelectedNode.FullPath.Substring(8, treeView1.SelectedNode.FullPath.Length - 8) + @"\" + copiedNode + name);
            reloadFolders();
        }

        private void Dupes_MouseLeave(object sender, EventArgs e)
        {
            isMouseOver = false;
        }

        private void Dupes_MouseEnter(object sender, EventArgs e)
        {
            isMouseOver = true;
        }

        private void Dupes_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void NewFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode == null)
            {
                String filename = Interaction.InputBox("Folder Name");
                if (File.Exists(frmLauncher.dupePath + "/" + filename))
                {
                    MessageBox.Show("File already exsists!", "SUPLauncher");
                    return;
                }
                else
                {
                    Directory.CreateDirectory(frmLauncher.dupePath + @"\" + filename);
                    reloadFolders();
                }
            }
            else
            {
                Directory.CreateDirectory(frmLauncher.dupePath + treeView1.SelectedNode.FullPath.Substring(8, treeView1.SelectedNode.FullPath.Length - 8) + @"\" + treeView1.SelectedNode.Name + "/" + Interaction.InputBox("Folder Name"));
                reloadFolders();
            }

        }

        private void DeleteFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult m = MessageBox.Show("Are you sure you want to delete " + treeView1.SelectedNode.Text, "SUPLauncher", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (m == DialogResult.Yes)
            {
                String[] folder = Directory.GetFiles(frmLauncher.dupePath + treeView1.SelectedNode.FullPath.Substring(8, treeView1.SelectedNode.FullPath.Length - 8));
                foreach (String f in folder)
                {
                    File.Delete(f);
                }
                String[] dirs = Directory.GetDirectories(frmLauncher.dupePath + treeView1.SelectedNode.FullPath.Substring(8, treeView1.SelectedNode.FullPath.Length - 8));
                foreach (String d in dirs)
                {
                    Directory.Delete(d);
                }
                Directory.Delete(frmLauncher.dupePath + treeView1.SelectedNode.FullPath.Substring(8, treeView1.SelectedNode.FullPath.Length - 8));
                reloadFolders();
                return;
            }
            else
            {
                return;
            }

        }


        private void ImportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Import.ShowDialog();
            String[] DupePath = Import.FileNames;
            int i = 0;
            foreach (String dupe in DupePath)
            {
                if (Path.GetFileName(dupe).EndsWith(".txt"))
                {
                    File.Copy(dupe, frmLauncher.dupePath + treeView1.SelectedNode.FullPath.Substring(8, treeView1.SelectedNode.FullPath.Length - 8) + @"\" + treeView1.SelectedNode.Name + "/" + Import.SafeFileNames[i]);
                    reloadDupes();
                }
                i = i + 1;
            }
        }
        private void ImportDupeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Import.ShowDialog();

            String[] DupePath = Import.FileNames;
            int i = 0;
            foreach (String dupe in DupePath)
            {
                if (Path.GetFileName(dupe).EndsWith(".txt"))
                {
                    File.Copy(dupe, frmLauncher.dupePath + treeView1.SelectedNode.FullPath.Substring(8, treeView1.SelectedNode.FullPath.Length - 8) + @"\" + treeView1.SelectedNode.Name + "/" + Import.SafeFileNames[i]);

                    reloadDupes();
                }
                i = i + 1;
            }
        }

        private void FolderMenu_Opening(object sender, CancelEventArgs e)
        {
            if (treeView1.SelectedNode == null)
            {
                deleteFolderToolStripMenuItem.Visible = false;
            }
            else
            {
                deleteFolderToolStripMenuItem.Visible = true;
            }
        }
        private void RoundButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void DupeManager_DragEnter(object sender, DragEventArgs e)
        {
            String[] DupePath = (string[])e.Data.GetData(DataFormats.FileDrop);
            foreach (String dupe in DupePath)
            {
                if (Path.GetFileName(dupe).EndsWith(".txt"))
                {
                    e.Effect = DragDropEffects.Copy;
                    Drop.Visible = true;
                    return;
                }
            }
        }

        private void DupeManager_DragLeave(object sender, EventArgs e)
        {
            Drop.Visible = false;
        }
        private void DupeManager_DragDrop(object sender, DragEventArgs e)
        {
            String[] DupePath = (string[])e.Data.GetData(DataFormats.FileDrop);
            {
                foreach (String dupe in DupePath)
                {
                    if (Path.GetFileName(dupe).EndsWith(".txt"))
                    {
                        File.Copy(dupe, frmLauncher.dupePath + treeView1.SelectedNode.FullPath.Substring(8, treeView1.SelectedNode.FullPath.Length - 8) + @"\" + treeView1.SelectedNode.Name + "/" + Path.GetFileName(dupe));
                    }
                }
                reloadDupes();
                Drop.Visible = false;
            }
        }

        private void Imgrefresh_Click(object sender, EventArgs e)
        {
            reloadFolders();
            Dupes.Items.Clear();
            new Thread(() =>
            {
                int i = 0;
                while (i != 10)
                {
                    i = i + 1;
                    Thread.Sleep(70);
                    rotateInThread(new Bitmap(refresh_img), 90);
                    imgrefresh.Image = refresh_img;
                }

                imgrefresh.Image = original_refreshimg;
                return;
            }).Start();
        }
        bool isTopPanelDragged = false;
        bool isWindowMaximized = false;
        Point offset;
        Size _normalWindowSize;
        Point _normalWindowLocation = Point.Empty;
        private void TopBar_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isTopPanelDragged = true;
                Point pointStartPosition = this.PointToScreen(new Point(e.X, e.Y));
                offset = new Point
                {
                    X = this.Location.X - pointStartPosition.X,
                    Y = this.Location.Y - pointStartPosition.Y
                };
            }
            else
            {
                isTopPanelDragged = false;
            }
            if (e.Clicks == 2)
            {
                isTopPanelDragged = false;

            }
        }

        private void TopBar_MouseMove(object sender, MouseEventArgs e)
        {
            if (isTopPanelDragged)
            {
                Point newPoint = TopBar.PointToScreen(new Point(e.X, e.Y));
                newPoint.Offset(offset);
                this.Location = newPoint;

                if (this.Location.X > 2 || this.Location.Y > 2)
                {
                    if (this.WindowState == FormWindowState.Maximized)
                    {
                        this.Location = _normalWindowLocation;
                        this.Size = _normalWindowSize;

                        isWindowMaximized = false;
                    }
                }
            }
        }

        private void TopBar_MouseUp(object sender, MouseEventArgs e)
        {
            isTopPanelDragged = false;
            if (this.Location.Y <= 5)
            {
                if (!isWindowMaximized)
                {
                    _normalWindowSize = this.Size;
                    _normalWindowLocation = this.Location;

                    Rectangle rect = Screen.PrimaryScreen.WorkingArea;
                    this.Location = new Point(0, 0);
                    this.Size = new System.Drawing.Size(rect.Width, rect.Height);

                    isWindowMaximized = true;
                }
            }
        }
    }
}
