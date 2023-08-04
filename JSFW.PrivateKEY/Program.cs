using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using static JSFW.PrivateKEY.StartForm;

namespace JSFW.PrivateKEY
{
    static class Program
    {
        /// <summary>
        /// 해당 응용 프로그램의 주 진입점입니다.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new TestForm());

            string filePath = "";
            string selGUID = "";

            if (args != null) {
                if (0 < args.Length) filePath = args[0].Trim();
                if (1 < args.Length) selGUID = args[1].Trim();
            }

            using (var login = new StartForm(filePath))
            {
                if (login.ShowDialog() == DialogResult.OK)
                {
                    if (login.IsDECComplite)
                    {
                        string content = "";
                        content = MainForm.ReadKeyPwdContent(content);
                        StartForm.KeyData mData = content.DeSerialize<StartForm.KeyData>();
                        if (string.IsNullOrWhiteSpace(selGUID) == false
                             && mData.Datas.Any(d => d.GUID == selGUID))
                        {
                            InData selData = mData.Datas.Find(f => f.GUID == selGUID);
                            string newPWD = "";
                            if (selData.IsInputPWD)
                            {
                                newPWD = ("" + selData.InputPWD).Dec(KeyData.SubKEY + selData.UID).Trim();
                            }
                            else
                            {
                                newPWD = selData.CreatePrivateKey();
                            }

                            if (!string.IsNullOrWhiteSpace(newPWD))
                            {
                                Clipboard.SetText(newPWD.Trim());
                                "복사완료!".Alert();
                            }
                            else {
                                "비밀번호 복사 에러!".Alert();
                            }
                        }
                        else
                        {
                            Application.Run(new MainForm(login.IsNEWFILE));
                        }
                    }
                }
            }
        }
    }
}
