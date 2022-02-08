using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using static Core.Layers.DEF_Error;
using static Core.Layers.DEF_Common;
using static Core.Layers.DEF_System;
using static Core.Layers.DEF_System.EObjectLayer;

namespace Core.Layers
{
    public class MSystemInfo
    {
        CObjectInfo[] arrayObjectInfo;

        public MSystemInfo()
        {
            arrayObjectInfo = new CObjectInfo[]
            {
                // 0-39 : Common & Hardware
                new CObjectInfo( (int)OBJ_SYSTEM,       "System",       0, "Core",          0,      "System",       LOG_ALL, LOG_DAY ),
                new CObjectInfo( (int)OBJ_DATAMANAGER,  "DataManger",   1, "DataManager",   100,    "DataManager",  LOG_ALL, LOG_DAY ),

	            new CObjectInfo( (int)OBJ_HL_IO,        "IO", 6, "Device Net", 1000, "IO", LOG_ALL, LOG_DAY ),
	            new CObjectInfo( (int)OBJ_HL_MELSEC,    "Melsec", 11, "Melsec", 1100, "Melsec", LOG_ALL, LOG_DAY ),
		
	            // 30-39 : Serial ------------------------------------------------------------------------
	            new CObjectInfo( (int)OBJ_HL_SERIAL, "Serial RS232", 30, "SHead1", 1200, "RS232_SHead1", LOG_ALL, LOG_DAY ),
                new CObjectInfo( (int)OBJ_HL_SERIAL, "Serial RS232", 31, "SHead2", 1200, "RS232_SHead2", LOG_ALL, LOG_DAY ),
		
                // 40-59 : Vision ------------------------------------------------------------------------
                new CObjectInfo( (int)OBJ_HL_VISION, "Vision", 40, "System",   1500, "VisionSystem", LOG_ALL, LOG_DAY ),
                new CObjectInfo( (int)OBJ_HL_VISION, "Vision", 42, "Camera1",  1600, "VisionCamera1", LOG_ALL, LOG_DAY ),
                new CObjectInfo( (int)OBJ_HL_VISION, "Vision", 43, "Camera2",  1600, "VisionCamera2", LOG_ALL, LOG_DAY ),
                new CObjectInfo( (int)OBJ_HL_VISION, "Vision", 46, "Display1", 1700, "VisionDisplay1", LOG_ALL, LOG_DAY ),
                new CObjectInfo( (int)OBJ_HL_VISION, "Vision", 47, "Display2", 1700, "VisionDisplay2", LOG_ALL, LOG_DAY ),
		
	            // 40-49 : Dummy Reserved
	            // 50-59 : Ethernet Reserved
	            // 60-69 : BarCode Reserved
	            // 70-79 : Melsec
		
	            // 80-99 : Reserved
		
	            // 100-149 :

	            // 150-199 :
		
	            // 200-209 :

	            // 210-249 :

	            // 250-299 :
		
	            // 300-349 : Mechanical Layer --------------------------------------------------------------------
	            new CObjectInfo( (int)OBJ_ML_OP_PANEL  , "Mechanical",   300, "OpPanel",  3000, "OpPanel", LOG_ALL, LOG_DAY ),
                new CObjectInfo( (int)OBJ_ML_VISION    , "Mechanical",   308, "Vision",   3600, "Vision", LOG_ALL, LOG_DAY ),


	            // 400-459 : Process Layer --------------------------------------------------------------------
                new CObjectInfo( (int)OBJ_PL_TRS_AUTO_MANAGER  , "Process",    400, "AutoManager",  7000, "TrsAutoManager", LOG_ALL, LOG_DAY ),
                new CObjectInfo( (int)OBJ_PL_TRS_STAGE1        , "Process",    403, "Stage1",       7300, "TrsStage1", LOG_ALL, LOG_DAY ),
            };

        }

        public bool GetObjectInfo(int ID, out CObjectInfo objInfo)
        {
            objInfo = new CObjectInfo();
            foreach(CObjectInfo objectInfo in arrayObjectInfo)
            {
                if(objectInfo.ID == ID)
                {
                    objInfo = objectInfo;
                    return true;
                }
            }

            //return ERR_SYSTEMINFO_NOT_REGISTED_OBJECTID;
            return false;
        }

        public CObjectInfo GetObjectInfo(int ID)
        {
            CObjectInfo objInfo = new CObjectInfo();
            foreach (CObjectInfo objectInfo in arrayObjectInfo)
            {
                if (objectInfo.ID == ID)
                {
                    objInfo = objectInfo;
                    break;
                }
            }
            return objInfo;
        }

        public string GetObjectName(int ID)
        {
            CObjectInfo objInfo = new CObjectInfo();
            foreach (CObjectInfo objectInfo in arrayObjectInfo)
            {
                if (objectInfo.ID == ID)
                {
                    objInfo = objectInfo;
                    break;
                }
            }
            return objInfo.Name;
        }

        public string GetTypeName(int ID)
        {
            CObjectInfo objInfo = new CObjectInfo();
            foreach (CObjectInfo objectInfo in arrayObjectInfo)
            {
                if (objectInfo.ID == ID)
                {
                    objInfo = objectInfo;
                    break;
                }
            }
            return objInfo.TypeName;
        }
    }
}
