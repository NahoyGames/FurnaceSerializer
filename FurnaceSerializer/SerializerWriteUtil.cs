using System;
using System.Text;

namespace FurnaceSerializer
{
    /// <summary>
    /// Utility for creating "ISerializer"s
    ///
    /// Provides the method ISerializer would normally implement, but in a static context.
    /// Supports commonly-used types to avoid code repetition.
    /// </summary>
    public static partial class SerializerUtil
    {
        /// <summary>
        /// Writes a single byte to the buffer
        /// </summary>
        public static bool WriteByte(byte value, byte[] buffer, ref int position)
        {
            if (position >= buffer.Length) { return false; }

            buffer[position++] = value;
            return true;
        }

        /// <summary>
        /// Appends the bytes of value, not byte[] itself, to the buffer.
        /// Use WriteArray&lt;byte&gt;() instead to write a byte[]. 
        /// </summary>
        public static bool WriteBytes(byte[] value, byte[] buffer, ref int position)
        {
            if (value.Length + position > buffer.Length) { return false; }

            foreach (var @byte in value)
            {
                buffer[position++] = @byte;
            }

            return true;
        }

        /// <summary>
        /// Write a boolean to the buffer
        /// </summary>
        public static bool WriteBool(bool value, byte[] buffer, ref int position) =>
            WriteBytes(BitConverter.GetBytes(value), buffer, ref position);
        
        /// <summary>
        /// Write a character to the buffer
        /// </summary>
        public static bool WriteChar(char value, byte[] buffer, ref int position) =>
            WriteBytes(BitConverter.GetBytes(value), buffer, ref position);
        
        /// <summary>
        /// Write a double floating point value to the buffer
        /// </summary>
        public static bool WriteDouble(double value, byte[] buffer, ref int position) =>
            WriteBytes(BitConverter.GetBytes(value), buffer, ref position);
        
        /// <summary>
        /// Write a floating point value to the buffer
        /// </summary>
        public static bool WriteFloat(float value, byte[] buffer, ref int position) =>
            WriteBytes(BitConverter.GetBytes(value), buffer, ref position);
        
        /// <summary>
        /// Write an 32-bit integer to the buffer
        /// </summary>
        public static bool WriteInt(int value, byte[] buffer, ref int position) =>
            WriteBytes(BitConverter.GetBytes(value), buffer, ref position);
        
        /// <summary>
        /// Write a 64-bit integer to the buffer
        /// </summary>
        public static bool WriteLong(long value, byte[] buffer, ref int position) =>
            WriteBytes(BitConverter.GetBytes(value), buffer, ref position);
        
        /// <summary>
        /// Write a signed byte to the buffer
        /// </summary>
        public static bool WriteSByte(sbyte value, byte[] buffer, ref int position) =>
            WriteByte((byte)(value + Math.Abs(sbyte.MinValue)), buffer, ref position);
        
        /// <summary>
        /// Write a 16-bit integer to the buffer
        /// </summary>
        public static bool WriteShort(short value, byte[] buffer, ref int position) =>
            WriteBytes(BitConverter.GetBytes(value), buffer, ref position);

        /// <summary>
        /// Write a string to the buffer
        /// </summary>
        public static bool WriteString(string value, byte[] buffer, ref int position) =>
            WriteUShort((ushort) value.Length, buffer, ref position) &&
            WriteBytes(Encoding.UTF8.GetBytes(value), buffer, ref position);
        
        /// <summary>
        /// Write an unsigned 32-bit integer to the buffer
        /// </summary>
        public static bool WriteUInt(uint value, byte[] buffer, ref int position) =>
            WriteBytes(BitConverter.GetBytes(value), buffer, ref position);
        
        /// <summary>
        /// Write an unsigned 64-bit integer to the buffer
        /// </summary>
        public static bool WriteULong(ulong value, byte[] buffer, ref int position) =>
            WriteBytes(BitConverter.GetBytes(value), buffer, ref position);
        
        /// <summary>
        /// Write an unsigned 16-bit integer to the buffer
        /// </summary>
        public static bool WriteUShort(ushort value, byte[] buffer, ref int position) =>
            WriteBytes(BitConverter.GetBytes(value), buffer, ref position);

        /// <summary>
        /// Strings are handled differently then the other types, and the sizeof() operator simply doesn't work.
        /// </summary>
        public static int SizeOfString(string value)
        {
            return sizeof(ushort) + Encoding.UTF8.GetByteCount(value);
        }
    }
}