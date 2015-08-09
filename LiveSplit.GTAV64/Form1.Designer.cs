namespace LiveSplit.GTAV64
{
    partial class Form1
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
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.flpLivesplitLink = new System.Windows.Forms.FlowLayoutPanel();
            this.lLivesplitLink = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.flpIsLoading = new System.Windows.Forms.FlowLayoutPanel();
            this.lIsLoading = new System.Windows.Forms.Label();
            this.lIsLoadingValue = new System.Windows.Forms.Label();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.gitHubToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tlpMain.SuspendLayout();
            this.flpLivesplitLink.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.flpIsLoading.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            //
            // tlpMain
            //
            this.tlpMain.AutoSize = true;
            this.tlpMain.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tlpMain.ColumnCount = 1;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpMain.Controls.Add(this.flpLivesplitLink, 0, 0);
            this.tlpMain.Controls.Add(this.flpIsLoading, 0, 1);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Top;
            this.tlpMain.Location = new System.Drawing.Point(0, 0);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 2;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpMain.Size = new System.Drawing.Size(274, 60);
            this.tlpMain.TabIndex = 0;
            //
            // flpLivesplitLink
            //
            this.flpLivesplitLink.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.flpLivesplitLink.AutoSize = true;
            this.flpLivesplitLink.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flpLivesplitLink.Controls.Add(this.lLivesplitLink);
            this.flpLivesplitLink.Controls.Add(this.pictureBox1);
            this.flpLivesplitLink.Location = new System.Drawing.Point(173, 0);
            this.flpLivesplitLink.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.flpLivesplitLink.Name = "flpLivesplitLink";
            this.flpLivesplitLink.Padding = new System.Windows.Forms.Padding(3);
            this.flpLivesplitLink.Size = new System.Drawing.Size(98, 20);
            this.flpLivesplitLink.TabIndex = 1;
            //
            // lLivesplitLink
            //
            this.lLivesplitLink.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lLivesplitLink.AutoSize = true;
            this.lLivesplitLink.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lLivesplitLink.Location = new System.Drawing.Point(3, 3);
            this.lLivesplitLink.Margin = new System.Windows.Forms.Padding(0);
            this.lLivesplitLink.Name = "lLivesplitLink";
            this.lLivesplitLink.Size = new System.Drawing.Size(76, 15);
            this.lLivesplitLink.TabIndex = 0;
            this.lLivesplitLink.Text = "LiveSplit link:";
            //
            // pictureBox1
            //
            this.pictureBox1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.pictureBox1.Image = global::LiveSplit.GTAV64.Properties.Resources.cross;
            this.pictureBox1.Location = new System.Drawing.Point(79, 3);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(16, 16);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            //
            // flpIsLoading
            //
            this.flpIsLoading.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.flpIsLoading.AutoSize = true;
            this.flpIsLoading.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flpIsLoading.Controls.Add(this.lIsLoading);
            this.flpIsLoading.Controls.Add(this.lIsLoadingValue);
            this.flpIsLoading.Location = new System.Drawing.Point(88, 30);
            this.flpIsLoading.Margin = new System.Windows.Forms.Padding(3, 10, 3, 10);
            this.flpIsLoading.Name = "flpIsLoading";
            this.flpIsLoading.Size = new System.Drawing.Size(97, 20);
            this.flpIsLoading.TabIndex = 2;
            //
            // lIsLoading
            //
            this.lIsLoading.AutoSize = true;
            this.lIsLoading.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lIsLoading.Location = new System.Drawing.Point(0, 0);
            this.lIsLoading.Margin = new System.Windows.Forms.Padding(0);
            this.lIsLoading.Name = "lIsLoading";
            this.lIsLoading.Size = new System.Drawing.Size(66, 20);
            this.lIsLoading.TabIndex = 0;
            this.lIsLoading.Text = "Loading:";
            //
            // lIsLoadingValue
            //
            this.lIsLoadingValue.AutoSize = true;
            this.lIsLoadingValue.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lIsLoadingValue.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lIsLoadingValue.Location = new System.Drawing.Point(66, 0);
            this.lIsLoadingValue.Margin = new System.Windows.Forms.Padding(0);
            this.lIsLoadingValue.Name = "lIsLoadingValue";
            this.lIsLoadingValue.Size = new System.Drawing.Size(31, 20);
            this.lIsLoadingValue.TabIndex = 1;
            this.lIsLoadingValue.Text = "-    ";
            //
            // contextMenuStrip1
            //
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingsToolStripMenuItem,
            this.toolStripSeparator1,
            this.gitHubToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(126, 54);
            //
            // settingsToolStripMenuItem
            //
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.settingsToolStripMenuItem.Text = "Settings...";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
            //
            // toolStripSeparator1
            //
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(122, 6);
            //
            // gitHubToolStripMenuItem
            //
            this.gitHubToolStripMenuItem.Name = "gitHubToolStripMenuItem";
            this.gitHubToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.gitHubToolStripMenuItem.Text = "GitHub";
            this.gitHubToolStripMenuItem.Click += new System.EventHandler(this.gitHubToolStripMenuItem_Click);
            //
            // Form1
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(274, 69);
            this.ContextMenuStrip = this.contextMenuStrip1;
            this.Controls.Add(this.tlpMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(290, 39);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "GTAV AutoSplitter 1.0";
            this.tlpMain.ResumeLayout(false);
            this.tlpMain.PerformLayout();
            this.flpLivesplitLink.ResumeLayout(false);
            this.flpLivesplitLink.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.flpIsLoading.ResumeLayout(false);
            this.flpIsLoading.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem gitHubToolStripMenuItem;
        private System.Windows.Forms.Label lLivesplitLink;
        private System.Windows.Forms.FlowLayoutPanel flpLivesplitLink;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.FlowLayoutPanel flpIsLoading;
        private System.Windows.Forms.Label lIsLoading;
        private System.Windows.Forms.Label lIsLoadingValue;
    }
}
