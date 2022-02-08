using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Data;
using System.Data.SQLite;
using System.Data.SQLite.Linq;
using System.IO;
using System.Diagnostics;

//### using Excel = Microsoft.Office.Interop.Excel;

using static Core.Layers.DEF_System;
using static Core.Layers.DEF_Common;
using static Core.Layers.DEF_Error;
using static Core.Layers.DEF_Thread;
using static Core.Layers.DEF_DataManager;
using static Core.Layers.DEF_MeStage;
using static Core.Layers.DEF_Vision;

namespace Core.Layers
{
    public class DEF_DataManager
    {
        public const int ERR_DATA_MANAGER_FAIL_BACKUP_DB                = 1;
        public const int ERR_DATA_MANAGER_FAIL_DELETE_DB                = 2;
        public const int ERR_DATA_MANAGER_FAIL_DROP_TABLES              = 3;
        public const int ERR_DATA_MANAGER_FAIL_BACKUP_ROW               = 4;
        public const int ERR_DATA_MANAGER_FAIL_SAVE_GENERAL_DATA        = 5;
        public const int ERR_DATA_MANAGER_FAIL_LOAD_GENERAL_DATA        = 6;
        public const int ERR_DATA_MANAGER_FAIL_SAVE_SYSTEM_DATA         = 7;
        public const int ERR_DATA_MANAGER_FAIL_LOAD_SYSTEM_DATA         = 8;
        public const int ERR_DATA_MANAGER_FAIL_SAVE_MODEL_DATA          = 11;
        public const int ERR_DATA_MANAGER_FAIL_LOAD_MODEL_DATA          = 12;
        public const int ERR_DATA_MANAGER_FAIL_SAVE_MODEL_LIST          = 13;
        public const int ERR_DATA_MANAGER_FAIL_DELETE_MODEL_DATA        = 14;
        public const int ERR_DATA_MANAGER_FAIL_LOAD_MODEL_LIST          = 15;
        public const int ERR_DATA_MANAGER_FAIL_SAVE_LOGIN_HISTORY       = 16;
        public const int ERR_DATA_MANAGER_FAIL_LOAD_LOGIN_HISTORY       = 17;
        public const int ERR_DATA_MANAGER_FAIL_SAVE_ALARM_INFO          = 18;
        public const int ERR_DATA_MANAGER_FAIL_LOAD_ALARM_INFO          = 19;
        public const int ERR_DATA_MANAGER_FAIL_SAVE_MESSAGE_INFO        = 20;
        public const int ERR_DATA_MANAGER_FAIL_LOAD_MESSAGE_INFO        = 21;
        public const int ERR_DATA_MANAGER_FAIL_SAVE_PARAMETER_INFO      = 22;
        public const int ERR_DATA_MANAGER_FAIL_LOAD_PARAMETER_INFO      = 23;
        public const int ERR_DATA_MANAGER_FAIL_SAVE_ALARM_HISTORY       = 24;
        public const int ERR_DATA_MANAGER_FAIL_LOAD_ALARM_HISTORY       = 25;
        public const int ERR_DATA_MANAGER_FAIL_DELETE_ROOT_FOLDER       = 26;
        public const int ERR_DATA_MANAGER_FAIL_DELETE_DEFAULT_MODEL     = 27;
        public const int ERR_DATA_MANAGER_FAIL_DELETE_CURRENT_MODEL     = 28;
        public const int ERR_DATA_MANAGER_FAIL_EXCEL_IMPORT             = 29;
        public const int ERR_DATA_MANAGER_FAIL_EXCEL_EXPORT             = 30;

        // define root folder & default model name
        public const string NAME_ROOT_FOLDER = "root";
        public const string NAME_DEFAULT_MODEL = "default";
        public const string NAME_DEFAULT_OPERATOR = "Operator";
        public const string NAME_DEFAULT_ENGINEER = "Engineer";
        public const string NAME_MAKER = "Maker";


        public class CSystemData
        {
            //////////////////////////////////////////////////////////////////////////////
            // System General
            public ELanguage Language = ELanguage.KOREAN;
            public string ModelName = NAME_DEFAULT_MODEL;

            public string PassWord;     // Engineer Password

            //////////////////////////////////////////////////////////////////////////////
            // 아래는 아직 미정리 내역들. 
            // * 혹시, 아래에서 사용하는것들은 이 주석 위로 올려주기 바람
            //

            public double data1;
            public double data2;


            public int SystemLanguageSelect;
            
            public CSystemData()
            {

            }
        }
 

        public class CSystemData_Light
        {
            public CLightData[] Light = new CLightData[(int)ELightController.MAX];
        }

        
        public class CLogParameter
        {
            public bool UseLogLevelTactTime;
            public bool UseLogLevelNormal;
            public bool UseLogLevelWarning;
            public bool UseLogLevelError;
            public int  LogKeepingDay;
        }

        public enum EListHeaderType
        {
            MODEL = 0,
            USERINFO,
            MAX,
        }

        /// <summary>
        /// Model, Cassette, WaferFrame Data 의 계층구조를 만들기 위해서 Header만 따로 떼어서 관리.
        /// Folder인 경우엔 IsFolder = true & CModelData는 따로 만들지 않음.
        /// Model인 경우엔 IsFolder = false & CModelData에 같은 이름으로 ModelData가 존재함.
        /// </summary>
        public class CListHeader
        {
            // Header
            public string Name;   // unique primary key
            public string Comment;
            public string Parent = NAME_ROOT_FOLDER; // if == "root", root
            public bool IsFolder = false; // true, if it is folder.
            public int TreeLevel = -1; // models = -1, root = 0, 1'st generation = 1, 2'nd generation = 2.. 3,4,5

            public void SetRootFolder()
            {
                SetFolder(NAME_ROOT_FOLDER, "Root Folder", "");
                TreeLevel = 0;
            }

            public void SetFolder(string Name, string Comment, string Parent)
            {
                SetModel(Name, Comment, Parent);
                IsFolder = true;
                TreeLevel = 1;                
            }

            public void SetDefaultModel()
            {
                SetModel(NAME_DEFAULT_MODEL, "Default Model", NAME_ROOT_FOLDER);
            }

            public void SetModel(string Name, string Comment, string Parent)
            {
                this.Name = Name;
                this.Comment = Comment;
                this.Parent = Parent;
                IsFolder = false;
                TreeLevel = -1;
            }
        }

        public class CModelData    // Model, Recipe
        {
            ///////////////////////////////////////////////////////////
            // Header
            public string Name = NAME_DEFAULT_MODEL;   // unique primary key       
            


            public CModelData()
            {

            }
        }        
    }

    public class MDataManager : MObject
    {
        public CDBInfo DBInfo { get; private set; }

        /////////////////////////////////////////////////////////////////////////////////
        // System Model Data
        public CSystemData SystemData { get; private set; } = new CSystemData();
        public CSystemData_Light SystemData_Light { get; private set; } = new CSystemData_Light();

        /////////////////////////////////////////////////////////////////////////////////
        // User Info
        public CLoginInfo LoginInfo { get; private set; }
        public List<CListHeader> UserInfoHeaderList { get; set; } = new List<CListHeader>();

