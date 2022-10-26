namespace JSFW.PrivateKEY
{
    partial class inDataDetailForm
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
            this.txtSiteName = new System.Windows.Forms.TextBox();
            this.txtUID = new System.Windows.Forms.TextBox();
            this.txtBIGO = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnGEN = new System.Windows.Forms.Button();
            this.txtPWD = new System.Windows.Forms.TextBox();
            this.txtPWDValidate = new System.Windows.Forms.TextBox();
            this.chkINPUTPWD = new System.Windows.Forms.CheckBox();
            this.numPWDLength = new System.Windows.Forms.NumericUpDown();
            this.pwD_SPECIALCHAR_Control1 = new JSFW.PrivateKEY.PWD_SPECIALCHAR_Control();
            this.lbGUID = new JSFW.PREZI.Controls.Label();
            this.label6 = new JSFW.PREZI.Controls.Label();
            this.label4 = new JSFW.PREZI.Controls.Label();
            this.label3 = new JSFW.PREZI.Controls.Label();
            this.label5 = new JSFW.PREZI.Controls.Label();
            this.label2 = new JSFW.PREZI.Controls.Label();
            this.label1 = new JSFW.PREZI.Controls.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numPWDLength)).BeginInit();
            this.SuspendLayout();
            // 
            // txtSiteName
            // 
            this.txtSiteName.Location = new System.Drawing.Point(118, 8);
            this.txtSiteName.Name = "txtSiteName";
            this.txtSiteName.ReadOnly = true;
            this.txtSiteName.Size = new System.Drawing.Size(234, 21);
            this.txtSiteName.TabIndex = 0;
            this.txtSiteName.TextChanged += new System.EventHandler(this.txtSiteName_TextChanged);
            this.txtSiteName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSiteName_KeyDown);
            // 
            // txtUID
            // 
            this.txtUID.Location = new System.Drawing.Point(118, 35);
            this.txtUID.Name = "txtUID";
            this.txtUID.ReadOnly = true;
            this.txtUID.Size = new System.Drawing.Size(133, 21);
            this.txtUID.TabIndex = 1;
            this.txtUID.TextChanged += new System.EventHandler(this.txtUID_TextChanged);
            this.txtUID.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtUID_KeyDown);
            // 
            // txtBIGO
            // 
            this.txtBIGO.Location = new System.Drawing.Point(118, 139);
            this.txtBIGO.Multiline = true;
            this.txtBIGO.Name = "txtBIGO";
            this.txtBIGO.Size = new System.Drawing.Size(369, 73);
            this.txtBIGO.TabIndex = 7;
            this.txtBIGO.TextChanged += new System.EventHandler(this.txtBIGO_TextChanged);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.ForestGreen;
            this.btnSave.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(358, 7);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(58, 25);
            this.btnSave.TabIndex = 8;
            this.btnSave.Text = "저장";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Location = new System.Drawing.Point(422, 7);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(58, 25);
            this.btnClose.TabIndex = 9;
            this.btnClose.Text = "닫기";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnGEN
            // 
            this.btnGEN.BackColor = System.Drawing.Color.Coral;
            this.btnGEN.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGEN.ForeColor = System.Drawing.Color.White;
            this.btnGEN.Location = new System.Drawing.Point(257, 34);
            this.btnGEN.Name = "btnGEN";
            this.btnGEN.Size = new System.Drawing.Size(56, 22);
            this.btnGEN.TabIndex = 2;
            this.btnGEN.Text = "생성";
            this.btnGEN.UseVisualStyleBackColor = false;
            this.btnGEN.Click += new System.EventHandler(this.btnGEN_Click);
            // 
            // txtPWD
            // 
            this.txtPWD.Location = new System.Drawing.Point(118, 62);
            this.txtPWD.Name = "txtPWD";
            this.txtPWD.ReadOnly = true;
            this.txtPWD.Size = new System.Drawing.Size(133, 21);
            this.txtPWD.TabIndex = 4;
            this.txtPWD.UseSystemPasswordChar = true;
            this.txtPWD.ReadOnlyChanged += new System.EventHandler(this.txtPWD_ReadOnlyChanged);
            this.txtPWD.TextChanged += new System.EventHandler(this.txtPWD_TextChanged);
            this.txtPWD.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPWD_KeyDown);
            this.txtPWD.Leave += new System.EventHandler(this.txtPWD_Leave);
            this.txtPWD.MouseLeave += new System.EventHandler(this.txtPWD_MouseLeave);
            this.txtPWD.MouseHover += new System.EventHandler(this.txtPWD_MouseHover);
            this.txtPWD.Validating += new System.ComponentModel.CancelEventHandler(this.txtPWD_Validating);
            // 
            // txtPWDValidate
            // 
            this.txtPWDValidate.Location = new System.Drawing.Point(257, 62);
            this.txtPWDValidate.Name = "txtPWDValidate";
            this.txtPWDValidate.ReadOnly = true;
            this.txtPWDValidate.Size = new System.Drawing.Size(133, 21);
            this.txtPWDValidate.TabIndex = 5;
            this.txtPWDValidate.UseSystemPasswordChar = true;
            this.txtPWDValidate.ReadOnlyChanged += new System.EventHandler(this.txtPWDValidate_ReadOnlyChanged);
            this.txtPWDValidate.TextChanged += new System.EventHandler(this.txtPWDValidate_TextChanged);
            this.txtPWDValidate.Leave += new System.EventHandler(this.txtPWDValidate_Leave);
            this.txtPWDValidate.MouseLeave += new System.EventHandler(this.txtPWDValidate_MouseLeave);
            this.txtPWDValidate.MouseHover += new System.EventHandler(this.txtPWDValidate_MouseHover);
            // 
            // chkINPUTPWD
            // 
            this.chkINPUTPWD.AutoSize = true;
            this.chkINPUTPWD.Location = new System.Drawing.Point(396, 65);
            this.chkINPUTPWD.Name = "chkINPUTPWD";
            this.chkINPUTPWD.Size = new System.Drawing.Size(72, 16);
            this.chkINPUTPWD.TabIndex = 3;
            this.chkINPUTPWD.Text = "직접입력";
            this.chkINPUTPWD.UseVisualStyleBackColor = true;
            this.chkINPUTPWD.CheckedChanged += new System.EventHandler(this.chkINPUTPWD_CheckedChanged);
            // 
            // numPWDLength
            // 
            this.numPWDLength.Location = new System.Drawing.Point(438, 114);
            this.numPWDLength.Maximum = new decimal(new int[] {
            16,
            0,
            0,
            0});
            this.numPWDLength.Minimum = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.numPWDLength.Name = "numPWDLength";
            this.numPWDLength.ReadOnly = true;
            this.numPWDLength.Size = new System.Drawing.Size(48, 21);
            this.numPWDLength.TabIndex = 10;
            this.numPWDLength.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numPWDLength.Value = new decimal(new int[] {
            16,
            0,
            0,
            0});
            this.numPWDLength.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // pwD_SPECIALCHAR_Control1
            // 
            this.pwD_SPECIALCHAR_Control1.Location = new System.Drawing.Point(118, 86);
            this.pwD_SPECIALCHAR_Control1.MaximumSize = new System.Drawing.Size(369, 23);
            this.pwD_SPECIALCHAR_Control1.MinimumSize = new System.Drawing.Size(369, 23);
            this.pwD_SPECIALCHAR_Control1.Name = "pwD_SPECIALCHAR_Control1";
            this.pwD_SPECIALCHAR_Control1.Size = new System.Drawing.Size(369, 23);
            this.pwD_SPECIALCHAR_Control1.TabIndex = 6;
            this.pwD_SPECIALCHAR_Control1.TabStop = false;
            this.pwD_SPECIALCHAR_Control1.CheckChanged += new System.EventHandler(this.pwD_SPECIALCHAR_Control1_CheckChanged);
            // 
            // lbGUID
            // 
            this.lbGUID.ForeColor = System.Drawing.Color.Maroon;
            this.lbGUID.Location = new System.Drawing.Point(373, 34);
            this.lbGUID.Name = "lbGUID";
            this.lbGUID.Size = new System.Drawing.Size(107, 23);
            this.lbGUID.TabIndex = 0;
            this.lbGUID.Text = "GUID";
            this.lbGUID.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lbGUID.Visible = false;
            // 
            // label6
            // 
            this.label6.ForeColor = System.Drawing.Color.Maroon;
            this.label6.Location = new System.Drawing.Point(118, 112);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(369, 23);
            this.label6.TabIndex = 0;
            this.label6.Text = "※ 특수문자 위치 지정, 필수 : 숫자 1자리 포함";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(12, 139);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 23);
            this.label4.TabIndex = 0;
            this.label4.Text = "비고";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(12, 88);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 23);
            this.label3.TabIndex = 0;
            this.label3.Text = "특수문자";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(12, 60);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(100, 23);
            this.label5.TabIndex = 0;
            this.label5.Text = "사용 비밀번호";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(12, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 23);
            this.label2.TabIndex = 0;
            this.label2.Text = "아이디";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "사이트";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // inDataDetailForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(490, 215);
            this.Controls.Add(this.numPWDLength);
            this.Controls.Add(this.chkINPUTPWD);
            this.Controls.Add(this.btnGEN);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.pwD_SPECIALCHAR_Control1);
            this.Controls.Add(this.txtBIGO);
            this.Controls.Add(this.txtPWDValidate);
            this.Controls.Add(this.txtPWD);
            this.Controls.Add(this.txtUID);
            this.Controls.Add(this.txtSiteName);
            this.Controls.Add(this.lbGUID);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "inDataDetailForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "상세정보";
            ((System.ComponentModel.ISupportInitialize)(this.numPWDLength)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private PREZI.Controls.Label label1;
        private PREZI.Controls.Label label2;
        private System.Windows.Forms.TextBox txtSiteName;
        private System.Windows.Forms.TextBox txtUID;
        private PWD_SPECIALCHAR_Control pwD_SPECIALCHAR_Control1;
        private PREZI.Controls.Label label3;
        private PREZI.Controls.Label label4;
        private System.Windows.Forms.TextBox txtBIGO;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnGEN;
        private PREZI.Controls.Label label5;
        private System.Windows.Forms.TextBox txtPWD;
        private System.Windows.Forms.TextBox txtPWDValidate;
        private System.Windows.Forms.CheckBox chkINPUTPWD;
        private PREZI.Controls.Label label6;
        private PREZI.Controls.Label lbGUID;
        private System.Windows.Forms.NumericUpDown numPWDLength;
    }
}