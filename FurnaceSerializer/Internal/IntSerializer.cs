using System;

namespace FurnaceSerializer.Internal
{
    internal sealed class IntSerializer : ISerializer
    {
        public Type Type => typeof(int);

        public int SizeOf(object value) => sizeof(int);

        public bool Write(object value, ByteBuffer buffer) => buffer.Write((int) value);

        public object Read(ByteBuffer buffer, bool peek = false) => buffer.ReadInt(peek);
    }
}