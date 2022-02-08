using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;
using Core.UI;

//#pragma warning disable CS0219

namespace Core
{
    static class Program
    {
        /// <summary>
        /// 해당 응용 프로그램의 주 진입점입니다.
        /// </summary>
        [STAThread]
        static void Main()
        {

            bool createdNew;

            Mutex dup = new Mutex(true, "ConnectedInsight Core", out createdNew);

            if (createdNew)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new CMainFrame());
                dup.ReleaseMutex();

            }
            else
            {
                MessageBox.Show("프로그램이 실행중입니다.");
                
            }
        }
    }
}
