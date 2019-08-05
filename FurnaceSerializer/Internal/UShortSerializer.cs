using System;

namespace FurnaceSerializer.Internal
{
    internal sealed class UShortSerializer : ISerializer
    {
        public Type Type => typeof(ushort);

        public int SizeOf(object value) => sizeof(ushort);

        public bool Write(object value, ByteBuffer buffer) => buffer.Write((ushort) value);

        public object Read(ByteBuffer buffer, bool peek = false) => buffer.ReadUShort(peek);
    }
}