        /////////////////////////////////////////////////////////////////////////////////
        // Model Data
        public CModelData ModelData { get; private set; } = new CModelData();
        public List<CListHeader> ModelHeaderList { get; set; } = new List<CListHeader>();

        /////////////////////////////////////////////////////////////////////////////////
        // General Information Data 
        // IO Name

        // Alarm Information & History
        public List<CAlarmInfo> AlarmInfoList { get; private set; } = new List<CAlarmInfo>();
        public List<CAlarm> AlarmHistory { get; private set; } = new List<CAlarm>();

        // Message Information for Message 표시할 때..
        public List<CMessageInfo> MessageInfoList { get; private set; } = new List<CMessageInfo>();

        // Parameter Information for Display에 보여줄때 다국어 지원을 위해서 관리
        public List<CParaInfo> ParaInfoList { get; private set; } = new List<CParaInfo>();

  
        public MDataManager(CObjectInfo objInfo, CDBInfo dbInfo)
            : base(objInfo)
        {
            DBInfo = dbInfo;
            Login("", true);
                      

            TestFunction();
        }

        public int Initialize()
        {
            int iResult = SUCCESS;
            iResult = LoadGeneralData();
            if (iResult != SUCCESS) return iResult;

            // 아래의 네가지 함수 콜은 Core의 Initialize에서 읽어들이는게 맞지만, 생성자에서 한번 더 읽어도 되기에.. 주석처리해도 상관없음
            iResult = LoadSystemData();
          
            if (iResult != SUCCESS) return iResult;
            iResult = LoadModelList();
            if (iResult != SUCCESS) return iResult;

            // MakeDefaultModel();
            iResult = ChangeModel(SystemData.ModelName);
            //iResult = ChangeModel(ModelData.Name);
            if (iResult != SUCCESS) return iResult;

            return SUCCESS;
        }

        public void TestFunction()
        {
            ///////////////////////////////////////
            if(false)
            {
                CListHeader header = new CListHeader();
                ModelHeaderList.Add(header);

                for (int i = 0; i < 3; i++)
                {
                    header = new CListHeader();
                    header.Name = $"Model{i}";
                    header.Comment = $"Comment{i}";
                    header.Parent = $"Parent{i}";
                    header.IsFolder = false;
                    ModelHeaderList.Add(header);
                }

                SaveSystemData();
            }

            // 초기에 alarm info 목록 저장 test routine
            if (false)
            {
                for (int i = 0; i < 10; i++)
                {
                    int index = 3200 + i;
                    CAlarmInfo info = new CAlarmInfo(index);
                    info.Description[(int)ELanguage.KOREAN] = $"{index}번 에러";
                    info.Solution[(int)ELanguage.KOREAN] = $"{index}번 해결책";
                    AlarmInfoList.Add(info);
                }

                for (int i = 0; i < 10; i++)
                {
                    CParaInfo info = new CParaInfo("Test", "Name" + i.ToString());
                    info.Description[(int)ELanguage.KOREAN] = $"Name{i} 변수";
                    ParaInfoList.Add(info);
                }

                SaveGeneralData();
            }
            
            ///////////////////////////////////////

            if (false)
            {
                Type type = typeof(CSystemData);
                Dictionary<string, string> fieldBook = ObjectExtensions.ToStringDictionary(SystemData, type);

                CSystemData systemData2 = new CSystemData();
                ObjectExtensions.FromStringDicionary(systemData2, type, fieldBook);
            }


        }

        public int BackupDB()
        {
            string[] dblist = new string[] { $"{DBInfo.DBConn}", $"{DBInfo.DBConn_Info}",
                $"{DBInfo.DBConn_DLog}", $"{DBInfo.DBConn_ELog}" };

            DateTime time = DateTime.Now;

            foreach(string source in dblist)
            {
                if (DBManager.BackupDB(source, time) == false)
                {
                    WriteLog("fail : backup db.", ELogType.Debug, ELogWType.D_Error);
                    return GenerateErrorCode(ERR_DATA_MANAGER_FAIL_BACKUP_DB);
                }
            }

            WriteLog("success : backup db.", ELogType.Debug);
            return SUCCESS;
        }

        public int DeleteDB(string dbConn = "")
        {
            string[] dblist;
            if (dbConn == "")
            {
                dblist = new string[] { $"{DBInfo.DBConn}", $"{DBInfo.DBConn_Backup}",
                $"{DBInfo.DBConn_Info}", $"{DBInfo.DBConn_DLog}", $"{DBInfo.DBConn_ELog}" };
            } else
            {
                dblist = new string[] { $"{dbConn}"};
            }

            foreach (string source in dblist)
            {
                if (DBManager.DeleteDB(source) == false)
                {
                    WriteLog("fail : delete db.", ELogType.Debug, ELogWType.D_Error);
                    return GenerateErrorCode(ERR_DATA_MANAGER_FAIL_DELETE_DB);
                }
            }

            WriteLog("success : delete db.", ELogType.Debug);
            return SUCCESS;
        }

        public int SaveSystemData(CSystemData system = null, CSystemData_Light systemLight = null)
        {
            // CSystemData
            if (system != null)
            {
                try
                {
                    SystemData = ObjectExtensions.Copy(system);
                    string output = JsonConvert.SerializeObject(SystemData);

                    if (DBManager.InsertRow(DBInfo.DBConn, DBInfo.TableSystem, "name", nameof(CSystemData), output,
                        true, DBInfo.DBConn_Backup) != true)
                    {
                        return GenerateErrorCode(ERR_DATA_MANAGER_FAIL_SAVE_SYSTEM_DATA);
                    }
                    WriteLog("success : save CSystemData.", ELogType.SYSTEM, ELogWType.SAVE);
                }
                catch (Exception ex)
                {
                    WriteExLog(ex.ToString());
                    return GenerateErrorCode(ERR_DATA_MANAGER_FAIL_SAVE_SYSTEM_DATA);
                }
            }

      
            
            // CSystemData_Light
            if (systemLight != null)
            {
                try
                {
                    SystemData_Light = ObjectExtensions.Copy(systemLight);
                    string output = JsonConvert.SerializeObject(SystemData_Light);

                    if (DBManager.InsertRow(DBInfo.DBConn, DBInfo.TableSystem, "name", nameof(CSystemData_Light), output,
                        true, DBInfo.DBConn_Backup) != true)
                    {
                        return GenerateErrorCode(ERR_DATA_MANAGER_FAIL_SAVE_SYSTEM_DATA);
                    }
                    WriteLog("success : save CSystemData_Align.", ELogType.SYSTEM, ELogWType.SAVE);
                }
                catch (Exception ex)
                {
                    WriteExLog(ex.ToString());
                    return GenerateErrorCode(ERR_DATA_MANAGER_FAIL_SAVE_SYSTEM_DATA);
                }
            }

            return SUCCESS;
        }

