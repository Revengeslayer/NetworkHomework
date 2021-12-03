using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW2
{
    class Program
    {
        private static byte[] m_PacketData;
        private static uint m_Pos;
        private static uint messageLength;

        public static void Main(string[] args)
        {
            m_PacketData = new byte[1024];
            m_Pos = 1;
            

            Write(109);
            Write(109.99f);          
            Write("Hello!");

            m_PacketData[0] = Convert.ToByte(messageLength);
            Console.WriteLine("\n----------作業二------------------------");
            _Read(m_PacketData);
        }

        private static bool Write(int i)
        {
            var bytes = BitConverter.GetBytes(i);
            _Write(bytes, bytes.Length);
            return true;
        }

        private static bool Write(float f)
        {
            var bytes = BitConverter.GetBytes(f);
            _Write(bytes, bytes.Length);
            return true;
        }

        private static bool Write(string s)
        {
            var bytes = Encoding.Unicode.GetBytes(s);
            _Write(bytes, bytes.Length);
            return true;
        }

        private static void _Write(byte[] byteData,int length)
        {
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(byteData);
            }
            byteData.CopyTo(m_PacketData, m_Pos);
            m_Pos += (uint)byteData.Length;

            messageLength = m_Pos;
        }

        private static void _Read(byte[] m_PacketData)
        {

            byte[] intbytes = new byte[4];
            byte[] floatbytes = new byte[4];
            uint str_len = m_Pos -1- 8;
            byte[] stringbytes = new byte[str_len];


            
            for (int i = 1; i < m_PacketData[0]; i++)
            {
                if (i < 5)
                {
                    intbytes[i-1] = m_PacketData[i];

                }
                
                if (i < 9 && i >= 5)
                {
                    floatbytes[i-1 - 4] = m_PacketData[i];
                }
                if (i < m_Pos && i >= 9)
                {
                    stringbytes[i-1 - 8] = m_PacketData[i];
                }
                
            }
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(intbytes);
                Array.Reverse(floatbytes);
                Array.Reverse(stringbytes);
            }

            int ans_i = BitConverter.ToInt32(intbytes, 0);
            Console.WriteLine(ans_i);

            float ans_f = BitConverter.ToSingle(floatbytes, 0);
            Console.WriteLine(ans_f);

            string ans_str = System.Text.Encoding.Unicode.GetString(stringbytes);
            Console.WriteLine(ans_str);
            
        }
    
    }
}
