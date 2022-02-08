using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.IO;

using static Core.Layers.DEF_Vision;
using static Core.Layers.DEF_Error;
using static Core.Layers.DEF_Common;


namespace Core.Layers
{
    public class MVisionSystem: MObject,IDisposable
    {        
        private int m_iSystemNo;
        private int m_iSystemIndex;
        private int m_iCheckCamNo;

        public int m_iResult { get; private set; }

        //=========================================================
        // System
        private MVisionCamera[] m_pCamera;
        private MVisionView[] m_pDisplay;
        

        //=========================================================
        // Image 

        //=========================================================
        // Pattern Maching 

        //=========================================================
        // Edge Find 


        public MVisionSystem(CObjectInfo objInfo) : base(objInfo)
        {
            m_iSystemNo     =   0;
            m_iSystemIndex  =   0;
            m_iCheckCamNo   =   0;
            m_iResult       =   0;
            m_pCamera = new MVisionCamera[DEF_MAX_CAMERA_NO];
            m_pDisplay = new MVisionView[DEF_MAX_CAMERA_NO];

#if SIM_VISION
            return ;
#endif        
        }

        ~MVisionSystem()
        {
            Dispose();
        }

        public void Dispose()
        {
            freeSystems();
        }

        // Gig-E System을 초기화 한다.
        public int Initialize()
        {                        
            

            // System Init 결과 저장
            m_iResult = SUCCESS;

            return SUCCESS;
        }
        

        public void SelectView(MVisionView m_Display)
        {
            m_pDisplay[m_Display.GetIdNum()] = m_Display;
        }

        public void SelectCamera(MVisionCamera m_Camera)
        {
            m_pCamera[m_Camera.GetCamID()] = m_Camera;
        }

        // System에서 Read한 Cam의 개수를 리턴한다.
        public int GetCamNum()
        {
            return m_iCheckCamNo;
        }
        public void freeSystems()
        {

        }


        public int ReloadModel(int iCamNo, ref CVisionPatternData pSData)
        {
            if (pSData.m_bIsModel == false) return GenerateErrorCode(ERR_VISION_PATTERN_NONE);

            //MIL_ID m_MilImage = MIL.M_NULL;
            //// Image Load...
            //string strLoadFileName = pSData.m_strFilePath + pSData.m_strFileName;
            //if (File.Exists(strLoadFileName))
            //    MIL.MbufRestore(strLoadFileName, m_MilSystem, ref m_MilImage);
            //else
            //    return GenerateErrorCode(ERR_VISION_PATTERN_NONE);

            ////Draw할 Rec을 생성한다.
            //Rectangle pRec = new Rectangle(pSData.m_rectModel.X - pSData.m_rectModel.Width / 2,
            //                               pSData.m_rectModel.Y - pSData.m_rectModel.Height / 2,
            //                               pSData.m_rectModel.Width, pSData.m_rectModel.Height);

            //// Allocate a normalized grayscale model.
            //MIL.MpatAllocModel(m_MilSystem, m_MilImage, pRec.X, pRec.Y,
            //                   pRec.Width, pRec.Height, MIL.M_NORMALIZED, ref pSData.m_milModel);

            //// Model Image Save (Image View Save용)
            //MIL.MbufAlloc2d(m_MilSystem, pRec.Width, pRec.Height, MIL.M_UNSIGNED + 8, MIL.M_IMAGE + MIL.M_PROC + MIL.M_DISP,
            //                    ref pSData.m_ModelImage);
            //MIL.MbufCopyColor2d(m_MilImage, pSData.m_ModelImage, MIL.M_ALL_BANDS, pRec.X, pRec.Y,
            //                   MIL.M_ALL_BANDS, 0, 0, pRec.Width, pRec.Height);

            //if (pSData.m_milModel == MIL.M_NULL) return GenerateErrorCode(ERR_VISION_PATTERN_NONE);

            //MIL.MpatAllocResult(m_MilSystem, MIL.M_DEFAULT, ref m_SearchResult);

            //// Set the search accuracy to high.
            //MIL.MpatSetAccuracy(pSData.m_milModel, MIL.M_HIGH);

            //MIL.MpatSetAcceptance(pSData.m_milModel, pSData.m_dAcceptanceThreshold);  // Acceptance Threshold Setting
            //MIL.MpatSetCertainty(pSData.m_milModel, pSData.m_dAcceptanceThreshold);   // Set Certainty Threshold
            //MIL.MpatSetCenter(pSData.m_milModel,                                      // Pattern Mark에서 Offset 설정함.
            //                  (double)pSData.m_pointReference.X,
            //                  (double)pSData.m_pointReference.Y);

            //// Set the search model speed to high.
            //MIL.MpatSetSpeed(pSData.m_milModel, MIL.M_HIGH);

            //// Preprocess the model.
            //MIL.MpatPreprocModel(m_MilImage, pSData.m_milModel, MIL.M_DEFAULT);            

            return SUCCESS;

        }

