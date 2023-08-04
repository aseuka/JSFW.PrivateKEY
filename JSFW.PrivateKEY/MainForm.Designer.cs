namespace JSFW.PrivateKEY
{
    partial class MainForm
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
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnItemAdd = new System.Windows.Forms.Button();
            this.btnItemDel = new System.Windows.Forms.Button();
            this.btnDelOK = new System.Windows.Forms.Button();
            this.btnDelCancel = new System.Windows.Forms.Button();
            this.btnPWDChange = new System.Windows.Forms.Button();
            this.txtPWDChange = new System.Windows.Forms.TextBox();
            this.label2 = new JSFW.PREZI.Controls.Label();
            this.label1 = new JSFW.PREZI.Controls.Label();
            this.SuspendLayout();
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(61, 6);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(131, 21);
            this.txtSearch.TabIndex = 0;
            this.txtSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyDown);
            // 
            // btnSearch
            // 
            this.btnSearch.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Location = new System.Drawing.Point(198, 6);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(42, 21);
            this.btnSearch.TabIndex = 1;
            this.btnSearch.Text = "검색";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(2, 30);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(443, 490);
            this.flowLayoutPanel1.TabIndex = 3;
            this.flowLayoutPanel1.WrapContents = false;
            // 
            // btnItemAdd
            // 
            this.btnItemAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnItemAdd.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnItemAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnItemAdd.Location = new System.Drawing.Point(375, 6);
            this.btnItemAdd.Name = "btnItemAdd";
            this.btnItemAdd.Size = new System.Drawing.Size(32, 21);
            this.btnItemAdd.TabIndex = 2;
            this.btnItemAdd.Text = "+";
            this.btnItemAdd.UseVisualStyleBackColor = true;
            this.btnItemAdd.Click += new System.EventHandler(this.btnItemAdd_Click);
            // 
            // btnItemDel
            // 
            this.btnItemDel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnItemDel.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnItemDel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnItemDel.Location = new System.Drawing.Point(413, 6);
            this.btnItemDel.Name = "btnItemDel";
            this.btnItemDel.Size = new System.Drawing.Size(32, 21);
            this.btnItemDel.TabIndex = 3;
            this.btnItemDel.Text = "-";
            this.btnItemDel.UseVisualStyleBackColor = true;
            this.btnItemDel.Click += new System.EventHandler(this.btnItemDel_Click);
            // 
            // btnDelOK
            // 
            this.btnDelOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDelOK.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnDelOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelOK.Location = new System.Drawing.Point(375, 6);
            this.btnDelOK.Name = "btnDelOK";
            this.btnDelOK.Size = new System.Drawing.Size(32, 21);
            this.btnDelOK.TabIndex = 2;
            this.btnDelOK.Text = "O";
            this.btnDelOK.UseVisualStyleBackColor = true;
            this.btnDelOK.Click += new System.EventHandler(this.btnDelOK_Click);
            // 
            // btnDelCancel
            // 
            this.btnDelCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDelCancel.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnDelCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelCancel.Location = new System.Drawing.Point(413, 6);
            this.btnDelCancel.Name = "btnDelCancel";
            this.btnDelCancel.Size = new System.Drawing.Size(32, 21);
            this.btnDelCancel.TabIndex = 2;
            this.btnDelCancel.Text = "X";
            this.btnDelCancel.UseVisualStyleBackColor = true;
            this.btnDelCancel.Click += new System.EventHandler(this.btnDelCancel_Click);
            // 
            // btnPWDChange
            // 
            this.btnPWDChange.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnPWDChange.Location = new System.Drawing.Point(285, 523);
            this.btnPWDChange.Name = "btnPWDChange";
            this.btnPWDChange.Size = new System.Drawing.Size(75, 23);
            this.btnPWDChange.TabIndex = 5;
            this.btnPWDChange.TabStop = false;
            this.btnPWDChange.Text = "변경";
            this.btnPWDChange.UseVisualStyleBackColor = true;
            this.btnPWDChange.Click += new System.EventHandler(this.btnPWDChange_Click);
            // 
            // txtPWDChange
            // 
            this.txtPWDChange.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtPWDChange.Location = new System.Drawing.Point(119, 524);
            this.txtPWDChange.MaxLength = 32;
            this.txtPWDChange.Name = "txtPWDChange";
            this.txtPWDChange.Size = new System.Drawing.Size(160, 21);
            this.txtPWDChange.TabIndex = 6;
            this.txtPWDChange.TabStop = false;
            this.txtPWDChange.UseSystemPasswordChar = true;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.Location = new System.Drawing.Point(0, 523);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(113, 23);
            this.label2.TabIndex = 7;
            this.label2.Text = "프로그램 비밀번호";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "사이트";
            // 
            // MainForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(447, 548);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtPWDChange);
            this.Controls.Add(this.btnPWDChange);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.btnItemDel);
            this.Controls.Add(this.btnItemAdd);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.btnDelCancel);
            this.Controls.Add(this.btnDelOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "비밀번호 관리";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button btnItemAdd;
        private System.Windows.Forms.Button btnItemDel;
        private PREZI.Controls.Label label1;
        private System.Windows.Forms.Button btnDelOK;
        private System.Windows.Forms.Button btnDelCancel;
        private System.Windows.Forms.Button btnPWDChange;
        private System.Windows.Forms.TextBox txtPWDChange;
        private PREZI.Controls.Label label2;
    }
}