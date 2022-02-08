// define구문은 실제로는 프로젝트 속성에서 정의해야 함

// PRE_DEFINE for Project setting
///////////////////////////////////////////////////////////////////////////////////
// 
//                    Camera, Scanner 축은 YMC, Stage 축은 ACS 로 구성
// WIN32,  SIM_VISION,  



// OP_HW_BUTTON : 물리적으로 Op Panel Button (start, stop, reset 등의 s/w가 있는지 여부)

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Diagnostics;

namespace Core.Layers
{
    public class DEF_System
    {

        // SYSTEM_VER 및 개인의 작업 내용에 대한 History 관리는 History.txt 에 기록합니다.
        public const string SYSTEM_VER = "Ver 0.1.0 17-12-23";
        


        public enum EVisionMode
        {
            CALIBRATION,
            MEASUREMENT,
        }
                


        public enum ELightController
        {
            No1 = 0,

            MAX,
        }
        public enum ELightChannel
        {
            CH1=0,
            CH2,
            CH3,
            CH4,
            MAX,
        }


        // Value Define 
        public const int DEF_MAX_OBJECT_NO = 256;
        public const int DEF_MAX_OBJECT_INFO_NO = 100;
        public const int DEF_MAX_OBJ_KINDS = 100;
        public const int DEF_MAX_OBJ_NAME_LENGTH = 64;
        public const int DEF_MAX_OBJ_LOG_PATH_NAME_LENGTH = 256;

        // Value Define 
        public const int DEF_MAX_SYSTEM_CAMERA_NO = 2;
        public const int DEF_MAX_SYSTEM_AXIS_NO = 24;
        public const int DEF_MAX_SYSTEM_LOG_ITEM = 200;



        public enum EObjectLayer
        {
            // Common Class
            OBJ_NONE = -1,
            OBJ_SYSTEM = 0,
            OBJ_DATAMANAGER,

            // Hardware Layer
            OBJ_HL_IO = 10,
            OBJ_HL_SERIAL,
            OBJ_HL_ETHERNET,
            OBJ_HL_MELSEC,
            OBJ_HL_RFID,
            OBJ_HL_BARCODE,
            OBJ_HL_MOTION_LIB,
            OBJ_HL_VISION,

            // Function Layer
            OBJ_ML_OP_PANEL = 40,
            OBJ_ML_LIGHTENING,
            OBJ_ML_ONLINE,
            OBJ_ML_STAGE,
            OBJ_ML_VISION,

            // Process Layer
            OBJ_PL_TRS_AUTO_MANAGER = 100,
            OBJ_PL_TRS_STAGE1,
            OBJ_PL_TRS_JOG,
        }

        public enum EPositionObject // 좌표셋을 저장할 수 있는 단위
        {
            ALL = -1,

            // Stage
            STAGE1,
            CAMERA1,
            SCANNER1,
            MAX,
        }
            
    }

    public class DEF_Common
    {
        public const int SUCCESS  = 0;
        public const int RUN_FAIL = 1;
        public const string MSG_UNDEFINED = "undefined";

        //
        public const int WhileSleepTime         = 10; // while interval time
        public const int UITimerInterval        = 100; // ui timer interval
        public const int SimulationSleepTime    = 100; // simulation sleep time for move

        //
        public const int TRUE                   = 1;
        public const int FALSE                  = 0;

        //
        public const int BIT_ON                 = 1;
        public const int BIT_OFF                = 0;

        //
        public const bool NOT_USE               = false;
        public const bool SET_USE               = true;

        public const int JOG_KEY_X = 0;
        public const int JOG_KEY_Y = 1;
        public const int JOG_KEY_T = 2;
        public const int JOG_KEY_Z = 3;

        public const int JOG_KEY_NON = 0;
        public const int JOG_KEY_POS = 1;
        public const int JOG_KEY_NEG = 2;
        public const int JOG_KEY_ALL = 3;

        // TimeType
        public enum ETimeType
        {
            NANOSECOND,
            MICROSECOND,
            MILLISECOND,
            SECOND,
            MINUTE,
            HOUR,
        }

        // Language
        public enum ELanguage
        {
            NONE = -1,
            KOREAN = 0,
            ENGLISH,
            CHINESE,
            JAPANESE,
            MAX,
        }

        // Login
        public enum ELoginType
        {
            OPERATOR = 0,
            ENGINEER,
            MAKER,
        }


        public class CUserInfo
        {
            public string Name          = "default";              // unique primary key
            public string Comment       = "default";
            public string Password      = "";
            public ELoginType Type      = ELoginType.OPERATOR;    // 필수

            public CUserInfo()
            { }

            public CUserInfo(string Name, string Comment, string Password, ELoginType Type = ELoginType.OPERATOR)
            {
                this.Name       = Name;
                this.Comment    = Comment;
                this.Password   = Password;
                this.Type       = Type;
            }

            public override string ToString()
            {
                return $"[{Type}] {Name}, {Comment}";
            }

            public void SetMaker()
            {
                Name    = ELoginType.MAKER.ToString();
                Comment = ELoginType.MAKER.ToString();
                Type    = ELoginType.MAKER;
            }

