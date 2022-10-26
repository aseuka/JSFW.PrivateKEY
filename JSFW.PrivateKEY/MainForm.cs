using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static JSFW.PrivateKEY.StartForm;

namespace JSFW.PrivateKEY
{
    public partial class MainForm : Form
    {
        KeyData MainData { get; set; }

        readonly Color SelectColor = Color.Coral;

        readonly Color NomalColor = Color.WhiteSmoke;

        private bool IsNEWFILE { get; set; } = false;

        InDataEditControl SelectedItem { get; set; } 

        public MainForm()
        {
            InitializeComponent();
            this.FormClosed += MainForm_FormClosed;
        }

        public MainForm(bool isnewFile) : this()
        {
            this.IsNEWFILE = isnewFile;
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            KeyData.SubKEY = null;
            SelectedItem = null;
            MainData = null;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e); 

            if (IsNEWFILE)
            {
                MainData = new KeyData();
                MainData.KEY = StartForm.PWD; 
            }
            else
            {
                string content = "";
                if (File.Exists(StartForm.FilePath))
                {

                    if (StartForm.isDebug)
                    {
                        content = File.ReadAllText($@"{StartForm.FilePath}.bak", Encoding.UTF8);
                    }
                    else
                    {
                        content = File.ReadAllText(StartForm.FilePath, Encoding.UTF8);
                        content = content.Dec(StartForm.PWD + KeyData.SubKEY);
                    }
                }
                MainData = content.DeSerialize<KeyData>(); 
            } 
            DataBind();
        }

        private void DataBind()
        {
            DataClear(); 
            foreach (var inData in MainData.Datas)
            {
                InDataEditControl newInDataEditor = new InDataEditControl();
                newInDataEditor.SetInData(inData);
                flowLayoutPanel1.Controls.Add(newInDataEditor);
                newInDataEditor.MouseDown += NewInDataEditor_MouseDown;
                newInDataEditor.MouseDoubleClick += NewInDataEditor_MouseDoubleClick;
            }
        }
         
        private void Save()
        {
            if (MainData == null) return;
             
            string content = MainData.Serialize();
            content = content.Enc(StartForm.PWD + KeyData.SubKEY);
            if (File.Exists(StartForm.FilePath))
            {
                File.Delete(StartForm.FilePath);
            }
            File.WriteAllText(StartForm.FilePath, content, Encoding.UTF8);

            //2022-02-14 :: 무슨이유에서인지 원래 있던 파일이 안열림! ( 패딩 에러? )
            //           :: 만일의 사태를 위해 pwd를 제외한 사이트와 아이디 정보를 별도로 기록!!
            //           :: 백업해야 할 필요성이 있음. 
            KeyData bak = MainData.Clone() as KeyData;
            if (bak != null)
            {
                bak.KEY = @"개인키로 변경 후 복원을 시도하여야 합니다";
                foreach (var item in bak.Datas)
                {
                    item.InputPWD = item.InputPWD.Enc(KeyData.SubKEY + item.UID); // 사용 비밀번호는 **** 마스킹
                }

                string content2 = bak.Serialize();
                if (File.Exists(StartForm.FilePath + ".bak"))
                {
                    File.Delete(StartForm.FilePath + ".bak");
                }
                File.WriteAllText(StartForm.FilePath + ".bak", content2, Encoding.UTF8);
            }
        }

