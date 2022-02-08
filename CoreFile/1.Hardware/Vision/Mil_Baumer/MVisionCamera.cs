using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;

using static Core.Layers.DEF_Vision;
using static Core.Layers.DEF_Error;
using static Core.Layers.DEF_Common;


namespace Core.Layers
{
    public class MVisionCamera: MObject
    {        
        public int m_iResult { get; private set; }

        private int m_iCamID;
        private MVisionView m_pDisplay;

        public Size m_CamPixelNum;
        private CCameraPara m_cCameraData;
        private CVisionPatternData[] m_rgsCSearchData;
        private CResultData[] m_rgsCResultData;
        private bool m_bLive;
        
        public MVisionCamera(CObjectInfo objInfo): base(objInfo)
        {
            int iMarkNum = (int)EPatternMarkType.ALIGN_MARK_COUNT;

            m_iCamID            = 0;
            m_iResult           = 0;
            m_bLive             = false;
            m_cCameraData       = new CCameraPara();
            m_rgsCSearchData    = new CVisionPatternData[iMarkNum];
            m_rgsCResultData    = new CResultData[iMarkNum];

            for (int i = 0; i < iMarkNum; i++)
            {
                // Search Data Init
                m_rgsCSearchData[i] = new CVisionPatternData();
                m_rgsCSearchData[i].m_bIsModel = false;
                m_rgsCSearchData[i].m_dAcceptanceThreshold = DEF_DEFAULT_ACCEP_THRESHOLD;
                m_rgsCSearchData[i].m_dCertaintyThreshold  = DEF_DEFAULT_CERTAIN_THRESHOLD;

                // Result Data Init
                m_rgsCResultData[i] = new CResultData();
                m_rgsCResultData[i].m_bSearchSuccess = false;
            }

            m_cCameraData.m_iGrabSettlingTime = 0;
            m_cCameraData.m_iCameraChangeTime = 0;
        }


        public int FreeCamera()
        {
            return 0;
        }
        
        /// <summary>
        /// SelectView : View와 카메라와 연결함
        /// </summary>
        /// <param name="m_Display"></param>
        /// <returns></returns>
        public int SelectView(MVisionView m_Display)
        {
            // Display 객체를 받아온다
            m_pDisplay = m_Display;

#if SIM_VISION
            return SUCCESS;
#endif

            return SUCCESS;
        }

        /// <summary>
        /// GetCamDeviceInfo : 카메라 정보를 반환한다.
        /// </summary>
        /// <returns></returns>
        public CCameraPara GetCamDeviceInfo()
        {
            return m_cCameraData;
        }

        /// <summary>
        /// GetCameraPixelNum: 카메라 영상의 가로/세로 사이즈를 반환한다
        /// </summary>
        /// <returns></returns>
        public Size GetCameraPixelNum()
        {
            return m_CamPixelNum;
        }
        /// <summary>
        /// 현재 Image를 보내준다.
        /// </summary>
        /// <returns></returns>
        
        public int GetCamID()
        {
            return m_iCamID;
        }
        /// <summary>
        /// SetLive: 카메라의 Live 상태를 지령한다.
        /// </summary>
        /// <param name="bRun"></param>
        public void SetLive(bool bRun)
        {
            int iRes;
            if (bRun == true)
            {
                //iRes = m_Camera.setStart(true);
                // Trigger Mode Off
                //m_Camera.setTrigger(false);
            }

            else
            {
                //iRes = m_Camera.setStart(false);
            }

            SetLiveFlag(bRun);

        }
        /// <summary>
        /// SetLiveFlag : 카메라 Live 상태 Bit를 설정한다.
        /// </summary>
        /// <param name="bLive"></param>
        public void SetLiveFlag(bool bLive)
        {
            m_bLive = bLive;
        }

        /// <summary>
        /// IsLive : 카메라의 Live 상태를 반환한다.
        /// </summary>
        /// <returns></returns>
        public bool IsLive()
        {
            return m_bLive;
        }

        /// <summary>
        /// SetTrigger: 카메라의 Trigger를 설정한다. (SW 방식)
        /// </summary>
        public void SetTrigger()
        {
            
        }
        /// <summary>
        /// MirrorImage: 영상의 이미지를 반전한다.
        /// </summary>
        public void MirrorImage()
        {
            // $$$ 기능 추가
            //return 0;
        }

        /// <summary>
        /// SetSearchData: Search할 Data를 카메라 내부로 할당한다.
        /// </summary>
        /// <param name="iModelNo"></param>
        /// <param name="pSearchData"></param>
        /// <returns></returns>
        public bool SetSearchData(int iModelNo, CSearchData pSearchData)
        {
            m_rgsCSearchData[iModelNo].m_bIsModel = pSearchData.m_bIsModel;
            m_rgsCSearchData[iModelNo].m_dAcceptanceThreshold = pSearchData.m_dAcceptanceThreshold;
            m_rgsCSearchData[iModelNo].m_pointReference = pSearchData.m_pointReference;
            m_rgsCSearchData[iModelNo].m_rectModel = pSearchData.m_rectModel;
            m_rgsCSearchData[iModelNo].m_rectSearch = pSearchData.m_rectSearch;
            m_rgsCSearchData[iModelNo].m_strFileName = pSearchData.m_strFileName;
            m_rgsCSearchData[iModelNo].m_strFilePath = pSearchData.m_strFilePath;
            
            return true;
        }
        /// <summary>
        /// GetSearchData: 할당된 Search Data를 반환한다.
        /// </summary>
        /// <param name="iModelNo"></param>
        /// <returns></returns>
        public CVisionPatternData GetSearchData(int iModelNo)
        { 
            return m_rgsCSearchData[iModelNo];
        }
        /// <summary>
        /// GetResultData: Pattern Search한 결과를 반환한다.
        /// </summary>
        /// <param name="iModelNo"></param>
        /// <returns></returns>
        public CResultData GetResultData(int iModelNo)
        {
            return m_rgsCResultData[iModelNo];
        }


        public void DeleteSearchModel(int iModelNo)
        {
            CVisionPatternData pSData = m_rgsCSearchData[iModelNo];
                       
        }

        public void RemoveModel()
        {
            //return 0;
        }
        
        public int GetGrabSettlingTime()
        {
            return m_cCameraData.m_iGrabSettlingTime;
        }
        public void SetGrabSettlingTime(int iGrabSettlingTime)
        {
            m_cCameraData.m_iGrabSettlingTime = iGrabSettlingTime;            
        }

        public int GetCameraChangeTime()
        {
            return m_cCameraData.m_iCameraChangeTime;
        }
        public void SetCameraChangeTime(int iCameraChangeTime)
        {
            m_cCameraData.m_iCameraChangeTime = iCameraChangeTime;            
        }
    }
}