            public string GetMakerPassword()
            {
                // if User is Maker, 
                string str = $"{(DateTime.Now.Day - 1).ToString("D2")}{(DateTime.Now.Month - 1).ToString("D2")}";
                return str;
            }
        }

        public class CLoginInfo
        {
            public CUserInfo User = new CUserInfo();
            public DateTime AccessTime = DateTime.Now;
            public bool AccessType = true; // true : login, false : logoff

            public CLoginInfo(CUserInfo User)
            {
                this.User = User;
            }

            public string GetAccessType()
            {
                string str = (AccessType == true) ? "login" : "logout";
                return str;
            }

            public override string ToString()
            {
                return $"{GetAccessType()} : {User}, {AccessTime}";
            }
        }

        //
        public const int ERR_MLOG_FILE_OPEN_ERROR        = 1;
        public const int ERR_MLOG_TOO_SHORT_KEEPING_DAYS = 2;

        public const byte DEF_MLOG_NONE_LOG_LEVEL        = 0x00;    // Log 안 함
        public const byte DEF_MLOG_ERROR_LOG_LEVEL       = 0x01;    // Error관련 Log
        public const byte DEF_MLOG_WARNING_LOG_LEVEL     = 0x02;    // Warning 관련 Log
        public const byte DEF_MLOG_NORMAL_LOG_LEVEL      = 0x04;    // 정상 동작 관련 Log
        public const byte DEF_MLOG_TACT_TIME_LOG_LEVEL   = 0x10;
        public const int LOG_ALL = DEF_MLOG_NONE_LOG_LEVEL | DEF_MLOG_ERROR_LOG_LEVEL | DEF_MLOG_WARNING_LOG_LEVEL | DEF_MLOG_NORMAL_LOG_LEVEL;
        //public const int LOG_ALL = DEF_MLOG_ERROR_LOG_LEVEL;

        public const int DEF_MLOG_DEFAULT_KEEPING_DAYS   = 30;
        public const int DEF_MLOG_NUM_FILES_TOBE_DELETED = 20;
        public const int DEF_MLOG_NUM_VIEW_DISPLAY_LOG   = 100;

        public const int LOG_DAY = DEF_MLOG_DEFAULT_KEEPING_DAYS;
        public const int DEF_SYSTEMINFO_MIN_LOG_KEEPING_DAYS = 5;
        public const int DEF_SYSTEMINFO_MAX_LOG_KEEPING_DAYS = 90;

        public enum ELogType
        {
            // Debug, Tact는 별도의 DB에 저장하고
            Debug = 0,
            Tact,

            // 이하의 DB는 ELog에 저장하는 형태로
            LOGINOUT,
            SECGEM,
            SYSTEM,
            RUN,
            //INI,
            //PON,
            //DMW,
        }

        public enum ELogWType
        {
            // 기본 Debug에서 쓰이는 3ea type
            // 소문자 Error는 Debug.Error이고, 대문자 ERROR는 자동운전중의 ERROR로 일단 구분해놓음
            D_Normal,
            D_Warning,
            D_Error,

            // 이하, 각 LogType에서 쓰이는 상세 type
            LOGIN,
            LOGOUT,
            SAVE,
            LOAD,
            FAIL,
            ALARM, //ERROR, // Error와의 혼동때문에 자동운전중의 ERROR -> ALARM 으로 변경
            START,
            COMPLETE,
        }



        public class CDBInfo
        {
            /////////////////////////////////////////////////////////////////////
            // Database
            private string DBDir             ; // DB Directory
            private string DBName            ; // Main System and Model Database
            private string DBName_Backup     ; // backup for main db
            private string DBName_Info       ; // Information DB

            private string DBDir_Log         ; // Log Directory
            private string DBName_DLog       ; // 개발자용 Log를 남기는 DB를 따로 만들어 둠.
            private string DBName_ELog       ; // Alarm, Login, Event 등의 History를 관리하는 DB

            /////////////////////////////////////////////////////////////////////
            // Database Connection
            public string DBConn            ; // DB Connection string
            public string DBConn_Backup     ; // DB Connection string for backup
            public string DBConn_Info       ; // DB Connection string for Information
            public string DBConn_DLog       ; // DB Connection string for DLog
            public string DBConn_ELog       ; // DB Connection string for ELog

            /////////////////////////////////////////////////////////////////////
            // Table
            public string TableSystem           { get; private set; } // System Data
            public string TableUserInfoHeader   { get; private set; } // User directory Header
            public string TableUserInfo         { get; private set; } // User

            public string TableModelHeader      { get; private set; } // Model and Parent directory Header
            public string TableModel            { get; private set; } // Model Data     
            public string TablePos              { get; private set; } // Position Data
            public string TableIO               { get; private set; } // IO Information
            public string TableAlarmInfo        { get; private set; } // Alarm Information
            public string TableMessageInfo      { get; private set; } // Message Information
            public string TableParameter        { get; private set; } // Parameter Description

            public string TableLoginHistory     { get; private set; } // Login History
            public string TableAlarmHistory     { get; private set; } // Alarm History
            public string TableDebugLog         { get; private set; } // 개발자용 Log
            public string TableEventLog         { get; private set; } // Event History

