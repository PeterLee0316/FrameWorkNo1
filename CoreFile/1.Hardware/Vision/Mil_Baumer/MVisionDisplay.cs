using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Diagnostics;

using static Core.Layers.DEF_Vision;
using static Core.Layers.DEF_Error;
using static Core.Layers.DEF_Common;

namespace Core.Layers
{
    public class MVisionView: MObject
    {
        public int m_iResult { get; private set; }

        private MVisionCamera m_pCamera;
        private int m_iViewID;        
        private bool m_bLocal;
        private double dGrabInterval = 0.0;
        
        private Byte[] m_ImgBits;
        private IntPtr m_ImageBuffer;
        private IntPtr m_ImageHandle;

        private int m_CameraWidth;
        private int m_CameraHeight;
        
        private Rectangle m_recImage;
        private PictureBox m_Picture;

        // Overlay 관련 변수
        private IntPtr m_hCustomDC = IntPtr.Zero;
        private Graphics m_DrawGraph;
        private Pen m_DrawPen;    
        
        private Point m_ptDrawStart;
        private Point m_ptDrawEnd;

        public MVisionView(CObjectInfo objInfo) : base(objInfo)
        {
            m_iViewID   = 0;
            m_Picture   = new PictureBox();

            m_ImageHandle = new IntPtr();
            m_ImageBuffer = new IntPtr();
            
            m_DrawPen = new Pen(Color.LightGreen);
            m_DrawPen.DashStyle = System.Drawing.Drawing2D.DashStyle.DashDot;
            m_ptDrawStart = new Point(0, 0);
            m_ptDrawEnd = new Point(0, 0);

        }

        public int Initialize(int iViewNo, MVisionCamera pCamera)
        {
            // Num 설정
            m_iViewID = iViewNo;
            

            // Camera Select
            if (SelectCamera(pCamera) == SUCCESS)
            {
                // System Init 결과 저장
                m_iResult = SUCCESS;

                return SUCCESS;
            }
            else
            {
                return GenerateErrorCode(ERR_VISION_CAMERA_CREATE_FAIL);
            }
            
        }
        
        public int GetIdNum()
        {
            return m_iViewID;
        }

        /// <summary>
        /// SelectCamera: 카메라의 영상의 크기로 Byte array 크기를 할당하고,
        /// Mil 변수에 영상을 할당한다.
        /// </summary>
        /// <param name="pCamera"></param>
        /// <returns></returns>
        public int SelectCamera(MVisionCamera pCamera)
        {
            if (pCamera == null)  return GenerateErrorCode(ERR_VISION_CAMERA_NON_USEFUL);

            Size CameraFovSize;

            // View의 Camera 주소에 객체를 대입한다.
            m_pCamera = pCamera;

            // Camera Pixel Size 대입
            CameraFovSize = m_pCamera.GetCameraPixelNum();

            m_CameraWidth = CameraFovSize.Width;
            m_CameraHeight = CameraFovSize.Height;

            if (m_CameraWidth == 0 || m_CameraHeight == 0) return GenerateErrorCode(ERR_VISION_CAMERA_IMAGE_SIZE_FAIL);

            // image byte 변수
            m_ImgBits = new Byte[m_CameraWidth * m_CameraHeight];

            // set source image size Rect size 
            m_recImage.X = 0;
            m_recImage.Y = 0;
            m_recImage.Width = m_CameraWidth;
            m_recImage.Height = m_CameraHeight;            

            return SUCCESS;
        }


        /// <summary>
        /// Pattern Search한 결과를 View의 화면에 String으로 표시한다.
        /// </summary>
        private void DisplaySearchResult()
        {
            double XOrg = 0.0;              // Original model position.
            double YOrg = 0.0;
            double x = 0.0;                 // Model position.
            double y = 0.0;
            double ErrX = 0.0;              // Model error position.
            double ErrY = 0.0;
            double Score = 0.0;             // Model correlation score.

            // Read results and draw a box around the model occurrence.
            //MIL.MpatGetResult(m_SearchResult, MIL.M_POSITION_X, ref x);
            //MIL.MpatGetResult(m_SearchResult, MIL.M_POSITION_Y, ref y);
            //MIL.MpatGetResult(m_SearchResult, MIL.M_SCORE, ref Score);


            String strResult = "";

            strResult = String.Format(" Pos X : {0:0.00} \n Pos Y : {1:0.00} \n Score : {2:0.00}", ErrX, ErrY, Score);            

            DrawString(strResult, new PointF(100, 800));

        }


        /// <summary>
        ///  현재 Grab하는 Image를 저장함.
        /// </summary>
        /// <param name="strPath"></param>
        /// <returns></returns>
        public bool SaveImage(string strPath)
        {

            
            return true;
        }

