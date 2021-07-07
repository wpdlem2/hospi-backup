namespace hospi_hospital_only
{
    partial class CheckMasterPW
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CheckMasterPW));
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.comboBoxMaster = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.textBoxPW = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
            this.groupBox7.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.comboBoxMaster);
            this.groupBox7.Controls.Add(this.label1);
            this.groupBox7.Controls.Add(this.button1);
            this.groupBox7.Controls.Add(this.textBoxPW);
            this.groupBox7.Controls.Add(this.label6);
            this.groupBox7.Controls.Add(this.button4);
            this.groupBox7.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.groupBox7.Location = new System.Drawing.Point(12, 12);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(281, 135);
            this.groupBox7.TabIndex = 37;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "관리자 로그인";
            // 
            // comboBoxMaster
            // 
            this.comboBoxMaster.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxMaster.FormattingEnabled = true;
            this.comboBoxMaster.Location = new System.Drawing.Point(100, 31);
            this.comboBoxMaster.Name = "comboBoxMaster";
            this.comboBoxMaster.Size = new System.Drawing.Size(160, 23);
            this.comboBoxMaster.TabIndex = 1;
            this.comboBoxMaster.SelectedIndexChanged += new System.EventHandler(this.comboBoxMaster_SelectedIndexChanged);
            this.comboBoxMaster.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.comboBoxMaster_KeyPress);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.Location = new System.Drawing.Point(15, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 23);
            this.label1.TabIndex = 40;
            this.label1.Text = "관리자명";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button1.Location = new System.Drawing.Point(15, 90);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(79, 30);
            this.button1.TabIndex = 38;
            this.button1.Text = "취소";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBoxPW
            // 
            this.textBoxPW.Location = new System.Drawing.Point(100, 60);
            this.textBoxPW.Name = "textBoxPW";
            this.textBoxPW.PasswordChar = '●';
            this.textBoxPW.Size = new System.Drawing.Size(160, 23);
            this.textBoxPW.TabIndex = 2;
            this.textBoxPW.Enter += new System.EventHandler(this.textBoxPW_Enter);
            this.textBoxPW.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxPW_KeyDown);
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label6.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label6.Location = new System.Drawing.Point(15, 60);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(79, 23);
            this.label6.TabIndex = 37;
            this.label6.Text = "비밀번호";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // button4
            // 
            this.button4.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button4.Location = new System.Drawing.Point(100, 90);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(160, 30);
            this.button4.TabIndex = 35;
            this.button4.Text = "확 인";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // CheckMasterPW
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(305, 155);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox7);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "CheckMasterPW";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "관리자 인증";
            this.Load += new System.EventHandler(this.CheckMasterPW_Load);
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBoxPW;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.ComboBox comboBoxMaster;
        private System.Windows.Forms.Label label1;
    }
}