            /////////////////////////////////////////////////////////////////////
            // Common Directory
            public string SystemDir         { get; private set; } // System Data가 저장되는 디렉토리
            public string ModelDir          { get; private set; } // Model Data가 저장되는 디렉토리 
            public string ImageLogDir       { get; private set; } // Vision에서 모델에 관계없이 image file 저장할 필요가 있을때 사용
            public string ImageDataDir { get; private set; } // Vision에서 모델에 관계없이 image file 저장할 필요가 있을때 사용


            public CDBInfo()
            {
                // System and Model DB
                DBDir                   = ConfigurationManager.AppSettings["AppFilePath"]/* + @"\Data\"*/;
                DBName                  = "LWD_Data_v01.db3";
                DBName_Backup           = "LWD_DataB_v01.db3";
                DBConn                  = $"Data Source={DBDir}{DBName}";
                DBConn_Backup           = $"Data Source={DBDir}{DBName_Backup}";
                DBName_Info             = "LWD_Info_v01.db3";
                DBConn_Info             = $"Data Source={DBDir}{DBName_Info}";

                TableSystem             = "SystemDB";

                TableUserInfoHeader     = "UserInfoHeader";
                TableUserInfo           = "UserDB";

                TableModelHeader        = "ModelHeader";
                TableModel              = "ModelDB";
                TablePos                = "PositionDB";

                TableIO                 = "IO";
                TableAlarmInfo          = "AlarmInfo";
                TableMessageInfo        = "MessageInfo";
                TableParameter          = "Parameter";

                // Developer's and Event Log DB
                DBDir_Log               = ConfigurationManager.AppSettings["AppFilePath"]/* + @"\Log\"*/;
                DBName_DLog             = "LWD_DLog_v01.db3";
                DBConn_DLog             = $"Data Source={DBDir_Log}{DBName_DLog}";
                DBName_ELog             = "LWD_ELog_v01.db3";
                DBConn_ELog             = $"Data Source={DBDir_Log}{DBName_ELog}";

                TableLoginHistory       = "LoginHistory";
                TableAlarmHistory       = "AlarmHistory";
                TableDebugLog           = "DLog";
                TableEventLog           = "ELog";


                // Model Dir
                SystemDir       = ConfigurationManager.AppSettings["AppFilePath"] + @"SystemData\";
                ModelDir        = ConfigurationManager.AppSettings["AppFilePath"] + @"ModelData\";
                ImageLogDir     = ConfigurationManager.AppSettings["AppFilePath"] + @"ImageLog\";
                ImageDataDir    = ConfigurationManager.AppSettings["AppFilePath"] + @"ImageData\";

                System.IO.Directory.CreateDirectory(DBDir);
                System.IO.Directory.CreateDirectory(DBDir_Log);
                System.IO.Directory.CreateDirectory(SystemDir);
                System.IO.Directory.CreateDirectory(ModelDir);
                System.IO.Directory.CreateDirectory(ImageLogDir);
                System.IO.Directory.CreateDirectory(ImageDataDir);
            }

        }

        public class CObjectInfo
        {
            public int Type;
            public string TypeName;
            public int ID;
            public string Name;
            public int ErrorBase;
            
            // Log
            public string DebugTableName;
            public byte LogLevel;
            public int LogDays;

            static public CDBInfo DBInfo;

            public CObjectInfo()
            {
            }

            public CObjectInfo(int Type, string TypeName, int ID, string Name,
                int ErrorBase, string DebugTableName, byte LogLevel, int LogDays)
            {
                this.Type        = Type;
                this.TypeName    = TypeName;
                this.ID          = ID;
                this.Name        = Name;
                this.ErrorBase   = ErrorBase;
                this.DebugTableName = DebugTableName;
                this.LogLevel    = LogLevel;
                this.LogDays     = LogDays;
            }
        }

        public enum EUnitType
        {
            // Text
            text,

            // bool
            boolean,

            // Length
            km,
            m,
            cm,
            mm,
            um,
            nm,
            inch,

            // square
            m2,

            // Weight
            g,
            kg,
            lb, // pound

            // Speed
            m_sec,
            km_hour,
            inch_sec,
            rpm,
            rad_sec,    // radius / sec

            // Time
            year,
            month,
            week,
            day,
            hour,
            minute,
            sec,

            // Hz
            MHz,
            KHz,
            Hz,

            // Newton
            N,

        }

        /// <summary>
        /// System, Model 등의 class에서 쓰이는 Parameter에 대한 정보를 관리하는 class
        /// Parameter Name 중복 관리를 피하기 위해서 Group, Name 각각의 필드를 이용해서 관리
        /// </summary>
        public class CParaInfo
        {
            public string Group;    // ex) System, Model, Scanner, Laser, etc
            public string Name;     // ex) Password, ModelName, InScanResolution, etc
            public string Unit;     // Unit ex) km, km/s, m/s^2 
            public EUnitType Type;  // Unit Type을 지정해서 자동으로 텍스트로 환산 및 계산하려고 하지만, 너무 많아서 일단 자리만 잡아놓고 not use

