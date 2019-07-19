using System;

namespace FurnaceSerializer.Internal
{
    internal sealed class UShortSerializer : ISerializer
    {
        public Type Type => typeof(ushort);

        public int SizeOf(object value) => sizeof(ushort);

        public bool Write(object value, byte[] buffer, ref int position) => 
            SerializerUtil.WriteUShort((ushort)value, buffer, ref position);

        public object Read(byte[] buffer, ref int position, bool peek = false) =>
            SerializerUtil.ReadUShort(buffer, ref position, peek);
    }
}