        public int LoadSystemData(bool loadSystem = true, bool loadVision = true, bool loadLight = true)
        {
                string output;

            // CSystemData
            if (loadSystem == true)
            {
                try
                {
                    if (DBManager.SelectRow(DBInfo.DBConn, DBInfo.TableSystem, out output, new CDBColumn("name", nameof(CSystemData))) == true)
                    {
                        CSystemData data = JsonConvert.DeserializeObject<CSystemData>(output);
                        SystemData = ObjectExtensions.Copy(data);
                        WriteLog("success : load CSystemData.", ELogType.SYSTEM, ELogWType.LOAD);
                    }
                    else
                    {                        
                        // save default
                        SystemData = new CSystemData();
                        int iResult = SaveSystemData(SystemData);
                        if (iResult != SUCCESS) return iResult;
                    }
                }
                catch (Exception ex)
                {
                    WriteExLog(ex.ToString());
                    return GenerateErrorCode(ERR_DATA_MANAGER_FAIL_LOAD_SYSTEM_DATA);
                }
            }
  

            // CSystemData_Light
            if (loadLight == true)
            {
                try
                {
                    if (DBManager.SelectRow(DBInfo.DBConn, DBInfo.TableSystem, out output, new CDBColumn("name", nameof(CSystemData_Light))) == true)
                    {
                        CSystemData_Light data = JsonConvert.DeserializeObject<CSystemData_Light>(output);
                        if (SystemData_Light.Light.Length == data.Light.Length)
                        {
                            SystemData_Light = ObjectExtensions.Copy(data);
                        }
                        else
                        {
                            for (int i = 0; i < SystemData_Light.Light.Length; i++)
                            {
                                if (i >= data.Light.Length) break;
                                SystemData_Light.Light[i] = ObjectExtensions.Copy(data.Light[i]);
                            }
                        }
                        WriteLog("success : load CSystemData_Light.", ELogType.SYSTEM, ELogWType.LOAD);
                    }
                    //else // temporarily do not return error for continuous loading
                    //{
                    //    return GenerateErrorCode(ERR_DATA_MANAGER_FAIL_LOAD_SYSTEM_DATA);
                    //}
                }
                catch (Exception ex)
                {
                    WriteExLog(ex.ToString());
                    return GenerateErrorCode(ERR_DATA_MANAGER_FAIL_LOAD_SYSTEM_DATA);
                }
            }
                        

            return SUCCESS;
        }
    

        private void GetTypeHeaderInfo(EListHeaderType type, out List<CListHeader> headerList, out string tableName)
        {
            switch (type)
            {
                case EListHeaderType.MODEL:
                    headerList = ModelHeaderList;
                    tableName = DBInfo.TableModelHeader;
                    break;
                case EListHeaderType.USERINFO:
                default:
                    headerList = UserInfoHeaderList;
                    tableName = DBInfo.TableUserInfoHeader;
                    break;
            }
        }

        private void GetTypeInfo(EListHeaderType type, out List<CListHeader> headerList, out string tableName)
        {
            switch (type)
            {
                case EListHeaderType.MODEL:
                    headerList = ModelHeaderList;
                    tableName = DBInfo.TableModel;
                    break;
                case EListHeaderType.USERINFO:
                default:
                    headerList = UserInfoHeaderList;
                    tableName = DBInfo.TableUserInfo;
                    break;
            }
        }

        /// <summary>
        /// UI에서 public 으로 선언된 ModelHeaderList를 편집한 후에 (data 무결성은 UI에서 책임)
        /// 이 함수를 호출하여 ModelHeader List를 저장한다
        /// </summary>
        /// <returns></returns>
        public int SaveModelHeaderList(EListHeaderType type)
        {
            List<CListHeader> headerList;
            string tableName;
            GetTypeHeaderInfo(type, out headerList, out tableName);

            try
            {
                List<string> querys = new List<string>();
                string query;

                // 0. create table
                query = $"CREATE TABLE IF NOT EXISTS {tableName} (name string primary key, data string)";
                querys.Add(query);

                // 1. delete all
                query = $"DELETE FROM {tableName}";
                querys.Add(query);

                // 2. save model list
                string output;
                foreach (CListHeader header in headerList)
                {
                    output = JsonConvert.SerializeObject(header);
                    query = $"INSERT INTO {tableName} VALUES ('{header.Name}', '{output}')";
                    querys.Add(query);
                }

                // 3. execute query
                if (DBManager.ExecuteNonQuerys(DBInfo.DBConn, querys) != true)
                {
                    return GenerateErrorCode(ERR_DATA_MANAGER_FAIL_SAVE_MODEL_LIST);
                }
            }
            catch (Exception ex)
            {
                WriteExLog(ex.ToString());
                return GenerateErrorCode(ERR_DATA_MANAGER_FAIL_SAVE_MODEL_LIST);
            }

            WriteLog($"success : save {type} header list", ELogType.Debug);
            return SUCCESS;
        }

        public int MakeDefaultModel()
        {
            int iResult = SUCCESS;
            bool bStatus = true;

            ////////////////////////////////////////////////////////////////////////////////
            // Model
            EListHeaderType type = EListHeaderType.MODEL;
            // make root folder
            if(IsModelHeaderExist(NAME_ROOT_FOLDER, type) == false)
            {
                CListHeader header = new CListHeader();
                header.SetRootFolder();
                ModelHeaderList.Add(header);
                iResult = SaveModelHeaderList(type);
                if (iResult != SUCCESS) return iResult;
            }

            // make default data
            if (IsModelHeaderExist(NAME_DEFAULT_MODEL, type) == false)
            {
                CListHeader header = new CListHeader();
                header.SetDefaultModel();
                ModelHeaderList.Add(header);
                iResult = SaveModelHeaderList(type);
                if (iResult != SUCCESS) return iResult;
            }
            if (IsModelExist(NAME_DEFAULT_MODEL, type) == false)
            {
                CModelData data = new CModelData();
                iResult = SaveModelData(data);
                if (iResult != SUCCESS) return iResult;
            }

            ////////////////////////////////////////////////////////////////////////////////
            // UserInfo
            type = EListHeaderType.USERINFO;

            // make root folder
            if (IsModelHeaderExist(NAME_ROOT_FOLDER, type) == false)
            {
                CListHeader header = new CListHeader();
                header.SetRootFolder();
                UserInfoHeaderList.Add(header);
                //iResult = SaveModelHeaderList(type);
                //if (iResult != SUCCESS) return iResult;
            }
            
            // make engineer folder
            if (IsModelHeaderExist(ELoginType.ENGINEER.ToString(), type) == false)
            {
                CListHeader header = new CListHeader();
                header.SetFolder(ELoginType.ENGINEER.ToString(), "", NAME_ROOT_FOLDER);
                UserInfoHeaderList.Add(header);
                //iResult = SaveModelHeaderList(type);
                //if (iResult != SUCCESS) return iResult;
            }

            // make operator folder
            if (IsModelHeaderExist(ELoginType.OPERATOR.ToString(), type) == false)
            {
                CListHeader header = new CListHeader();
                header.SetFolder(ELoginType.OPERATOR.ToString(), "", NAME_ROOT_FOLDER);
                UserInfoHeaderList.Add(header);
                //iResult = SaveModelHeaderList(type);
                //if (iResult != SUCCESS) return iResult;
            }

            // make maker folder
            if (IsModelHeaderExist(ELoginType.MAKER.ToString(), type) == false)
            {
                CListHeader header = new CListHeader();
                header.SetFolder(ELoginType.MAKER.ToString(), "", NAME_ROOT_FOLDER);
                UserInfoHeaderList.Add(header);
                //iResult = SaveModelHeaderList(type);
                //if (iResult != SUCCESS) return iResult;
            }

            // save header list
            iResult = SaveModelHeaderList(type);
            if (iResult != SUCCESS) return iResult;

            // make default operator
            if (IsModelHeaderExist(NAME_DEFAULT_OPERATOR, type) == false)
            {
                CListHeader header = new CListHeader();
                header.SetModel(NAME_DEFAULT_OPERATOR, NAME_DEFAULT_OPERATOR, ELoginType.OPERATOR.ToString());
                UserInfoHeaderList.Add(header);
                iResult = SaveModelHeaderList(type);
                if (iResult != SUCCESS) return iResult;
            }

            if (IsModelExist(NAME_DEFAULT_OPERATOR, type) == false)
            {
                CUserInfo data = new CUserInfo(NAME_DEFAULT_OPERATOR, NAME_DEFAULT_OPERATOR, "", ELoginType.OPERATOR);
                iResult = SaveUserData(data);
                if (iResult != SUCCESS) return iResult;
            }

            // make maker
            if (IsModelHeaderExist(NAME_MAKER, type) == false)
            {
                CListHeader header = new CListHeader();
                header.SetModel(NAME_MAKER, NAME_MAKER, ELoginType.MAKER.ToString());
                UserInfoHeaderList.Add(header);
                iResult = SaveModelHeaderList(type);
                if (iResult != SUCCESS) return iResult;
            }
            if (IsModelExist(NAME_MAKER, type) == false)
            {
                CUserInfo data = new CUserInfo(NAME_MAKER, NAME_MAKER, "", ELoginType.MAKER);
                iResult = SaveUserData(data);
                if (iResult != SUCCESS) return iResult;
            }
            

            return SUCCESS;
        }