            public string[] DisplayName = new string[(int)DEF_Common.ELanguage.MAX];
            public string[] Description = new string[(int)DEF_Common.ELanguage.MAX];

            public CParaInfo(string Group = "group", string Name = "parameter", string Unit = "[-]", EUnitType Type = EUnitType.mm)
            {
                this.Group = Group;
                this.Name  = Name;
                this.Unit  = Unit;
                this.Type  = Type; // temporarily 

                for (int i = 0; i < (int)ELanguage.MAX; i++)
                {
                    DisplayName[i] = MSG_UNDEFINED;
                    Description[i] = MSG_UNDEFINED;
                }
            }

            public bool IsEqual(CParaInfo info)
            {
                if (this.Group != info.Group) return false;
                if (this.Name  != info.Name ) return false;
                if (this.Unit  != info.Unit ) return false;
                if (this.Type  != info.Type ) return false;

                for (int i = 0; i < (int)ELanguage.MAX; i++)
                {
                    if (this.Description[i] != info.Description[i]) return false;
                    if (this.DisplayName[i] != info.DisplayName[i]) return false;
                }

                return true;
            }

            public string GetDisplayName(DEF_Common.ELanguage lang = DEF_Common.ELanguage.ENGLISH)
            {
                return DisplayName[(int)lang];
            }

            public string GetDescription(DEF_Common.ELanguage lang = DEF_Common.ELanguage.ENGLISH)
            {
                return Description[(int)lang];
            }

            public bool Update(CParaInfo info)
            {
                bool bUpdated = false;
                string str;
                for (int i = 0; i < (int)ELanguage.MAX; i++)
                {
                    str = info.Description[i];
                    if (str != MSG_UNDEFINED && string.IsNullOrWhiteSpace(str) == false
                        && Description[i] != str)
                    {
                        bUpdated = true;
                        Description[i] = str;
                    }

                    str = info.DisplayName[i];
                    if (str != MSG_UNDEFINED && string.IsNullOrWhiteSpace(str) == false
                        && DisplayName[i] != str)
                    {
                        bUpdated = true;
                        DisplayName[i] = str;
                    }
                }

                //if(bUpdated)
                {
                    Type = info.Type;
                    Unit = info.Unit;
                }
                return bUpdated;
            }
        }

        public enum EMessageType
        {
            NONE = -1,
            OK,
            OK_CANCEL,
            CONFIRM_CANCEL,
            MAX,
        }

        /// <summary>
        /// Message Information
        /// </summary>
        public class CMessageInfo
        {
            public int Index = -1;
            public EMessageType Type = EMessageType.OK;

            public string[] Message = new string[(int)DEF_Common.ELanguage.MAX];

            public CMessageInfo()
            {
                for (int i = 0; i < (int)ELanguage.MAX; i++)
                {
                    Message[i] = MSG_UNDEFINED;
                }
            }

            public bool IsEqual(CMessageInfo info)
            {
                if (this.Index != info.Index) return false;
                if (this.Type != info.Type) return false;

                for (int i = 0; i < (int)ELanguage.MAX; i++)
                {
                    if (this.Message[i] != info.Message[i]) return false;
                }

                return true;
            }

            public string GetMessage(DEF_Common.ELanguage lang = DEF_Common.ELanguage.ENGLISH)
            {
                return Message[(int)lang];
            }

            public bool IsEqual(string strMsg)
            {
                strMsg = strMsg.ToLower();
                foreach (string str in Message)
                {
                    if (string.IsNullOrWhiteSpace(str)) continue;
                    string str1 = str.ToLower();

                    // Message 특성상 마침표등의 문제로 문자열을 포함하면 같은 메세지인걸로 판단하도록.
                    if (str1 == strMsg || str1.IndexOf(strMsg) >= 0 || strMsg.IndexOf(str1) >= 0)
                        return true;
                }
                return false;
            }

            public bool Update(CMessageInfo info)
            {
                bool bUpdated = false;
                string str;
                for (int i = 0; i < (int)ELanguage.MAX; i++)
                {
                    str = info.Message[i];
                    if (str != MSG_UNDEFINED && string.IsNullOrWhiteSpace(str) == false
                        && Message[i] != str)
                    {
                        bUpdated = true;
                        Message[i] = str;
                    }
                }

                //if(bUpdated)
                {
                    Type = info.Type;
                }
                return bUpdated;
            }
        }


        public enum EErrorType
        {
            E1, // Error의 경알람 중알람 등등을 정의?
            E2,
            E3,
        }



        /// <summary>
        /// Alarm 에 대한 정보 class
        /// ErrorBase와 ErrorCode 조합 형식때문에 각 ErrorBase당 ErrorCode는 최대 100개로 제한됨
        /// </summary>
        public class CAlarmInfo
        {
            public int Index;           // Primary Key : ErrorBase + ErrorCode 조합
            public EErrorType Type;     // Error Type
            public string Esc = "X:0,Y:0";

            public string[] Description = new string[(int)DEF_Common.ELanguage.MAX];
            public string[] Solution = new string[(int)DEF_Common.ELanguage.MAX];

