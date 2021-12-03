using System;
using System.Text;

namespace ChatCoreTest
{
  internal class Program
  {
    private static byte[] m_PacketData;
    private static uint m_Pos;

    public static void Main(string[] args)
    {
      m_PacketData = new byte[1024];
      m_Pos = 0;

      Write(109); 
      Write(109.99f);
      Write("Hello!");

      Console.Write($"Output Byte array(length:{m_Pos}): ");
      for (var i = 0; i < m_Pos; i++)
      {
        Console.Write(m_PacketData[i] + ", ");
      }

            Console.WriteLine("\n----------作業一------------------------");
            _Read(m_PacketData);
    }

    // write an integer into a byte array
    private static bool Write(int i)
    {
      // convert int to byte array
      var bytes = BitConverter.GetBytes(i);
      _Write(bytes);
      return true;
    }

    // write a float into a byte array
    private static bool Write(float f)
    {
      // convert int to byte array
      var bytes = BitConverter.GetBytes(f);
      _Write(bytes);
      return true;
    }

    // write a string into a byte array
    private static bool Write(string s)
    {
      // convert string to byte array
      var bytes = Encoding.Unicode.GetBytes(s);

      // write byte array length to packet's byte array
      if (Write(bytes.Length) == false)
      {
        return false;
      }

      _Write(bytes);
      return true;
    }

    // write a byte array into packet's byte array
    private static void _Write(byte[] byteData)
    {
      // converter little-endian to network's big-endian
      if (BitConverter.IsLittleEndian)
      {
        Array.Reverse(byteData);
      }

      byteData.CopyTo(m_PacketData, m_Pos);
      m_Pos += (uint)byteData.Length;
    }

        private static void _Read(byte[] m_PacketData)
        {
            
            byte[] intbytes = new byte[4];
            byte[] floatbytes = new byte[4];
            uint str_len = m_Pos - 8;
            byte[] stringbytes = new byte[str_len];
            
            for (int i=0; i<8+ str_len; i++)
            {
                

                if (i < 4)
                {
                    intbytes[i] = m_PacketData[i];
                    
                }
                if (i < 8 && i >= 4)
                {
                    floatbytes[i - 4] = m_PacketData[i];
                }
                if (i < m_Pos && i >= 8)
                {
                    stringbytes[i - 8] = m_PacketData[i];
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

            float ans_f = BitConverter.ToSingle(floatbytes,0);
            Console.WriteLine(ans_f);

            string ans_str = System.Text.Encoding.Unicode.GetString(stringbytes);
            Console.WriteLine(ans_str);
             
        }
  }
}
