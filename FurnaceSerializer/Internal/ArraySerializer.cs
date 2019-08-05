using System;

namespace FurnaceSerializer.Internal
{
    internal class ArraySerializer : ISerializer
    {
        private readonly Serializer _main;
        private readonly Type _elementType;
        
        public Type Type { get; }

        public ArraySerializer(Type arrayType, Serializer main)
        {
            Type = arrayType;

            _elementType = Type.GetElementType();
            _main = main;

            if (!Type.IsArray)
            {
                throw new ArgumentException("ArraySerializers can only serialize arrays!");
            }
        }

        public int SizeOf(object value)
        {
            var size = sizeof(ushort); // Length
            
            foreach (var element in (Array)value) // Elements
            {
                size += _main.SizeOf(element);
            }

            return size;
        }

        public bool Write(object value, ByteBuffer buffer)
        {
            buffer.Write((ushort) ((Array) value).Length); // Length

            foreach (var element in (Array)value)
            {
                if (!_main.Write(element, buffer))
                {
                    return false;
                }
            }

            return true;
        }

        public object Read(ByteBuffer buffer, bool peek = false)
        {
            int length = buffer.ReadUShort(peek); // Length
            var instance = Array.CreateInstance(_elementType, length);
            
            for (var i = 0; i < instance.Length; i++)
            {
                instance.SetValue(_main.Read(_elementType, buffer, peek), i); // Elements
            }

            return instance;
        }
    }
}