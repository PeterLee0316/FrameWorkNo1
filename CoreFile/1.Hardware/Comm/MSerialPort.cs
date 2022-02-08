using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;


using static Core.Layers.DEF_SerialPort;
using static Core.Layers.DEF_Common;
using static Core.Layers.DEF_Error;

using Core.UI;

namespace Core.Layers
{
    public class MSerialPort : MObject, ISerialPort, IDisposable
    {
        CSerialPortData m_Data;
       
        SerialPort m_SerialPort;
        Queue<string> m_ReceivedQueue = new Queue<string>();

        public MSerialPort(CObjectInfo objInfo, CSerialPortData data) 
            : base(objInfo)
        {
            SetData(data);

            Initialize();
        }

        ~MSerialPort()
        {
            Dispose();
        }

        #region Common : Manage Data, Position, Use Flag and Initialize
        public int SetData(CSerialPortData source)
        {
            m_Data = ObjectExtensions.Copy(source);
            return SUCCESS;
        }

        public int GetData(out CSerialPortData target)
        {
            target = ObjectExtensions.Copy(m_Data);

            return SUCCESS;
        }
        #endregion

        public void Dispose()
        {
            ClosePort();
        }

        public bool IsOpened()
        {
            return m_SerialPort?.IsOpen ?? false;
        }

        private int Initialize()
        {
            try
            {
                m_SerialPort = new SerialPort(m_Data.PortName, m_Data.BaudRate, m_Data._Parity,
                    m_Data.DataBits, m_Data._StopBits);

               // m_SerialPort.DataReceived += new SerialDataReceivedEventHandler(SerialPortReceiveEvent);
            }
            catch (Exception ex)
            {
                WriteExLog(ex.ToString());
                return GenerateErrorCode(ERR_SERIALPORT_CREATEPORT_FAIL);
            }

            return SUCCESS;
        }

        public void SerialPortReceiveEvent(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                //SerialPort sp;
                //sp = (SerialPort)sender;

                //string message = m_SerialPort.ReadLine();

                //message.Trim();

                //if (message.Length > 0)
                //{
                //    m_ReceivedQueue.Enqueue(message);
                //}

                byte[] byteData = new byte[1024];
                int byteCount = 0;
                string strText = "";
                byteCount = m_SerialPort.Read(byteData, 0, 1024);

                for (int i = 0; i < byteCount; i++)
                {
                    strText += (char)byteData[i];
                }

                strText.Trim();

                m_ReceivedQueue.Enqueue(strText);
            }
            catch (Exception ex)
            {
                WriteExLog(ex.ToString());
                //return GenerateErrorCode(ERR_SERIALPORT_CREATEPORT_FAIL);
            }
        }


        public int OpenPort()
        {
            try
            {
                if (IsOpened() == false)
                {
                    m_SerialPort?.Open();

                    m_SerialPort.DataReceived += new SerialDataReceivedEventHandler(SerialPortReceiveEvent);
                }
            }
            catch (Exception ex)
            {
                WriteExLog(ex.ToString());
                return GenerateErrorCode(ERR_SERIALPORT_CREATEPORT_FAIL);
            }
            return SUCCESS;
        }

        public int ClosePort()
        {
            try
            {
                if (IsOpened() == true)
                {
                    m_SerialPort.Close();

                    m_SerialPort.DataReceived -= new SerialDataReceivedEventHandler(SerialPortReceiveEvent);
                }
            }
            catch (Exception ex)
            {
                WriteExLog(ex.ToString());
                return GenerateErrorCode(ERR_SERIALPORT_CLOSEPORT_FAIL);
            }
            return SUCCESS;
        }

        public int SendMessage(string message)
        {
            if(IsOpened() == false)
            {
                return GenerateErrorCode(ERR_SERIALPORT_CLOSEPORT_FAIL);
            }

            try
            {
                //m_SerialPort.WriteLine(message);
                m_SerialPort?.Write(message);
            }
            catch (Exception ex)
            {
                WriteExLog(ex.ToString());
                return GenerateErrorCode(ERR_SERIALPORT_SENDMESSAGE_FAIL);
            }

            return SUCCESS;
        }

        public int ReceiveMessage(out string message, out int leftQueueSize)
        {
            leftQueueSize = m_ReceivedQueue.Count;
            if(leftQueueSize == 0)
            {
                message = "";
                return GenerateErrorCode(ERR_SERIALPORT_RECEIVEDQUE_EMPTY);
            }

            message = m_ReceivedQueue.Dequeue();

            return SUCCESS;
        }

        public void ClearReceiveQue()
        {
            m_ReceivedQueue.Clear();
        }

    }
}
