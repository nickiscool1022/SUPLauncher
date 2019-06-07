namespace SUPLauncher
{
    partial class frmLauncher
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLauncher));
            this.btnDanktown = new System.Windows.Forms.Button();
            this.btnSundown = new System.Windows.Forms.Button();
            this.btnC18 = new System.Windows.Forms.Button();
            this.btnZombies = new System.Windows.Forms.Button();
            this.btnMilRP = new System.Windows.Forms.Button();
            this.btnCW1 = new System.Windows.Forms.Button();
            this.btnCW2 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnDanktown
            // 
            this.btnDanktown.Location = new System.Drawing.Point(12, 30);
            this.btnDanktown.Name = "btnDanktown";
            this.btnDanktown.Size = new System.Drawing.Size(112, 41);
            this.btnDanktown.TabIndex = 0;
            this.btnDanktown.Text = "Danktown";
            this.btnDanktown.UseVisualStyleBackColor = true;
            this.btnDanktown.Click += new System.EventHandler(this.btnDanktown_Click);
            // 
            // btnSundown
            // 
            this.btnSundown.Location = new System.Drawing.Point(12, 79);
            this.btnSundown.Name = "btnSundown";
            this.btnSundown.Size = new System.Drawing.Size(112, 41);
            this.btnSundown.TabIndex = 1;
            this.btnSundown.Text = "Sundown";
            this.btnSundown.UseVisualStyleBackColor = true;
            this.btnSundown.Click += new System.EventHandler(this.btnSundown_Click);
            // 
            // btnC18
            // 
            this.btnC18.Location = new System.Drawing.Point(12, 128);
            this.btnC18.Name = "btnC18";
            this.btnC18.Size = new System.Drawing.Size(112, 41);
            this.btnC18.TabIndex = 2;
            this.btnC18.Text = "C18";
            this.btnC18.UseVisualStyleBackColor = true;
            this.btnC18.Click += new System.EventHandler(this.btnC18_Click);
            // 
            // btnZombies
            // 
            this.btnZombies.Location = new System.Drawing.Point(12, 177);
            this.btnZombies.Name = "btnZombies";
            this.btnZombies.Size = new System.Drawing.Size(112, 41);
            this.btnZombies.TabIndex = 3;
            this.btnZombies.Text = "Zombies";
            this.btnZombies.UseVisualStyleBackColor = true;
            this.btnZombies.Click += new System.EventHandler(this.btnZombies_Click);
            // 
            // btnMilRP
            // 
            this.btnMilRP.Location = new System.Drawing.Point(181, 195);
            this.btnMilRP.Name = "btnMilRP";
            this.btnMilRP.Size = new System.Drawing.Size(112, 41);
            this.btnMilRP.TabIndex = 4;
            this.btnMilRP.Text = "MilitaryRP";
            this.btnMilRP.UseVisualStyleBackColor = true;
            this.btnMilRP.Click += new System.EventHandler(this.btnMilRP_Click);
            // 
            // btnCW1
            // 
            this.btnCW1.Location = new System.Drawing.Point(350, 79);
            this.btnCW1.Name = "btnCW1";
            this.btnCW1.Size = new System.Drawing.Size(112, 41);
            this.btnCW1.TabIndex = 5;
            this.btnCW1.Text = "Clonewars #1";
            this.btnCW1.UseVisualStyleBackColor = true;
            this.btnCW1.Click += new System.EventHandler(this.btnCW1_Click);
            // 
            // btnCW2
            // 
            this.btnCW2.Location = new System.Drawing.Point(350, 128);
            this.btnCW2.Name = "btnCW2";
            this.btnCW2.Size = new System.Drawing.Size(112, 41);
            this.btnCW2.TabIndex = 6;
            this.btnCW2.Text = "Clonewars #2";
            this.btnCW2.UseVisualStyleBackColor = true;
            this.btnCW2.Click += new System.EventHandler(this.btnCW2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Lucida Bright", 18F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.Control;
            this.label1.Location = new System.Drawing.Point(216, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(203, 28);
            this.label1.TabIndex = 7;
            this.label1.Text = "SuperiorServers";
            // 
            // frmLauncher
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::SUPLauncher.Properties.Resources.background;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(474, 248);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCW2);
            this.Controls.Add(this.btnCW1);
            this.Controls.Add(this.btnMilRP);
            this.Controls.Add(this.btnZombies);
            this.Controls.Add(this.btnC18);
            this.Controls.Add(this.btnSundown);
            this.Controls.Add(this.btnDanktown);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmLauncher";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SUP Launcher";
            this.Load += new System.EventHandler(this.frmLauncher_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnDanktown;
        private System.Windows.Forms.Button btnSundown;
        private System.Windows.Forms.Button btnC18;
        private System.Windows.Forms.Button btnZombies;
        private System.Windows.Forms.Button btnMilRP;
        private System.Windows.Forms.Button btnCW1;
        private System.Windows.Forms.Button btnCW2;
        private System.Windows.Forms.Label label1;
    }
}

