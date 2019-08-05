using System;
using System.Numerics;

namespace FurnaceSerializer.Tests.SerializerExtensions
{
    public class Vector2Serializer : ISerializer
    {
        public Type Type => typeof(Vector2);

        public int SizeOf(object value) => sizeof(float) * 2;

        public bool Write(object value, ByteBuffer buffer) => buffer.Write(((Vector2)value).X) && buffer.Write(((Vector2)value).Y);
        

        public object Read(ByteBuffer buffer, bool peek = false)
        {
            float x = buffer.ReadFloat(peek);
            float y = buffer.ReadFloat(peek);
            
            return new Vector2(x, y);
        }
    }
}