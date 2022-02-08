using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

using Core.Layers;

using static Core.Layers.DEF_Common;
using static Core.Layers.DEF_Error;
using static Core.Layers.MDataManager;
using static Core.Layers.DEF_DataManager;

namespace Core.UI
{
    public partial class FormLogScreen : Form
    {
        private DateTime m_StartDate;
        private DateTime m_EndDate;

        private List<CAlarm> m_AlarmList;

        private int [] nProcessID;

        private int nPageNo;

        private DataTable datatable;

        ELogDisplayType LogType;

        const int ResultRowCount = 23;

        enum ELogDisplayType
        {
            ALARM,
            EVENT,
            LOGIN,
            DEVELOPER,
        }

        public FormLogScreen()
        {
            InitializeComponent();

            InitializeForm();

            DateStart.Value = DateTime.Today;
            DateEnd.Value = DateTime.Today;

            m_StartDate = DateTime.Today;
            m_EndDate = DateTime.Today;

            m_AlarmList = CMainFrame.mCore.m_DataManager.AlarmHistory;

            ComboType.Items.Add($"{ELogDisplayType.ALARM}");
            ComboType.Items.Add($"{ELogDisplayType.EVENT}");
            ComboType.Items.Add($"{ELogDisplayType.LOGIN}");

            // test 기간동안은 풀어놓자
            //if(CMainFrame.DataManager.LoginInfo.User.Type == ELoginType.MAKER)
            {
                ComboType.Items.Add($"{ELogDisplayType.DEVELOPER}");
            }
            ComboType.SelectedIndex = 0;

            InitGrid();
        }

