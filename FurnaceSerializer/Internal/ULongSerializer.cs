using System;

namespace FurnaceSerializer.Internal
{
    internal sealed class ULongSerializer : ISerializer
    {
        public Type Type => typeof(ulong);

        public int SizeOf(object value) => sizeof(ulong);

        public bool Write(object value, ByteBuffer buffer) => buffer.Write((ulong) value);

        public object Read(ByteBuffer buffer, bool peek = false) => buffer.ReadULong(peek);
    }
}