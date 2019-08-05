using System;

namespace FurnaceSerializer.Internal
{
    internal sealed class UIntSerializer : ISerializer
    {
        public Type Type => typeof(uint);

        public int SizeOf(object value) => sizeof(uint);

        public bool Write(object value, ByteBuffer buffer) => buffer.Write((uint) value);

        public object Read(ByteBuffer buffer, bool peek = false) => buffer.ReadUInt(peek);
    }
}