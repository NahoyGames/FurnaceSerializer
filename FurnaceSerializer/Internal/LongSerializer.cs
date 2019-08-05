using System;

namespace FurnaceSerializer.Internal
{
    internal sealed class LongSerializer : ISerializer
    {
        public Type Type => typeof(long);

        public int SizeOf(object value) => sizeof(long);

        public bool Write(object value, ByteBuffer buffer) => buffer.Write((long) value);

        public object Read(ByteBuffer buffer, bool peek = false) => buffer.ReadLong(peek);
    }
}