        protected virtual void InitializeForm()
        {
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.DesktopLocation = new Point(DEF_UI.MAIN_POS_X, DEF_UI.MAIN_POS_Y);
            this.Size = new Size(DEF_UI.MAIN_SIZE_WIDTH, DEF_UI.MAIN_SIZE_HEIGHT);
            this.FormBorderStyle = FormBorderStyle.None;
        }

     
        private void InitGrid()
        {
            int nCol = 0, nRow = 0;

            
            GridViewCont.ReadOnly = true; // Cell Click 시 커서가 생성되지 않도록 
            GridViewCont.Height = (ResultRowCount+1) * 30;  
            GridViewCont.DefaultCellStyle.SelectionBackColor = Color.Transparent;
            GridViewCont.DefaultCellStyle.SelectionForeColor = Color.Black;

            // 스크롤 바가 생성되지 않도록 
            GridViewCont.ScrollBars = ScrollBars.None;

            // 마지막 행이 남아있지 않도록 (커밋되지 않은 행 삭제)
            GridViewCont.AllowUserToAddRows = false;

            // Header
            GridViewCont.EnableHeadersVisualStyles = false;

            // Row Header Hide...
            GridViewCont.RowHeadersVisible = false;
            GridViewCont.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;

            // Column Header Setting 
            GridViewCont.ColumnHeadersVisible = true;
            GridViewCont.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            GridViewCont.ColumnHeadersDefaultCellStyle.BackColor = Color.Wheat;
            GridViewCont.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            GridViewCont.ColumnHeadersDefaultCellStyle.Font = new Font("Tahoma", 9, FontStyle.Bold);
            GridViewCont.ColumnHeadersHeight = 30;
            GridViewCont.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;

            switch (LogType)
            {
                case ELogDisplayType.ALARM:
                    nCol = 9;
                    nRow = 1;

                    // Column,Row 개수
                    GridViewCont.ColumnCount = nCol;
                    GridViewCont.RowCount = nRow;

                    // Column 가로 크기설정
                    GridViewCont.Columns[0].Width = 40;   // No.
                    GridViewCont.Columns[1].Width = 115;  // Process Name
                    GridViewCont.Columns[2].Width = 115;  // Obj.Name
                    GridViewCont.Columns[3].Width = 60;   // Code
                    GridViewCont.Columns[4].Width = 60;   // Type
                    GridViewCont.Columns[5].Width = 160;  // 발생시간  
                    GridViewCont.Columns[6].Width = 160;  // 해제시간
                    GridViewCont.Columns[7].Width = 403;  // 내용
                    GridViewCont.Columns[8].Width = 55;   // pid
                    
                    // Text Display
                    GridViewCont.Columns[0].Name = "No";
                    GridViewCont.Columns[1].Name = "Pro.Name";
                    GridViewCont.Columns[2].Name = "Obj.Name";
                    GridViewCont.Columns[3].Name = "Code";
                    GridViewCont.Columns[4].Name = "Type";
                    GridViewCont.Columns[5].Name = "발생시간";  
                    GridViewCont.Columns[6].Name = "해제시간";
                    GridViewCont.Columns[7].Name = "내용";
                    GridViewCont.Columns[8].Name = "P.ID";

                    for (int i = 0; i < nCol; i++)
                    {
                        GridViewCont.Columns[i].Resizable = DataGridViewTriState.False;
                        GridViewCont.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                    }
                    break;

                case ELogDisplayType.EVENT:
                case ELogDisplayType.DEVELOPER:
                    nCol = 7;
                    nRow = 1;

                    // Column,Row 개수
                    GridViewCont.ColumnCount = nCol;
                    GridViewCont.RowCount = nRow;

                    // Column 가로 크기설정
                    GridViewCont.Columns[0].Width = 60;   // No.
                    GridViewCont.Columns[1].Width = 160;  // Time
                    GridViewCont.Columns[2].Width = 140;  // Name
                    GridViewCont.Columns[3].Width = 75;   // Type
                    GridViewCont.Columns[4].Width = 518;  // Comment
                    GridViewCont.Columns[5].Width = 140;  // File  
                    GridViewCont.Columns[6].Width = 75;   // Line

                    // Text Display
                    GridViewCont.Columns[0].Name = "No";
                    GridViewCont.Columns[1].Name = "Time";
                    GridViewCont.Columns[2].Name = "Name";
                    GridViewCont.Columns[3].Name = "Type";
                    GridViewCont.Columns[4].Name = "Comment";
                    GridViewCont.Columns[5].Name = "File";
                    GridViewCont.Columns[6].Name = "Line";

                    for (int i = 0; i < nCol; i++)
                    {
                        GridViewCont.Columns[i].Resizable = DataGridViewTriState.False;
                        GridViewCont.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                    }
                    break;

                case ELogDisplayType.LOGIN:
                    nCol = 6;
                    nRow = 1;

                    // Column,Row 개수
                    GridViewCont.ColumnCount = nCol;
                    GridViewCont.RowCount = nRow;

                    // Column 가로 크기설정
                    GridViewCont.Columns[0].Width = 60;   // No.
                    GridViewCont.Columns[1].Width = 160;  // access Time
                    GridViewCont.Columns[2].Width = 160;  // access Type
                    GridViewCont.Columns[3].Width = 160;   // name
                    GridViewCont.Columns[4].Width = 160;  // Comment
                    GridViewCont.Columns[5].Width = 160;  // group  

                    GridViewCont.Columns[0].Name = "No";
                    GridViewCont.Columns[1].Name = "Access Time";
                    GridViewCont.Columns[2].Name = "Access Type";
                    GridViewCont.Columns[3].Name = "Name";
                    GridViewCont.Columns[4].Name = "Comment";
                    GridViewCont.Columns[5].Name = "Group";

                    for (int i = 0; i < nCol; i++)
                    {
                        GridViewCont.Columns[i].Resizable = DataGridViewTriState.False;
                        GridViewCont.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                    }
                    break;
            }     

            GridViewCont.DefaultCellStyle.Font = new Font("Tahoma", 9, FontStyle.Regular);


            for (int i = 0; i < nCol + 1; i++)
            {
                if (i == 6)
                {
                    GridViewCont.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                }
                else
                {
                    GridViewCont.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }                
            }


            GridViewCont.Rows.Clear();
        }
     
        private void UpdataScreen()
        {
            InitGrid();
            UpdataEvent();
        }

