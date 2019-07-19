using System;

namespace FurnaceSerializer.Internal
{
    internal sealed class ULongSerializer : ISerializer
    {
        public Type Type => typeof(ulong);

        public int SizeOf(object value) => sizeof(ulong);

        public bool Write(object value, byte[] buffer, ref int position) => 
            SerializerUtil.WriteULong((ulong)value, buffer, ref position);

        public object Read(byte[] buffer, ref int position, bool peek = false) =>
            SerializerUtil.ReadULong(buffer, ref position, peek);
    }
}