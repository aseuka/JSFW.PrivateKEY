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
using System.IO;

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

        private void btnLink_Click(object sender, EventArgs e)
        {
            // 바로가기 만들기!!
            CreateDesckTopShortcut(Data.GUID, Data.SiteName);
        }


        private void CreateDesckTopShortcut(string guid, string siteName )
        {
            if (string.IsNullOrEmpty(("" + guid).Trim()) == false)
            {
                if (string.IsNullOrWhiteSpace(siteName)) {
                    siteName = "사이트명";
                }

                /*
                   바로가기 만들기!
                   [참조] - C:\Windows\System32\Shell32.dll [추가]
                */
                string MMPS_Application_FolderPath = Application.ExecutablePath;
                string MMPS_DiskTop_Path = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                string Application_ShortCut_Name = siteName;
                Shell32.Shell ShellClass_iNstance = new Shell32.Shell();
                if (File.Exists(MMPS_DiskTop_Path + @"\" + Application_ShortCut_Name + ".lnk"))
                    File.Delete(MMPS_DiskTop_Path + @"\" + Application_ShortCut_Name + ".lnk");

                using (System.IO.StreamWriter StreamWriter_iNstance = new System.IO.StreamWriter(MMPS_DiskTop_Path + @"\" + Application_ShortCut_Name + ".lnk", false))
                {
                    StreamWriter_iNstance.Close();
                    Shell32.Folder DeskTop_Folder = ShellClass_iNstance.NameSpace(MMPS_DiskTop_Path);
                    Shell32.FolderItem DeskTop_FolderiTem = DeskTop_Folder.Items().Item(Application_ShortCut_Name + ".lnk");
                    Shell32.ShellLinkObject ShortCut_Link = (Shell32.ShellLinkObject)DeskTop_FolderiTem.GetLink;
                    ShortCut_Link.Path = MMPS_Application_FolderPath;
                    ShortCut_Link.Arguments = @"""""" + StartForm.FilePath.Trim() + @""""" " + @"""""" + guid.Trim() + @"""""";
                    ShortCut_Link.Description = Application_ShortCut_Name;
                    ShortCut_Link.SetIconLocation(@"D:\.net source\JSFW\JSFW.PrivateKEY\Resources\pwd.ico", 0);
                    ShortCut_Link.WorkingDirectory = Environment.CurrentDirectory + "\\";
                    ShortCut_Link.Save();
                }
            }
        }
    }
}
