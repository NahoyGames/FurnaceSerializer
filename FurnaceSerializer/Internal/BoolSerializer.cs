using System;

namespace FurnaceSerializer.Internal
{
    internal sealed class BoolSerializer : ISerializer
    {
        public Type Type => typeof(bool);

        public int SizeOf(object value) => sizeof(bool);

        public bool Write(object value, ByteBuffer buffer) => buffer.Write((bool) value);

        public object Read(ByteBuffer buffer, bool peek = false) => buffer.ReadBool(peek);
    }
}