        private void UpdataEvent()
        {
            // 뭔지 모르겠으나 Query 문 검색 스타트 날자 조건이 걸리지 않아 강제로 하루를 늦춤
            DateTime time = m_StartDate.AddDays(-1);
            m_StartDate = time;

            if (LogType == ELogDisplayType.ALARM)
            {
                string query = $"SELECT * FROM {CMainFrame.mCore.m_DataManager.DBInfo.TableAlarmHistory} WHERE OccurTime BETWEEN '{m_StartDate.Date}' AND '{m_EndDate.Date}' ORDER BY OccurTime DESC";

                if (DBManager.GetTable(CMainFrame.mCore.m_DataManager.DBInfo.DBConn_ELog, query, out datatable) != true)
                {
                    CMainFrame.DisplayMsg("Failed to query database");
                    return;
                }
            }
            else if (LogType == ELogDisplayType.EVENT)
            {
                string query = $"SELECT * FROM {CMainFrame.mCore.m_DataManager.DBInfo.TableEventLog} WHERE Time BETWEEN '{m_StartDate.Date}' AND '{m_EndDate.Date}' ORDER BY Time DESC";

                if (DBManager.GetTable(CMainFrame.mCore.m_DataManager.DBInfo.DBConn_ELog, query, out datatable) != true)
                {
                    CMainFrame.DisplayMsg("Failed to query database");
                    return;
                }
            }
            else if (LogType == ELogDisplayType.LOGIN)
            {
                string query = $"SELECT * FROM {CMainFrame.mCore.m_DataManager.DBInfo.TableLoginHistory} WHERE AccessTime BETWEEN '{m_StartDate.Date}' AND '{m_EndDate.Date}' ORDER BY AccessTime DESC";

                if (DBManager.GetTable(CMainFrame.mCore.m_DataManager.DBInfo.DBConn_ELog, query, out datatable) != true)
                {
                    CMainFrame.DisplayMsg("Failed to query database");
                    return;
                }
            }
            else if (LogType == ELogDisplayType.DEVELOPER)
            {
                string query = $"SELECT * FROM {CMainFrame.mCore.m_DataManager.DBInfo.TableDebugLog} WHERE Time BETWEEN '{m_StartDate.Date}' AND '{m_EndDate.Date}' ORDER BY Time DESC";

                if (DBManager.GetTable(CMainFrame.mCore.m_DataManager.DBInfo.DBConn_DLog, query, out datatable) != true)
                {
                    CMainFrame.DisplayMsg("Failed to query database");
                    return;
                }
            }

            nPageNo = 0;
            DisplayGrid();
        }
        
