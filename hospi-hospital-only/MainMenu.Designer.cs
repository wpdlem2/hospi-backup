namespace hospi_hospital_only
{
    partial class MainMenu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainMenu));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.comboBoxReceptionist = new System.Windows.Forms.ComboBox();
            this.buttonReception = new System.Windows.Forms.Button();
            this.buttonOffice = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.comboBoxOffice = new System.Windows.Forms.ComboBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.listView2 = new System.Windows.Forms.ListView();
            this.번호 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.제목 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.게시일 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label3 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.ddddToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.관리자메뉴ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.관리자계정관리MToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.운영정보설정SToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.과목및접수자편집EToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.공지사항등록DToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.끝내기XToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.comboBoxReceptionist);
            this.groupBox1.Controls.Add(this.buttonReception);
            this.groupBox1.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.groupBox1.Location = new System.Drawing.Point(12, 76);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(263, 62);
            this.groupBox1.TabIndex = 30;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "접수과";
            // 
            // comboBoxReceptionist
            // 
            this.comboBoxReceptionist.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxReceptionist.FormattingEnabled = true;
            this.comboBoxReceptionist.Location = new System.Drawing.Point(15, 26);
            this.comboBoxReceptionist.Name = "comboBoxReceptionist";
            this.comboBoxReceptionist.Size = new System.Drawing.Size(150, 23);
            this.comboBoxReceptionist.TabIndex = 33;
            this.comboBoxReceptionist.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.comboBoxReceptionist_KeyPress);
            // 
            // buttonReception
            // 
            this.buttonReception.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.buttonReception.Location = new System.Drawing.Point(171, 25);
            this.buttonReception.Name = "buttonReception";
            this.buttonReception.Size = new System.Drawing.Size(79, 25);
            this.buttonReception.TabIndex = 1;
            this.buttonReception.Text = "접수처";
            this.buttonReception.UseVisualStyleBackColor = true;
            this.buttonReception.Click += new System.EventHandler(this.buttonReception_Click);
            // 
            // buttonOffice
            // 
            this.buttonOffice.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.buttonOffice.Location = new System.Drawing.Point(171, 25);
            this.buttonOffice.Name = "buttonOffice";
            this.buttonOffice.Size = new System.Drawing.Size(79, 25);
            this.buttonOffice.TabIndex = 0;
            this.buttonOffice.Text = "진료실";
            this.buttonOffice.UseVisualStyleBackColor = true;
            this.buttonOffice.Click += new System.EventHandler(this.buttonOffice_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.comboBoxOffice);
            this.groupBox2.Controls.Add(this.buttonOffice);
            this.groupBox2.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.groupBox2.Location = new System.Drawing.Point(281, 76);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(263, 62);
            this.groupBox2.TabIndex = 31;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "진료실";
            // 
            // comboBoxOffice
            // 
            this.comboBoxOffice.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxOffice.FormattingEnabled = true;
            this.comboBoxOffice.Location = new System.Drawing.Point(15, 26);
            this.comboBoxOffice.Name = "comboBoxOffice";
            this.comboBoxOffice.Size = new System.Drawing.Size(150, 23);
            this.comboBoxOffice.TabIndex = 34;
            this.comboBoxOffice.Tag = "";
            this.comboBoxOffice.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.comboBoxOffice_KeyPress);
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.button2);
            this.groupBox6.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.groupBox6.Location = new System.Drawing.Point(550, 76);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(149, 62);
            this.groupBox6.TabIndex = 35;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "의료 영상실";
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button2.Location = new System.Drawing.Point(8, 22);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(129, 27);
            this.button2.TabIndex = 42;
            this.button2.Text = "촬영실";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.listView2);
            this.groupBox4.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.groupBox4.Location = new System.Drawing.Point(12, 144);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(687, 173);
            this.groupBox4.TabIndex = 32;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "공지사항";
            // 
            // listView2
            // 
            this.listView2.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.번호,
            this.제목,
            this.게시일,
            this.columnHeader2,
            this.columnHeader1,
            this.columnHeader3,
            this.columnHeader4});
            this.listView2.FullRowSelect = true;
            this.listView2.GridLines = true;
            this.listView2.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listView2.HideSelection = false;
            this.listView2.LabelWrap = false;
            this.listView2.Location = new System.Drawing.Point(16, 29);
            this.listView2.MultiSelect = false;
            this.listView2.Name = "listView2";
            this.listView2.Size = new System.Drawing.Size(659, 127);
            this.listView2.TabIndex = 34;
            this.listView2.UseCompatibleStateImageBehavior = false;
            this.listView2.View = System.Windows.Forms.View.Details;
            this.listView2.ColumnWidthChanging += new System.Windows.Forms.ColumnWidthChangingEventHandler(this.listView2_ColumnWidthChanging);
            this.listView2.SelectedIndexChanged += new System.EventHandler(this.listView2_SelectedIndexChanged);
            this.listView2.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listView2_MouseDoubleClick);
            // 
            // 번호
            // 
            this.번호.Text = "No.";
            this.번호.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.번호.Width = 48;
            // 
            // 제목
            // 
            this.제목.Text = "제목";
            this.제목.Width = 417;
            // 
            // 게시일
            // 
            this.게시일.Text = "게시자";
            this.게시일.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.게시일.Width = 90;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "게시일";
            this.columnHeader2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader2.Width = 90;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "id";
            this.columnHeader1.Width = 0;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "end";
            this.columnHeader3.Width = 0;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "today";
            this.columnHeader4.Width = 0;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.label3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label3.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(6, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(183, 19);
            this.label3.TabIndex = 39;
            this.label3.Text = "병원명 표시 라벨";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.Control;
            this.menuStrip1.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ddddToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(5, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(713, 24);
            this.menuStrip1.TabIndex = 42;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuStrip1_ItemClicked);
            // 
            // ddddToolStripMenuItem
            // 
            this.ddddToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.관리자메뉴ToolStripMenuItem,
            this.toolStripMenuItem1,
            this.끝내기XToolStripMenuItem});
            this.ddddToolStripMenuItem.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.ddddToolStripMenuItem.Name = "ddddToolStripMenuItem";
            this.ddddToolStripMenuItem.Size = new System.Drawing.Size(66, 20);
            this.ddddToolStripMenuItem.Text = "메뉴(&M) ";
            this.ddddToolStripMenuItem.Click += new System.EventHandler(this.ddddToolStripMenuItem_Click);
            // 
            // 관리자메뉴ToolStripMenuItem
            // 
            this.관리자메뉴ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.관리자계정관리MToolStripMenuItem,
            this.운영정보설정SToolStripMenuItem,
            this.과목및접수자편집EToolStripMenuItem,
            this.공지사항등록DToolStripMenuItem});
            this.관리자메뉴ToolStripMenuItem.Name = "관리자메뉴ToolStripMenuItem";
            this.관리자메뉴ToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.관리자메뉴ToolStripMenuItem.Text = "관리자 메뉴(&A)";
            // 
            // 관리자계정관리MToolStripMenuItem
            // 
            this.관리자계정관리MToolStripMenuItem.Name = "관리자계정관리MToolStripMenuItem";
            this.관리자계정관리MToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.관리자계정관리MToolStripMenuItem.Text = "관리자 계정 관리(&M) ";
            this.관리자계정관리MToolStripMenuItem.Click += new System.EventHandler(this.관리자계정관리MToolStripMenuItem_Click);
            // 
            // 운영정보설정SToolStripMenuItem
            // 
            this.운영정보설정SToolStripMenuItem.Name = "운영정보설정SToolStripMenuItem";
            this.운영정보설정SToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.운영정보설정SToolStripMenuItem.Text = "운영 정보 설정(&S)";
            this.운영정보설정SToolStripMenuItem.Click += new System.EventHandler(this.운영정보설정SToolStripMenuItem_Click);
            // 
            // 과목및접수자편집EToolStripMenuItem
            // 
            this.과목및접수자편집EToolStripMenuItem.Name = "과목및접수자편집EToolStripMenuItem";
            this.과목및접수자편집EToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.과목및접수자편집EToolStripMenuItem.Text = "과목 및 접수자 편집(&E)";
            this.과목및접수자편집EToolStripMenuItem.Click += new System.EventHandler(this.과목및접수자편집EToolStripMenuItem_Click);
            // 
            // 공지사항등록DToolStripMenuItem
            // 
            this.공지사항등록DToolStripMenuItem.Name = "공지사항등록DToolStripMenuItem";
            this.공지사항등록DToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.공지사항등록DToolStripMenuItem.Text = "공지사항 등록(&D)";
            this.공지사항등록DToolStripMenuItem.Click += new System.EventHandler(this.공지사항등록DToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(151, 6);
            // 
            // 끝내기XToolStripMenuItem
            // 
            this.끝내기XToolStripMenuItem.Name = "끝내기XToolStripMenuItem";
            this.끝내기XToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.끝내기XToolStripMenuItem.Text = "끝내기(&X)";
            this.끝내기XToolStripMenuItem.Click += new System.EventHandler(this.끝내기XToolStripMenuItem_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.groupBox3.Location = new System.Drawing.Point(315, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(229, 58);
            this.groupBox3.TabIndex = 42;
            this.groupBox3.TabStop = false;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label1.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(6, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(217, 19);
            this.label1.TabIndex = 39;
            this.label1.Text = "0000-00-00 월요일 00:00:00";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.label2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label2.Font = new System.Drawing.Font("Algerian", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Green;
            this.label2.Location = new System.Drawing.Point(7, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 43);
            this.label2.TabIndex = 43;
            this.label2.Text = "Hospi";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label2.DoubleClick += new System.EventHandler(this.label2_DoubleClick);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button1.Location = new System.Drawing.Point(8, 21);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(129, 27);
            this.button1.TabIndex = 43;
            this.button1.Text = "종 료";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.끝내기XToolStripMenuItem_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.button1);
            this.groupBox5.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.groupBox5.Location = new System.Drawing.Point(550, 12);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(149, 58);
            this.groupBox5.TabIndex = 42;
            this.groupBox5.TabStop = false;
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.label3);
            this.groupBox8.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.groupBox8.Location = new System.Drawing.Point(114, 12);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(195, 58);
            this.groupBox8.TabIndex = 44;
            this.groupBox8.TabStop = false;
            // 
            // MainMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(713, 325);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox8);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox4);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainMenu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "메인 메뉴";
            this.Load += new System.EventHandler(this.MainMenu_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox8.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button buttonOffice;
        private System.Windows.Forms.Button buttonReception;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.ComboBox comboBoxReceptionist;
        private System.Windows.Forms.ComboBox comboBoxOffice;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem ddddToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ListView listView2;
        private System.Windows.Forms.ColumnHeader 번호;
        private System.Windows.Forms.ColumnHeader 제목;
        private System.Windows.Forms.ColumnHeader 게시일;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ToolStripMenuItem 관리자메뉴ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 관리자계정관리MToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 운영정보설정SToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 과목및접수자편집EToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 공지사항등록DToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 끝내기XToolStripMenuItem;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.GroupBox groupBox8;
    }
}