            public CAlarmInfo(int Index = 0, EErrorType Type = EErrorType.E1)
            {
                this.Index = Index;
                this.Type = Type;
                Description[(int)DEF_Common.ELanguage.KOREAN] = "에러메시지가 정의되지 않았습니다.";
                Description[(int)DEF_Common.ELanguage.ENGLISH] = "Error text is not defined";
                Description[(int)DEF_Common.ELanguage.CHINESE] = "预留";
                Description[(int)DEF_Common.ELanguage.JAPANESE] = "リザーブド";

                Solution[(int)DEF_Common.ELanguage.KOREAN] = "해결방법이 정의되지 않았습니다.";
                Solution[(int)DEF_Common.ELanguage.ENGLISH] = "Solution text is not defined";
                Solution[(int)DEF_Common.ELanguage.CHINESE] = "解法";
                Solution[(int)DEF_Common.ELanguage.JAPANESE] = "解決策";

                for (int i = 0; i < (int)ELanguage.MAX; i++)
                {
                    Description[i] = MSG_UNDEFINED;
                    Solution[i] = MSG_UNDEFINED;
                }
            }

            public bool IsEqual(CAlarmInfo info)
            {
                if (this.Index != info.Index) return false;
                if (this.Type != info.Type) return false;
                if (this.Esc != info.Esc) return false;

                for (int i = 0; i < (int)ELanguage.MAX; i++)
                {
                    if (this.Description[i] != info.Description[i]) return false;
                    if (this.Solution[i] != info.Solution[i]) return false;
                }

                return true;
            }

            public string GetAlarmText(DEF_Common.ELanguage lang = DEF_Common.ELanguage.ENGLISH)
            {
                return Description[(int)lang];
            }

            public string GetSolutionText(DEF_Common.ELanguage lang = DEF_Common.ELanguage.ENGLISH)
            {
                return Solution[(int)lang];
            }

            public override string ToString()
            {
                return $"Alarm : {GetAlarmText()}, Solution : {GetSolutionText()}";
            }

            public bool Update(CAlarmInfo info)
            {
                bool bUpdated = false;
                string str;
                for (int i = 0; i < (int)ELanguage.MAX; i++)
                {
                    str = info.Description[i];
                    if (str != MSG_UNDEFINED && string.IsNullOrWhiteSpace(str) == false
                        && Description[i] != str)
                    {
                        bUpdated = true;
                        Description[i] = str;
                    }

                    str = info.Solution[i];
                    if (str != MSG_UNDEFINED && string.IsNullOrWhiteSpace(str) == false
                        && Solution[i] != str)
                    {
                        bUpdated = true;
                        Solution[i] = str;
                    }
                }

                //if(bUpdated)
                {
                    Type = info.Type;
                    if (info.Esc != "X:0,Y:0") Esc = info.Esc;
                }
                return bUpdated;
            }
        }

        /// <summary>
        /// CAlarmInfo를 가지고 있는 실제 발생한 Alarm 정보
        /// </summary>
        public class CAlarm
        {
            public int ProcessID;

            // Alarm Code = (ObjectID << 16) + ErrorBase + ErrorCode
            public int ObjectID;
            public int ErrorBase;
            public int ErrorCode;

            // Alarm을 보고한 Process의 정보
            public string ProcessName;
            public string ProcessType;

            // 실제 Alarm이 발생한 Object의 정보
            public string ObjectName;
            public string ObjectType;

            public DateTime OccurTime = DateTime.Now; // 발생시간
            public DateTime ResetTime = DateTime.Now; // 조치시간 (현재는 미정)

            public CAlarmInfo Info = new CAlarmInfo();

            public int GetIndex()
            {
                return ErrorBase + ErrorCode;
            }

            public override string ToString()
            {
                return $"Index : {GetIndex()}, Process : [{ProcessType}]{ProcessName}, Object : [{ObjectType}]{ObjectName}";
            }
        }
    }


    public class DEF_Thread
    {
        /// <summary>
        /// define auto / manual mode
        /// </summary>
        public enum EAutoManual
        {
            AUTO,     // 자동 동작 모드
            MANUAL,   // 수동 동작 모드
        }


        /// <summary>
        /// Thread Status, RunStatus
        /// </summary>
        public enum EAutoRunStatus
        {
            NONE = -1,
            STS_MANUAL       = 0,    // System 수동 동작 상태
            STS_RUN_READY       ,    // View Start 버튼이 눌러졌음
            STS_RUN             ,    // System RUN 상태
            STS_STEP_STOP       ,    // STEP_STOP을 진행중임
            STS_ERROR_STOP      ,    // ERROR_STOP을 진행중미
            STS_CYCLE_STOP      ,    // CYCLE_STOP을 진행중임
        }

        /// <summary>
        /// Thread ID, List
        /// </summary>
        public enum EThreadChannel
        {
            NONE = -1,
            TrsSelfChannel = 0,
            TrsAutoManager       ,
            TrsStage1            ,
            TrsCam1 ,
            TrsCam2 ,
            TrsCam3 ,
            TrsCam4 ,
            MAX,
        }

