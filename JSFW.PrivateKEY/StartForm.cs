using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Management;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JSFW.PrivateKEY
{
    public partial class StartForm : Form
    {
        internal static string FilePath { get; set; } = null;
        internal static string PWD { get; private set; } = null; 
        internal bool IsDECComplite = false;

        int PWD_INPUT_Count = 2;

        internal bool IsNEWFILE { get { return chkIsNew.Checked; } set { chkIsNew.Checked = value; } }

        internal static bool isDebug = false;

        private string ctorArgs_FilePath = "";

        public StartForm() 
        {
            InitializeComponent(); 
            IsNEWFILE = false;

            txtPassword.PasswordChar = '*';
            SetDEBUG();
        }

        public StartForm(string filePath) : this()
        {
            ctorArgs_FilePath = filePath;
        }

        [Conditional("DEBUG")]
        private void SetDEBUG()
        {
            txtPassword.PasswordChar = '\0';
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            txtPassword.Focus();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            StartForm.PWD = "";
            StartForm.FilePath = "";

            IsDECComplite = false;
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        { 
            StartForm.PWD = "";
            StartForm.FilePath = "";

            string serialNumber = "";

            bool is복원 = false;
             
            try
            {
                if (Form.ModifierKeys == Keys.Control)
                {
                    is복원 = true;
                }

                if (string.IsNullOrEmpty(txtPassword.Text.Trim()))
                {
                    "비밀번호 입력 필수!".Alert();
                    return;
                }

                if (IsNEWFILE)
                {
                    using (SaveFileDialog sfd = new SaveFileDialog())
                    {
                        if (sfd.ShowDialog() == DialogResult.OK)
                        {
                            StartForm.FilePath = sfd.FileName;

                            string drvName = Path.GetPathRoot(sfd.FileName);

                            serialNumber = UtilEx.GetDeviceSerialNumber(drvName);

                            KeyData newFile = new KeyData() { KEY = txtPassword.Text.Trim() };

                            if (File.Exists(StartForm.FilePath))
                            {
                                File.Delete(StartForm.FilePath);
                            }

                            string content = newFile.Serialize();
                            content = content.Enc(newFile.KEY + serialNumber);
                            File.AppendAllText(sfd.FileName, content);
                        }
                        else
                        {
                            StartForm.FilePath = "";
                            return;
                        }
                    }
                }
                else
                {
                    Properties.Settings.Default.Reload();

                    if (string.IsNullOrWhiteSpace(ctorArgs_FilePath)
                        || File.Exists(ctorArgs_FilePath) == false)
                    {
                        using (OpenFileDialog ofd = new OpenFileDialog())
                        {
                            ofd.InitialDirectory = Properties.Settings.Default.InitialDirectory;
                            ofd.Multiselect = false;
                            ofd.Filter = ""; // 확장자를 뭘로 쓸까? .txt?
                            if (ofd.ShowDialog() == DialogResult.OK)
                            {
                                Properties.Settings.Default.InitialDirectory = Path.GetDirectoryName(ofd.FileName);
                                Properties.Settings.Default.Save();
                                StartForm.FilePath = ofd.FileName;
                            }
                            else
                            {
                                StartForm.FilePath = "";
                                return;
                            }
                        }
                    }
                    else
                    {
                        StartForm.FilePath = ctorArgs_FilePath;
                    }
                }
                
                if (!File.Exists(StartForm.FilePath))
                {
                    "선택한 파일이 없음!".Alert();
                    PWD_INPUT_Count--;
                    return;
                }
                 
                StartForm.PWD = txtPassword.Text.Trim();

                if (is복원 && MessageBox.Show("파일 복원하시겠습니까?", "주의!", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    //복원!!!
                    if (File.Exists($@"{StartForm.FilePath}.bak"))
                    {
                        string drvName = Path.GetPathRoot(StartForm.FilePath);
                        serialNumber = UtilEx.GetDeviceSerialNumber(drvName);

                        string 복원파일 = File.ReadAllText($@"{StartForm.FilePath}.bak", Encoding.UTF8);

                        KeyData 복원데이타 = 복원파일.DeSerialize<KeyData>();

                        if (복원데이타.KEY != StartForm.PWD)
                        {
                            MessageBox.Show($@"개인키 정보가 다릅니다.
* 파일저장소[드라이브]와 백업(.bak)파일의 제일 위의 <KEY>값을 개인키로 변경 후 복원 작업을 다시 해주십시오.");
                            return;
                        }
                        // 백업(.bak)파일을 읽어서 > 원본파일에 덮어씌움.
                        // 이때 백업(.bak)파일의 KEY데이타와 입력받은 KEY와 비교! 

                        // 만약: 복원이 제대로 안된다면? ( 안되는거지 뭐 ... )
                        //       KEY가 잘못된 경우!
                        
                        복원파일 = 복원파일.Enc(StartForm.PWD + serialNumber);
                        if (File.Exists(StartForm.FilePath))
                        {
                            File.Delete(StartForm.FilePath);
                        }
                        File.WriteAllText(StartForm.FilePath, 복원파일, Encoding.UTF8);

                        MessageBox.Show($@"복원하였습니다.
[반드시 백업(.bak)파일의 개인키를 삭제하여 주십시오.]");
                    }
                }

                IsDECComplite = true; // 암호화 해제!! 

                if (IsNEWFILE)
                {
                    KeyData.SubKEY = serialNumber;
                    DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    // 해제 후 => 원본의 PWD와 비교 후 같으면 성공! 
                    string content = "";
                    try
                    {
                        if (File.Exists(StartForm.FilePath))
                        {
                            string drvName = Path.GetPathRoot(StartForm.FilePath);
                            serialNumber = UtilEx.GetDeviceSerialNumber(drvName);
                            content = File.ReadAllText(StartForm.FilePath, Encoding.UTF8);
                             
                            // "{ "PWD":"***", "SNO":"***"} " 입력이면 파싱해서 각각 설정하여 기존파일을 읽을수 있게 수정. 
                            if (StartForm.PWD.Contains("PWD") && StartForm.PWD.Contains("SNO"))
                            {
                                HIDE_LOCK_INFO info = Newtonsoft.Json.JsonConvert.DeserializeObject<HIDE_LOCK_INFO>(StartForm.PWD);
                                StartForm.PWD = info.PWD;
                                serialNumber = info.SNO;
                            }
                            if (isDebug)
                            {
                                content = File.ReadAllText($@"{StartForm.FilePath}.bak", Encoding.UTF8);
                            }
                            else
                            {
                                content = content.Dec(StartForm.PWD + serialNumber);
                                //'     WD-WCC6Y2RTNT71':E:\jisong\Dokebi_Private   
                            }
                        }
                    }
                    catch (Exception exc)
                    {
                        "비밀번호 X(복호화에러)".Alert();
                        PWD_INPUT_Count--;
                        return;
                    }

                    if (string.IsNullOrEmpty(("" + content).Trim()))
                    {
                        "비밀번호 X(내용없음)".Alert();
                        PWD_INPUT_Count--;
                        return;
                    }

                    KeyData tmp = content.DeSerialize<KeyData>();
                    if (tmp.KEY == StartForm.PWD)
                    {
                        KeyData.SubKEY = serialNumber;
                        tmp = null;
                        DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else
                    {
                        "비밀번호 X(파일에러)".Alert();
                        PWD_INPUT_Count--;
                    }
                }
            }
            finally
            {
                if (PWD_INPUT_Count < 0)
                {
                    DialogResult = DialogResult.Cancel;
                    this.Close();
                }
            }
        } 

        public class KeyData : ICloneable
        {
            internal static readonly string SPECIAL = @"!@#$%^&*()";

            public string KEY { get; set; } = @"!@#ASDFWZ#!@#123"; // PWD
             
            public List<InData> Datas { get; set; }

            //internal static readonly string SubKEY = @"_서브키001"; 

            internal static string SubKEY { get; set; } = "";

            public KeyData()
            {
                Datas = new List<InData>();
            }

            internal string NewSpecialChars()
            {
                Random rnd = new Random();
                // 특수문자 섞기 
                List<int> diz = new List<int>();
                do
                {
                    int magicNumber = rnd.Next(100, 99999) % SPECIAL.Length;
                    if (diz.Contains(magicNumber)) continue;

                    diz.Add(magicNumber);
                } while (diz.Count < SPECIAL.Length);

                char[] result = new char[SPECIAL.Length];
                for (int idx = 0; idx < diz.Count; idx++)
                {
                    result[idx] = SPECIAL[diz[idx]];
                }
                return string.Join("", result);
            }

            public object Clone()
            {
                KeyData bak = new KeyData();
                bak.KEY = KEY;
                foreach (var item in Datas)
                {
                    bak.Datas.Add(item.Clone() as InData);
                }
                return bak;
            }
        }

        public class InData : ICloneable
        {
            /// <summary>
            /// 사이트 생성시 인위적으로 KeyData의 특수문자를 섞어서 할당해주고
            /// 이를 바탕으로 패스워드 생성시 특정 자리수를 채워준다.
            /// </summary>
            public string SPECIAL { get; set; } = "";

            readonly string SubKEY = "TempSubKEY";

            public string SiteName { get; set; }

            public string UID { get; set; }

            public string DESC { get; set; }

            public string GUID { get; set; }
             
            public List<int> SpecialNumberIndexs { get; set; }
            public string InputPWD { get; set; }
            public bool IsInputPWD { get; set; }

            /// <summary>
            /// 비밀번호 길이.
            /// </summary>
            public int LenPWD { get; set; } = 16;

            public InData()
            {
                SpecialNumberIndexs = new List<int>();
                GUID = "8745123069";
            }

            public string CreatePrivateKey()
            {
                string publicKey = StartForm.PWD + "_"+ GUID + "_" + SubKEY; 
                string pwd = Encrypt.EncSHA1(publicKey.Trim(), "^" + SiteName + "＠" + UID + "$");
                pwd = pwd.Replace("_", "").Replace("+", "").Replace("=", "").Replace("/", "").Replace("\\", "").Replace(" ", "").Trim();
                //https://www.ibm.com/support/knowledgecenter/ko/SSFTN5_8.5.0/com.ibm.wbpm.admin.doc/topics/rsec_characters.html
                //비밀번호 허용문자!
                if (pwd.StartsWith("."))
                {
                    pwd = pwd.TrimStart('.');
                }
                if (pwd.StartsWith("-"))
                {
                    pwd = pwd.TrimStart('-');
                }

                if (16 < pwd.Length) pwd = pwd.Substring(0, 16); // 전체 자리수!
                                                                 //+ 숫자[ 1자리 필수 ]

                string newPWD = ReplaceSpecialCharFromPWD(pwd);

                if (LenPWD < newPWD.Length) // 비밀번호를 지정된 숫자로 자르기. 기본 16자리 인데 G마켓은 15자리..
                {
                    newPWD = newPWD.Substring(0, LenPWD);
                }
                return newPWD;
            } 

            private string ReplaceSpecialCharFromPWD(string pwd = "")
            {
                // 아싸컴은 4~12자리 특수문자X 
                if (pwd == null) pwd = "";
                if (SPECIAL == null) SPECIAL = "";

                if ("".Equals(SPECIAL)) return pwd;
                // 16자리로 자른 후
                // 특수문자 위치값을 지정하여 특수문자로 교체.   
                List<char> newPwdList = new List<char>();
                for (int loop = 0; loop < pwd.Length; loop++)
                { 
                    if (SpecialNumberIndexs.Contains( loop ))
                    {
                        newPwdList.Add(SPECIAL[pwd[loop] % SPECIAL.Length]);
                    }
                    else
                    {
                        newPwdList.Add(pwd[loop]);
                    }
                }

                bool haveNumber = newPwdList.Any(p => char.IsNumber(p));  
                for (int loop = 0; haveNumber == false && loop < pwd.Length; loop++)
                {
                    if (!SpecialNumberIndexs.Contains(loop))
                    {
                        // 특수문자가 아닌 자리에 숫자로 교체!
                        int num = SpecialNumberIndexs.Sum();                        
                        newPwdList[loop] = (char)('0'+( num % 10));
                        break;
                    }                    
                } 
                return string.Join("", newPwdList.ToArray());
            }

            public object Clone()
            {
                InData newData = new InData();
                newData.GUID = GUID;
                newData.SiteName = SiteName;
                newData.UID = UID;
                newData.IsInputPWD = IsInputPWD;
                newData.InputPWD = InputPWD;
                newData.DESC = DESC;
                newData.SPECIAL = SPECIAL;
                newData.SpecialNumberIndexs.Clear();
                newData.SpecialNumberIndexs.AddRange( SpecialNumberIndexs.ToArray() );
                newData.LenPWD = LenPWD;
                return newData;
            }
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (string.IsNullOrEmpty(txtPassword.Text.Trim()) == false)
                {
                    btnOK.PerformClick();
                }
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }

    public class HIDE_LOCK_INFO { 
        public string @PWD { get; set; }
        public string @SNO { get; set; }
    }
}
