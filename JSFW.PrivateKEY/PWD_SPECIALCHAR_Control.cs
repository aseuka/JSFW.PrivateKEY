using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JSFW.PrivateKEY
{
    public partial class PWD_SPECIALCHAR_Control : UserControl
    {
        bool _isBinding = false;

        public event EventHandler CheckChanged = null;

        public PWD_SPECIALCHAR_Control()
        {
            InitializeComponent();
        }

        public void Set(int[] numbers)
        {
            try
            {
                _isBinding = true; 

                foreach (CheckBox chk in Controls)
                {
                    chk.FlatAppearance.CheckedBackColor = Color.Empty;
                    chk.Checked = false;
                }

                for (int idx = 0; idx < numbers.Length; idx++)
                {
                    int num = numbers[idx];
                    Control[] ctrls = Controls.Find("chk" + num, false) as Control[];
                    if (ctrls != null && 0 < ctrls.Length)
                    {
                        CheckBox chk = ctrls[0] as CheckBox;
                        if (chk != null)
                        {
                            chk.Checked = true;
                        }
                    }
                }
            }
            finally
            {
                _isBinding = false;
            }
        }

        public int[] Get()
        {
            List<int> nums = new List<int>();

            for (int idx = 0; idx < 16; idx++)
            {
                Control[] ctrls = Controls.Find("chk" + idx, false) as Control[];
                if (ctrls != null && 0 < ctrls.Length)
                {
                    CheckBox chk = ctrls[0] as CheckBox;
                    if (chk != null && chk.Checked == true)
                    {
                        nums.Add(idx);
                    }
                }
            }
            return nums.ToArray();
        }

        private void chk0_CheckedChanged(object sender, EventArgs e)
        {
            // 체크값 변경!
            if (_isBinding) return;

            if (CheckChanged != null) CheckChanged(this, e);
        }

        private void chk0_MouseHover(object sender, EventArgs e)
        {
            // ON
            foreach (CheckBox chk in Controls)
            {
                chk.FlatAppearance.CheckedBackColor = Color.Coral;
            }
            Refresh();
        }

        private void chk0_MouseLeave(object sender, EventArgs e)
        {
            // OFF
            foreach (CheckBox chk in Controls)
            {
                chk.FlatAppearance.CheckedBackColor = Color.Empty;
            }
            Refresh();
        }
    }
}
