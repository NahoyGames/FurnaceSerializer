using System;
using System.Text;

namespace FurnaceSerializer.Internal
{
    internal sealed class StringSerializer : ISerializer
    {
        public Type Type => typeof(string);

        public int SizeOf(object value) => sizeof(ushort) + Encoding.UTF8.GetByteCount((string) value);

        public bool Write(object value, ByteBuffer buffer) => buffer.Write((string) value);

        public object Read(ByteBuffer buffer, bool peek = false) => buffer.ReadString(peek);
    }
}