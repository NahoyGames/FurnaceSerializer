using System;

namespace FurnaceSerializer.Internal
{
    internal sealed class UIntSerializer : ISerializer
    {
        public Type Type => typeof(uint);

        public int SizeOf(object value) => sizeof(uint);

        public bool Write(object value, byte[] buffer, ref int position) => 
            SerializerUtil.WriteUInt((uint)value, buffer, ref position);

        public object Read(byte[] buffer, ref int position, bool peek = false) =>
            SerializerUtil.ReadUInt(buffer, ref position, peek);
    }
}