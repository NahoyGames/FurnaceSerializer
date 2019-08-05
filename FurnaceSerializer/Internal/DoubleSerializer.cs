using System;

namespace FurnaceSerializer.Internal
{
    internal sealed class DoubleSerializer : ISerializer
    {
        public Type Type => typeof(double);

        public int SizeOf(object value) => sizeof(double);

        public bool Write(object value, ByteBuffer buffer) => buffer.Write((double) value);

        public object Read(ByteBuffer buffer, bool peek = false) => buffer.ReadDouble(peek);
    }
}