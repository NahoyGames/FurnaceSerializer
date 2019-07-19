using System;

namespace FurnaceSerializer.Internal
{
    internal sealed class ByteSerializer : ISerializer
    {
        public Type Type => typeof(byte);

        public int SizeOf(object value) => sizeof(byte);

        public bool Write(object value, byte[] buffer, ref int position) => 
            SerializerUtil.WriteByte((byte)value, buffer, ref position);

        public object Read(byte[] buffer, ref int position, bool peek = false) =>
            SerializerUtil.ReadByte(buffer, ref position, peek);
    }
}