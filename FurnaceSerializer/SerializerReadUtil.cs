using System;
using System.Text;

namespace FurnaceSerializer
{
    public static partial class SerializerUtil
    {
        /// <summary>
        /// Read a boolean from the buffer
        /// </summary>
        public static bool ReadBool(byte[] buffer, ref int position, bool peek)
        {
            var value = BitConverter.ToBoolean(buffer, position);
            position += peek ? 0 : sizeof(bool);

            return value;
        }
        
        /// <summary>
        /// Read a byte from the buffer
        /// </summary>
        public static byte ReadByte(byte[] buffer, ref int position, bool peek)
        {
            var value = buffer[position];
            position += peek ? 0 : sizeof(byte);

            return value;
        }
        
        /// <summary>
        /// Read a character from the buffer
        /// </summary>
        public static char ReadChar(byte[] buffer, ref int position, bool peek)
        {
            var value = BitConverter.ToChar(buffer, position);
            position += peek ? 0 : sizeof(char);

            return value;
        }
        
        /// <summary>
        /// Read a double precision floating-point value from the buffer
        /// </summary>
        public static double ReadDouble(byte[] buffer, ref int position, bool peek)
        {
            var value = BitConverter.ToDouble(buffer, position);
            position += peek ? 0 : sizeof(double);

            return value;
        }
        
        /// <summary>
        /// Read a single precision floating-point value from the buffer
        /// </summary>
        public static float ReadFloat(byte[] buffer, ref int position, bool peek)
        {
            var value = BitConverter.ToSingle(buffer, position);
            position += peek ? 0 : sizeof(float);

            return value;
        }
        
        /// <summary>
        /// Read a 32-bit integer from the buffer
        /// </summary>
        public static int ReadInt(byte[] buffer, ref int position, bool peek)
        {
            var value = BitConverter.ToInt32(buffer, position);
            position += peek ? 0 : sizeof(int);

            return value;
        }
        
        /// <summary>
        /// Read a 64-bit integer from the buffer
        /// </summary>
        public static long ReadLong(byte[] buffer, ref int position, bool peek)
        {
            var value = BitConverter.ToInt64(buffer, position);
            position += peek ? 0 : sizeof(long);

            return value;
        }
        
        /// <summary>
        /// Read a signed byte from the buffer
        /// </summary>
        public static sbyte ReadSByte(byte[] buffer, ref int position, bool peek)
        {
            var value = (sbyte) (buffer[position] - Math.Abs(sbyte.MinValue));
            position += peek ? 0 : sizeof(sbyte);

            return value;
        }
        
        /// <summary>
        /// Read a 16-bit integer from the buffer
        /// </summary>
        public static short ReadShort(byte[] buffer, ref int position, bool peek)
        {
            var value = BitConverter.ToInt16(buffer, position);
            position += peek ? 0 : sizeof(short);

            return value;
        }
        
        /// <summary>
        /// Read a string from the buffer
        /// </summary>
        public static string ReadString(byte[] buffer, ref int position, bool peek)
        {
            ushort length = ReadUShort(buffer, ref position, peek);
            var value = Encoding.UTF8.GetString(buffer, position, length);
            position += peek ? 0 : Encoding.UTF8.GetByteCount(value);

            return value;
        }
        
        /// <summary>
        /// Read an unsigned 32-bit integer from the buffer
        /// </summary>
        public static uint ReadUInt(byte[] buffer, ref int position, bool peek)
        {
            var value = BitConverter.ToUInt32(buffer, position);
            position += peek ? 0 : sizeof(uint);

            return value;
        }
        
        /// <summary>
        /// Read an unsigned 64-bit integer from the buffer
        /// </summary>
        public static ulong ReadULong(byte[] buffer, ref int position, bool peek)
        {
            var value = BitConverter.ToUInt64(buffer, position);
            position += peek ? 0 : sizeof(ulong);

            return value;
        }
        
        /// <summary>
        /// Read an unsgined 16-bit integer from the buffer
        /// </summary>
        public static ushort ReadUShort(byte[] buffer, ref int position, bool peek)
        {
            var value = BitConverter.ToUInt16(buffer, position);
            position += peek ? 0 : sizeof(char);

            return value;
        }
    }
}