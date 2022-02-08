using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;

using static Core.Layers.DEF_Common;
using static Core.Layers.DEF_Error;
using static Core.Layers.DEF_Common.ETimeType;

namespace Core.Layers
{
    public abstract class MObject
    {
        protected int ObjectID;         // Object Unique ID
        protected CObjectInfo ObjInfo;  // Object Information
        protected MLog LogManager;      // Log Manager

        public int ErrorCode { get; private set; }          // Error Code
        public static string ErrorSubMsg { get; protected set; }   // for Error Message by Hardware Library was generated

        protected MTickTimer m_waitTimer = new MTickTimer();

        public MObject(CObjectInfo ObjInfo)
        {
            ObjectID     = ObjInfo.ID;
            this.ObjInfo = ObjInfo;
            LogManager   = new MLog(ObjInfo.ID);
            LogManager.SetLogAttribute(ObjInfo.DebugTableName, ObjInfo.LogLevel, ObjInfo.LogDays);
            LogManager.SetDBInfo(CObjectInfo.DBInfo);

            string str = $"{this} Created OK";
            WriteLog(str);
        }

        public int GenerateErrorCode(int error, bool writeLog = true, bool useErrorSubMsg = false)
        {
            if (error == SUCCESS) // Success
            {
                ErrorSubMsg = "";
                ErrorCode = SUCCESS;
            } else
            {
                if (useErrorSubMsg == false) ErrorSubMsg = "";
                ErrorCode = (ObjInfo.ID << 16) + (ObjInfo.ErrorBase + error);
            }
            return ErrorCode;
        }

        public override string ToString()
        {
            return $"[Object] ID : {ObjInfo.ID}, Name : {ObjInfo.Name}";
        }

        public int WriteLog(string strLog, ELogType logType = ELogType.Debug, ELogWType writeType = ELogWType.D_Normal, bool ShowOutputWindow = false, int skipFrames = 2)
        {
            // for break when error occured
            if(logType == ELogType.Debug && writeType == ELogWType.D_Error)
            {
                int i = 0;
            }
            return LogManager.WriteLog(strLog, logType, writeType, ShowOutputWindow, skipFrames);
        }

        public void WriteExLog(string strLog, ELogType logType = ELogType.Debug, ELogWType writeType = ELogWType.D_Error, bool ShowOutputWindow = true, int skipFrames = 3)
        {
            LogManager.WriteLog(strLog, logType, writeType, ShowOutputWindow, skipFrames);
        }

        public void Sleep<T>(T gap, ETimeType type = ETimeType.MILLISECOND)
        {
            try
            {
                int time = Convert.ToInt32(gap);
                if (time <= 0) return;
                switch (type)
                {
                    case NANOSECOND:
                        time /= (1000 * 1000);
                        break;

                    case MICROSECOND:
                        time /= 1000;
                        break;

                    case MILLISECOND:
                        break;

                    case SECOND:
                        time *= 1000;
                        break;

                    case MINUTE:
                        time *= (1000 * 60);
                        break;

                    case HOUR:
                        time *= (1000 * 60 * 60);
                        break;
                }

                //int time = Convert.ToInt32(millisec);
                if (time <= 0) return;
                Thread.Sleep(time);
            }catch (Exception ex)
            {
                WriteExLog(ex.ToString());
            }
        }

        protected void Assert(bool condition)
        {
            Debug.Assert(condition);
        }

        protected void WriteLine(object msg)
        {
            Debug.WriteLine(msg.ToString());
        }

    }
}
