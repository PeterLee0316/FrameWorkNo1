using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace Core.Layers
{
    public static class CUtils
    {
        public static class AnimateEffect
        {
            public const int AW_HOR_POSITIVE = 0X1;
            public const int AW_HOR_NEGATIVE = 0X2;
            public const int AW_VER_POSITIVE = 0X4;
            public const int AW_VER_NEGATIVE = 0X8;
            public const int AW_CENTER = 0X10;
            public const int AW_HIDE = 0X10000;
            public const int AW_ACTIVATE = 0X20000;
            public const int AW_SLIDE = 0X40000;
            public const int AW_BLEND = 0X80000;

            [DllImport("user32.dll", CharSet = CharSet.Auto)]
            public static extern int AnimateWindow(IntPtr hwand, int dwTime, int dwFlags);
        }

        public static int IntTryParse(string str)
        {
            int n;
            n = int.TryParse(str, out n) ? n : 0;
            return n;
        }

        public static double RadToDeg(double pRad)
        {
            double mDeg = pRad * 180 / Math.PI;
            return mDeg;
        }

        public static double DegToRad(double pDeg)
        {
            double mRad = pDeg * Math.PI / 180;
            return mRad;
        }


        /*------------------------------------------------------------------------------------
        * Date : 2016.02.24
        * Author : HSLEE
        * Function : GetValue(String Section, String Key, String iniPath)
        * Description : Text File Data Load 처리
        ------------------------------------------------------------------------------------*/
        public static String GetValue(String Section, String Key, String iniPath)
        {
            StringBuilder temp = new StringBuilder(255);
            int i = GetPrivateProfileString(Section, Key, "", temp, 255, iniPath);
            return temp.ToString();
        }

        /*------------------------------------------------------------------------------------
         * Date : 2016.02.24
         * Author : HSLEE
         * Function : SetValue(String Section, String Key, String Value, String iniPath)
         * Description : Text File Data Save 처리
         ------------------------------------------------------------------------------------*/
        public static bool SetValue(String Section, String Key, String Value, String iniPath)
        {
            bool bRet = WritePrivateProfileString(Section, Key, Value, iniPath);
            return WritePrivateProfileString(Section, Key, Value, iniPath);
        }


        [DllImport("kernel32.dll")]
        public static extern int GetPrivateProfileString(
                                    String section,
                                    String key,
                                    String def,
                                    StringBuilder retVal,
                                    int size,
                                    String filePath);

        [DllImport("kernel32.dll")]
        public static extern bool WritePrivateProfileString(
                                    String section,
                                    String key,
                                    String val,
                                    String filePath);
    }


   
}
