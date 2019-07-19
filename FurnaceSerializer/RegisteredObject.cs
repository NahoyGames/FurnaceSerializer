using System;
using System.Collections.Generic;
using System.Reflection;

namespace FurnaceSerializer
{
    internal class RegisteredObject
    {
        public readonly Type Type;
        private readonly ushort _header;

        private readonly FurnaceSerializer _main;

        private readonly Dictionary<FieldInfo, ISerializer> _fields;
        private readonly Dictionary<PropertyInfo, ISerializer> _properties;


        public RegisteredObject(FurnaceSerializer main, Type type, ushort header)
        {
            Type = type;
            _header = header;

            _main = main;

            // Fields
            _fields = new Dictionary<FieldInfo, ISerializer>();
            foreach (var field in type.GetFields())
            {
                if (_main.TryFindSerializer(field.GetType(), out var serializer))
                {
                    _fields.Add(field, serializer);
                }
                else
                {
                    throw new NotSupportedException("The field " + field.Name + " of type " + field.FieldType.Name + " is missing a serializer!");
                }
            }
            // Properties
            _properties = new Dictionary<PropertyInfo, ISerializer>();
            foreach (var property in type.GetProperties())
            {
                if (main.TryFindSerializer(property.GetType(), out var serializer))
                {
                    _properties.Add(property, serializer);
                }
                else
                {
                    throw new NotSupportedException("The property " + property.Name + " of type " + property.PropertyType.Name + " is missing a serializer!");
                }
            }
        }

        private int GetBufferSize(object obj)
        {
            var size = sizeof(ushort); // Header
            
            foreach (var (field, serializer) in _fields) // Fields
            {
                var s = serializer.SizeOf(field.GetValue(obj));
                if (s < 0)
                {
                    return -1;
                }

                size += s;
            }
            foreach (var (property, serializer) in _properties) // Properties
            {
                var s = serializer.SizeOf(property.GetValue(obj));
                if (s < 0)
                {
                    return -1;
                }

                size += s;
            }

            return size;
        }

        public byte[] Serialize(object obj)
        {
            if (Type != obj.GetType()) { throw new ArgumentException("Cannot serialize an object of a different type!"); }

            var buffer = new byte[GetBufferSize(obj)];
            var position = 0;

            SerializerUtil.WriteUShort(_header, buffer, ref position); // Header
            
            foreach (var (field, serializer) in _fields) // Fields
            {
                if (!serializer.Write(field.GetValue(obj), buffer, ref position))
                {
                    throw new Exception("An error occured while serializing...");
                }
            }
            foreach (var (property, serializer) in _properties) // Properties
            {
                if (!serializer.Write(property.GetValue(obj), buffer, ref position))
                {
                    throw new Exception("An error occured while serializing...");
                }
            }

            return buffer;
        }
    }
}