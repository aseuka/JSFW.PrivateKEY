using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static JSFW.PrivateKEY.StartForm;

namespace JSFW.PrivateKEY
{
    public partial class InDataEditControl : UserControl
    {
        internal InData Data { get; private set; } 

        public bool IsSelected
        {
            get { return chkSelect.Checked; }
            private set { chkSelect.Checked = value; }
        }
         
        public InDataEditControl()
        {
            InitializeComponent();
            this.Disposed += InDataEditControl_Disposed;
        }

        private void InDataEditControl_Disposed(object sender, EventArgs e)
        {
            DataClear();
            Data = null;
        }

        public void SetInData(InData data)
        {
            try
            { 
                Data = data;
                DataBind();
            }
            finally
            {
                
            }
        }

        private void DataBind()
        {  
            DataClear();
            if (Data != null)
            {
                txtSiteName.Text = Data.SiteName;
                txtUID.Text = Data.UID.LastMasking(); 
            }
        }
         
        private void DataClear()
        {
            txtSiteName.ResetText();
            txtUID.ResetText();
        }

        public void ShowCheckBox()
        {
            chkSelect.Checked = false;
            chkSelect.Visible = true;
        }

        public void HideCheckBox()
        {
            chkSelect.Checked = false;
            chkSelect.Visible = false;
        }
            
        private void txtSiteName_MouseDown(object sender, MouseEventArgs e)
        {
            OnMouseDown(e);
        }

        private void txtUID_MouseDown(object sender, MouseEventArgs e)
        {
            OnMouseDown(e);
        }
 
        private void txtSiteName_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            OnMouseDoubleClick(e);
        }

        private void txtUID_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (!string.IsNullOrEmpty(Data.UID))
            {
                Clipboard.SetText(Data.UID);// "복사완료!".Alert();
            }
        }

        private void btnGEN_Click(object sender, EventArgs e)
        {
            // 비밀번호 생성. 
            if (Data.IsInputPWD)
            {
                if (string.IsNullOrEmpty(("" + Data.InputPWD).Trim()))
                {
                    "비밀번호를 먼저 등록".Alert();
                    return;
                }
                Clipboard.SetText(("" + Data.InputPWD).Dec(KeyData.SubKEY + Data.UID).Trim()); "복사완료!".Alert();
            }
            else
            {
                string newPWD = Data.CreatePrivateKey();
                if (!string.IsNullOrEmpty(newPWD.Trim()))
                {
                    Clipboard.SetText(newPWD); "복사완료!".Alert();
                }
            }
        }
    }
}