        private void DisplayGrid()
        {
            if (datatable == null) return;
            int nEventCount = 0;
            int nTotalPage;


            GridViewCont.RowCount = ResultRowCount; // 출력할 Row 의 개수 
            GridViewCont.Refresh();

            nTotalPage = datatable.Rows.Count / GridViewCont.RowCount;

            // 현재 Page Num 화면에 출력
            pageCount.Text = string.Format("{0} / {1}", nPageNo+1, nTotalPage+1);

            // Row 개수 정리 
            int currentPageRowCount = datatable.Rows.Count - (nPageNo * GridViewCont.RowCount);


            
            // GridVIew 컨트롤의 높이 설정 
            if ( nPageNo == nTotalPage) // 마지막 페이지
            {
                GridViewCont.Height = (currentPageRowCount + 1) * 30; // Column Header 까지 포함해서 +1
            }
            else
            {
                GridViewCont.Height = (ResultRowCount + 1) * 30; // Column Header 까지 포함해서 +1
            }

            


            for (int i=0;i< ResultRowCount; i++)
            {
                int nIndex = i + (nPageNo * ResultRowCount);

                // Datatable 에 값이 없는데 Row 가 있을 경우 for 문 계속 돌면서 빈 Row 를 지운다 
                if (nIndex >= datatable.Rows.Count)
                {
                    GridViewCont.Rows.Remove(GridViewCont.Rows[currentPageRowCount]); // 비어있는 셀 중 가장 낮은 인덱스 번호로 삭제 
                    continue;
                }
                else
                {
                    GridViewCont.Rows[i].Height = 30;
                }
                


                // 생성되는 Row 의 크기 조절 방지 
                GridViewCont.Rows[i].Resizable = DataGridViewTriState.False;

                // Row 의 첫 열 배경 색과 Font 스타일 변경 (번호 열)
                GridViewCont.Rows[i].Cells[0].Style.BackColor = Color.Wheat;
                GridViewCont.Rows[i].Cells[0].Style.ForeColor = Color.Black;
                GridViewCont.Rows[i].Cells[0].Style.Font = new Font("Tahoma", 9, FontStyle.Bold);


                if (LogType == ELogDisplayType.DEVELOPER || LogType == ELogDisplayType.EVENT)
                {
                    GridViewCont.Rows[i].Cells[0].Value = (nIndex+1).ToString();
                    GridViewCont.Rows[i].Cells[1].Value = datatable.Rows[nIndex]["Time"].ToString();
                    GridViewCont.Rows[i].Cells[2].Value = datatable.Rows[nIndex]["Name"].ToString();
                    GridViewCont.Rows[i].Cells[3].Value = datatable.Rows[nIndex]["Type"].ToString();
                    GridViewCont.Rows[i].Cells[4].Value = datatable.Rows[nIndex]["Comment"].ToString();
                    GridViewCont.Rows[i].Cells[5].Value = datatable.Rows[nIndex]["File"].ToString();
                    GridViewCont.Rows[i].Cells[6].Value = datatable.Rows[nIndex]["Line"].ToString();

                    GridViewCont[1,i].Style.BackColor = Color.FromArgb(230, 210, 255);
                    GridViewCont[2,i].Style.BackColor = Color.FromArgb(230, 210, 255);
                    GridViewCont[3,i].Style.BackColor = Color.FromArgb(230, 210, 255);
                    GridViewCont[4,i].Style.BackColor = Color.White;
                    GridViewCont[5,i].Style.BackColor = Color.FromArgb(255, 230, 255);
                    GridViewCont[6,i].Style.BackColor = Color.FromArgb(255, 230, 255);
                }
                else if (LogType == ELogDisplayType.LOGIN)
                {
                    GridViewCont.Rows[i].Cells[0].Value = (nIndex + 1).ToString();
                    GridViewCont.Rows[i].Cells[1].Value = datatable.Rows[nIndex]["accesstime"].ToString();
                    GridViewCont.Rows[i].Cells[2].Value = datatable.Rows[nIndex]["accesstype"].ToString();
                    GridViewCont.Rows[i].Cells[3].Value = datatable.Rows[nIndex]["name"].ToString();
                    GridViewCont.Rows[i].Cells[4].Value = datatable.Rows[nIndex]["Comment"].ToString();
                    GridViewCont.Rows[i].Cells[5].Value = datatable.Rows[nIndex]["type"].ToString();

                    GridViewCont[1, i].Style.BackColor = Color.FromArgb(230, 210, 255);
                    GridViewCont[2, i].Style.BackColor = Color.FromArgb(230, 210, 255);
                    GridViewCont[3, i].Style.BackColor = Color.FromArgb(230, 210, 255);
                    GridViewCont[4, i].Style.BackColor = Color.White;
                    GridViewCont[5, i].Style.BackColor = Color.FromArgb(255, 230, 255);
                }
                else if (LogType == ELogDisplayType.ALARM)
                {
                    CAlarm alarm = JsonConvert.DeserializeObject<CAlarm>(datatable.Rows[nIndex]["data"].ToString());

                    GridViewCont.Rows[i].Cells[0].Value = (nIndex + 1).ToString();
                    GridViewCont.Rows[i].Cells[1].Value = alarm.ProcessName;
                    GridViewCont.Rows[i].Cells[2].Value = alarm.ObjectName;
                    GridViewCont.Rows[i].Cells[3].Value = String.Format("{0}", alarm.Info.Index);
                    GridViewCont.Rows[i].Cells[4].Value = alarm.Info.Type.ToString();
                    GridViewCont.Rows[i].Cells[5].Value = alarm.OccurTime.ToString();
                    GridViewCont.Rows[i].Cells[6].Value = alarm.ResetTime.ToString();
                    GridViewCont.Rows[i].Cells[7].Value = alarm.Info.Description[(int)ELanguage.ENGLISH];
                    GridViewCont.Rows[i].Cells[8].Value = String.Format("{0}", alarm.ProcessID);
                    
                    GridViewCont[1, i].Style.BackColor = Color.FromArgb(230, 210, 255);
                    GridViewCont[2, i].Style.BackColor = Color.FromArgb(230, 210, 255);
                    GridViewCont[3, i].Style.BackColor = Color.FromArgb(230, 210, 255);
                    GridViewCont[4, i].Style.BackColor = Color.FromArgb(230, 210, 255);
                    GridViewCont[5, i].Style.BackColor = Color.FromArgb(255, 230, 255);
                    GridViewCont[6, i].Style.BackColor = Color.FromArgb(255, 230, 255);
                }

            }

            LabelCount.Text = Convert.ToString(datatable.Rows.Count);
            GridViewCont.ClearSelection();
        }
        

