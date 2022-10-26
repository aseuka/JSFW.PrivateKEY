using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static JSFW.PrivateKEY.StartForm;

// txtPWD.Text.Trim().Enc(KeyData.SubKEY + txtUID.Text.Trim());

namespace JSFW.PrivateKEY
{
    public partial class inDataDetailForm : Form
    {
        public InData Data { get; private set; } = null;

        public bool IsNew { get; set; }

        bool _isBinding = false;

        bool _isDirty = false;
        bool IsDirty
        {
            get { return _isDirty; }
            set
            {
                _isDirty = value;
                btnSave.Enabled = _isDirty;
                btnSave.BackColor = IsDirty ? Color.ForestGreen : Color.WhiteSmoke;
            }
        }

        public inDataDetailForm()
        {
            InitializeComponent();
            IsDirty = false;

            //txtPWD.PasswordChar = '*';
            //txtPWDValidate.PasswordChar = '*';
            SetDEBUG();

            this.Disposed += InDataDetailForm_Disposed;
        }

        [Conditional("DEBUG")]
        private void SetDEBUG()
        {
            txtPWD.PasswordChar = '\0';
            txtPWDValidate.PasswordChar = '\0'; 
        } 
        private void InDataDetailForm_Disposed(object sender, EventArgs e)
        {
            Data = null;
        }

        public void SetInData(InData data)
        {
            Data = null;

            Data = data;//.Clone() as InData;
            try
            {
                _isBinding = true;
                DataBind();
            }
            finally
            {
                IsDirty = false;
                _isBinding = false;
            }
        }

        private void DataBind()
        {
            DataClear(); 
            if (Data != null)
            {
                lbGUID.Text = Data.GUID;
                txtSiteName.Text = ""+Data.SiteName;
                txtUID.Text = ""+Data.UID;
                txtBIGO.Text = (""+Data.DESC).Replace("\n", Environment.NewLine ); 
                pwD_SPECIALCHAR_Control1.Set(Data.SpecialNumberIndexs.ToArray());

                numPWDLength.Value = Data.LenPWD;

                txtSiteName.ReadOnly = !IsNew;
                txtUID.ReadOnly = !IsNew;

                chkINPUTPWD.Checked = Data.IsInputPWD;

                numPWDLength.Enabled = !chkINPUTPWD.Checked;

                if (Data.IsInputPWD)
                {
                    string pwd = (""+Data.InputPWD).Dec(KeyData.SubKEY + Data.UID);
                    txtPWD.Text = pwd;
                    txtPWDValidate.Text = pwd;
                }

                txtPWD.ReadOnly = true;
                txtPWDValidate.ReadOnly = true;
                pwD_SPECIALCHAR_Control1.Enabled = !chkINPUTPWD.Checked;
            }
        }

        private void DataClear()
        {
            lbGUID.Text = "";
            txtSiteName.Text = "";
            txtUID.Text = "";
            txtBIGO.Text = "";
            chkINPUTPWD.Checked = false;
            txtPWD.Text = "";
            txtPWDValidate.Text = "";
            numPWDLength.Value = 16;

            numPWDLength.Enabled = !chkINPUTPWD.Checked;

            pwD_SPECIALCHAR_Control1.Set(new int[0]);
        }

        private void txtSiteName_TextChanged(object sender, EventArgs e)
        {
            if (_isBinding) return; 
            IsDirty = true;
        }
         
