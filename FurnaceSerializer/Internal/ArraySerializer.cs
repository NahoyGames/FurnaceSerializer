using System;

namespace FurnaceSerializer.Internal
{
    internal class ArraySerializer : ISerializer
    {
        private readonly FurnaceSerializer _main;
        private readonly Type _elementType;
        
        public Type Type { get; }

        public ArraySerializer(Type arrayType, FurnaceSerializer main)
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

        public bool Write(object value, byte[] buffer, ref int position)
        {
            SerializerUtil.WriteUShort((ushort)((Array)value).Length, buffer, ref position); // Length

            foreach (var element in (Array)value)
            {
                if (!_main.Write(element, buffer, ref position))
                {
                    return false;
                }
            }

            return true;
        }

        public object Read(byte[] buffer, ref int position, bool peek = false)
        {
            int length = SerializerUtil.ReadUShort(buffer, ref position, peek);
            var instance = Array.CreateInstance(_elementType, length);
            
            for (var i = 0; i < instance.Length; i++)
            {
                instance.SetValue(_main.Read(_elementType, buffer, ref position, peek), i);
            }

            return instance;
        }
    }
}