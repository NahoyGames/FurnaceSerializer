using System;

namespace FurnaceSerializer.Internal
{
    internal sealed class SByteSerializer : ISerializer
    {
        public Type Type => typeof(sbyte);

        public int SizeOf(object value) => sizeof(sbyte);

        public bool Write(object value, byte[] buffer, ref int position) => 
            SerializerUtil.WriteSByte((sbyte)value, buffer, ref position);

        public object Read(byte[] buffer, ref int position, bool peek = false) =>
            SerializerUtil.ReadSByte(buffer, ref position, peek);
    }
}