using System;

namespace FurnaceSerializer.Internal
{
    internal sealed class IntSerializer : ISerializer
    {
        public Type Type => typeof(int);

        public int SizeOf(object value) => sizeof(int);

        public bool Write(object value, byte[] buffer, ref int position) => 
            SerializerUtil.WriteInt((int)value, buffer, ref position);

        public object Read(byte[] buffer, ref int position, bool peek = false) =>
            SerializerUtil.ReadInt(buffer, ref position, peek);
    }
}