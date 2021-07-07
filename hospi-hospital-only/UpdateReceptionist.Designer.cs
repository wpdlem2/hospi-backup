namespace hospi_hospital_only
{
    partial class UpdateReceptionist
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UpdateReceptionist));
            this.groupBox11 = new System.Windows.Forms.GroupBox();
            this.buttonHistory = new System.Windows.Forms.Button();
            this.labelID = new System.Windows.Forms.Label();
            this.buttonSearch = new System.Windows.Forms.Button();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.listBoxReceptionist = new System.Windows.Forms.ListBox();
            this.buttonFinish = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonSearch2 = new System.Windows.Forms.Button();
            this.labelID2 = new System.Windows.Forms.Label();
            this.textBoxName2 = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.listBoxDelete = new System.Windows.Forms.ListBox();
            this.groupBox11.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox11
            // 
            this.groupBox11.Controls.Add(this.buttonHistory);
            this.groupBox11.Controls.Add(this.labelID);
            this.groupBox11.Controls.Add(this.buttonSearch);
            this.groupBox11.Controls.Add(this.textBoxName);
            this.groupBox11.Controls.Add(this.buttonAdd);
            this.groupBox11.Controls.Add(this.buttonDelete);
            this.groupBox11.Controls.Add(this.listBoxReceptionist);
            this.groupBox11.Controls.Add(this.buttonFinish);
            this.groupBox11.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.groupBox11.Location = new System.Drawing.Point(12, 12);
            this.groupBox11.Name = "groupBox11";
            this.groupBox11.Size = new System.Drawing.Size(369, 229);
            this.groupBox11.TabIndex = 33;
            this.groupBox11.TabStop = false;
            this.groupBox11.Text = "접수자 편집";
            // 
            // buttonHistory
            // 
            this.buttonHistory.Location = new System.Drawing.Point(251, 155);
            this.buttonHistory.Name = "buttonHistory";
            this.buttonHistory.Size = new System.Drawing.Size(102, 25);
            this.buttonHistory.TabIndex = 36;
            this.buttonHistory.Text = "삭제 기록";
            this.buttonHistory.UseVisualStyleBackColor = true;
            this.buttonHistory.Click += new System.EventHandler(this.buttonHistory_Click);
            // 
            // labelID
            // 
            this.labelID.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.labelID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelID.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.labelID.Location = new System.Drawing.Point(15, 28);
            this.labelID.Name = "labelID";
            this.labelID.Size = new System.Drawing.Size(68, 23);
            this.labelID.TabIndex = 37;
            this.labelID.Text = "접수자명";
            this.labelID.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // buttonSearch
            // 
            this.buttonSearch.Location = new System.Drawing.Point(251, 27);
            this.buttonSearch.Name = "buttonSearch";
            this.buttonSearch.Size = new System.Drawing.Size(102, 25);
            this.buttonSearch.TabIndex = 36;
            this.buttonSearch.Text = "검색";
            this.buttonSearch.UseVisualStyleBackColor = true;
            this.buttonSearch.Click += new System.EventHandler(this.buttonSearch_Click);
            // 
            // textBoxName
            // 
            this.textBoxName.Location = new System.Drawing.Point(89, 28);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(156, 23);
            this.textBoxName.TabIndex = 0;
            this.textBoxName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxName_KeyDown);
            // 
            // buttonAdd
            // 
            this.buttonAdd.Location = new System.Drawing.Point(251, 58);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(102, 25);
            this.buttonAdd.TabIndex = 34;
            this.buttonAdd.Text = "추가";
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // buttonDelete
            // 
            this.buttonDelete.Location = new System.Drawing.Point(251, 89);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(102, 25);
            this.buttonDelete.TabIndex = 35;
            this.buttonDelete.Text = "삭제";
            this.buttonDelete.UseVisualStyleBackColor = true;
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            // 
            // listBoxReceptionist
            // 
            this.listBoxReceptionist.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.listBoxReceptionist.FormattingEnabled = true;
            this.listBoxReceptionist.ItemHeight = 17;
            this.listBoxReceptionist.Location = new System.Drawing.Point(15, 57);
            this.listBoxReceptionist.Name = "listBoxReceptionist";
            this.listBoxReceptionist.Size = new System.Drawing.Size(230, 157);
            this.listBoxReceptionist.TabIndex = 34;
            this.listBoxReceptionist.SelectedIndexChanged += new System.EventHandler(this.listBoxReceptionist_SelectedIndexChanged);
            // 
            // buttonFinish
            // 
            this.buttonFinish.Location = new System.Drawing.Point(251, 186);
            this.buttonFinish.Name = "buttonFinish";
            this.buttonFinish.Size = new System.Drawing.Size(102, 28);
            this.buttonFinish.TabIndex = 1;
            this.buttonFinish.Text = "종료";
            this.buttonFinish.UseVisualStyleBackColor = true;
            this.buttonFinish.Click += new System.EventHandler(this.buttonFinish_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buttonSearch2);
            this.groupBox1.Controls.Add(this.labelID2);
            this.groupBox1.Controls.Add(this.textBoxName2);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.button3);
            this.groupBox1.Controls.Add(this.listBoxDelete);
            this.groupBox1.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.groupBox1.Location = new System.Drawing.Point(397, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(369, 229);
            this.groupBox1.TabIndex = 34;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "삭제 내역";
            // 
            // buttonSearch2
            // 
            this.buttonSearch2.Location = new System.Drawing.Point(251, 27);
            this.buttonSearch2.Name = "buttonSearch2";
            this.buttonSearch2.Size = new System.Drawing.Size(102, 25);
            this.buttonSearch2.TabIndex = 40;
            this.buttonSearch2.Text = "검색";
            this.buttonSearch2.UseVisualStyleBackColor = true;
            this.buttonSearch2.Click += new System.EventHandler(this.buttonSearch2_Click);
            // 
            // labelID2
            // 
            this.labelID2.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.labelID2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelID2.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.labelID2.Location = new System.Drawing.Point(15, 28);
            this.labelID2.Name = "labelID2";
            this.labelID2.Size = new System.Drawing.Size(68, 23);
            this.labelID2.TabIndex = 39;
            this.labelID2.Text = "접수자명";
            this.labelID2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBoxName2
            // 
            this.textBoxName2.Location = new System.Drawing.Point(89, 28);
            this.textBoxName2.Name = "textBoxName2";
            this.textBoxName2.Size = new System.Drawing.Size(156, 23);
            this.textBoxName2.TabIndex = 38;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(251, 189);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(102, 25);
            this.button2.TabIndex = 36;
            this.button2.Text = "기록 닫기";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(251, 60);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(102, 25);
            this.button3.TabIndex = 34;
            this.button3.Text = "복구";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // listBoxDelete
            // 
            this.listBoxDelete.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.listBoxDelete.FormattingEnabled = true;
            this.listBoxDelete.ItemHeight = 17;
            this.listBoxDelete.Location = new System.Drawing.Point(15, 57);
            this.listBoxDelete.Name = "listBoxDelete";
            this.listBoxDelete.Size = new System.Drawing.Size(230, 157);
            this.listBoxDelete.TabIndex = 34;
            // 
            // UpdateReceptionist
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(393, 250);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox11);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "UpdateReceptionist";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "접수자 추가/삭제";
            this.Load += new System.EventHandler(this.UpdateReceptionist_Load);
            this.groupBox11.ResumeLayout(false);
            this.groupBox11.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox11;
        private System.Windows.Forms.Button buttonFinish;
        private System.Windows.Forms.ListBox listBoxReceptionist;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.Button buttonSearch;
        private System.Windows.Forms.Label labelID;
        private System.Windows.Forms.Button buttonHistory;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.ListBox listBoxDelete;
        private System.Windows.Forms.Button buttonSearch2;
        private System.Windows.Forms.Label labelID2;
        private System.Windows.Forms.TextBox textBoxName2;
    }
}