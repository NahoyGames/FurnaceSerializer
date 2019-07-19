using System;

namespace FurnaceSerializer.Internal
{
    internal sealed class FloatSerializer : ISerializer
    {
        public Type Type => typeof(float);

        public int SizeOf(object value) => sizeof(float);

        public bool Write(object value, byte[] buffer, ref int position) => 
            SerializerUtil.WriteFloat((float)value, buffer, ref position);

        public object Read(byte[] buffer, ref int position, bool peek = false) =>
            SerializerUtil.ReadFloat(buffer, ref position, peek);
    }
}