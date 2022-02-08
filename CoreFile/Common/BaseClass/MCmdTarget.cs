using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Messaging;
using System.Diagnostics;

using static Core.Layers.DEF_Thread;
using static Core.Layers.DEF_Common;

namespace Core.Layers
{
    public abstract class MCmdTarget : MObject
    {
        // Message Queue
        private Queue<MEvent>  m_eventQ;
        private object _Lock = new object();
        private bool m_bWriteDebug = false;

        public MCmdTarget(CObjectInfo objInfo) : base(objInfo)
        {
            m_eventQ = new Queue<MEvent>();
        }

        /// <summary>
        /// message 전송 함수로 event로 만들어서 전송한다.
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="wParam"></param>
        /// <param name="lParam"></param>
        /// <returns></returns>
        public int PostMsg(int msg, int wParam = -1, int lParam = -1)
		{ 
            return PostMsg(new MEvent(msg, wParam, lParam)); 
        }

        /// <summary>
        /// 전송받은 이벤트를 이벤트 큐에 보관.
        /// </summary>
        /// <param name="evnt"></param>
        /// <returns></returns>
        protected int PostMsg(MEvent evnt)
        {
            lock(_Lock)
            {
                if(m_bWriteDebug)
                Debug.WriteLine("[EnQueue] " + evnt.ToString());

                m_eventQ.Enqueue(evnt);
            }
            Sleep(ThreadSleepTime); // for processing multi thread
            return SUCCESS;
        }

        /// <summary>
        /// 자기 자신에게 메세지를 보냄 (자신의 Event Handler를 호출함)
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="wParam"></param>
        /// <param name="lParam"></param>
        /// <returns></returns>
        protected int SendMsg(int msg, int wParam = -1, int lParam = -1)
        { 
            return SendMsg(new MEvent(msg, wParam, lParam)); 
        }

        public int SendMsg(EThreadMessage msg, int wParam = -1, int lParam = -1)
        {
            return SendMsg((int)msg, wParam, lParam);
        }

        /// <summary>
        /// 자기 자신에게 메세지를 보냄 (자신의 Event Handler를 호출함)
        /// </summary>
        /// <param name="evnt"></param>
        /// <returns></returns>
        public int SendMsg(MEvent evnt)
        {
	        return ProcessMsg( evnt );
        }

        /// <summary>
        /// event의 처리를 담당하는 함수 
        /// </summary>
        /// <param name="evnt"></param>
        /// <returns></returns>
        protected virtual int ProcessMsg(MEvent evnt)
        {
            if(m_bWriteDebug)
                Debug.WriteLine("Process " + evnt.ToString());

            return SUCCESS;
        }
        
        /// <summary>
        /// event queue를 검사하여 새로운 event가 있으면 꺼내서 처리해준다.
        /// </summary>
        /// <param name="nMsgCount"></param>
        protected virtual void CheckMsg(int nMsgCount = 2)
        {
            while (m_eventQ.Count > 0)
            {
                MEvent evnt = GetMsg();
                if (evnt == null)
                {
                    break;
                }

                ProcessMsg(evnt);
                if (--nMsgCount <= 0)
                {
                    break;
                }
            }
        }

        /// <summary>
        /// event queue에서 event를 꺼내 return
        /// </summary>
        /// <returns></returns>
        MEvent GetMsg() 
        {
            lock (_Lock)
            {
                try
                {
                    MEvent evnt = (MEvent)m_eventQ.Dequeue();
                    if (m_bWriteDebug)
                        Debug.WriteLine("[DeQueue] " + evnt.ToString());
                    return evnt;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }

                return null;
            }
        }
    }
}