        private void FormLogScreen_Activated(object sender, EventArgs e)
        {

        }

        private void BtnSerch_Click(object sender, EventArgs e)
        {
            UpdataScreen();
        }

        private void DateStart_ValueChanged(object sender, EventArgs e)
        {
            m_StartDate = DateStart.Value.Date;
        }

        private void DateEnd_ValueChanged(object sender, EventArgs e)
        {
            m_EndDate = DateEnd.Value.Date;
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            LabelCount.Text = "";
        }

        private void BtnExport_Click(object sender, EventArgs e)
        {
            int iRow;
            int iCol;
            String strBuf, strFileName = $"_{m_StartDate.Year}{m_StartDate.Month}{m_StartDate.Day}_{m_EndDate.Year}{m_EndDate.Month}{m_EndDate.Day}";

            if (datatable == null) return;

            switch (LogType)
            {
                case ELogDisplayType.ALARM:
                    strFileName = "Alarm" + strFileName;
                    break;

                case ELogDisplayType.EVENT:
                    strFileName = "Event" + strFileName;
                    break;

                case ELogDisplayType.LOGIN:
                    strFileName = "Login" + strFileName;
                    break;

                case ELogDisplayType.DEVELOPER:
                default:
                    strFileName = "Dev" + strFileName;
                    break;
            }

            //  저장할 경로 확인...
            SaveFileDialog savefile = new SaveFileDialog();

            savefile.CreatePrompt = true;       //존재하지 않는 파일을 지정할 때 파일을 새로 만들 것인지 대화 상자에 표시되면 true이고 ,대화 상자에서 자동으로 새 파일을 만들면 false. 
            savefile.OverwritePrompt = true;    //이미 존재하는 파일 이름을 지정할 때 Save As 대화 상자에 경고가 표시되는지 여부를 나타내는 값을 가져오거나 설정. 
            savefile.FileName = strFileName;
            savefile.DefaultExt = "csv";
            savefile.Filter = "Excel files (*.csv)|*.csv";
            // savefile.InitialDirectory

            DialogResult result = savefile.ShowDialog();
            if (result != DialogResult.OK) return;

            try
            {
                StreamWriter sw = new StreamWriter(savefile.FileName, true, Encoding.GetEncoding("ks_c_5601-1987"));

                if (LogType == ELogDisplayType.DEVELOPER || LogType == ELogDisplayType.EVENT)
                {
                    // 첫줄 타이틀
                    strBuf = "Time," + "Name," + "Type," + "Comment," + "File," + "Line,";
                    sw.WriteLine(strBuf);

                    for (iRow = 0 ; iRow < datatable.Rows.Count ; iRow++)
                    {
                        strBuf = "";
                        strBuf = strBuf + Convert.ToString(datatable.Rows[iRow]["Time"].ToString()) + " ,";
                        strBuf = strBuf + Convert.ToString(datatable.Rows[iRow]["Name"].ToString()) + " ,";
                        strBuf = strBuf + Convert.ToString(datatable.Rows[iRow]["Type"].ToString()) + " ,";
                        strBuf = strBuf + Convert.ToString(datatable.Rows[iRow]["Comment"].ToString()).Replace(",", ".") + " ,";
                        strBuf = strBuf + Convert.ToString(datatable.Rows[iRow]["File"].ToString()) + " ,";
                        strBuf = strBuf + Convert.ToString(datatable.Rows[iRow]["Line"].ToString()) + " ,";

                        sw.WriteLine(strBuf);
                    }
                }

                if (LogType == ELogDisplayType.LOGIN)
                {
                    // 첫줄 타이틀
                    strBuf = "Access Time," + "Access Type," + "Name," + "Comment," + "Group,";
                    sw.WriteLine(strBuf);

                    for (iRow = 0; iRow < datatable.Rows.Count; iRow++)
                    {
                        strBuf = "";
                        strBuf = strBuf + Convert.ToString(datatable.Rows[iRow]["accesstime"].ToString()) + " ,";
                        strBuf = strBuf + Convert.ToString(datatable.Rows[iRow]["accesstype"].ToString()) + " ,";
                        strBuf = strBuf + Convert.ToString(datatable.Rows[iRow]["name"].ToString()) + " ,";
                        strBuf = strBuf + Convert.ToString(datatable.Rows[iRow]["Comment"].ToString()).Replace(",", ".") + " ,";
                        strBuf = strBuf + Convert.ToString(datatable.Rows[iRow]["type"].ToString()) + " ,";

                        sw.WriteLine(strBuf);
                    }
                }

                if (LogType == ELogDisplayType.ALARM)
                {
                    // 첫줄 타이틀
                    strBuf = "Pro.Name," + "Obj.Name," + "Code," + "Type," + "발생시간," + "해제시간," + "내용," + "Process ID,";
                    sw.WriteLine(strBuf);

                    for (iRow = 0; iRow < datatable.Rows.Count; iRow++)
                    {
                        CAlarm alarm = JsonConvert.DeserializeObject<CAlarm>(datatable.Rows[iRow]["data"].ToString());

                        strBuf = "";
                        strBuf = strBuf + Convert.ToString(alarm.ProcessName) + " ,";
                        strBuf = strBuf + Convert.ToString(alarm.ObjectName) + " ,";
                        strBuf = strBuf + Convert.ToString(alarm.Info.Index) + " ,";
                        strBuf = strBuf + Convert.ToString(alarm.Info.Type) + " ,";
                        strBuf = strBuf + Convert.ToString(alarm.OccurTime) + " ,";
                        strBuf = strBuf + Convert.ToString(alarm.ResetTime) + " ,";
                        //strBuf = strBuf + Convert.ToString(alarm.Info.Description[(int)CMainFrame.mCore.m_DataManager.SystemData.Language]).Replace(",", ".") + " ,";
                        strBuf = strBuf + Convert.ToString(alarm.Info.Description[(int)ELanguage.ENGLISH]).Replace(",", ".") + " ,";
                        strBuf = strBuf + Convert.ToString(alarm.ProcessID) + " ,";

                        sw.WriteLine(strBuf);
                    }
                }

                sw.Close();
                sw.Dispose();
            }
            catch (Exception)
            {
            }
        }
      

