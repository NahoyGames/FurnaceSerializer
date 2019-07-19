using System;

namespace FurnaceSerializer.Internal
{
    internal sealed class ArraySerializer : ISerializer
    {
        private readonly FurnaceSerializer _main; // Needs access to FindSerializer(...);

        public ArraySerializer(FurnaceSerializer main)
        {
            _main = main;
        }

        public Type Type => typeof(Array);

        public int SizeOf(object value)
        {
            var size = sizeof(ushort); // Num items

            if (!FindSerializer(value, out var itemSerializer)) { return -1; } // Serializer for array's elements
            
            foreach (var item in (Array)value) // Elements
            {
                var s = itemSerializer.SizeOf(item);
                
                if (s < 0) { return -1; }

                size += s;
            }

            return size;
        }

        public bool Write(object value, byte[] buffer, ref int position)
        {
            var startIndex = position; // Return to start if something goes wrong

            if (!FindSerializer(value, out var itemSerializer)) { return false; } // Serializer for array's elements
            
            SerializerUtil.WriteUShort((ushort)((Array) value).Length, buffer, ref position); // Num items
            
            foreach (var item in (Array)value) // Elements
            {
                if (!itemSerializer.Write(item, buffer, ref position))
                {
                    position = startIndex;
                    return false;
                }
            }

            return true;
        }

        public object Read(byte[] buffer, ref int position, bool peek = false)
        {
            throw new NotImplementedException("Array needs to know element type! Use ReadArray() instead.");
        }

        public Array ReadArray(byte[] buffer, ref int position, Type array, bool peek = false)
        {
            var element = array.GetElementType();
            if (!FindSerializer(element, out var itemSerializer)) { return null; }
            
            var arr = Array.CreateInstance(element, SerializerUtil.ReadUShort(buffer, ref position, peek));

            for (var i = 0; i < arr.Length; i++)
            {
                arr.SetValue(itemSerializer.Read(buffer, ref position, peek), i);
            }

            return arr;
        }

        private bool FindSerializer(object array, out ISerializer value)
        {
            value = _main.FindSerializer(array.GetType().GetElementType());
            return value != null;
        }
    }
}