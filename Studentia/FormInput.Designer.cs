namespace Studentia
{
    partial class FormInput
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
            this.grbStudent = new System.Windows.Forms.GroupBox();
            this.lblGrade = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.lblNumber = new System.Windows.Forms.Label();
            this.chbGrade = new System.Windows.Forms.CheckBox();
            this.chbName = new System.Windows.Forms.CheckBox();
            this.chbNumber = new System.Windows.Forms.CheckBox();
            this.nudGrade = new System.Windows.Forms.NumericUpDown();
            this.nudNumber = new System.Windows.Forms.NumericUpDown();
            this.txbName = new System.Windows.Forms.TextBox();
            this.btnAction = new System.Windows.Forms.Button();
            this.lblRecentEvents = new System.Windows.Forms.Label();
            this.grbStudent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudGrade)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudNumber)).BeginInit();
            this.SuspendLayout();
            // 
            // grbStudent
            // 
            this.grbStudent.Controls.Add(this.lblGrade);
            this.grbStudent.Controls.Add(this.lblName);
            this.grbStudent.Controls.Add(this.lblNumber);
            this.grbStudent.Controls.Add(this.chbGrade);
            this.grbStudent.Controls.Add(this.chbName);
            this.grbStudent.Controls.Add(this.chbNumber);
            this.grbStudent.Controls.Add(this.nudGrade);
            this.grbStudent.Controls.Add(this.nudNumber);
            this.grbStudent.Controls.Add(this.txbName);
            this.grbStudent.Location = new System.Drawing.Point(12, 12);
            this.grbStudent.Name = "grbStudent";
            this.grbStudent.Size = new System.Drawing.Size(383, 150);
            this.grbStudent.TabIndex = 0;
            this.grbStudent.TabStop = false;
            this.grbStudent.Text = "Search Add Modify";
            // 
            // lblGrade
            // 
            this.lblGrade.AutoSize = true;
            this.lblGrade.Location = new System.Drawing.Point(34, 113);
            this.lblGrade.Name = "lblGrade";
            this.lblGrade.Size = new System.Drawing.Size(63, 25);
            this.lblGrade.TabIndex = 7;
            this.lblGrade.Text = "Grade:";
            this.lblGrade.Click += new System.EventHandler(this.lblItem_Click);
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(34, 77);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(63, 25);
            this.lblName.TabIndex = 4;
            this.lblName.Text = "Name:";
            this.lblName.Click += new System.EventHandler(this.lblItem_Click);
            // 
            // lblNumber
            // 
            this.lblNumber.AutoSize = true;
            this.lblNumber.Location = new System.Drawing.Point(34, 39);
            this.lblNumber.Name = "lblNumber";
            this.lblNumber.Size = new System.Drawing.Size(81, 25);
            this.lblNumber.TabIndex = 1;
            this.lblNumber.Text = "Number:";
            this.lblNumber.Click += new System.EventHandler(this.lblItem_Click);
            // 
            // chbGrade
            // 
            this.chbGrade.AutoSize = true;
            this.chbGrade.Location = new System.Drawing.Point(6, 116);
            this.chbGrade.Name = "chbGrade";
            this.chbGrade.Size = new System.Drawing.Size(22, 21);
            this.chbGrade.TabIndex = 6;
            this.chbGrade.UseVisualStyleBackColor = true;
            // 
            // chbName
            // 
            this.chbName.AutoSize = true;
            this.chbName.Location = new System.Drawing.Point(6, 80);
            this.chbName.Name = "chbName";
            this.chbName.Size = new System.Drawing.Size(22, 21);
            this.chbName.TabIndex = 3;
            this.chbName.UseVisualStyleBackColor = true;
            // 
            // chbNumber
            // 
            this.chbNumber.AutoSize = true;
            this.chbNumber.Location = new System.Drawing.Point(6, 42);
            this.chbNumber.Name = "chbNumber";
            this.chbNumber.Size = new System.Drawing.Size(22, 21);
            this.chbNumber.TabIndex = 0;
            this.chbNumber.UseVisualStyleBackColor = true;
            // 
            // nudGrade
            // 
            this.nudGrade.DecimalPlaces = 2;
            this.nudGrade.Location = new System.Drawing.Point(121, 111);
            this.nudGrade.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.nudGrade.Name = "nudGrade";
            this.nudGrade.Size = new System.Drawing.Size(96, 31);
            this.nudGrade.TabIndex = 8;
            this.nudGrade.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nudGrade.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudGrade.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.fields_KeyPress_Enter);
            // 
            // nudNumber
            // 
            this.nudNumber.Location = new System.Drawing.Point(121, 37);
            this.nudNumber.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.nudNumber.Name = "nudNumber";
            this.nudNumber.Size = new System.Drawing.Size(64, 31);
            this.nudNumber.TabIndex = 2;
            this.nudNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nudNumber.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudNumber.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.fields_KeyPress_Enter);
            // 
            // txbName
            // 
            this.txbName.Location = new System.Drawing.Point(121, 74);
            this.txbName.Name = "txbName";
            this.txbName.Size = new System.Drawing.Size(256, 31);
            this.txbName.TabIndex = 5;
            this.txbName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.fields_KeyPress_Enter);
            // 
            // btnAction
            // 
            this.btnAction.Location = new System.Drawing.Point(283, 168);
            this.btnAction.Name = "btnAction";
            this.btnAction.Size = new System.Drawing.Size(112, 34);
            this.btnAction.TabIndex = 2;
            this.btnAction.Text = "Action";
            this.btnAction.UseVisualStyleBackColor = true;
            this.btnAction.Click += new System.EventHandler(this.btnAction_Click);
            // 
            // lblRecentEvents
            // 
            this.lblRecentEvents.Location = new System.Drawing.Point(12, 168);
            this.lblRecentEvents.Name = "lblRecentEvents";
            this.lblRecentEvents.Size = new System.Drawing.Size(220, 34);
            this.lblRecentEvents.TabIndex = 1;
            this.lblRecentEvents.Text = "recent events";
            this.lblRecentEvents.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FormInput
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(407, 208);
            this.Controls.Add(this.lblRecentEvents);
            this.Controls.Add(this.btnAction);
            this.Controls.Add(this.grbStudent);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "FormInput";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "FormInput";
            this.grbStudent.ResumeLayout(false);
            this.grbStudent.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudGrade)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudNumber)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private GroupBox grbStudent;
        private NumericUpDown nudNumber;
        private TextBox txbName;
        private NumericUpDown nudGrade;
        private CheckBox chbGrade;
        private CheckBox chbName;
        private CheckBox chbNumber;
        private Button btnAction;
        private Label lblGrade;
        private Label lblName;
        private Label lblNumber;
        private Label lblRecentEvents;
    }
}