        private void BtnPageTop_Click(object sender, EventArgs e)
        {
            nPageNo = 0;
           DisplayGrid();
        }

        private void BtnPageUp_Click(object sender, EventArgs e)
        {
            nPageNo--;

            if (nPageNo < 0)
            {
                nPageNo = 0;
                return;
            }

            DisplayGrid();
        }

        private void BtnPageDown_Click(object sender, EventArgs e)
        {
            if (datatable == null) return;
            int nTotalPage = datatable.Rows.Count / ResultRowCount;
            if (datatable.Rows.Count % ResultRowCount > 0) nTotalPage++;

            if(nPageNo + 1 >= nTotalPage)
            {
                return;
            }

            nPageNo++;
            DisplayGrid();
        }

        private void BtnPageBot_Click(object sender, EventArgs e)
        {
            if (datatable == null) return;
            int nTotalPage = datatable.Rows.Count / ResultRowCount;
            if (datatable.Rows.Count % ResultRowCount > 0) nTotalPage++;
            nPageNo = nTotalPage - 1;

            DisplayGrid();
        }

        private void ComboType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ComboType.SelectedIndex == 0) LogType = ELogDisplayType.ALARM;
            if (ComboType.SelectedIndex == 1) LogType = ELogDisplayType.EVENT;
            if (ComboType.SelectedIndex == 2) LogType = ELogDisplayType.LOGIN;
            if (ComboType.SelectedIndex == 3) LogType = ELogDisplayType.DEVELOPER;

            bool bVisible = true;

            BtnPageTop.Visible = bVisible;
            BtnPageUp.Visible = bVisible;
            BtnPageDown.Visible = bVisible;
            BtnPageBot.Visible = bVisible;

            datatable = null;
        }
    }
}