        /// <summary>
        /// SaveModelImage: Model Image를 저장함.
        /// </summary>
        /// <param name="strPath"></param>
        /// <param name="iModelNo"></param>
        /// <returns></returns>
        public bool SaveModelImage(string strPath,int iModelNo)
        {
            CVisionPatternData pSData = m_pCamera.GetSearchData(iModelNo);
           
            
            return true;
        }
        

        public void DrawLine(Point ptStart, Point ptEnd, Pen pPen)
        {            
            double dStartX = (double)ptStart.X;
            double dStartY = (double)ptStart.Y;
            double dEndX = (double)ptEnd.X; 
            double dEndY = (double)ptEnd.Y;
            
        }

        public void GraphDrawLine(Point ptStart, Point ptEnd, Pen pPen)
        {
            // Overlay DC를 가져 온다
            if (GetOverlayDC() == false) return;

            double dStartX = (double)ptStart.X;
            double dStartY = (double)ptStart.Y;
            double dEndX = (double)ptEnd.X;
            double dEndY = (double)ptEnd.Y;

            m_DrawGraph.DrawLine(pPen,ptStart,ptEnd);

            // Overlay 화면 갱신
            UpdataOverlay();
        }

        public void GraphDrawCircle(Point ptPos, int radius, Pen pPen)
        {
            // Overlay DC를 가져 온다
            if (GetOverlayDC() == false) return;

            Rectangle recCircle = new Rectangle();
            recCircle.X = ptPos.X;
            recCircle.Y = ptPos.Y;
            recCircle.Width = radius;
            recCircle.Height = radius;

            m_DrawGraph.DrawEllipse(pPen, recCircle);

            // Overlay 화면 갱신
            UpdataOverlay();
        }

        public void DrawCrossMark(Point Center, int Width, int Height,Color colorMark)
        {
            // Overlay DC를 가져 온다
            if (GetOverlayDC() == false) return;

            //==================================================
            // Pen Type 설정
            m_DrawPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            m_DrawPen.Color = colorMark;
            m_DrawPen.Width = 4;

            //==================================================
            // 중심 라인 Draw
             
            // Width 라인 Draw
            m_ptDrawStart.X = Center.X - Width / 2;
            m_ptDrawStart.Y = Center.Y;
            m_ptDrawEnd.X = Center.X + Width / 2;
            m_ptDrawEnd.Y = Center.Y;
            GraphDrawLine(m_ptDrawStart, m_ptDrawEnd, m_DrawPen);

            // Hight 라인 Draw
            m_ptDrawStart.X = Center.X;
            m_ptDrawStart.Y = Center.Y - Height/2;
            m_ptDrawEnd.X = Center.X;
            m_ptDrawEnd.Y = Center.Y + Height / 2;
            GraphDrawLine(m_ptDrawStart, m_ptDrawEnd, m_DrawPen);

            // Overlay 화면 갱신
            UpdataOverlay();
        }

        public void DrawCrossMark(int Width, int Height,Color colorMark)
        {
            // Overlay DC를 가져 온다
            if (GetOverlayDC() == false) return;

            Point posCenter = new Point(0, 0);

            //==================================================
            // 중심 라인 Draw
            

            //==================================================
            // Pen Type 설정
            m_DrawPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            m_DrawPen.Color = colorMark;
            m_DrawPen.Width = 4;

            //==================================================
            // 중심 라인 Draw

            // Width 라인 Draw
            m_ptDrawStart.X = posCenter.X - Width / 2;
            m_ptDrawStart.Y = posCenter.Y;
            m_ptDrawEnd.X   = posCenter.X + Width / 2;
            m_ptDrawEnd.Y   = posCenter.Y;
            GraphDrawLine(m_ptDrawStart, m_ptDrawEnd, m_DrawPen);

            // Hight 라인 Draw
            m_ptDrawStart.X = posCenter.X;
            m_ptDrawStart.Y = posCenter.Y - Height / 2;
            m_ptDrawEnd.X   = posCenter.X;
            m_ptDrawEnd.Y   = posCenter.Y + Height / 2;
            GraphDrawLine(m_ptDrawStart, m_ptDrawEnd, m_DrawPen);

            // Overlay 화면 갱신
            UpdataOverlay();

            //DrawCrossMark(posCenter, Width, Height, colorMark);

        }

        public void DrawBox(Size recBox)
        {
            // Overlay DC를 가져 온다
            if (GetOverlayDC() == false) return;

            Point recCenter = new Point(0, 0);

            //==================================================
            // Pen Type 설정
            m_DrawPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            m_DrawPen.Color = Color.Red;
            m_DrawPen.Width = 2.0f;
            
            
            //Draw할 Rec을 생성한다.
            Rectangle pRec = new Rectangle(recCenter.X, recCenter.Y, recBox.Width, recBox.Height);           
            
            m_DrawGraph.DrawRectangle(m_DrawPen, pRec);

            // Overlay 화면 갱신
            UpdataOverlay();
        }