        // Thread Run
        public const int ThreadSleepTime     = 1;          // millisecond
        public const int ThreadSuspendedTime = 100;         // millisecond
        public const int ThreadInterfaceTime = 100;     // millisecond

        /// <summary>
        /// initialize thread unit index
        /// </summary>
        public enum EThreadUnit
        {
            NONE = -1,
            AUTOMANAGER,    // automanager는 실제로 일은 하지 않지만 연관되는 것들때문에..
            STAGE1,
            MAX,
        }


        /// <summary>
        /// Thread 사이의 interface 통신을 위해 사용하는 class
        /// </summary>
        public class CThreadInterface
        {
            // Common
            public int TimeLimit = 15;            // second, interface time limit
            public int TimeKeepOn = 1 * 1000;       // millisecond, interface에서 마지막 신호의 유지 시간

            // handshake 도중에 상대편에게서 에러가 발생했을때 굳이 interface time limit까지 기다리지 않고 바로 나가기 위해서
            // 상대편의 에러만 체크하는것은, 다른에러에는 반응하지 않고 handshake를 마무리 짓기 위해서임
            public bool[] ErrorOccured = new bool[(int)EThreadUnit.MAX];

            // interface time limit over
            public bool[] TimeOver = new bool[(int)EThreadUnit.MAX];

            // Message Format : Sender_Receiver_Message

            //////////////////////////////////////////////////////////////////////////////////
            // TrsLoader Message
            // with PushPull
            public bool Loader_PushPull_WaitBeginLoading      ; // unused
            public bool Loader_PushPull_BeginHandshake_Load   ; // begin handshake
            public bool Loader_PushPull_LoadStep1             ; // move to load
            public bool Loader_PushPull_LoadStep2             ; // vacuum absorb
            public bool Loader_PushPull_FinishHandshake_Load  ; // finish handshake


            //////////////////////////////////////////////////////////////////////////////////
            // TrsHandler Message

            // with Stage1
            public bool Handler_Stage1_WaitBeginLoading         ; // unused
            public bool Handler_Stage1_BeginHandshake_Load      ; // begin handshake
            public bool Handler_Stage1_LoadStep1                ; // move to load, vacuum absorb
            public bool Handler_Stage1_LoadStep2                ; // move to wait
            public bool Handler_Stage1_FinishHandshake_Load     ; // finish handshake

            //////////////////////////////////////////////////////////////////////////////////
            // TrsStage Message
            // with Handler
            public bool Stage1_Handler_BeginHandshake_Load      ; // begin handshake
            public bool Stage1_Handler_LoadStep1                ; // move to load
            public bool Stage1_Handler_LoadStep2                ; // vacuum absorb
            public bool Stage1_Handler_FinishHandshake_Load     ; // finish handshake


            public void ResetInterface(int selfAddr)
            {
                ErrorOccured[selfAddr] = false;
                TimeOver[selfAddr] = false;

                EThreadUnit cnvt = EThreadUnit.NONE;
                try
                {
                    cnvt = (EThreadUnit)Enum.Parse(typeof(EThreadUnit), selfAddr.ToString());
                }
                catch (System.Exception ex)
                {
                    Debug.WriteLine(ex);
                }
                switch (cnvt)
                {
                    case EThreadUnit.AUTOMANAGER:
                        break ;


                    case EThreadUnit.STAGE1:
                        // with Handler
                        Stage1_Handler_BeginHandshake_Load                                   = false;
                        Stage1_Handler_LoadStep1                                             = false;
                        Stage1_Handler_LoadStep2                                             = false;
                        Stage1_Handler_FinishHandshake_Load                                  = false;
                        
                        break;
                }
            }
        }

        // Common Thread Message inter Threads
        public enum EThreadMessage
        {
            NONE = -1,
            // _CNF command는 _CMD command에 대한 response 임
            MSG_MANUAL_CMD = 10,                 // 수동 모드로의 전환
            MSG_MANUAL_CNF,                      // 
            MSG_READY_RUN_CMD,                   // 화면에서 시작 버튼을 누름, 즉 manual -> start ready 상태로 변경
            MSG_READY_RUN_CNF,                   // 
            MSG_START_CMD,                       // OP Panel에서 시작 버튼을 누름 / start dialog에서 시작 버튼을 클릭, 즉 start ready -> start 상태로 바뀜
            MSG_START_CNF,                       //
            MSG_ERROR_STOP_CMD,                  // Error Stop 스위치를 누름
            MSG_ERROR_STOP_CNF,
            MSG_STEP_STOP_CMD,                   // cycle stop 상태에서 한번더 op panel의 정지 버튼을 누름 / step stop dialog에서 정지 버튼을 클릭
            MSG_STEP_STOP_CNF,
            MSG_CYCLE_STOP_CMD,                  // 자동운전 상태에서 op panel의 정지 버튼을 누름 / 화면에서 운전 정지 버튼을 클릭.
            MSG_CYCLE_STOP_CNF,
            MSG_PANEL_SUPPLY_START,              // 화면에서 Panel Supply Stop 버튼을 누름
            MSG_PANEL_SUPPLY_STOP,
            MSG_START_CASSETTE_SUPPLY,
            MSG_STAGE_LOADING_START,             // AK 측과의 물류시 안전신호 체크용으로 쓰임
            MSG_STAGE_LOADING_END,
            MSG_PANEL_INPUT,            // BUFFER(S), WORKBENCH(G)측의 PANEL 투입시점 Check
            MSG_PANEL_OUTPUT,			// WORKBENCH(S), UNLOADHANDLER(G)측의 PANEL 아웃지점 Check
            MSG_UNLOADHANDLER_UNLOADING_START,   // PCB 측과의 물류시 안전신호 체크용으로 쓰임
            MSG_UNLOADHANDLER_UNLOADING_END,
            MSG_CONFIRM_ENG_DOWN,

