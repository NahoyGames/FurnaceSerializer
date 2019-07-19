using System;

namespace FurnaceSerializer.Internal
{
    internal sealed class StringSerializer : ISerializer
    {
        public Type Type => typeof(string);

        public int SizeOf(object value) => SerializerUtil.SizeOfString((string)value);

        public bool Write(object value, byte[] buffer, ref int position) => 
            SerializerUtil.WriteString((string)value, buffer, ref position);

        public object Read(byte[] buffer, ref int position, bool peek = false) =>
            SerializerUtil.ReadString(buffer, ref position, peek);
    }
}