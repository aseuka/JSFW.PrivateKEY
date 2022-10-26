using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JSFW.PrivateKEY
{
    static class Program
    {
        /// <summary>
        /// 해당 응용 프로그램의 주 진입점입니다.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //Application.Run(new TestForm());

            using (var login = new StartForm())
            {
                if (login.ShowDialog() == DialogResult.OK)
                {
                    if (login.IsDECComplite)
                    {
                        Application.Run(new MainForm( login.IsNEWFILE ));
                    }
                }
            }
        }
    }
}
