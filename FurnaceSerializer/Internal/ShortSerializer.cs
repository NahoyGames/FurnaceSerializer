using System;

namespace FurnaceSerializer.Internal
{
    internal sealed class ShortSerializer : ISerializer
    {
        public Type Type => typeof(short);

        public int SizeOf(object value) => sizeof(short);

        public bool Write(object value, byte[] buffer, ref int position) => 
            SerializerUtil.WriteShort((short)value, buffer, ref position);

        public object Read(byte[] buffer, ref int position, bool peek = false) =>
            SerializerUtil.ReadShort(buffer, ref position, peek);
    }
}