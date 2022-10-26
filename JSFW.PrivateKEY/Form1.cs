using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static JSFW.PrivateKEY.StartForm;
/*
프라이빗 키!

각 사이트별 가입 아이디와 패스워드를 관리.

특정 알고리즘을 생성하여 패스워드를 단방향으로 만들어주고, 이를 가입된 사이트에 패스워드로 이용하는 방법!

설정된 정보는 USB에 별도로 가지고 다니며, 이 내용은 또다른 암호화 로직으로 암호화. 파일내용에 접근할때만 암/복호화를 한다.

# 파일의 내용에 암/복호화 키는 따로 저장하지 않으며 프로그램 실행할때 입력받고 메모리에서 보관 하였다가 프로그램 종료시 소거된다.


*/

namespace JSFW.PrivateKEY
{
    public partial class TestForm : Form
    {
        public TestForm()
        {
            InitializeComponent();

            //TestFileEncDec(); 
            //TestSHA1EncDec();
            //TestSpecialCharDiz();
            TestInData();
        }

        private void TestInData()
        {
            InData data = new InData()
            {
                SiteName = "공유기",
                DESC = "우리집 공유기", 
                UID = "dokebi",
            };
            data.SpecialNumberIndexs.Add(2);
            data.SpecialNumberIndexs.Add(4);
            data.SpecialNumberIndexs.Add(5);

            //7i9QKgWdhcLDkZOeD3oe8uho9Q
            string newPWD = data.CreatePrivateKey();
             
            data.SPECIAL = KeyData.SPECIAL;


            //7i* Q^$WdhcLDkZOeD3oe8uho9Q
            string newPWD2 = data.CreatePrivateKey();

            inDataEditControl1.SetInData(data);

        }

        private void TestSpecialCharDiz()
        {
            Random rnd = new Random();
            // 특수문자 섞기
            string specialchar = @"!@#$%^&*()";
            List<int> diz = new List<int>();
            do
            {
                int magicNumber = rnd.Next(100, 99999) % specialchar.Length;
                if (diz.Contains( magicNumber)) continue;

                diz.Add(magicNumber); 
            } while (diz.Count < specialchar.Length);

            char[] result = new char[specialchar.Length];
            for (int idx = 0; idx < diz.Count; idx++)
            {
                result[idx] = specialchar[diz[idx]];
            }
            string ss = string.Join("", result);
        }

        private void TestSHA1EncDec()
        {
            // 단방향 암/복호화 
            // 사이트, 아이디 = 패스워드 ( 2차? ) + 난수값 을 조합하여  단방향으로 암호화 함.
            string siteInfo = "www.daum.net";
            string uid = "abcdefg123";
            string publicKey = "O123G";
            string pwd = Encrypt.EncSHA1(publicKey, "^" + siteInfo + "＠" + uid + "$");
            pwd = pwd.Replace("_", "").Replace("+", "").Replace("=", "").Replace(" ", "").Trim();
            //https://www.ibm.com/support/knowledgecenter/ko/SSFTN5_8.5.0/com.ibm.wbpm.admin.doc/topics/rsec_characters.html
            //비밀번호 허용문자!
            if (pwd.StartsWith(".")) {
                pwd = pwd.TrimStart('.');
            }
            if (pwd.StartsWith("-"))
            {
                pwd = pwd.TrimStart('-');
            }
            string newPWD = ReplaceSpecialCharFromPWD(pwd); 
        }
         
        private static string ReplaceSpecialCharFromPWD(string pwd = "")
        {
            // 아싸컴은 4~12자리 특수문자X

            // 16자리로 자르 후
            // 특수문자 위치값을 지정하여 특수문자로 교체. 
            string specialchar = @"!@#$%^&*()";
             
            List<char> newPwdList = new List<char>();
            for (int loop = 0; loop < pwd.Length; loop++)
            {
                if (loop == 2 || loop == 6 || loop == 9)
                {
                    newPwdList.Add(specialchar[pwd[loop] % specialchar.Length]);
                }
                else
                {
                    newPwdList.Add(pwd[loop]);
                }
            }
            return string.Join("", newPwdList.ToArray());
        }
         
        private void TestFileEncDec()
        {
            string fileName = @"C:\Users\aseuk\OneDrive\Documents\Visual Studio 2015\Projects\JSFW.PrivateKEY\JSFW.PrivateKEY\TestTargetFile.txt";

            if (File.Exists(fileName))
                File.Delete(fileName);

            List<Data> lst = new List<Data>();
            for (int loop = 0; loop < 10; loop++)
            {
                lst.Add(new PrivateKEY.TestForm.Data() { Name = "key_" + loop.ToString("D5") });
            }
            /*// 
            string sr = lst.Serialize();
            var et = Encrypt.AESEncrypt256(sr); 
            File.AppendAllText(fileName, et);
            //# 리스트 직렬화 : 문자열 
            //# 문자열 암호화 : 문자열 
            //# 문자열 저장 

            //# 문자열 읽기
            //# 문자열 복호화 : 문자열
            //# 문자열 역직렬화 : 리스트
            var rt = File.ReadAllText(fileName); 
            var dt = Encrypt.AESDecrypt256(rt);
            List<Data> rlst = new List<Data>(); 
            rlst.AddRange(dt.DeSerialize<List<Data>>());
            */

            string sr = lst.Serialize();
            var et = Encrypt.AESEncrypt256(sr);
            byte[] data = GZIP.Compress(Encoding.UTF8.GetBytes(et));
            File.AppendAllText(fileName, Convert.ToBase64String( data ), Encoding.UTF8);
            //# 리스트 직렬화 : 문자열 
            //# 문자열 암호화 : 문자열 
            //# 문자열 저장 

            //# 문자열 읽기
            //# 문자열 복호화 : 문자열
            //# 문자열 역직렬화 : 리스트
            var rt = File.ReadAllText(fileName, Encoding.UTF8);
            data = GZIP.Decompress(Convert.FromBase64String(rt));
            var sst = Encoding.UTF8.GetString(data);
            var dt = Encrypt.AESDecrypt256(sst);
            List<Data> rlst = new List<Data>();
            rlst.AddRange(dt.DeSerialize<List<Data>>());
        }
         
        public class Data
        {
            public string Name { get; set; }
             
        }
    }
}
