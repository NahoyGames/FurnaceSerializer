using System;

namespace FurnaceSerializer
{
    public partial class ByteBuffer
    {
        private byte[] _data;
        private int _index, _length, _offset;

        /// <summary>
        /// Create a new buffer on top of provided data with custom start index and length
        /// </summary>
        public ByteBuffer(byte[] data, int length, int offset = 0)
        {
            _data = data;
            _offset = offset;
            _length = length;
            _index = 0;

            if (_length + _offset > _data.Length)
            {
                throw new Exception("Length parameter exceeds data's length");
            }
        }

        /// <summary>
        /// Create a new buffer on top of provided data
        /// </summary>
        public ByteBuffer(byte[] data) : this(data, data.Length) { }

        /// <summary>
        /// Create a new empty buffer with provided length and offset
        /// </summary>
        public ByteBuffer(int length, int offset = 0) : this(new byte[length + offset], length, offset) { }
        
        /// <summary>
        /// The raw data of the buffer
        /// </summary>
        public byte[] Data => _data;

        /// <summary>
        /// The length of the buffer
        /// </summary>
        public int Length
        {
            get => _length;
            set
            {
                _length = value;
                
                if (_data.Length < _length)
                {
                    Array.Resize(ref _data, _length);
                }
            }
        }

        public int Index
        {
            get => _index;
            set => _index = value;
        }
        
        public int Offset
        {
            get => _offset;
            set => _offset = value;
        }

        /// <summary>
        /// Write a single byte to the buffer
        /// </summary>
        public bool Write(byte value)
        {
            if (_offset + _index >= _length) { return false; }
            
            _data[_offset + _index++] = value;
            return true;
        }

        /// <summary>
        /// Write multiple bytes at once to the buffer
        /// </summary>
        public bool Write(byte[] value)
        {
            if (_offset + _index + value.Length > _length) { return false; }

            foreach (var b in value)
            {
                _data[_offset + _index] = b;
                _index++;
            }

            return true;
        }

        /// <summary>
        /// Read a single byte from the buffer
        /// </summary>
        public byte ReadByte(bool peek = false)
        {
            byte value =  _data[_offset + _index];

            if (!peek)
            {
                _index++;
            }

            return value;
        }
    }
}