using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Core.UI
{
    public partial class FormMsgStart : Form
    {
        public enum EStartMsgMode
        {
            NONE = -1,
            START_READY,
            STEP_STOPPING,
            STEP_STOP,
            ERROR_STOP,
            MAX,
        }

        private string[] m_Message = new string[(int)EStartMsgMode.MAX];
        private string m_MsgStartReady = "[ Start Ready ] Push Start or Stop button";
        private string m_MsgStepStop   = "[ Step Stop ] Push Start or Stop button";
        private string m_MsgErrorStop  = "[ Error Stop ] Push Start or Stop button";

        public FormMsgStart()
        {
            m_Message[(int)EStartMsgMode.START_READY]   = "[ Start Ready ] Push Start or Stop button";
            m_Message[(int)EStartMsgMode.STEP_STOPPING] = "[ Step Stopping ] Wait for Step Stop";
            m_Message[(int)EStartMsgMode.STEP_STOP]     = "[ Step Stop ] Push Stop button";
            m_Message[(int)EStartMsgMode.ERROR_STOP]    = "[ Error Stop ] Push Stop button";

            InitializeComponent();
        }

        private void FormMsgStart_Load(object sender, EventArgs e)
        {

        }

        public void SetMode(EStartMsgMode mode)
        {
            // Label_Title.Text = m_Message[(int)mode];

            btnStart.Visible = false;
            btnStop.Visible = false;
            btnReset.Visible = false;
            btnEMO.Visible = false;

            switch (mode)
            {
                case EStartMsgMode.START_READY:
                    btnStart.Visible = true;
                    btnStop.Visible = true;
                    break;

                case EStartMsgMode.STEP_STOPPING:
                    break;

                case EStartMsgMode.STEP_STOP:
                    btnStop.Visible = true;
                    break;

                case EStartMsgMode.ERROR_STOP:
                    btnStop.Visible = true;
                    break;
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
        }

        private void btnEMO_Click(object sender, EventArgs e)
        {
        }

        protected override void OnShown(EventArgs e)
        {
            //this.TopMost = true;
            //this.StartPosition = FormStartPosition.CenterParent;
            //Call the original OnShown.
            base.OnShown(e);
        }
    }
}
