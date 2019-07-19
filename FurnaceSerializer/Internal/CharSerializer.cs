using System;

namespace FurnaceSerializer.Internal
{
    internal sealed class CharSerializer : ISerializer
    {
        public Type Type => typeof(char);

        public int SizeOf(object value) => sizeof(char);

        public bool Write(object value, byte[] buffer, ref int position) => 
            SerializerUtil.WriteChar((char)value, buffer, ref position);

        public object Read(byte[] buffer, ref int position, bool peek = false) =>
            SerializerUtil.ReadChar(buffer, ref position, peek);
    }
}