using System;

namespace FurnaceSerializer.Internal
{
    internal sealed class ShortSerializer : ISerializer
    {
        public Type Type => typeof(short);

        public int SizeOf(object value) => sizeof(short);

        public bool Write(object value, ByteBuffer buffer) => buffer.Write((short) value);

        public object Read(ByteBuffer buffer, bool peek = false) => buffer.ReadShort(peek);
    }
}