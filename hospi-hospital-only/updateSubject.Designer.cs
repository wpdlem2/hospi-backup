namespace hospi_hospital_only
{
    partial class UpdateSubject
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UpdateSubject));
            this.groupBox11 = new System.Windows.Forms.GroupBox();
            this.buttonHistory = new System.Windows.Forms.Button();
            this.labelID = new System.Windows.Forms.Label();
            this.buttonSearch = new System.Windows.Forms.Button();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.listBoxSubject = new System.Windows.Forms.ListBox();
            this.buttonFinish = new System.Windows.Forms.Button();
            this.groupBox11.SuspendLayout();
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
            this.groupBox11.Controls.Add(this.listBoxSubject);
            this.groupBox11.Controls.Add(this.buttonFinish);
            this.groupBox11.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.groupBox11.Location = new System.Drawing.Point(12, 12);
            this.groupBox11.Name = "groupBox11";
            this.groupBox11.Size = new System.Drawing.Size(369, 229);
            this.groupBox11.TabIndex = 34;
            this.groupBox11.TabStop = false;
            this.groupBox11.Text = "과목 편집";
            // 
            // buttonHistory
            // 
            this.buttonHistory.Location = new System.Drawing.Point(251, 155);
            this.buttonHistory.Name = "buttonHistory";
            this.buttonHistory.Size = new System.Drawing.Size(102, 25);
            this.buttonHistory.TabIndex = 36;
            this.buttonHistory.Text = "삭제 기록";
            this.buttonHistory.UseVisualStyleBackColor = true;
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
            this.labelID.Text = "과목명";
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
            // 
            // textBoxName
            // 
            this.textBoxName.Location = new System.Drawing.Point(89, 28);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(156, 23);
            this.textBoxName.TabIndex = 0;
            // 
            // buttonAdd
            // 
            this.buttonAdd.Location = new System.Drawing.Point(251, 58);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(102, 25);
            this.buttonAdd.TabIndex = 34;
            this.buttonAdd.Text = "추가";
            this.buttonAdd.UseVisualStyleBackColor = true;
            // 
            // buttonDelete
            // 
            this.buttonDelete.Location = new System.Drawing.Point(251, 89);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(102, 25);
            this.buttonDelete.TabIndex = 35;
            this.buttonDelete.Text = "삭제";
            this.buttonDelete.UseVisualStyleBackColor = true;
            // 
            // listBoxSubject
            // 
            this.listBoxSubject.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.listBoxSubject.FormattingEnabled = true;
            this.listBoxSubject.ItemHeight = 17;
            this.listBoxSubject.Location = new System.Drawing.Point(15, 57);
            this.listBoxSubject.Name = "listBoxSubject";
            this.listBoxSubject.Size = new System.Drawing.Size(230, 157);
            this.listBoxSubject.TabIndex = 34;
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
            // UpdateSubject
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(392, 250);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox11);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "UpdateSubject";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "UpdateSubject";
            this.Load += new System.EventHandler(this.UpdateSubject_Load);
            this.groupBox11.ResumeLayout(false);
            this.groupBox11.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox11;
        private System.Windows.Forms.Button buttonHistory;
        private System.Windows.Forms.Label labelID;
        private System.Windows.Forms.Button buttonSearch;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.ListBox listBoxSubject;
        private System.Windows.Forms.Button buttonFinish;
    }
}