            MSG_AUTO_LOADER_REQUEST_LOAD_CASSETTE,
            MSG_AUTO_LOADER_REQUEST_UNLOAD_CASSETTE,

            MSG_PROCESS_ALARM = 99,	                 // Process Alarm 메세지




            // TrsStage1 Message
            MSG_STAGE1_UPPER_HANDLER_REQUEST_LOADING = 500,
            //MSG_STAGE1_UPPER_HANDLER_START_UNLOADING,
            MSG_STAGE1_UPPER_HANDLER_RELEASE_COMPLETE,
            //MSG_STAGE1_UPPER_HANDLER_COMPLETE_UNLOADING,

            MSG_STAGE1_LOWER_HANDLER_REQUEST_UNLOADING,
            //MSG_STAGE1_LOWER_HANDLER_START_LOADING,
            MSG_STAGE1_LOWER_HANDLER_ABSORB_COMPLETE,
            //MSG_STAGE1_LOWER_HANDLER_COMPLETE_LOADING,

        }

        public enum EWindowMessage
        {
            NONE = -1,

            // message from control class to GUI
            WM_SW_STATUS,
            WM_AUTO_STATUS,
            WM_ALARM_MSG,
            WM_STEP_DISPLAY_END,
            WM_HEIGHT_DISPLAY_END,
            WM_DISPLAY_HELPVIEW,
            WM_DISPLAY_HELP_ID,

            WM_MSGBOX_MSG,          // MyMessageBox 띄우기
            WM_ALIGN_MSG,           // Align 인식 실패 시 처리하는 Dialog 띄우기
            WM_DISPLAYUPDATE_MSG,   // Display Update Msg
            WM_START_RUN_MSG,       // 자동운전 시작
            WM_START_READY_MSG,     // 자동운전 준비 단계
            WM_START_MANUAL_MSG,
            WM_ERRORSTOP_MSG,
            WM_STEPSTOP_MSG,
            WM_CHECK_ENG_DOWN_MSG,

            WM_DISPLAY_STATUSBAR_1,
            WM_DISPLAY_STATUSBAR_2,
            WM_DISPLAY_STATUSBAR_3,

            WM_DISP_PANEL_DISTANCE_MSG,
            WM_DISP_PANEL_DISTANCE_MSG1,
            WM_DISP_PANEL_DISTANCE_MSG2,
            WM_DISP_TACTTIME_MSG,

            // 생산량 증가 MSG
            WM_DISP_PRODUCT_OUT_MSG,
            WM_DISP_PRODUCT_IN_MSG,

            // Panel Supply
            WM_CELL_SUPPLY_STOP_MSG,

            WM_DISP_RUN_MODE,
            WM_DISP_EQSTOP_MSG,

            WM_DISP_DISPLAY_UVLED_LIGHT,
            WM_DISP_REPORT_AUTO_UV_CHECK,
        }

        public enum EThreadStep
        {
            STEP_NONE = -1,
            ///////////////////////////////////////////////////////////////////
            // TrsLoader Step
            ///////////////////////////////////////////////////////////////////
            //
            TRS_LOADER_WAITFOR_MESSAGE,

            // handle cassette
            TRS_LOADER_READY_LOAD_CASSETTE,
            TRS_LOADER_WAITFOR_CASSETTE_LOADED,
            TRS_LOADER_LOAD_CASSETTE,
            TRS_LOADER_CHECK_STACK_OF_CASSETTE,

            TRS_LOADER_READY_UNLOAD_CASSETTE,
            TRS_LOADER_WAITFOR_CASSETTE_REMOVED,

            // process load with pushpull
            TRS_LOADER_LOADING_FROM_PUSHPULL_ONESTEP,       // handshake by one step

            TRS_LOADER_BEGIN_LOADING_FROM_PUSHPULL,         // wait for handshake signal, send begin handshake
            TRS_LOADER_LOAD_STEP1_FROM_PUSHPULL,            // move to load pos, send load ready signal
            TRS_LOADER_WAITFOR_PUSHPULL_UNLOAD_STEP1,       // wait for response signal
            TRS_LOADER_LOAD_STEP2_FROM_PUSHPULL,         // vacuum absorb, send load complete signal
            TRS_LOADER_WAITFOR_PUSHPULL_UNLOAD_STEP2,    // wait for response signal
            TRS_LOADER_FINISH_LOADING_FROM_PUSHPULL,        // send finish handshake signal
            TRS_LOADER_WAITFOR_PUSHPULL_FINISH_UNLOAD,      // wait for handshake response

