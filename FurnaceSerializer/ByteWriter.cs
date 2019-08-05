using System;
using System.Text;

namespace FurnaceSerializer
{
    public partial class ByteBuffer
    {
        /// <summary>
        /// Write a boolean to the buffer
        /// </summary>
        public bool Write(bool value) => Write(BitConverter.GetBytes(value));
        
        /// <summary>
        /// Write a character to the buffer
        /// </summary>
        public bool Write(char value) => Write(BitConverter.GetBytes(value));
        
        /// <summary>
        /// Write a double floating point value to the buffer
        /// </summary>
        public bool Write(double value) => Write(BitConverter.GetBytes(value));

        /// <summary>
        /// Write a floating point value to the buffer
        /// </summary>
        public bool Write(float value) => Write(BitConverter.GetBytes(value));

        /// <summary>
        /// Write an 32-bit integer to the buffer
        /// </summary>
        public bool Write(int value) => Write(BitConverter.GetBytes(value));

        /// <summary>
        /// Write a 64-bit integer to the buffer
        /// </summary>
        public bool Write(long value) => Write(BitConverter.GetBytes(value));

        /// <summary>
        /// Write a signed byte to the buffer
        /// </summary>
        public bool Write(sbyte value) => Write((byte) (value + Math.Abs(sbyte.MinValue)));

        /// <summary>
        /// Write a 16-bit integer to the buffer
        /// </summary>
        public bool Write(short value) => Write(BitConverter.GetBytes(value));

        /// <summary>
        /// Write a string to the buffer
        /// </summary>
        public bool WriteString(string value) => Write((ushort) value.Length) && Write(Encoding.UTF8.GetBytes(value));
        
        /// <summary>
        /// Write an unsigned 32-bit integer to the buffer
        /// </summary>
        public bool Write(uint value) => Write(BitConverter.GetBytes(value));

        /// <summary>
        /// Write an unsigned 64-bit integer to the buffer
        /// </summary>
        public bool Write(ulong value) => Write(BitConverter.GetBytes(value));

        /// <summary>
        /// Write an unsigned 16-bit integer to the buffer
        /// </summary>
        public bool Write(ushort value) => Write(BitConverter.GetBytes(value));
    }
}