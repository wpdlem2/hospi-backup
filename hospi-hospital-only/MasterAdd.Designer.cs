namespace hospi_hospital_only
{
    partial class MasterAdd
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MasterAdd));
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.pwLabel2 = new System.Windows.Forms.Label();
            this.pwLabel1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxPW2 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxPW1 = new System.Windows.Forms.TextBox();
            this.buttonCheck = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.groupBox9.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.button1);
            this.groupBox9.Controls.Add(this.button4);
            this.groupBox9.Controls.Add(this.pwLabel2);
            this.groupBox9.Controls.Add(this.pwLabel1);
            this.groupBox9.Controls.Add(this.label3);
            this.groupBox9.Controls.Add(this.textBoxPW2);
            this.groupBox9.Controls.Add(this.label2);
            this.groupBox9.Controls.Add(this.textBoxPW1);
            this.groupBox9.Controls.Add(this.buttonCheck);
            this.groupBox9.Controls.Add(this.label1);
            this.groupBox9.Controls.Add(this.textBoxName);
            this.groupBox9.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.groupBox9.Location = new System.Drawing.Point(12, 12);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(370, 192);
            this.groupBox9.TabIndex = 38;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "신규 관리자 등록";
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button1.Location = new System.Drawing.Point(16, 147);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(96, 32);
            this.button1.TabIndex = 6;
            this.button1.Text = "취 소";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button4
            // 
            this.button4.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button4.Location = new System.Drawing.Point(118, 147);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(243, 32);
            this.button4.TabIndex = 5;
            this.button4.Text = "관리자 생성";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // pwLabel2
            // 
            this.pwLabel2.AutoSize = true;
            this.pwLabel2.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.pwLabel2.ForeColor = System.Drawing.Color.ForestGreen;
            this.pwLabel2.Location = new System.Drawing.Point(289, 110);
            this.pwLabel2.Name = "pwLabel2";
            this.pwLabel2.Size = new System.Drawing.Size(23, 21);
            this.pwLabel2.TabIndex = 50;
            this.pwLabel2.Text = "✓";
            this.pwLabel2.Visible = false;
            // 
            // pwLabel1
            // 
            this.pwLabel1.AutoSize = true;
            this.pwLabel1.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.pwLabel1.ForeColor = System.Drawing.Color.ForestGreen;
            this.pwLabel1.Location = new System.Drawing.Point(289, 81);
            this.pwLabel1.Name = "pwLabel1";
            this.pwLabel1.Size = new System.Drawing.Size(23, 21);
            this.pwLabel1.TabIndex = 49;
            this.pwLabel1.Text = "✓";
            this.pwLabel1.Visible = false;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label3.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label3.Location = new System.Drawing.Point(16, 108);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(96, 23);
            this.label3.TabIndex = 46;
            this.label3.Text = "비밀번호 확인";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBoxPW2
            // 
            this.textBoxPW2.Location = new System.Drawing.Point(118, 108);
            this.textBoxPW2.Name = "textBoxPW2";
            this.textBoxPW2.PasswordChar = '●';
            this.textBoxPW2.Size = new System.Drawing.Size(165, 23);
            this.textBoxPW2.TabIndex = 4;
            this.textBoxPW2.TextChanged += new System.EventHandler(this.textBoxPW2_TextChanged);
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label2.Location = new System.Drawing.Point(16, 79);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 23);
            this.label2.TabIndex = 44;
            this.label2.Text = "비밀번호";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBoxPW1
            // 
            this.textBoxPW1.Location = new System.Drawing.Point(118, 79);
            this.textBoxPW1.Name = "textBoxPW1";
            this.textBoxPW1.PasswordChar = '●';
            this.textBoxPW1.Size = new System.Drawing.Size(165, 23);
            this.textBoxPW1.TabIndex = 3;
            this.textBoxPW1.TextChanged += new System.EventHandler(this.textBoxPW1_TextChanged);
            // 
            // buttonCheck
            // 
            this.buttonCheck.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.buttonCheck.Location = new System.Drawing.Point(289, 32);
            this.buttonCheck.Name = "buttonCheck";
            this.buttonCheck.Size = new System.Drawing.Size(72, 25);
            this.buttonCheck.TabIndex = 2;
            this.buttonCheck.Text = "중복 확인";
            this.buttonCheck.UseVisualStyleBackColor = true;
            this.buttonCheck.Click += new System.EventHandler(this.buttonCheck_Click);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.Location = new System.Drawing.Point(16, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 23);
            this.label1.TabIndex = 42;
            this.label1.Text = "관리자명";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBoxName
            // 
            this.textBoxName.Location = new System.Drawing.Point(118, 34);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(165, 23);
            this.textBoxName.TabIndex = 1;
            this.textBoxName.TextChanged += new System.EventHandler(this.textBoxName_TextChanged);
            this.textBoxName.Enter += new System.EventHandler(this.textBoxName_Enter);
            // 
            // MasterAdd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(392, 215);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox9);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MasterAdd";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "관리자 등록";
            this.Load += new System.EventHandler(this.MasterAdd_Load);
            this.groupBox9.ResumeLayout(false);
            this.groupBox9.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox9;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxPW2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxPW1;
        private System.Windows.Forms.Button buttonCheck;
        private System.Windows.Forms.Label pwLabel2;
        private System.Windows.Forms.Label pwLabel1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button4;
    }
}