        private void NewInDataEditor_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            // 뷰!
            InDataEditControl inDataEditor = sender as InDataEditControl;
            if (inDataEditor != null)
            {
                using (inDataDetailForm edit = new inDataDetailForm())
                {
                    edit.Shown += delegate {
                        this.Hide();
                    };
                    edit.FormClosed += delegate {
                        this.Show();
                    };
                    edit.IsNew = false;
                    edit.SetInData(inDataEditor.Data); // 새 사이트 만들때마다 특수문자를 섞어줌.
                    if (edit.ShowDialog() == DialogResult.OK) // 저장.
                    {
                        Save();
                        inDataEditor.SetInData(edit.Data);
                    }
                }
            }
        }

        private void NewInDataEditor_MouseDown(object sender, MouseEventArgs e)
        {
             SetItem( sender as InDataEditControl);
        }

        private void DataClear()
        { 
            for (int loop = flowLayoutPanel1.Controls.Count - 1; loop >= 0; loop--)
            {
                using (flowLayoutPanel1.Controls[loop])
                {
                    flowLayoutPanel1.Controls.RemoveAt(loop);
                }
            } 
        }


        private void SetItem(InDataEditControl item)
        {
            if (SelectedItem != null)
            {
                SelectedItem.ForeColor = ForeColor;
                SelectedItem.BackColor = NomalColor;
            }

            SelectedItem = item;

            if (SelectedItem != null)
            {
                SelectedItem.ForeColor = SystemColors.HighlightText;
                SelectedItem.BackColor = SelectColor;
            }
        }

        private void btnItemAdd_Click(object sender, EventArgs e)
        {
            // 새거!
            using (inDataDetailForm edit = new inDataDetailForm())
            {
                edit.Shown += delegate {
                    this.Hide();
                };
                edit.FormClosed += delegate {
                    this.Show();
                };
                edit.IsNew = true;
                edit.SetInData(new StartForm.InData() { SPECIAL = MainData.NewSpecialChars() }); // 새 사이트 만들때마다 특수문자를 섞어줌.
                if (edit.ShowDialog() == DialogResult.OK) // 저장.
                {
                    InData newData = edit.Data.Clone() as InData;
                    if (newData != null)
                    {
                        MainData.Datas.Add(newData); 
                        InDataEditControl newInDataEditor = new InDataEditControl();
                        newInDataEditor.SetInData(newData);
                        flowLayoutPanel1.Controls.Add(newInDataEditor);

                        newInDataEditor.MouseDown += NewInDataEditor_MouseDown;
                        newInDataEditor.MouseDoubleClick += NewInDataEditor_MouseDoubleClick;

                        SetItem(newInDataEditor);
                        Save();
                    }
                }
            }
        }

        private void btnItemDel_Click(object sender, EventArgs e)
        {
            foreach (InDataEditControl item in flowLayoutPanel1.Controls)
            {
                item.ShowCheckBox();
            }

            btnDelCancel.BringToFront();
            btnDelOK.BringToFront();
        }
         
        private void btnDelOK_Click(object sender, EventArgs e)
        {
            List<InDataEditControl> removeItems = new List<InDataEditControl>(); 
            foreach (InDataEditControl item in flowLayoutPanel1.Controls)
            {
                if (item.IsSelected)
                {
                    removeItems.Add(item);
                    if (SelectedItem == item) SetItem(null);
                }
            }
             
            for (int loop = removeItems.Count - 1; loop >= 0; loop--)
            {
                using (InDataEditControl item = removeItems[loop])
                {
                    MainData.Datas.Remove(item.Data);
                    flowLayoutPanel1.Controls.Remove(item);
                    item.MouseDown -= NewInDataEditor_MouseDown;
                    item.MouseDoubleClick -= NewInDataEditor_MouseDoubleClick;
                }
            }
            removeItems.Clear();
            Save();

            foreach (InDataEditControl item in flowLayoutPanel1.Controls)
            {
                item.HideCheckBox();
            } 
            btnDelCancel.SendToBack();
            btnDelOK.SendToBack();
        }

        private void btnDelCancel_Click(object sender, EventArgs e)
        {
            foreach (InDataEditControl item in flowLayoutPanel1.Controls)
            {
                item.HideCheckBox();
            }
            btnDelCancel.SendToBack();
            btnDelOK.SendToBack();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            // 검색어 
            if (e.KeyCode == Keys.Enter)
            {
                btnSearch.PerformClick();
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            // 검색!
            SetItem(null);

            string searchText = ("" + txtSearch.Text).Trim();

            foreach (InDataEditControl item in flowLayoutPanel1.Controls)
            {
                item.Visible = true;
                if (searchText != "" && !item.Data.SiteName.ToUpper().Contains( searchText.ToUpper()))
                {
                    item.Visible = false;
                }
            }
        }

        private void btnPWDChange_Click(object sender, EventArgs e)
        {
            // 프로그램 비밀번호 변경
            string newPWD = txtPWDChange.Text.Trim();
            if (0 < newPWD.Length )
            {
                if ("비밀번호를 바꿀까?".Confirm())
                {
                    string drvName = Path.GetPathRoot(StartForm.FilePath);

                    string serialNumber = UtilEx.GetDeviceSerialNumber(drvName);

                    MainData.KEY = newPWD;

                    if (File.Exists(StartForm.FilePath))
                    {
                        File.Delete(StartForm.FilePath);
                    }
                    string content = MainData.Serialize();
                    content = content.Enc(MainData.KEY + serialNumber);
                    File.AppendAllText(StartForm.FilePath, content, Encoding.UTF8);

                    Application.Restart();
                }
            }
            else
            {
                "새 비밀번호를 입력해야함.".Alert();
            }
        }
    }
}