        public int LoadModelList()
        {
            LoadModelList(EListHeaderType.MODEL);
            LoadModelList(EListHeaderType.USERINFO);

            return SUCCESS;
        }

        public int LoadModelList(EListHeaderType type)
        {
            List<CListHeader> headerList;
            string tableName;
            GetTypeHeaderInfo(type, out headerList, out tableName);

            try
            {
                string query;

                // 0. select table
                query = $"SELECT * FROM {tableName}";

                // 1. get table
                DataTable datatable;
                if (DBManager.GetTable(DBInfo.DBConn, query, out datatable) != true)
                {
                    return GenerateErrorCode(ERR_DATA_MANAGER_FAIL_LOAD_MODEL_LIST);
                }

                // 2. delete list
                headerList.Clear();

                // 3. get list
                foreach (DataRow row in datatable.Rows)
                {
                    string output = row["data"].ToString();
                    CListHeader header = JsonConvert.DeserializeObject<CListHeader>(output);
                    headerList.Add(header);
                }
            }
            catch (Exception ex)
            {
                WriteExLog(ex.ToString());
                return GenerateErrorCode(ERR_DATA_MANAGER_FAIL_LOAD_MODEL_LIST);
            }

            switch (type)
            {
                case EListHeaderType.MODEL:
                    ModelHeaderList = ObjectExtensions.Copy(headerList);
                    break;
                case EListHeaderType.USERINFO:
                    UserInfoHeaderList = ObjectExtensions.Copy(headerList);
                    break;
            }

            WriteLog($"success : load {type} header list", ELogType.Debug);
            return SUCCESS;
        }

        public int GetModelHeaderCount(EListHeaderType type)
        {
            List<CListHeader> headerList;
            string tableName;
            GetTypeHeaderInfo(type, out headerList, out tableName);

            int nCount = headerList.Count;
            return nCount;
        }

        public bool IsModelHeaderExist(string name, EListHeaderType type)
        {
            if (string.IsNullOrWhiteSpace(name)) return false;

            List<CListHeader> headerList;
            string tableName;
            GetTypeHeaderInfo(type, out headerList, out tableName);

            foreach (CListHeader header in headerList)
            {
                if(header.Name == name)
                {
                    return true;
                }
            }
            return false;
        }

        public bool IsModelExist(string name, EListHeaderType type)
        {
            if (string.IsNullOrWhiteSpace(name)) return false;

            List<CListHeader> headerList;
            string tableName;
            GetTypeInfo(type, out headerList, out tableName);

            try
            {
                // 1. load model
                string output;
                if (DBManager.SelectRow(DBInfo.DBConn, tableName, out output, new CDBColumn("name", name)) == true)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                WriteExLog(ex.ToString());
                return false;
            }

            return false;
        }

        public bool IsModelFolder(string name, EListHeaderType type)
        {
            List<CListHeader> headerList;
            string tableName;
            GetTypeHeaderInfo(type, out headerList, out tableName);

            foreach (CListHeader header in headerList)
            {
                if (header.Name == name)
                {
                    return header.IsFolder;
                }
            }
            return false;
        }

        public int GetModelTreeLevel(string name, EListHeaderType type)
        {
            List<CListHeader> headerList;
            string tableName;
            GetTypeHeaderInfo(type, out headerList, out tableName);

            foreach (CListHeader header in headerList)
            {
                if (header.Name == name)
                {
                    return header.TreeLevel;
                }
            }
            return 0;
        }

        public int DeleteModelHeader(string name, EListHeaderType type)
        {
            List<CListHeader> headerList;
            string tableName;
            GetTypeHeaderInfo(type, out headerList, out tableName);

            if (name == NAME_ROOT_FOLDER) return GenerateErrorCode(ERR_DATA_MANAGER_FAIL_DELETE_ROOT_FOLDER);
            if (name == NAME_DEFAULT_MODEL) return GenerateErrorCode(ERR_DATA_MANAGER_FAIL_DELETE_DEFAULT_MODEL);
            if (IsModelHeaderExist(name, type) == false) return SUCCESS;

            int index = 0;
            foreach (CListHeader header in headerList)
            {
                if (header.Name == name)
                {
                    headerList.RemoveAt(index);
                    break;
                }
                index++;
            }

            int iResult = SaveModelHeaderList(type);
            if (iResult != SUCCESS) return iResult;

            return SUCCESS;
        }

        public int DeleteModelData(string name, EListHeaderType type)
        {
            List<CListHeader> headerList;
            string tableName;
            GetTypeInfo(type, out headerList, out tableName);

            if (name == NAME_DEFAULT_MODEL) return GenerateErrorCode(ERR_DATA_MANAGER_FAIL_DELETE_DEFAULT_MODEL);
            if (IsModelExist(name, type) == false) return SUCCESS;

            // cannot delete current model
            switch (type)
            {
                case EListHeaderType.MODEL:
                    if (name == SystemData.ModelName) return GenerateErrorCode(ERR_DATA_MANAGER_FAIL_DELETE_CURRENT_MODEL);
                    break;
                case EListHeaderType.USERINFO:
                    if (name == LoginInfo.User.Name) return GenerateErrorCode(ERR_DATA_MANAGER_FAIL_DELETE_CURRENT_MODEL);
                    break;
            }

            try
            {
                if (DBManager.DeleteRow(DBInfo.DBConn, tableName, "name", name, true, DBInfo.DBConn_Backup) != true)
                {
                    return GenerateErrorCode(ERR_DATA_MANAGER_FAIL_DELETE_MODEL_DATA);
                }
            }
            catch (Exception ex)
            {
                WriteExLog(ex.ToString());
                return GenerateErrorCode(ERR_DATA_MANAGER_FAIL_DELETE_MODEL_DATA);
            }

            WriteLog($"success : delete {type} name : {name}.", ELogType.SYSTEM, ELogWType.SAVE);
            return SUCCESS;
        }

