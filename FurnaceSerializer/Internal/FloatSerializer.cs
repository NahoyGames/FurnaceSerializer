using System;

namespace FurnaceSerializer.Internal
{
    internal sealed class FloatSerializer : ISerializer
    {
        public Type Type => typeof(float);

        public int SizeOf(object value) => sizeof(float);

        public bool Write(object value, ByteBuffer buffer) => buffer.Write((float) value);

        public object Read(ByteBuffer buffer, bool peek = false) => buffer.ReadFloat(peek);
    }
}