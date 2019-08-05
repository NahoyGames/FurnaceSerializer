using System;

namespace FurnaceSerializer.Internal
{
    internal sealed class SByteSerializer : ISerializer
    {
        public Type Type => typeof(sbyte);

        public int SizeOf(object value) => sizeof(sbyte);

        public bool Write(object value, ByteBuffer buffer) => buffer.Write((sbyte) value);

        public object Read(ByteBuffer buffer, bool peek = false) => buffer.ReadSByte(peek);
    }
}