using System;

namespace FurnaceSerializer.Internal
{
    internal sealed class LongSerializer : ISerializer
    {
        public Type Type => typeof(long);

        public int SizeOf(object value) => sizeof(long);

        public bool Write(object value, byte[] buffer, ref int position) => 
            SerializerUtil.WriteLong((long)value, buffer, ref position);

        public object Read(byte[] buffer, ref int position, bool peek = false) =>
            SerializerUtil.ReadLong(buffer, ref position, peek);
    }
}