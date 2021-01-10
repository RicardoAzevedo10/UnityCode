using System;
using System.Text;

namespace ServerOne
{
    public class Tlv
    {
        public byte Tag { get; private set; }
        public byte Length { get; private set; }
        public byte[] Value { get; private set; }
        
        public Tlv() {}
        
        public Tlv(byte tag, string value) : this()
        {
            int byteCount = Encoding.ASCII.GetByteCount(value);
            int length = byteCount;
            Tag = tag;
            Length = (byte) length;
            Value = Encoding.ASCII.GetBytes(value);
        }

        public Tlv(byte tag, int value) : this()
        {
            Tag = tag;
            Length = 4;
            Value = BitConverter.GetBytes(value);
        }

        public byte[] Encode()
        {
            byte[] buffer = new byte[2 + Value.Length];
            buffer[0] = Tag;
            buffer[1] = Length;
            Value.CopyTo(buffer, 2);
            return buffer;
        }

        public static byte[] Encode(byte tag, string value)
        {
            int byteCount = Encoding.ASCII.GetByteCount(value);
            byte[] buffer = new byte[2 + byteCount];
            buffer[0] = tag;
            buffer[1] = (byte) byteCount;
            Encoding.ASCII.GetBytes(value, 0, byteCount, buffer, 2);
            return buffer;
        }

        public static byte[] Encode(byte tag, int value)
        {
            byte[] buffer = new byte[6];
            buffer[0] = tag;
            buffer[1] = 4;
            byte[] intBuffer = BitConverter.GetBytes(value);

            for (int i = 2; i < 4; i++)
            {
                buffer[i] = intBuffer[i];
            }

            return buffer;
        }

        public static Tlv Decode(byte[] raw)
        {
            Tlv tlv = new Tlv();
            tlv.Tag = raw[0];
            tlv.Length = raw[1];
            tlv.Value = raw[2..(2 + tlv.Length)];
            return tlv;
        }
    }
}