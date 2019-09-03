namespace SUPLauncher
{
    partial class DupeManager
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DupeManager));
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.FolderMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.newFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Dupes = new System.Windows.Forms.ListBox();
            this.DupesMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createNewFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importDupeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Import = new System.Windows.Forms.OpenFileDialog();
            this.TopBar = new System.Windows.Forms.Panel();
            this.button2 = new System.Windows.Forms.Button();
            this.Drop = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.imgrefresh = new System.Windows.Forms.PictureBox();
            this.FolderMenu.SuspendLayout();
            this.DupesMenu.SuspendLayout();
            this.TopBar.SuspendLayout();
            this.Drop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgrefresh)).BeginInit();
            this.SuspendLayout();
            // 
            // treeView1
            // 
            this.treeView1.ContextMenuStrip = this.FolderMenu;
            this.treeView1.Location = new System.Drawing.Point(7, 44);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(350, 291);
            this.treeView1.TabIndex = 0;
            this.treeView1.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.TreeView1_NodeMouseClick);
            // 
            // FolderMenu
            // 
            this.FolderMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newFolderToolStripMenuItem,
            this.importToolStripMenuItem,
            this.deleteFolderToolStripMenuItem});
            this.FolderMenu.Name = "FolderMenu";
            this.FolderMenu.Size = new System.Drawing.Size(144, 70);
            this.FolderMenu.Opening += new System.ComponentModel.CancelEventHandler(this.FolderMenu_Opening);
            // 
            // newFolderToolStripMenuItem
            // 
            this.newFolderToolStripMenuItem.Name = "newFolderToolStripMenuItem";
            this.newFolderToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.newFolderToolStripMenuItem.Text = "New Folder";
            this.newFolderToolStripMenuItem.Click += new System.EventHandler(this.NewFolderToolStripMenuItem_Click);
            // 
            // importToolStripMenuItem
            // 
            this.importToolStripMenuItem.Name = "importToolStripMenuItem";
            this.importToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.importToolStripMenuItem.Text = "Import Dupe";
            this.importToolStripMenuItem.Click += new System.EventHandler(this.ImportToolStripMenuItem_Click);
            // 
            // deleteFolderToolStripMenuItem
            // 
            this.deleteFolderToolStripMenuItem.Name = "deleteFolderToolStripMenuItem";
            this.deleteFolderToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.deleteFolderToolStripMenuItem.Text = "Delete Folder";
            this.deleteFolderToolStripMenuItem.Click += new System.EventHandler(this.DeleteFolderToolStripMenuItem_Click);
            // 
            // Dupes
            // 
            this.Dupes.ContextMenuStrip = this.DupesMenu;
            this.Dupes.FormattingEnabled = true;
            this.Dupes.Location = new System.Drawing.Point(363, 44);
            this.Dupes.Name = "Dupes";
            this.Dupes.Size = new System.Drawing.Size(363, 290);
            this.Dupes.TabIndex = 29;
            this.Dupes.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Dupes_MouseClick);
            this.Dupes.MouseEnter += new System.EventHandler(this.Dupes_MouseEnter);
            this.Dupes.MouseLeave += new System.EventHandler(this.Dupes_MouseLeave);
            // 
            // DupesMenu
            // 
            this.DupesMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteToolStripMenuItem,
            this.copyToolStripMenuItem,
            this.pasteToolStripMenuItem,
            this.createNewFolderToolStripMenuItem,
            this.importDupeToolStripMenuItem});
            this.DupesMenu.Name = "contextMenuStrip1";
            this.DupesMenu.Size = new System.Drawing.Size(141, 114);
            this.DupesMenu.Opening += new System.ComponentModel.CancelEventHandler(this.ContextMenuStrip1_Opening);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.DeleteToolStripMenuItem_Click);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.copyToolStripMenuItem.Text = "Copy";
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.CopyToolStripMenuItem_Click);
            // 
            // pasteToolStripMenuItem
            // 
            this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            this.pasteToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.pasteToolStripMenuItem.Text = "Paste";
            this.pasteToolStripMenuItem.Click += new System.EventHandler(this.PasteToolStripMenuItem_Click);
            // 
            // createNewFolderToolStripMenuItem
            // 
            this.createNewFolderToolStripMenuItem.Name = "createNewFolderToolStripMenuItem";
            this.createNewFolderToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.createNewFolderToolStripMenuItem.Text = "New Folder";
            this.createNewFolderToolStripMenuItem.Click += new System.EventHandler(this.CreateNewFolderToolStripMenuItem_Click);
            // 
            // importDupeToolStripMenuItem
            // 
            this.importDupeToolStripMenuItem.Name = "importDupeToolStripMenuItem";
            this.importDupeToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.importDupeToolStripMenuItem.Text = "Import dupe";
            this.importDupeToolStripMenuItem.Click += new System.EventHandler(this.ImportDupeToolStripMenuItem_Click);
            // 
            // Import
            // 
            this.Import.FileName = "Import";
            this.Import.Multiselect = true;
            this.Import.Title = "Select a dupe to import...";
            // 
            // TopBar
            // 
            this.TopBar.BackColor = System.Drawing.Color.Black;
            this.TopBar.Controls.Add(this.button2);
            this.TopBar.Cursor = System.Windows.Forms.Cursors.SizeAll;
            this.TopBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.TopBar.Location = new System.Drawing.Point(0, 0);
            this.TopBar.Name = "TopBar";
            this.TopBar.Size = new System.Drawing.Size(795, 22);
            this.TopBar.TabIndex = 32;
            this.TopBar.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TopBar_MouseDown);
            this.TopBar.MouseMove += new System.Windows.Forms.MouseEventHandler(this.TopBar_MouseMove);
            this.TopBar.MouseUp += new System.Windows.Forms.MouseEventHandler(this.TopBar_MouseUp);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.Transparent;
            this.button2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.ForeColor = System.Drawing.Color.White;
            this.button2.Location = new System.Drawing.Point(758, 0);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(37, 22);
            this.button2.TabIndex = 52;
            this.button2.Text = "X";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // Drop
            // 
            this.Drop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(16)))), ((int)(((byte)(22)))), ((int)(((byte)(29)))));
            this.Drop.Controls.Add(this.label1);
            this.Drop.Location = new System.Drawing.Point(0, 341);
            this.Drop.Name = "Drop";
            this.Drop.Size = new System.Drawing.Size(795, 42);
            this.Drop.TabIndex = 33;
            this.Drop.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Prototype", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(287, 2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(270, 33);
            this.label1.TabIndex = 0;
            this.label1.Text = "Drop to import dupe";
            // 
            // imgrefresh
            // 
            this.imgrefresh.BackColor = System.Drawing.Color.Transparent;
            this.imgrefresh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.imgrefresh.Image = ((System.Drawing.Image)(resources.GetObject("imgrefresh.Image")));
            this.imgrefresh.Location = new System.Drawing.Point(732, 44);
            this.imgrefresh.Name = "imgrefresh";
            this.imgrefresh.Size = new System.Drawing.Size(17, 20);
            this.imgrefresh.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.imgrefresh.TabIndex = 49;
            this.imgrefresh.TabStop = false;
            this.imgrefresh.Click += new System.EventHandler(this.Imgrefresh_Click);
            // 
            // DupeManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(795, 382);
            this.ControlBox = false;
            this.Controls.Add(this.imgrefresh);
            this.Controls.Add(this.Drop);
            this.Controls.Add(this.TopBar);
            this.Controls.Add(this.Dupes);
            this.Controls.Add(this.treeView1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DupeManager";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.DupeManager_Load);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.DupeManager_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.DupeManager_DragEnter);
            this.DragLeave += new System.EventHandler(this.DupeManager_DragLeave);
            this.FolderMenu.ResumeLayout(false);
            this.DupesMenu.ResumeLayout(false);
            this.TopBar.ResumeLayout(false);
            this.Drop.ResumeLayout(false);
            this.Drop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgrefresh)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.ListBox Dupes;
        private System.Windows.Forms.ContextMenuStrip DupesMenu;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createNewFolderToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip FolderMenu;
        private System.Windows.Forms.ToolStripMenuItem newFolderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteFolderToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog Import;
        private System.Windows.Forms.ToolStripMenuItem importDupeToolStripMenuItem;
        private System.Windows.Forms.Panel TopBar;
        private System.Windows.Forms.Panel Drop;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox imgrefresh;
        private System.Windows.Forms.Button button2;
    }
}