            ///////////////////////////////////////////////////////////////////
            // TrsPushPull Step
            ///////////////////////////////////////////////////////////////////
            TRS_PUSHPULL_MOVETO_WAIT_POS,
            TRS_PUSHPULL_WAITFOR_MESSAGE,

            // process load with loader
            TRS_PUSHPULL_LOADING_FROM_LOADER_ONESTEP,       // handshake by one step

            TRS_PUSHPULL_BEGIN_LOADING_FROM_LOADER,         // send begin handshake signal
            TRS_PUSHPULL_WAITFOR_LOADER_BEGIN_UNLOAD,       // wait for handshake response
            TRS_PUSHPULL_WAITFOR_LOADER_UNLOAD_STEP1,       // wait for response signal
            TRS_PUSHPULL_LOAD_STEP1_FROM_LOADER,            // move to load pos, vacuum absorb, send load ready signal
            TRS_PUSHPULL_WAITFOR_LOADER_UNLOAD_STEP2,       // wait for response signal
            TRS_PUSHPULL_LOAD_STEP2_FROM_LOADER,            // move to wait pos, send load complete signal
            TRS_PUSHPULL_WAITFOR_LOADER_FINISH_UNLOAD,      // wait for handshake response
            TRS_PUSHPULL_FINISH_LOADING_FROM_LOADER,        // send finish handshake signal




        };
    }

    public class DEF_Error
    {
        ////////////////////////////////////////////////////////////////////
        // Process Layer
        ////////////////////////////////////////////////////////////////////

        // TrsStage1
        public const int ERR_TRS_STAGE1_INTERFACE_TIMELIMIT_OVER                  = 1;
        public const int ERR_TRS_STAGE1_NEXT_PROCESS_IS_ABNORMAL = 2;

        public const int ERR_TRS_STAGE1_PANEL_DATA_NULL                           = 2;
        public const int ERR_TRS_STAGE1_PANEL_ID_NOT_SAME                         = 3;
        public const int ERR_TRS_STAGE1_PANEL_HISTORY                             = 4;
        public const int ERR_TRS_STAGE1_REPAIR_COUNT                              = 5;
        public const int ERR_TRS_STAGE1_PANEL_DETECTED_BEFORE_LOADING             = 6;
        public const int ERR_TRS_STAGE1_EXCEED_MAX_WAIT_TIME_FOR_SIGNAL           = 7;


        ////////////////////////////////////////////////////////////////////
        // Mechanical Layer
        ////////////////////////////////////////////////////////////////////
        // MOpPanel
        public const int ERR_OPPANEL_INVALID_OBJECTID                             = 1;
        public const int ERR_OPPANEL_INVALID_ERRORBASE                            = 2;
        public const int ERR_OPPANEL_INVALID_POINTER                              = 3;
        public const int ERR_OPPANEL_INVALID_JOG_KEY_TYPE                         = 4;
        public const int ERR_OPPANEL_INVALID_JOG_UNIT_INDEX                       = 5;
        public const int ERR_OPPANEL_INVALID_INIT_UNIT_INDEX                      = 6;
        public const int ERR_OPPANEL_INVALID_SERVO_UNIT_INDEX                     = 7;
        public const int ERR_OPPANEL_AMP_FAULT                                    = 8;
        public const int ERR_OPPANEL_DOOR_ADDRESS_NOT_DEFINED                     = 9;
        public const int ERR_OPPANEL_INVALID_DOOR_GROUP                           = 10;
        public const int ERR_OPPANEL_INVALID_DOOR_INDEX                           = 11;

        ////////////////////////////////////////////////////////////////////
        // Hardware Layer
        ////////////////////////////////////////////////////////////////////
    }



    public class DEF_UI
    {
        /// <summary>
        /// Main UI
        /// </summary>
        public static readonly int FORM_POS_X = 0;
        public static readonly int FORM_POS_Y = 0;
        public static readonly int FORM_SIZE_WIDTH  = 1925; //   1920// Main Frame은 약간 크게 
        public static readonly int FORM_SIZE_HEIGHT = 1085; //   1080; + MenuBar +25
        

        public static readonly int TOP_POS_X = 0;
        public static readonly int TOP_POS_Y = 0;
        public static readonly int TOP_SIZE_WIDTH = 1920;
        public static readonly int TOP_SIZE_HEIGHT = 75; //98


        public static readonly int MAIN_POS_X = 0;
        public static readonly int MAIN_POS_Y = 75;
        public static readonly int MAIN_SIZE_WIDTH = 1920;
        public static readonly int MAIN_SIZE_HEIGHT = 902; //880

        public static readonly int BOT_POS_X = 0;
        public static readonly int BOT_POS_Y = 980; //980
        public static readonly int BOT_SIZE_WIDTH = 1920;
        public static readonly int BOT_SIZE_HEIGHT = 100;

        
        
        public enum EFormType
        {
            NONE = -1,
            AUTO,
            MANUAL,
            DATA,
            TEACH,
            LOG,
            HELP,
            MAX,
        }
    }


}