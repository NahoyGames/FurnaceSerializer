using System;

namespace FurnaceSerializer.Internal
{
    internal sealed class ByteSerializer : ISerializer
    {
        public Type Type => typeof(byte);

        public int SizeOf(object value) => sizeof(byte);

        public bool Write(object value, ByteBuffer buffer) => buffer.Write((byte) value);

        public object Read(ByteBuffer buffer, bool peek = false) => buffer.ReadByte(peek);
    }
}