        /// <summary>
        /// Model Data 변경시에 저장 
        /// </summary>
        /// <param name="modelData"></param>
        /// <returns></returns>
        public int SaveModelData(CModelData data)
        {
            EListHeaderType type = EListHeaderType.MODEL;
            string tableName = DBInfo.TableModel;
            try
            {
                ModelData = ObjectExtensions.Copy(data);
                string output = JsonConvert.SerializeObject(data);

                if (DBManager.InsertRow(DBInfo.DBConn, tableName, "name", data.Name, output,
                    true, DBInfo.DBConn_Backup) != true)
                {
                    return GenerateErrorCode(ERR_DATA_MANAGER_FAIL_SAVE_MODEL_DATA);
                }
            }
            catch (Exception ex)
            {
                WriteExLog(ex.ToString());
                return GenerateErrorCode(ERR_DATA_MANAGER_FAIL_SAVE_MODEL_DATA);
            }

            WriteLog($"success : save {type} model [{data.Name}].", ELogType.SYSTEM, ELogWType.SAVE);
            return SUCCESS;
        }


        /// <summary>
        /// CUserInfo List는 ModelData는 아니지만, 함수를 같이 사용하기 위해서 SaveModelData를 이용해서 호출할수 있도록
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public int SaveUserData(CUserInfo data)
        {
            EListHeaderType type = EListHeaderType.USERINFO;
            string tableName = DBInfo.TableUserInfo;
            try
            {
                //ModelData = ObjectExtensions.Copy(data);
                string output = JsonConvert.SerializeObject(data);

                if (DBManager.InsertRow(DBInfo.DBConn, tableName, "name", data.Name, output,
                    true, DBInfo.DBConn_Backup) != true)
                {
                    return GenerateErrorCode(ERR_DATA_MANAGER_FAIL_SAVE_GENERAL_DATA);
                }
            }
            catch (Exception ex)
            {
                WriteExLog(ex.ToString());
                return GenerateErrorCode(ERR_DATA_MANAGER_FAIL_SAVE_GENERAL_DATA);
            }

            WriteLog($"success : save {type} user [{data.Name}].", ELogType.SYSTEM, ELogWType.SAVE);
            return SUCCESS;
        }

        public int ChangeModel(string name)
        {
            EListHeaderType type = EListHeaderType.MODEL;
            int iResult = SUCCESS;
            // 0. check exist
            if(string.IsNullOrWhiteSpace(name))
            {
                return GenerateErrorCode(ERR_DATA_MANAGER_FAIL_LOAD_MODEL_DATA);
            }
            if(IsModelExist(name, type) == false)
            {
                return GenerateErrorCode(ERR_DATA_MANAGER_FAIL_LOAD_MODEL_DATA);
            }

            CModelData data = null;
            try
            {
                string output;
                // 1. load model
                if (DBManager.SelectRow(DBInfo.DBConn, DBInfo.TableModel, out output, new CDBColumn("name", name)) == true)
                {
                    data = JsonConvert.DeserializeObject<CModelData>(output);
                }
                else
                {
                    return GenerateErrorCode(ERR_DATA_MANAGER_FAIL_LOAD_MODEL_DATA);
                }

                // 2. save system data
                string prev_model = SystemData.ModelName;
                SystemData.ModelName = name;
                iResult = SaveSystemData(SystemData);
                if(iResult != SUCCESS)
                {
                    SystemData.ModelName = prev_model;
                    return iResult;
                }

                // 3. set data
                if (data != null)
                {
                    ModelData = ObjectExtensions.Copy(data);
                }
                
                // 3.1 load waferframe data
                //iResult = LoadWaferFrameData(data.WaferFrameName);
                //if (iResult != SUCCESS) return iResult;
                

                if (iResult != SUCCESS) return iResult;

                WriteLog($"success : change model : {ModelData.Name}.", ELogType.SYSTEM, ELogWType.LOAD);
            }
            catch (Exception ex)
            {
                WriteExLog(ex.ToString());
                return GenerateErrorCode(ERR_DATA_MANAGER_FAIL_LOAD_MODEL_DATA);
            }


            // 5. make model folder
            System.IO.Directory.CreateDirectory(DBInfo.ModelDir+$"\\{name}");

            return SUCCESS;
        }

        public int LoadUserInfo(string name, out CUserInfo data)
        {
            data = new CUserInfo();
            EListHeaderType type = EListHeaderType.USERINFO;
            int iResult = SUCCESS;
            // 0. check exist
            if (string.IsNullOrWhiteSpace(name))
            {
                return GenerateErrorCode(ERR_DATA_MANAGER_FAIL_LOAD_MODEL_DATA);
            }
            if (IsModelExist(name, type) == false)
            {
                return GenerateErrorCode(ERR_DATA_MANAGER_FAIL_LOAD_MODEL_DATA);
            }

            try
            {
                string output;
                // 1.2. load cassette data
                if (DBManager.SelectRow(DBInfo.DBConn, DBInfo.TableUserInfo, out output, new CDBColumn("name", name)) == true)
                {
                    data = JsonConvert.DeserializeObject<CUserInfo>(output);
                }
                else
                {
                    //return GenerateErrorCode(ERR_DATA_MANAGER_FAIL_LOAD_MODEL_DATA);
                }
            }
            catch (Exception ex)
            {
                WriteExLog(ex.ToString());
                return GenerateErrorCode(ERR_DATA_MANAGER_FAIL_LOAD_MODEL_DATA);
            }

            return SUCCESS;
        }


        public int ViewModelData(string name, out CModelData data)
        {
            data = new CModelData();
            // 0. check exist
            if (IsModelExist(name, EListHeaderType.MODEL) == false)
            {
                return GenerateErrorCode(ERR_DATA_MANAGER_FAIL_LOAD_MODEL_DATA);
            }

            try
            {
                // 1. load model
                string output;
                if (DBManager.SelectRow(DBInfo.DBConn, DBInfo.TableModel, out output, new CDBColumn("name", name)) == true)
                {
                    data = JsonConvert.DeserializeObject<CModelData>(output);
                }
                else
                {
                    return GenerateErrorCode(ERR_DATA_MANAGER_FAIL_LOAD_MODEL_DATA);
                }
            }
            catch (Exception ex)
            {
                WriteExLog(ex.ToString());
                return GenerateErrorCode(ERR_DATA_MANAGER_FAIL_LOAD_MODEL_DATA);
            }

            return SUCCESS;
        }
        
        public int Logout()
        {
            int iResult = Login(NAME_DEFAULT_OPERATOR);
            return iResult;
        }

