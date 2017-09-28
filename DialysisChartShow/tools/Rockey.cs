using System;
using System.Text;
using System.Net;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.Threading.Tasks;
using Ext.Net;
using System.Data;
using System.Configuration;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Runtime.InteropServices;
using NLog;


namespace Dialysis_Chart_Show.tools
{
    public class Rockey4ND
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private static ushort p1, p2, p3, p4;

        [DllImport("Rockey4ND.dll")]
        static extern ushort Rockey(ushort function, out ushort handle, out uint lp1, out uint lp2, out ushort p1, out ushort p2, out ushort p3, out ushort p4, ref byte buffer);

        const ushort RY_FIND = 1;
        const ushort RY_FIND_NEXT = 2;
        const ushort RY_OPEN = 3;
        const ushort RY_CLOSE = 4;
        const ushort RY_READ = 5;
        const ushort RY_WRITE = 6;
        const ushort RY_RANDOM = 7;
        const ushort RY_SEED = 8;
        const ushort RY_WRITE_USERID = 9;
        const ushort RY_READ_USERID = 10;
        const ushort RY_SET_MOUDLE = 11;
        const ushort RY_CHECK_MOUDLE = 12;
        const ushort RY_WRITE_ARITHMETIC = 13;
        const ushort RY_CALCULATE1 = 14;
        const ushort RY_CALCULATE2 = 15;
        const ushort RY_CALCULATE3 = 16;
        const ushort RY_DECREASE = 17;

        ushort[] m_Handle = new ushort[32];
        int m_HandleNum = 0;

        /// <summary> 
        /// 判定是否運行於64bit作業系統
        /// </summary> 
        /// <returns>是否為64bit的作業系統</returns> 
        public static bool Is64bit()
        {
            return IntPtr.Size == 8;
        }

        public string open()
        {
            ushort ret;
            uint lp1, lp2;
            byte[] buffer = new byte[1024];
            p1 = 0x5f31;
            p2 = 0x6f53;
            p3 = 0x900b;
            p4 = 0x6f63;

            //DllInvoke Rockey4ND = new DllInvoke("Rockey4ND.dll");

            ret = Rockey(RY_FIND, out m_Handle[0], out lp1, out lp2, out p1, out p2, out p3, out p4, ref buffer[0]);
            if (ret != 0)
            {
                Common._ErrorMsgShow("请插入USB授权钥匙");
                return "";
            }
            //else
            //{
            //    Common._ErrorMsgShow("USB授权钥匙正确" + lp1.ToString("X"));
            //}

            ret = Rockey(RY_OPEN, out m_Handle[0], out lp1, out lp2, out p1, out p2, out p3, out p4, ref buffer[0]);
            if (ret != 0)
            {
                logger.Error("USB授权钥匙, 读取错误!!!");
                return "";
            }
            else
            {
                m_HandleNum = 1;
                return lp1.ToString("X");
            }
            /*
            while (ret == 0)
            {
                ret = Rockey(RY_FIND_NEXT, out m_Handle[0], out lp1, out lp2, out p1, out p2, out p3, out p4, ref buffer[0]);
                if (ret != 0)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("HID: " + lp1.ToString("X"));
                    
                }
                ret = Rockey(RY_OPEN, out m_Handle[0], out lp1, out lp2, out p1, out p2, out p3, out p4, ref buffer[0]);
                if (ret != 0)
                {
                    break;
                }
                m_HandleNum = m_HandleNum + 1;
            }
             */
        }

        public void close()
        {
            int i;
            ushort ret;
            ushort p1, p2, p3, p4;
            uint lp1, lp2;
            byte[] buffer = new byte[1024];

            for (i = 0; i < m_HandleNum; i++)
            {
                ret = Rockey(RY_CLOSE, out m_Handle[0], out lp1, out lp2, out p1, out p2, out p3, out p4, ref buffer[0]);
                if (ret != 0)
                {
                    logger.Error("RY_CLOSE error");
                }
            }
        }

        public Boolean verify(string s1, string s2)
        {
            int i, k = 0;
            ushort ret;
            uint lp1, lp2;
            byte[] buffer = new byte[1024];
            String[] str1 = new String[m_HandleNum];
            ushort p1, p2, p3, p4;
            for (i = 0; i < m_HandleNum; i++)
            {
                p1 = 520; //offset
                p2 = 64; //length
                ret = Rockey(RY_READ, out m_Handle[0], out lp1, out lp2, out p1, out p2, out p3, out p4, ref buffer[0]);
                if (ret != 0)
                {
                    logger.Error("RY_READ error");
                    return false;
                }
                else
                {
                    for (k = 0; k < p2; k++)
                    {
                        if (buffer[k] == 0)
                        {
                            break;
                        }
                        str1[i] += Convert.ToChar(buffer[k]);
                    }
                }
            }
            if (k > 0)
            {
                JiaMiJieMi aeskey = new JiaMiJieMi();
                string str_id = aeskey.AES_Decrypt(aeskey.Base64Decrypt(str1[0].Substring(0, 32)));
#if DEBUG
                //if (str1[0].Substring(32, 32) == s2.Substring(32, 32))
                    return true;
#else
                if (str_id == s1 && str1[0].ToString() == s2)
                    return true;
                else
                    return false;
#endif
            }
            return false;
        }

        public Boolean WriteKey(string str1)
        {

            int i;
            ushort ret;
            ushort p1, p2, p3, p4;
            uint lp1, lp2;
            byte[] buffer = new byte[1024];
            for (i = 0; i < str1.Length; i++)
            {
                buffer[i] = Convert.ToByte(str1[i]);
            }
            buffer[str1.Length] = 0;

            for (i = 0; i < m_HandleNum; i++)
            {
                p1 = 520; //offset
                p2 = 64; //length
                ret = Rockey(RY_WRITE, out m_Handle[0], out lp1, out lp2, out p1, out p2, out p3, out p4, ref buffer[0]);
                if (ret != 0)
                {
                    logger.Error("RY_READ error");
                    return false;
                }
                else
                {
                    return true;
                }
            }
            return false;
        }

        public string ReadKey()
        {
            int i, k = 0;
            ushort ret;
            uint lp1, lp2;
            byte[] buffer = new byte[1024];
            String[] str1 = new String[m_HandleNum];
            ushort p1, p2, p3, p4;
            for (i = 0; i < m_HandleNum; i++)
            {
                p1 = 520; //offset
                p2 = 64; //length
                ret = Rockey(RY_READ, out m_Handle[0], out lp1, out lp2, out p1, out p2, out p3, out p4, ref buffer[0]);
                if (ret != 0)
                {
                    logger.Error("RY_READ error");
                    return "0";
                }
                else
                {
                    for (k = 0; k < p2; k++)
                    {
                        if (buffer[k] == 0)
                        {
                            break;
                        }
                        str1[i] += Convert.ToChar(buffer[k]);
                    }
                }
            }
            if (k > 0)
            {
                return str1[0].ToString();
            }
            else
            {
                return "0";
            }
        }
    }
}