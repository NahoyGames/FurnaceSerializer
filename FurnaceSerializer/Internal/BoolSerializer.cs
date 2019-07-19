using System;

namespace FurnaceSerializer.Internal
{
    internal sealed class BoolSerializer : ISerializer
    {
        public Type Type => typeof(bool);

        public int SizeOf(object value) => sizeof(bool);

        public bool Write(object value, byte[] buffer, ref int position) => 
            SerializerUtil.WriteBool((bool)value, buffer, ref position);

        public object Read(byte[] buffer, ref int position, bool peek = false) =>
            SerializerUtil.ReadBool(buffer, ref position, peek);
    }
}