        public int Login(string name, bool IsSystemStart = false)
        {
            CUserInfo user = new CUserInfo();
            if(IsSystemStart == false)
            {
                if (LoginInfo.User.Name == name)
                    return SUCCESS;

                // 1. logout and save
                LoginInfo.AccessTime = DateTime.Now;
                LoginInfo.AccessType = false;
                SaveLoginHistory(LoginInfo);
                DBManager.SetOperator(LoginInfo.User.Name, LoginInfo.User.Type.ToString());
                WriteLog($"{LoginInfo}", ELogType.LOGINOUT, ELogWType.LOGOUT);

                // 2. login and save
                LoadUserInfo(name, out user);
                LoginInfo = new CLoginInfo(user);
                LoginInfo.AccessTime = DateTime.Now;
                LoginInfo.AccessType = true;
                SaveLoginHistory(LoginInfo);
                DBManager.SetOperator(user.Name, user.Type.ToString());
                WriteLog($"{LoginInfo}", ELogType.LOGINOUT, ELogWType.LOGIN);

            }
            else
            {
                user.SetMaker();

                // 2. login and save
                LoginInfo = new CLoginInfo(user);
                LoginInfo.AccessTime = DateTime.Now;
                LoginInfo.AccessType = true;
                SaveLoginHistory(LoginInfo);
                DBManager.SetOperator(user.Name, user.Type.ToString());
                WriteLog($"{LoginInfo}", ELogType.LOGINOUT, ELogWType.LOGIN);
            }

            return SUCCESS;
        }
        
        public int SaveLoginHistory(CLoginInfo login)
        {
            // write login history
            string create_query = $"CREATE TABLE IF NOT EXISTS {DBInfo.TableLoginHistory} (accesstime datetime, accesstype string, name string, comment string, type string)";
            string query = $"INSERT INTO {DBInfo.TableLoginHistory} VALUES ('{DBManager.DateTimeSQLite(login.AccessTime)}', '{login.GetAccessType()}', '{login.User.Name}', '{login.User.Comment}', '{login.User.Type}')";

            if (DBManager.ExecuteNonQuerys(DBInfo.DBConn_ELog, create_query, query) == false)
            {
                return GenerateErrorCode(ERR_DATA_MANAGER_FAIL_SAVE_LOGIN_HISTORY);
            }

            return SUCCESS;
        }

        public int SaveAlarmHistory(CAlarm alarm)
        {
            try
            {
                List<string> querys = new List<string>();

                // 0. create table
                string create_query = $"CREATE TABLE IF NOT EXISTS {DBInfo.TableAlarmHistory} (occurtime datetime, resettime datetime, alarm_key string, data string)";
                querys.Add(create_query);

                // 1. delete all

                // 2. save list
                string output = JsonConvert.SerializeObject(alarm);
                string query = $"INSERT INTO {DBInfo.TableAlarmHistory} VALUES ('{DBManager.DateTimeSQLite(alarm.OccurTime)}', '{DBManager.DateTimeSQLite(alarm.ResetTime)}', '{alarm.GetIndex()}', '{output}')";
                querys.Add(query);

                // 3. execute query
                if (DBManager.ExecuteNonQuerys(DBInfo.DBConn_ELog, querys) != true)
                {
                    return GenerateErrorCode(ERR_DATA_MANAGER_FAIL_SAVE_ALARM_HISTORY);
                }
            }
            catch (Exception ex)
            {
                WriteExLog(ex.ToString());
                return GenerateErrorCode(ERR_DATA_MANAGER_FAIL_SAVE_ALARM_HISTORY);
            }

            WriteLog($"success : save alarm history", ELogType.Debug);
            return SUCCESS;
        }

        public int LoadAlarmHistory()
        {
            try
            {
                string query;

                // 0. select table
                query = $"SELECT * FROM {DBInfo.TableAlarmHistory} WHERE occurtime ORDER BY occurtime DESC";

                // 1. get table
                DataTable datatable;
                if (DBManager.GetTable(DBInfo.DBConn_ELog, query, out datatable) != true)
                {
                    return GenerateErrorCode(ERR_DATA_MANAGER_FAIL_LOAD_ALARM_HISTORY);
                }

                // 2. delete list
                AlarmHistory.Clear();

                // 3. get list
                foreach (DataRow row in datatable.Rows)
                {
                    string output = row["data"].ToString();
                    CAlarm alarm = JsonConvert.DeserializeObject<CAlarm>(output);

                    AlarmHistory.Add(alarm);
                }
            }
            catch (Exception ex)
            {
                WriteExLog(ex.ToString());
                return GenerateErrorCode(ERR_DATA_MANAGER_FAIL_LOAD_ALARM_HISTORY);
            }

            WriteLog($"success : load alarm history", ELogType.Debug);
            return SUCCESS;
        }

        public int LoadGeneralData()
        {
            int iResult = SUCCESS;

            //ImportDataFromExcel(EInfoExcel_Sheet.Skip);

            iResult = LoadAlarmInfoList();
            //if (iResult != SUCCESS) return iResult;

            iResult = LoadMessageInfoList();
            //if (iResult != SUCCESS) return iResult;

            iResult = LoadParameterList();
            //if (iResult != SUCCESS) return iResult;

            return SUCCESS;
        }

        public int LoadAlarmInfoList()
        {
            try
            {
                string query;

                // 0. select table
                query = $"SELECT * FROM {DBInfo.TableAlarmInfo}";

                // 1. get table
                DataTable datatable;
                if (DBManager.GetTable(DBInfo.DBConn_Info, query, out datatable) != true)
                {
                    return GenerateErrorCode(ERR_DATA_MANAGER_FAIL_LOAD_ALARM_INFO);
                }

                // 2. delete list
                AlarmInfoList.Clear();

                // 3. get list
                foreach (DataRow row in datatable.Rows)
                {
                    int index;
                    if (int.TryParse(row["name"].ToString(), out index))
                    {
                        string output = row["data"].ToString();
                        CAlarmInfo info = JsonConvert.DeserializeObject<CAlarmInfo>(output);

                        AlarmInfoList.Add(info);
                    }
                }
            }
            catch (Exception ex)
            {
                WriteExLog(ex.ToString());
                return GenerateErrorCode(ERR_DATA_MANAGER_FAIL_LOAD_ALARM_INFO);
            }

            WriteLog($"success : load alarm info list", ELogType.Debug);
            return SUCCESS;
        }

        public int UpdateAlarmInfo(CAlarmInfo info, bool bMerge = true, bool bSaveToDB = true)
        {
            CAlarmInfo prevInfo = ObjectExtensions.Copy(info);
            for(int i = 0; i < AlarmInfoList.Count; i++)
            {
                if(AlarmInfoList[i].Index == info.Index)
                {
                    if (AlarmInfoList[i].IsEqual(info)) return SUCCESS;
                    prevInfo = ObjectExtensions.Copy(AlarmInfoList[i]);
                    AlarmInfoList.RemoveAt(i);
                    break;
                }
            }

            if(bMerge && prevInfo.Index != 0)
            {
                prevInfo.Update(info);
                AlarmInfoList.Add(prevInfo);
            } else
            {
                AlarmInfoList.Add(info);
            }

            if (bSaveToDB == false) return SUCCESS;
            return SaveAlarmInfoList();
        }

