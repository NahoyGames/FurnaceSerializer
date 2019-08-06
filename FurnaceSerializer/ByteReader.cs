using System;
using System.Text;

namespace FurnaceSerializer
{
    public partial class ByteBuffer
    {
        /// <summary>
        /// Read a boolean from the buffer
        /// </summary>
        public bool ReadBool(bool peek = false)
        {
            var value = BitConverter.ToBoolean(_data, _index + _offset);
            _index += peek ? 0 : sizeof(bool);

            return value;
        }

        /// <summary>
        /// Read a character from the buffer
        /// </summary>
        public char ReadChar(bool peek = false)
        {
            var value = BitConverter.ToChar(_data, _index + _offset);
            _index += peek ? 0 : sizeof(char);

            return value;
        }
        
        /// <summary>
        /// Read a double precision floating-point value from the buffer
        /// </summary>
        public double ReadDouble(bool peek = false)
        {
            var value = BitConverter.ToDouble(_data, _index + _offset);
            _index += peek ? 0 : sizeof(double);

            return value;
        }
        
        /// <summary>
        /// Read a single precision floating-point value from the buffer
        /// </summary>
        public float ReadFloat(bool peek = false)
        {
            var value = BitConverter.ToSingle(_data, _index + _offset);
            _index += peek ? 0 : sizeof(float);

            return value;
        }
        
        /// <summary>
        /// Read a 32-bit integer from the buffer
        /// </summary>
        public int ReadInt(bool peek = false)
        {
            var value = BitConverter.ToInt32(_data, _index + _offset);
            _index += peek ? 0 : sizeof(int);

            return value;
        }
        
        /// <summary>
        /// Read a 64-bit integer from the buffer
        /// </summary>
        public long ReadLong(bool peek = false)
        {
            var value = BitConverter.ToInt64(_data, _index + _offset);
            _index += peek ? 0 : sizeof(long);

            return value;
        }
        
        /// <summary>
        /// Read a signed byte from the buffer
        /// </summary>
        public sbyte ReadSByte(bool peek = false)
        {
            var value = (sbyte) (_data[_index + _offset] - Math.Abs(sbyte.MinValue));
            _index += peek ? 0 : sizeof(sbyte);

            return value;
        }
        
        /// <summary>
        /// Read a 16-bit integer from the buffer
        /// </summary>
        public short ReadShort(bool peek = false)
        {
            var value = BitConverter.ToInt16(_data, _index + _offset);
            _index += peek ? 0 : sizeof(short);

            return value;
        }
        
        /// <summary>
        /// Read a string from the buffer
        /// </summary>
        public string ReadString(bool peek = false)
        {
            ushort length = ReadUShort(peek);
            var value = Encoding.UTF8.GetString(_data, _index + _offset, length);
            _index += peek ? 0 : Encoding.UTF8.GetByteCount(value);

            return value;
        }
        
        /// <summary>
        /// Read an unsigned 32-bit integer from the buffer
        /// </summary>
        public uint ReadUInt(bool peek = false)
        {
            var value = BitConverter.ToUInt32(_data, _index + _offset);
            _index += peek ? 0 : sizeof(uint);

            return value;
        }
        
        /// <summary>
        /// Read an unsigned 64-bit integer from the buffer
        /// </summary>
        public ulong ReadULong(bool peek = false)
        {
            var value = BitConverter.ToUInt64(_data, _index + _offset);
            _index += peek ? 0 : sizeof(ulong);

            return value;
        }
        
        /// <summary>
        /// Read an unsigned 16-bit integer from the buffer
        /// </summary>
        public ushort ReadUShort(bool peek = false)
        {
            var value = BitConverter.ToUInt16(_data, _index + _offset);
            _index += peek ? 0 : sizeof(ushort);

            return value;
        }
    }
}