using System;

namespace FurnaceSerializer.Internal
{
    internal sealed class DoubleSerializer : ISerializer
    {
        public Type Type => typeof(double);

        public int SizeOf(object value) => sizeof(double);

        public bool Write(object value, byte[] buffer, ref int position) => 
            SerializerUtil.WriteDouble((double)value, buffer, ref position);

        public object Read(byte[] buffer, ref int position, bool peek = false) =>
            SerializerUtil.ReadDouble(buffer, ref position, peek);
    }
}