        public int LoadAlarmInfo(int index, out CAlarmInfo info)
        {
            info = new CAlarmInfo();
            info.Index = index;
            if(AlarmInfoList.Count > 0)
            {
                foreach(CAlarmInfo item in AlarmInfoList)
                {
                    if(item.Index == index)
                    {
                        info = ObjectExtensions.Copy(item);
                        return SUCCESS;
                    }
                }
            }

            try
            {
                string output;

                // select row
                if (DBManager.SelectRow(DBInfo.DBConn_Info, DBInfo.TableAlarmInfo, out output, new CDBColumn("name", index.ToString())) == true)
                {
                    info = JsonConvert.DeserializeObject<CAlarmInfo>(output);
                    return SUCCESS;
                }
            }
            catch (Exception ex)
            {
                WriteExLog(ex.ToString());
                return GenerateErrorCode(ERR_DATA_MANAGER_FAIL_LOAD_ALARM_INFO);
            }

            WriteLog($"fail : load alarm info [index = {index}]", ELogType.Debug);
            return GenerateErrorCode(ERR_DATA_MANAGER_FAIL_LOAD_ALARM_INFO);
        }

        public int LoadMessageInfoList()
        {
            try
            {
                string query;

                // 0. select table
                query = $"SELECT * FROM {DBInfo.TableMessageInfo}";

                // 1. get table
                DataTable datatable;
                if (DBManager.GetTable(DBInfo.DBConn_Info, query, out datatable) != true)
                {
                    return GenerateErrorCode(ERR_DATA_MANAGER_FAIL_LOAD_MESSAGE_INFO);
                }

                // 2. delete list
                MessageInfoList.Clear();

                // 3. get list
                foreach (DataRow row in datatable.Rows)
                {
                    int index;
                    if (int.TryParse(row["name"].ToString(), out index))
                    {
                        string output = row["data"].ToString();
                        CMessageInfo info = JsonConvert.DeserializeObject<CMessageInfo>(output);

                        MessageInfoList.Add(info);
                    }
                }
            }
            catch (Exception ex)
            {
                WriteExLog(ex.ToString());
                return GenerateErrorCode(ERR_DATA_MANAGER_FAIL_LOAD_MESSAGE_INFO);
            }

            //WriteLog($"success : load message info list", ELogType.Debug);
            return SUCCESS;
        }

        public int UpdateMessageInfo(CMessageInfo info, bool bSaveToDB = true)
        {
            if(info.Index != -1) // update
            {
                CMessageInfo prevInfo = ObjectExtensions.Copy(info);
                for (int i = 0; i < MessageInfoList.Count; i++)
                {
                    if (MessageInfoList[i].Index == info.Index)
                    {
                        if (MessageInfoList[i].IsEqual(info)) return SUCCESS;
                        prevInfo = ObjectExtensions.Copy(MessageInfoList[i]);
                        MessageInfoList.RemoveAt(i);
                        break;
                    }
                }
                prevInfo.Update(info);
                MessageInfoList.Add(prevInfo);
            } else // add new
            {
                info.Index = GetNextMessageIndex();
                MessageInfoList.Add(info);
            }

            if (bSaveToDB == false) return SUCCESS;
            return SaveMessageInfoList();
        }

        public int LoadMessageInfo(int index, out CMessageInfo info)
        {
            info = new CMessageInfo();
            info.Index = index;
            if (MessageInfoList.Count > 0)
            {
                foreach (CMessageInfo item in MessageInfoList)
                {
                    if (item.Index == index)
                    {
                        info = ObjectExtensions.Copy(item);
                        return SUCCESS;
                    }
                }
            }

            try
            {
                string output;

                // select row
                if (DBManager.SelectRow(DBInfo.DBConn_Info, DBInfo.TableMessageInfo, out output, new CDBColumn("name", index.ToString())) == true)
                {
                    info = JsonConvert.DeserializeObject<CMessageInfo>(output);
                    return SUCCESS;
                }
            }
            catch (Exception ex)
            {
                WriteExLog(ex.ToString());
                return GenerateErrorCode(ERR_DATA_MANAGER_FAIL_LOAD_MESSAGE_INFO);
            }

            WriteLog($"fail : load message info [index = {index}]", ELogType.Debug);
            return GenerateErrorCode(ERR_DATA_MANAGER_FAIL_LOAD_MESSAGE_INFO);
        }

        public int LoadMessageInfo(string strMsg, out CMessageInfo info)
        {
            info = new CMessageInfo();
            if (MessageInfoList.Count > 0)
            {
                foreach (CMessageInfo item in MessageInfoList)
                {
                    if (item.IsEqual(strMsg))
                    {
                        info = ObjectExtensions.Copy(item);
                        return SUCCESS;
                    }
                }
            }

            WriteLog($"fail : load message info", ELogType.Debug);
            return GenerateErrorCode(ERR_DATA_MANAGER_FAIL_LOAD_MESSAGE_INFO);
        }

        public int GetNextMessageIndex(int nStartAfter = 0)
        {
            int index = nStartAfter + 1;
            foreach (CMessageInfo item in MessageInfoList)
            {
                if (item.Index >= index)
                {
                    index = item.Index + 1;
                }

            }
            return index;
        }

        public int LoadParameterList()
        {
            try
            {
                string query;

                // 0. select table
                query = $"SELECT * FROM {DBInfo.TableParameter}";

                // 1. get table
                DataTable datatable;
                if (DBManager.GetTable(DBInfo.DBConn_Info, query, out datatable) != true)
                {
                    return GenerateErrorCode(ERR_DATA_MANAGER_FAIL_LOAD_PARAMETER_INFO);
                }

                // 2. delete list
                ParaInfoList.Clear();

                // 3. get list
                foreach (DataRow row in datatable.Rows)
                {
                    string output = row["data"].ToString();
                    CParaInfo info = JsonConvert.DeserializeObject<CParaInfo>(output);

                    //// 저장할 때, Group + "__" + Name 형태로 저장하기 때문에, desirialize시에 "__" + Group + "__" + Name 환원되는 문제 해결
                    //if(info.Name.Length >= 2 && info.Name.Substring(0, 2) == "__")
                    //{
                    //    info.Name = info.Name.Remove(0, 2);
                    //}

                    ParaInfoList.Add(info);
                }
            }
            catch (Exception ex)
            {
                WriteExLog(ex.ToString());
                return GenerateErrorCode(ERR_DATA_MANAGER_FAIL_LOAD_PARAMETER_INFO);
            }

            WriteLog($"success : load para info list", ELogType.Debug);
            return SUCCESS;
        }

        public int UpdateParaInfo(CParaInfo info, bool bMerge = true, bool bSaveToDB = true)
        {
            CParaInfo prevInfo = ObjectExtensions.Copy(info);
            for (int i = 0; i < ParaInfoList.Count; i++)
            {
                if (ParaInfoList[i].Name.ToLower() == info.Name.ToLower() && ParaInfoList[i].Group.ToLower() == info.Group.ToLower())
                {
                    if (ParaInfoList[i].IsEqual(info)) return SUCCESS;
                    prevInfo = ObjectExtensions.Copy(ParaInfoList[i]);
                    ParaInfoList.RemoveAt(i);
                    break;
                }
            }

            if (bMerge)
            {
                prevInfo.Update(info);
                ParaInfoList.Add(prevInfo);
            }
            else
            {
                ParaInfoList.Add(info);
            }

            if (bSaveToDB == false) return SUCCESS;
            return SaveParaInfoList();
        }