        public bool RegisterMarkModel(int iCamNo, ref CVisionPatternData pSData)
        {
            // 0 위치를 화면의 중앙으로 설정함.
            //pSData.m_rectModel.X = m_pDisplay[iCamNo].GetImageWidth() / 2;
            //pSData.m_rectModel.Y = m_pDisplay[iCamNo].GetImageHeight() / 2;

            //MIL_ID m_MilImage = m_pDisplay[iCamNo].GetImage();
            //MIL_ID m_DisplayGraph = m_pDisplay[iCamNo].GetViewGraph();

            ////Draw할 Rec을 생성한다.
            //Rectangle pRec = new Rectangle(pSData.m_rectModel.X - pSData.m_rectModel.Width / 2,
            //                               pSData.m_rectModel.Y - pSData.m_rectModel.Height / 2,
            //                               pSData.m_rectModel.Width, pSData.m_rectModel.Height);

            //// Allocate a normalized grayscale model.
            //MIL.MpatAllocModel(m_MilSystem, m_MilImage, pRec.X, pRec.Y,
            //                   pRec.Width, pRec.Height, MIL.M_NORMALIZED, ref pSData.m_milModel);

            //// Model Image Save (Image View Save용)
            //MIL.MbufAlloc2d(m_MilSystem, pRec.Width, pRec.Height, MIL.M_UNSIGNED + 8, MIL.M_IMAGE + MIL.M_PROC + MIL.M_DISP,
            //                    ref pSData.m_ModelImage);
            //MIL.MbufCopyColor2d(m_MilImage, pSData.m_ModelImage, MIL.M_ALL_BANDS, pRec.X, pRec.Y,
            //                   MIL.M_ALL_BANDS,0,0, pRec.Width, pRec.Height);            

            //if (pSData.m_milModel == MIL.M_NULL) return false;

            //MIL.MpatAllocResult(m_MilSystem, MIL.M_DEFAULT, ref m_SearchResult);

            //// Set the search accuracy to high.
            //MIL.MpatSetAccuracy(pSData.m_milModel, MIL.M_HIGH);

            //MIL.MpatSetAcceptance(pSData.m_milModel, pSData.m_dAcceptanceThreshold);  // Acceptance Threshold Setting
            //MIL.MpatSetCertainty(pSData.m_milModel, pSData.m_dAcceptanceThreshold);   // Set Certainty Threshold
            //MIL.MpatSetCenter(pSData.m_milModel,                                      // Pattern Mark에서 Offset 설정함.
            //                  (double)pSData.m_pointReference.X,
            //                  (double)pSData.m_pointReference.Y);

            //// Set the search model speed to high.
            //MIL.MpatSetSpeed(pSData.m_milModel, MIL.M_HIGH);

            ////================================================================================================
            ////// Angle 설정 
            ////MIL.MpatSetAngle(pSData.m_milModel, MIL.M_SEARCH_ANGLE_MODE, MIL.M_ENABLE);
            ////MIL.MpatSetAngle(pSData.m_milModel, MIL.M_SEARCH_ANGLE_DELTA_NEG, 3.0);
            ////MIL.MpatSetAngle(pSData.m_milModel, MIL.M_SEARCH_ANGLE_DELTA_POS, 3.0);
            ////MIL.MpatSetAngle(pSData.m_milModel, MIL.M_SEARCH_ANGLE_ACCURACY, 0.25);
            ////================================================================================================

            //// Preprocess the model.
            //MIL.MpatPreprocModel(m_MilImage, pSData.m_milModel, MIL.M_DEFAULT);

            //// Draw a box around the model in the model image.
            
            //MIL.MpatDraw(MIL.M_DEFAULT, pSData.m_milModel, m_DisplayGraph,
            //             MIL.M_DRAW_BOX , MIL.M_DEFAULT, MIL.M_ORIGINAL);

            // Save Image Bitmap

            return true;

        }

        /// <summary>
        /// SetEdgeFindParameter
        /// Edge검출 영역 설정
        /// </summary>
        /// <param name="mPos"></param>
        /// <param name="dWidth"></param>
        /// <param name="dHeight"></param>
        /// <param name="dAng"></param>
        /// <returns></returns>
        public int SetEdgeFindParameter(Point mPos, double dWidth,double dHeight,double dAng)
        {
            

            return SUCCESS;
        }

               

    }
}
