using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static Core.Layers.DEF_Error;
using static Core.Layers.DEF_Common;
using static Core.Layers.DEF_MeStage;
using static Core.Layers.DEF_System;
using Core.UI;

namespace Core.Layers
{
    public class DEF_MeStage
    {
        #region Data Define

        // Error Define
        public const int ERR_STAGE_NOT_ORIGIN_RETURNED                      = 1;
        public const int ERR_STAGE_UNABLE_TO_USE_CYL                        = 2;
        public const int ERR_STAGE_UNABLE_TO_USE_VCC                        = 3;
        public const int ERR_STAGE_UNABLE_TO_USE_AXIS                       = 4;



        /// <summary>
        /// Camera Unit Position
        /// </summary>
        public enum ECameraPos
        {
            NONE = -1,
            INSPEC_FOCUS,
            FINE_FOCUS,
            MAX,
        }



        public enum EStagePos
        {
            NONE = -1,
            STAGE_CENTER_PRE,       // Stage의 Center 위치 (Pre Cam 기준)
            STAGE_CENTER_FINE,      // Stage의 Center 위치 (Fine Cam 기준)
            STAGE_CENTER_INSPECT,   // Stage의 Center 위치 (Inspect Cam 기준)

            MAX,
        }


        public class CMeStageRefComp
        {

        }

        public class CMeStageData
        {

            public CMeStageData()
            {
            }            
        }

#endregion
    }

    public class MFuncAlignment : MFunctionalLayer
    {
        private CMeStageRefComp m_RefComp;
        private CMeStageData m_Data;


        public MFuncAlignment(CObjectInfo objInfo, CMeStageRefComp refComp, CMeStageData data)
            : base(objInfo)
        {
            m_RefComp = refComp;
            SetData(data);
        }

        // Data & Flag 설정
        #region Data & Flag 설정
        public int SetData(CMeStageData source)
        {
            m_Data = ObjectExtensions.Copy(source);
            return SUCCESS;
        }

        public int GetData(out CMeStageData target)
        {
            target = ObjectExtensions.Copy(m_Data);

            return SUCCESS;
        }

        #endregion
        

        

    }
}
