namespace Studentia
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lsvStudents = new System.Windows.Forms.ListView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblFileName = new System.Windows.Forms.Label();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnAddStuds = new System.Windows.Forms.Button();
            this.btnModify = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.New_ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Open_ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenDefault_ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SaveClassAs_ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Delete_ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lblAvg = new System.Windows.Forms.Label();
            this.lblRecentEvents = new System.Windows.Forms.Label();
            this.cbbBest = new System.Windows.Forms.CheckBox();
            this.cbbWorst = new System.Windows.Forms.CheckBox();
            this.lblBestWorst = new System.Windows.Forms.Label();
            this.sfd1 = new System.Windows.Forms.SaveFileDialog();
            this.ofd1 = new System.Windows.Forms.OpenFileDialog();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lsvStudents
            // 
            this.lsvStudents.Location = new System.Drawing.Point(130, 172);
            this.lsvStudents.Name = "lsvStudents";
            this.lsvStudents.Size = new System.Drawing.Size(520, 256);
            this.lsvStudents.TabIndex = 0;
            this.lsvStudents.UseCompatibleStateImageBehavior = false;
            this.lsvStudents.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lsvStudents_ColumnClick);
            this.lsvStudents.SelectedIndexChanged += new System.EventHandler(this.lsvStudents_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Segoe Print", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label1.ForeColor = System.Drawing.Color.Navy;
            this.label1.Location = new System.Drawing.Point(0, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(780, 64);
            this.label1.TabIndex = 1;
            this.label1.Text = "Studentia";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label1.Click += new System.EventHandler(this.UnselectListViewItems_OnLabelsClick);
            // 
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Font = new System.Drawing.Font("Segoe Script", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label2.ForeColor = System.Drawing.Color.Navy;
            this.label2.Location = new System.Drawing.Point(0, 97);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(780, 48);
            this.label2.TabIndex = 2;
            this.label2.Text = "keeping track of students grades";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label2.Click += new System.EventHandler(this.UnselectListViewItems_OnLabelsClick);
            // 
            // label3
            // 
            this.label3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label3.Location = new System.Drawing.Point(0, 456);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(780, 38);
            this.label3.TabIndex = 3;
            this.label3.Text = "CINEL CET77 - UF5089 - Avaliação 2 - Dário Dias";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label3.Click += new System.EventHandler(this.UnselectListViewItems_OnLabelsClick);
            // 
            // lblFileName
            // 
            this.lblFileName.Location = new System.Drawing.Point(130, 145);
            this.lblFileName.Name = "lblFileName";
            this.lblFileName.Size = new System.Drawing.Size(520, 24);
            this.lblFileName.TabIndex = 4;
            this.lblFileName.Text = "file name";
            this.lblFileName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblFileName.Click += new System.EventHandler(this.UnselectListViewItems_OnLabelsClick);
            // 
            // btnSearch
            // 
            this.btnSearch.Enabled = false;
            this.btnSearch.Location = new System.Drawing.Point(12, 172);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(112, 34);
            this.btnSearch.TabIndex = 6;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btns_OpenFormInput_Click);
            // 
            // btnAddStuds
            // 
            this.btnAddStuds.Location = new System.Drawing.Point(656, 172);
            this.btnAddStuds.Name = "btnAddStuds";
            this.btnAddStuds.Size = new System.Drawing.Size(112, 34);
            this.btnAddStuds.TabIndex = 9;
            this.btnAddStuds.Text = "Add Studs";
            this.btnAddStuds.UseVisualStyleBackColor = true;
            this.btnAddStuds.Click += new System.EventHandler(this.btns_OpenFormInput_Click);
            // 
            // btnModify
            // 
            this.btnModify.Enabled = false;
            this.btnModify.Location = new System.Drawing.Point(656, 212);
            this.btnModify.Name = "btnModify";
            this.btnModify.Size = new System.Drawing.Size(112, 34);
            this.btnModify.TabIndex = 10;
            this.btnModify.Text = "Modify";
            this.btnModify.UseVisualStyleBackColor = true;
            this.btnModify.Click += new System.EventHandler(this.btns_OpenFormInput_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.Enabled = false;
            this.btnRemove.Location = new System.Drawing.Point(656, 252);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(112, 34);
            this.btnRemove.TabIndex = 11;
            this.btnRemove.Text = "Remove";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(656, 354);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(112, 34);
            this.btnSave.TabIndex = 12;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(656, 394);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(112, 34);
            this.btnExit.TabIndex = 13;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(780, 33);
            this.menuStrip1.TabIndex = 16;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.New_ToolStripMenuItem,
            this.Open_ToolStripMenuItem,
            this.OpenDefault_ToolStripMenuItem,
            this.SaveClassAs_ToolStripMenuItem,
            this.Delete_ToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(68, 29);
            this.fileToolStripMenuItem.Text = "Class";
            // 
            // New_ToolStripMenuItem
            // 
            this.New_ToolStripMenuItem.Name = "New_ToolStripMenuItem";
            this.New_ToolStripMenuItem.Size = new System.Drawing.Size(270, 34);
            this.New_ToolStripMenuItem.Text = "New";
            this.New_ToolStripMenuItem.Click += new System.EventHandler(this.New_ToolStripMenuItem_Click);
            // 
            // Open_ToolStripMenuItem
            // 
            this.Open_ToolStripMenuItem.Name = "Open_ToolStripMenuItem";
            this.Open_ToolStripMenuItem.Size = new System.Drawing.Size(270, 34);
            this.Open_ToolStripMenuItem.Text = "Open";
            this.Open_ToolStripMenuItem.Click += new System.EventHandler(this.Open_ToolStripMenuItem_Click);
            // 
            // OpenDefault_ToolStripMenuItem
            // 
            this.OpenDefault_ToolStripMenuItem.Name = "OpenDefault_ToolStripMenuItem";
            this.OpenDefault_ToolStripMenuItem.Size = new System.Drawing.Size(270, 34);
            this.OpenDefault_ToolStripMenuItem.Text = "Open Default";
            this.OpenDefault_ToolStripMenuItem.Click += new System.EventHandler(this.OpenDefault_ToolStripMenuItem_Click);
            // 
            // SaveClassAs_ToolStripMenuItem
            // 
            this.SaveClassAs_ToolStripMenuItem.Name = "SaveClassAs_ToolStripMenuItem";
            this.SaveClassAs_ToolStripMenuItem.Size = new System.Drawing.Size(270, 34);
            this.SaveClassAs_ToolStripMenuItem.Text = "Save Class as";
            this.SaveClassAs_ToolStripMenuItem.Click += new System.EventHandler(this.SaveClassAs_ToolStripMenuItem_Click);
            // 
            // Delete_ToolStripMenuItem
            // 
            this.Delete_ToolStripMenuItem.Name = "Delete_ToolStripMenuItem";
            this.Delete_ToolStripMenuItem.Size = new System.Drawing.Size(270, 34);
            this.Delete_ToolStripMenuItem.Text = "Delete";
            this.Delete_ToolStripMenuItem.Click += new System.EventHandler(this.Delete_ToolStripMenuItem_Click);
            // 
            // lblAvg
            // 
            this.lblAvg.BackColor = System.Drawing.Color.Azure;
            this.lblAvg.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblAvg.Location = new System.Drawing.Point(656, 289);
            this.lblAvg.Name = "lblAvg";
            this.lblAvg.Size = new System.Drawing.Size(112, 62);
            this.lblAvg.TabIndex = 15;
            this.lblAvg.Text = "Average";
            this.lblAvg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblAvg.Click += new System.EventHandler(this.UnselectListViewItems_OnLabelsClick);
            // 
            // lblRecentEvents
            // 
            this.lblRecentEvents.ForeColor = System.Drawing.Color.Navy;
            this.lblRecentEvents.Location = new System.Drawing.Point(130, 432);
            this.lblRecentEvents.Name = "lblRecentEvents";
            this.lblRecentEvents.Size = new System.Drawing.Size(520, 24);
            this.lblRecentEvents.TabIndex = 5;
            this.lblRecentEvents.Text = "recent events";
            this.lblRecentEvents.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblRecentEvents.Click += new System.EventHandler(this.UnselectListViewItems_OnLabelsClick);
            // 
            // cbbBest
            // 
            this.cbbBest.Appearance = System.Windows.Forms.Appearance.Button;
            this.cbbBest.Location = new System.Drawing.Point(12, 212);
            this.cbbBest.Name = "cbbBest";
            this.cbbBest.Size = new System.Drawing.Size(112, 34);
            this.cbbBest.TabIndex = 7;
            this.cbbBest.Text = "Best";
            this.cbbBest.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cbbBest.UseVisualStyleBackColor = true;
            this.cbbBest.Click += new System.EventHandler(this.cbbBestWorst_Click);
            // 
            // cbbWorst
            // 
            this.cbbWorst.Appearance = System.Windows.Forms.Appearance.Button;
            this.cbbWorst.Location = new System.Drawing.Point(12, 252);
            this.cbbWorst.Name = "cbbWorst";
            this.cbbWorst.Size = new System.Drawing.Size(112, 34);
            this.cbbWorst.TabIndex = 8;
            this.cbbWorst.Text = "Worst";
            this.cbbWorst.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cbbWorst.UseVisualStyleBackColor = true;
            this.cbbWorst.Click += new System.EventHandler(this.cbbBestWorst_Click);
            // 
            // lblBestWorst
            // 
            this.lblBestWorst.BackColor = System.Drawing.Color.Azure;
            this.lblBestWorst.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblBestWorst.Location = new System.Drawing.Point(12, 289);
            this.lblBestWorst.Name = "lblBestWorst";
            this.lblBestWorst.Size = new System.Drawing.Size(112, 139);
            this.lblBestWorst.TabIndex = 14;
            this.lblBestWorst.Text = "Highest Lowest";
            this.lblBestWorst.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblBestWorst.Click += new System.EventHandler(this.UnselectListViewItems_OnLabelsClick);
            // 
            // sfd1
            // 
            this.sfd1.DefaultExt = "txt";
            this.sfd1.Filter = "Text files|*.txt";
            this.sfd1.InitialDirectory = "StartupPath";
            // 
            // ofd1
            // 
            this.ofd1.Filter = "Text files|*.txt";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(780, 494);
            this.Controls.Add(this.lblBestWorst);
            this.Controls.Add(this.cbbWorst);
            this.Controls.Add(this.cbbBest);
            this.Controls.Add(this.lblRecentEvents);
            this.Controls.Add(this.lblAvg);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.btnModify);
            this.Controls.Add(this.btnAddStuds);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.lblFileName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lsvStudents);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Studentia";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ListView lsvStudents;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label lblFileName;
        private Button btnSearch;
        private Button btnAddStuds;
        private Button btnModify;
        private Button btnRemove;
        private Button btnSave;
        private Button btnExit;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem New_ToolStripMenuItem;
        private ToolStripMenuItem Open_ToolStripMenuItem;
        private ToolStripMenuItem OpenDefault_ToolStripMenuItem;
        private ToolStripMenuItem Delete_ToolStripMenuItem;
        private Label lblAvg;
        private Label lblRecentEvents;
        private CheckBox cbbBest;
        private CheckBox cbbWorst;
        private Label lblBestWorst;
        private SaveFileDialog sfd1;
        private ToolStripMenuItem SaveClassAs_ToolStripMenuItem;
        private OpenFileDialog ofd1;
    }
}