        public void DrawString( string pStr, PointF pPos)
        {
            // Overlay DC를 가져 온다
            if (GetOverlayDC() == false) return;

            SolidBrush Brush = new SolidBrush(Color.Red);
            Font OverlayFont = new Font(FontFamily.GenericSansSerif, 40, FontStyle.Bold);
            if (m_DrawGraph == null)
            {
                UpdataOverlay();
                return;
            }
            m_DrawGraph.DrawString(pStr, OverlayFont, Brush, pPos);
            // Overlay 화면 갱신
            UpdataOverlay();            
        }
        public void DrawGrid()
        {

            
        }        

        public void DrawHairLine(int Width)
        {
            if (Width < DEF_HAIRLINE_MIN) return;
            if (Width > DEF_HAIRLINE_MAX) return;

            // Overlay DC를 가져 온다
            if (GetOverlayDC() == false) return;

            //==================================================
            // Pen Type 설정
            //MIL.MgraColor(m_MilOverLayID, MIL.M_COLOR_GREEN);
            m_DrawPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            //m_DrawPen.DashCap = System.Drawing.Drawing2D.DashCap.Round;
            m_DrawPen.Color = Color.Red;
            m_DrawPen.Width = 3;

            //==================================================
            // 중심 라인 Draw     
            


            //==================================================
            // Pen Type 설정
            m_DrawPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            //m_DrawPen.DashCap = System.Drawing.Drawing2D.DashCap.Round;
            m_DrawPen.DashPattern = new float[] { 5.0F, 2.0F, 1.0F, 2.0F };
            m_DrawPen.Color = Color.LightGreen;
            m_DrawPen.Width = 4;

            //==================================================
            // 점선 라인 Draw


            // Overlay 화면 갱신
            UpdataOverlay();
        }


        public void DrawCircle(int radius)
        {

            if (radius < DEF_CIRCLE_MIN) return;
            if (radius > DEF_CIRCLE_MAX) return;

            // Overlay DC를 가져 온다
            if (GetOverlayDC() == false) return;

            //==================================================
            // Pen Type 설정
            //MIL.MgraColor(m_MilOverLayID, MIL.M_COLOR_GREEN);
            m_DrawPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            //m_DrawPen.DashCap = System.Drawing.Drawing2D.DashCap.Round;
            m_DrawPen.Color = Color.Red;
            m_DrawPen.Width = 2.0f;

            //==================================================
            // 중심 라인 Draw     
            

            //==================================================
            // Pen Type 설정
            m_DrawPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            //m_DrawPen.DashCap = System.Drawing.Drawing2D.DashCap.Round;
            m_DrawPen.DashPattern = new float[] { 5.0F, 2.0F, 1.0F, 2.0F };
            m_DrawPen.Color = Color.Red;
            m_DrawPen.Width = 2.0f;

            //==================================================
            // Circle 라인 Draw

            // Overlay 화면 갱신
            UpdataOverlay();
        }

        public void SetDisplayWindow(IntPtr pDisplayObject)
        {
            double ZoomX;
            double ZoomY;
            if (pDisplayObject == null) return;
            // Display하는 Panel의 사이즈를 읽어온다.
            Size DisplaySize = ContainerControl.FromHandle(pDisplayObject).Size;
            

            SetLocalOverlay(true);

            // 기존 Overlay Clear
            ClearOverlay();

            m_bLocal = true;

        }
        
        public bool IsLocalView()
        {
            // 할당된 Picture Handle이 없을 경우 
            if (m_Picture.Handle == IntPtr.Zero)
                return false;
            else
                return true;
        }
        

        public void ClearOverlay()
        {
        }
        public bool GetOverlayDC()
        {

            return true;
        }
        public void UpdataOverlay()
        {
        }

        public void FreeLocalOverlay()
        {
            
        }
        public int SetLocalOverlay(bool milSystem)
        {
            

            return 0;
        }
                
        public void DestroyLocalView()
        {
            // Picture Handle값 초기화
            m_ImageHandle = IntPtr.Zero;
            

            m_bLocal = false;

        }

        public IntPtr GetViewHandle()
        {
            return m_ImageHandle;
        }       

        public void FreeDisplay()
        {

#if SIM_VISION
                return ;
#endif
            DestroyLocalView();

            try
            {
                m_hCustomDC = IntPtr.Zero;
            }
            catch { }
        }
    }
}
