using JSFW.PREZI.Controls;

namespace JSFW.PrivateKEY
{
    partial class InDataEditControl
    {
        /// <summary> 
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 구성 요소 디자이너에서 생성한 코드

        /// <summary> 
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.chkSelect = new System.Windows.Forms.CheckBox();
            this.btnGEN = new System.Windows.Forms.Button();
            this.txtSiteName = new JSFW.PREZI.Controls.Label();
            this.txtUID = new JSFW.PREZI.Controls.Label();
            this.SuspendLayout();
            // 
            // chkSelect
            // 
            this.chkSelect.AutoSize = true;
            this.chkSelect.Dock = System.Windows.Forms.DockStyle.Left;
            this.chkSelect.Location = new System.Drawing.Point(4, 2);
            this.chkSelect.Name = "chkSelect";
            this.chkSelect.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.chkSelect.Size = new System.Drawing.Size(17, 22);
            this.chkSelect.TabIndex = 1;
            this.chkSelect.UseVisualStyleBackColor = true;
            this.chkSelect.Visible = false;
            // 
            // btnGEN
            // 
            this.btnGEN.BackColor = System.Drawing.Color.Coral;
            this.btnGEN.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnGEN.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGEN.ForeColor = System.Drawing.Color.White;
            this.btnGEN.Location = new System.Drawing.Point(301, 2);
            this.btnGEN.Name = "btnGEN";
            this.btnGEN.Size = new System.Drawing.Size(41, 22);
            this.btnGEN.TabIndex = 0;
            this.btnGEN.Text = "GEN";
            this.btnGEN.UseVisualStyleBackColor = false;
            this.btnGEN.Click += new System.EventHandler(this.btnGEN_Click);
            // 
            // txtSiteName
            // 
            this.txtSiteName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSiteName.Location = new System.Drawing.Point(21, 2);
            this.txtSiteName.Name = "txtSiteName";
            this.txtSiteName.Size = new System.Drawing.Size(164, 22);
            this.txtSiteName.TabIndex = 0;
            this.txtSiteName.Text = "Site Name";
            this.txtSiteName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.txtSiteName.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.txtSiteName_MouseDoubleClick);
            this.txtSiteName.MouseDown += new System.Windows.Forms.MouseEventHandler(this.txtSiteName_MouseDown);
            // 
            // txtUID
            // 
            this.txtUID.AutoEllipsis = true;
            this.txtUID.Dock = System.Windows.Forms.DockStyle.Right;
            this.txtUID.Location = new System.Drawing.Point(185, 2);
            this.txtUID.Name = "txtUID";
            this.txtUID.Size = new System.Drawing.Size(116, 22);
            this.txtUID.TabIndex = 0;
            this.txtUID.Text = "12345678901234567890";
            this.txtUID.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.txtUID.UseCompatibleTextRendering = true;
            this.txtUID.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.txtUID_MouseDoubleClick);
            this.txtUID.MouseDown += new System.Windows.Forms.MouseEventHandler(this.txtUID_MouseDown);
            // 
            // InDataEditControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.Controls.Add(this.txtSiteName);
            this.Controls.Add(this.txtUID);
            this.Controls.Add(this.chkSelect);
            this.Controls.Add(this.btnGEN);
            this.Name = "InDataEditControl";
            this.Padding = new System.Windows.Forms.Padding(4, 2, 2, 2);
            this.Size = new System.Drawing.Size(344, 26);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private JSFW.PREZI.Controls.Label txtSiteName;
        private JSFW.PREZI.Controls.Label txtUID;
        private System.Windows.Forms.CheckBox chkSelect;
        private System.Windows.Forms.Button btnGEN;
    }
}
