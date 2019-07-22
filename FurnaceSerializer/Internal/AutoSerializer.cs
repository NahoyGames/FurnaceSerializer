using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace FurnaceSerializer.Internal
{
    /// <summary>
    /// A serializer for objects and structs, which recursively attempts to serialize the type's fields.
    ///
    /// All field marked with the attribute [FurnaceSerializable] will be processed by the AutoSerializer.
    /// Additionally, every field types MUST have a registered ISerializer to match it.
    /// </summary>
    internal class AutoSerializer : ISerializer
    {
        private readonly IEnumerable<FieldInfo> _fields;
        private readonly Serializer _main;
        
        public Type Type { get; }

        public AutoSerializer(Type type, Serializer main)
        {
            Type = type;
            _main = main;

            _fields = type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                .Where(f => Attribute.IsDefined(f, typeof(FurnaceSerializableAttribute)));
        }

        public int SizeOf(object value) => _fields.Sum(field => _main.SizeOf(field.GetValue(value)));

        public bool Write(object value, byte[] buffer, ref int position)
        {
            foreach (var field in _fields)
            {
                if (!_main.Write(field.GetValue(value), buffer, ref position))
                {
                    return false;
                }
            }

            return true;
        }

        public object Read(byte[] buffer, ref int position, bool peek = false)
        {
            var instance = Activator.CreateInstance(Type);
            
            foreach (var field in _fields)
            {
                field.SetValue(instance, _main.Read(field.FieldType, buffer, ref position, peek));
            }

            return instance;
        }
    }
}