        public int LoadParaInfo(string group, string name, out CParaInfo info)
        {
            info = new CParaInfo(group, name);
            name = name.ToLower();
            group = group.ToLower();
            if(ParaInfoList.Count > 0)
            {
                foreach(CParaInfo item in ParaInfoList)
                {
                    if(item.Group.ToLower() == group && item.Name.ToLower() == name)
                    {
                        info = ObjectExtensions.Copy(item);
                        return SUCCESS;
                    }
                }
            }

            try
            {
                string output;

                // select row
                if (DBManager.SelectRow(DBInfo.DBConn_Info, DBInfo.TableParameter, out output, new CDBColumn("pgroup", info.Group), new CDBColumn("name", info.Name)) == true)
                {
                    info = JsonConvert.DeserializeObject<CParaInfo>(output);
                    return SUCCESS;
                }
            }
            catch (Exception ex)
            {
                WriteExLog(ex.ToString());
                return GenerateErrorCode(ERR_DATA_MANAGER_FAIL_LOAD_PARAMETER_INFO);
            }

            WriteLog($"fail : load para info [group = {info.Group}, name = {info.Name}]", ELogType.Debug);
            return GenerateErrorCode(ERR_DATA_MANAGER_FAIL_LOAD_PARAMETER_INFO);
        }

        public int SaveGeneralData()
        {
            int iResult = SUCCESS;

            iResult = SaveAlarmInfoList();
            //if (iResult != SUCCESS) return iResult;

            iResult = SaveMessageInfoList();
            //if (iResult != SUCCESS) return iResult;

            iResult = SaveParaInfoList();
            //if (iResult != SUCCESS) return iResult;

            return SUCCESS;
        }


        public int SaveAlarmInfoList()
        {

            AlarmInfoList.Sort(delegate (CAlarmInfo a, CAlarmInfo b) { return a.Index.CompareTo(b.Index); });
            try
            {
                List<string> querys = new List<string>();
                string query;

                // 0. create table
                query = $"CREATE TABLE IF NOT EXISTS {DBInfo.TableAlarmInfo} (name string primary key, data string)";
                querys.Add(query);

                // 1. delete all
                query = $"DELETE FROM {DBInfo.TableAlarmInfo}";
                querys.Add(query);

                // 2. save list
                string output;
                foreach (CAlarmInfo info in AlarmInfoList)
                {
                    // index = 0 은 저장할 필요 없음
                    if (info.Index == 0) continue;
                    output = JsonConvert.SerializeObject(info);
                    query = $"INSERT INTO {DBInfo.TableAlarmInfo} VALUES ('{info.Index}', '{output}')";
                    querys.Add(query);
                }

                // 3. execute query
                if (DBManager.ExecuteNonQuerys(DBInfo.DBConn_Info, querys) != true)
                {
                    return GenerateErrorCode(ERR_DATA_MANAGER_FAIL_SAVE_ALARM_INFO);
                }
            }
            catch (Exception ex)
            {
                WriteExLog(ex.ToString());
                return GenerateErrorCode(ERR_DATA_MANAGER_FAIL_SAVE_ALARM_INFO);
            }

            WriteLog($"success : save alarm info list", ELogType.Debug);
            return SUCCESS;
        }

        public int SaveMessageInfoList()
        {
            try
            {
                List<string> querys = new List<string>();
                string query;

                // 0. create table
                query = $"CREATE TABLE IF NOT EXISTS {DBInfo.TableMessageInfo} (name string primary key, data string)";
                querys.Add(query);

                // 1. delete all
                query = $"DELETE FROM {DBInfo.TableMessageInfo}";
                querys.Add(query);

                // 2. save list
                string output;
                foreach (CMessageInfo info in MessageInfoList)
                {
                    output = JsonConvert.SerializeObject(info);
                    query = $"INSERT INTO {DBInfo.TableMessageInfo} VALUES ('{info.Index}', '{output}')";
                    querys.Add(query);
                }

                // 3. execute query
                if (DBManager.ExecuteNonQuerys(DBInfo.DBConn_Info, querys) != true)
                {
                    return GenerateErrorCode(ERR_DATA_MANAGER_FAIL_SAVE_MESSAGE_INFO);
                }
            }
            catch (Exception ex)
            {
                WriteExLog(ex.ToString());
                return GenerateErrorCode(ERR_DATA_MANAGER_FAIL_SAVE_MESSAGE_INFO);
            }

            WriteLog($"success : save message info list", ELogType.Debug);
            return SUCCESS;
        }

        public int DeleteInfoTable(string table)
        {
            if (string.IsNullOrWhiteSpace(table)) return SUCCESS;
            if(table == DBInfo.TableAlarmInfo)
            {
                AlarmInfoList.Clear();
            } else if (table == DBInfo.TableMessageInfo)
            {
                MessageInfoList.Clear();
            }
            else if (table == DBInfo.TableParameter)
            {
                ParaInfoList.Clear();
            }

            try
            {
                List<string> querys = new List<string>();
                string query;

                // 0. drop table
                query = $"DROP TABLE IF EXISTS {table}";
                querys.Add(query);

                // 3. execute query
                if (DBManager.ExecuteNonQuerys(DBInfo.DBConn_Info, querys) != true)
                {
                    return GenerateErrorCode(ERR_DATA_MANAGER_FAIL_DROP_TABLES);
                }
            }
            catch (Exception ex)
            {
                WriteExLog(ex.ToString());
                return GenerateErrorCode(ERR_DATA_MANAGER_FAIL_DROP_TABLES);
            }

            WriteLog($"success : drop info table", ELogType.Debug);
            return SUCCESS;
        }

        public int SaveParaInfoList()
        {
            try
            {
                List<string> querys = new List<string>();
                string query;

                // 0. create table
                query = $"CREATE TABLE IF NOT EXISTS {DBInfo.TableParameter} (pgroup string, name string, data string)";
                querys.Add(query);

                // 1. delete all
                query = $"DELETE FROM {DBInfo.TableParameter}";
                querys.Add(query);

                // 2. save list
                string output;
                foreach (CParaInfo info in ParaInfoList)
                {
                    output = JsonConvert.SerializeObject(info);
                    query = $"INSERT INTO {DBInfo.TableParameter} VALUES ('{info.Group}', '{info.Name}', '{output}')";
                    querys.Add(query);
                }

                // 3. execute query
                if (DBManager.ExecuteNonQuerys(DBInfo.DBConn_Info, querys) != true)
                {
                    return GenerateErrorCode(ERR_DATA_MANAGER_FAIL_SAVE_PARAMETER_INFO);
                }
            }
            catch (Exception ex)
            {
                WriteExLog(ex.ToString());
                return GenerateErrorCode(ERR_DATA_MANAGER_FAIL_SAVE_PARAMETER_INFO);
            }

            WriteLog($"success : save para info list", ELogType.Debug);
            return SUCCESS;
        }
               

    }
}