        private void txtSiteName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                txtSiteName.ReadOnly = false;
            }
            else if (e.KeyCode == Keys.Escape)
            {
                txtSiteName.ReadOnly = !IsNew;
            }
            else if (e.KeyCode == Keys.Enter)
            {
                txtSiteName.ReadOnly = !IsNew;
            }
        }

        private void txtUID_TextChanged(object sender, EventArgs e)
        {
            if (_isBinding) return;
            IsDirty = true;
        }
         
        private void txtUID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                txtUID.ReadOnly = false;
            }
            else if (e.KeyCode == Keys.Escape)
            {
                txtUID.ReadOnly = !IsNew;
            }
            else if (e.KeyCode == Keys.Enter)
            {
                txtUID.ReadOnly = !IsNew;
            }
        }

        private void txtBIGO_TextChanged(object sender, EventArgs e)
        {
            if (_isBinding) return;
            IsDirty = true;
        }

        private void pwD_SPECIALCHAR_Control1_CheckChanged(object sender, EventArgs e)
        {
            if (_isBinding) return;
            IsDirty = true; 
        }

        string pwdText { get; set; } = "";
        private void txtPWD_TextChanged(object sender, EventArgs e)
        {
            if (_isBinding) return;
            IsDirty = true;

            pwdText = txtPWD.Text;
        }
          
        private void txtPWDValidate_TextChanged(object sender, EventArgs e)
        {
            if (_isBinding) return;
            IsDirty = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // 저장!
            if (IsDirty)
            {
                if (string.IsNullOrWhiteSpace(txtSiteName.Text.Trim()))
                {
                    "사이트명 입력".Alert();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtUID.Text.Trim()))
                {
                    "아이디입력".Alert();
                    return;
                }

                if (chkINPUTPWD.Checked)
                {
                    if (string.IsNullOrEmpty(txtPWD.Text.Trim()))
                    {
                        "비밀번호를 먼저 등록".Alert();
                        return;
                    }

                    if (txtPWD.Text.Trim() != txtPWDValidate.Text.Trim())
                    {
                        "비밀번호와 유효성 확인 비밀번호를 틀림.".Alert();
                        return;
                    }

                    Data.IsInputPWD = true;
                    Data.InputPWD = txtPWD.Text.Trim().Enc(KeyData.SubKEY + txtUID.Text.Trim());
                }
                else
                {
                    Data.IsInputPWD = false;
                    Data.InputPWD = "";
                }

                Data.GUID = lbGUID.Text;
                Data.SiteName = txtSiteName.Text.Trim();
                Data.UID = txtUID.Text.Trim();
                Data.DESC = txtBIGO.Text.Trim();
                Data.SpecialNumberIndexs.Clear();
                Data.LenPWD = (int)numPWDLength.Value;

                int[] nums = pwD_SPECIALCHAR_Control1.Get();
                if (nums != null && 0 < nums.Length)
                    Data.SpecialNumberIndexs.AddRange(nums);

                _isDirty = false; 
            } 
            DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            // 닫기.
            if (IsDirty && !"변경사항이 있음. 닫을까?".Confirm())
            {
                return;
            }
            IsDirty = false; // 닫아버림.
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            if (IsDirty && !"변경사항이 있음. 닫을까?".Confirm())
            {
                e.Cancel = true;
            }
        }

        private void btnGEN_Click(object sender, EventArgs e)
        {
            InData tmp = Data.Clone() as InData;

            if (chkINPUTPWD.Checked)
            {
                if (string.IsNullOrEmpty(txtPWD.Text.Trim()))
                {
                    "비밀번호를 먼저 등록".Alert();
                    return;
                }

                if (txtPWD.Text.Trim() != txtPWDValidate.Text.Trim())
                {
                    "비밀번호와 유효성 확인 비밀번호를 틀림.".Alert();
                    return;
                }
                Clipboard.SetText(txtPWD.Text.Trim());
                ((IsDirty ? "[저장 X]" : "") + "복사완료!").Alert();
            }
            else
            {
                lbGUID.Text = tmp.GUID = Guid.NewGuid().ToString("N");
                IsDirty = true; // GUID가 바뀌므로... //무조건 저장여부 확인이 필요함!

                tmp.SiteName = txtSiteName.Text.Trim();
                tmp.UID = txtUID.Text.Trim();
                tmp.SpecialNumberIndexs.Clear();
                int[] magicNumbers = pwD_SPECIALCHAR_Control1.Get();
                tmp.LenPWD = (int)numPWDLength.Value;

                if (magicNumbers != null && 0 < magicNumbers.Length)
                {
                    tmp.SpecialNumberIndexs.AddRange(magicNumbers.ToArray());
                } 
                string newPWD = tmp.CreatePrivateKey();
                if (!string.IsNullOrEmpty(newPWD.Trim()))
                { 
                   // txtPWD.Text = txtPWDValidate.Text = newPWD;
                    Clipboard.SetText(newPWD);
                    ((IsDirty ? "[저장 X]" : "") + "복사완료!").Alert();
                }
                tmp = null;
            }
        }
 
        private void chkINPUTPWD_CheckedChanged(object sender, EventArgs e)
        {
            if (_isBinding) return;
            IsDirty = true;
 
            txtPWD.ReadOnly = !chkINPUTPWD.Checked;
            txtPWDValidate.ReadOnly = !chkINPUTPWD.Checked;
            pwD_SPECIALCHAR_Control1.Enabled = !chkINPUTPWD.Checked;

            numPWDLength.Enabled = !chkINPUTPWD.Checked;

            txtPWD.ResetText();
            txtPWDValidate.ResetText();
            txtPWD.Focus();
        }

        private void txtPWD_ReadOnlyChanged(object sender, EventArgs e)
        {
             txtPWD.UseSystemPasswordChar = txtPWD.ReadOnly;
        }

        bool IsLeave = false;
        private void txtPWD_Leave(object sender, EventArgs e)
        {
            if (IsMLeave) return;

            IsLeave = true;
            if (txtPWD.Modified)
            {
                this.Focus();
                Action ac = new Action(() =>
                {
                    System.Threading.Thread.Sleep(500);
                    IsLeave = false;
                    this.DoAsync(c =>
                    {
                        txtPWDValidate.Text = txtPWD.Text;                        
                        txtPWD.UseSystemPasswordChar = !txtPWD.Focused || txtPWD.ReadOnly;
                        txtPWDValidate.UseSystemPasswordChar = !txtPWDValidate.Focused || txtPWD.ReadOnly;
                    });
                });
                ac.BeginInvoke(ir => ac.EndInvoke(ir), null);
            }
        }
         
        private void txtPWD_MouseHover(object sender, EventArgs e)
        {
            if (chkINPUTPWD.Checked)
            {
                txtPWD.UseSystemPasswordChar = false;
            }
        }
         
        bool IsMLeave = false;
        private void txtPWD_MouseLeave(object sender, EventArgs e)
        {
            IsMLeave = true;
            if (txtPWD.Modified)
            {
                this.Focus();
                Action ac = new Action(() =>
                {
                    System.Threading.Thread.Sleep(500);
                    IsMLeave = false;
                    this.DoAsync(c =>
                    {
                        txtPWDValidate.Text = txtPWD.Text;
                        txtPWD.UseSystemPasswordChar = !txtPWD.Focused || txtPWD.ReadOnly;
                        txtPWDValidate.UseSystemPasswordChar = !txtPWDValidate.Focused || txtPWD.ReadOnly;
                    });
                });
                ac.BeginInvoke(ir => ac.EndInvoke(ir), null);
            }
        }

        private void txtPWDValidate_ReadOnlyChanged(object sender, EventArgs e)
        {
            txtPWDValidate.UseSystemPasswordChar = txtPWDValidate.ReadOnly;
        }

        private void txtPWDValidate_Leave(object sender, EventArgs e)
        {
            txtPWDValidate.UseSystemPasswordChar = true;
        }

        private void txtPWDValidate_MouseHover(object sender, EventArgs e)
        {
            //직접 입력 >> 일때! 수정가능 여부...
            if (chkINPUTPWD.Checked)
            {
                txtPWDValidate.UseSystemPasswordChar = false;
            }
        }

        private void txtPWDValidate_MouseLeave(object sender, EventArgs e)
        {
            txtPWDValidate.UseSystemPasswordChar = true;
        }

        private void txtPWD_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void txtPWD_Validating(object sender, CancelEventArgs e)
        {

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            if (_isBinding) return;
            IsDirty = true; 
        }
    }
}
