using System;

namespace FurnaceSerializer.Internal
{
    internal sealed class CharSerializer : ISerializer
    {
        public Type Type => typeof(char);

        public int SizeOf(object value) => sizeof(char);

        public bool Write(object value, ByteBuffer buffer) => buffer.Write((char) value);

        public object Read(ByteBuffer buffer, bool peek = false) => buffer.ReadChar(peek);
    }
}