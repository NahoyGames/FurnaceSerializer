using System;
using System.Numerics;

namespace FurnaceSerializer.Tests.SerializerExtensions
{
    public class Vector2Serializer : ISerializer
    {
        public Type Type => typeof(Vector2);

        public int SizeOf(object value) => sizeof(float) * 2;

        public bool Write(object value, byte[] buffer, ref int position) =>
            SerializerUtil.WriteFloat(((Vector2)value).X, buffer, ref position)
            && SerializerUtil.WriteFloat(((Vector2)value).Y, buffer, ref position);
        

        public object Read(byte[] buffer, ref int position, bool peek = false)
        {
            float x = SerializerUtil.ReadFloat(buffer, ref position, peek);
            float y = SerializerUtil.ReadFloat(buffer, ref position, peek);
            
            return new Vector2(x, y);
        }
    }
}