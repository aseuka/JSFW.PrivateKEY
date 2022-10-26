namespace JSFW.PrivateKEY
{
    partial class TestForm
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

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.inDataEditControl1 = new JSFW.PrivateKEY.InDataEditControl();
            this.SuspendLayout();
            // 
            // inDataEditControl1
            // 
            this.inDataEditControl1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.inDataEditControl1.Location = new System.Drawing.Point(12, 12);
            this.inDataEditControl1.Name = "inDataEditControl1";
            this.inDataEditControl1.Padding = new System.Windows.Forms.Padding(4, 2, 2, 2);
            this.inDataEditControl1.Size = new System.Drawing.Size(1047, 27);
            this.inDataEditControl1.TabIndex = 0;
            // 
            // TestForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1148, 275);
            this.Controls.Add(this.inDataEditControl1);
            this.Name = "TestForm";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private InDataEditControl inDataEditControl1;
    }
}

