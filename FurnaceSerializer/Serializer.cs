using System;
using System.Collections.Generic;
using FurnaceSerializer.Internal;

namespace FurnaceSerializer
{
    /// <summary>
    /// A modular serializer with networking in mind
    /// </summary>
    public class Serializer
    {
        private readonly Dictionary<Type, RegisteredSerializer> _serializers;
        private readonly List<ISerializer> _headers; // Index is header

        /// <summary>
        /// Creates an instance of the FurnaceSerializer
        ///
        /// If params 'buffer' is null, the Serialize() method will return an exact-size byte[],
        /// otherwise, it will populate the given buffer and return the length
        /// </summary>
        public Serializer(bool useDefaultSerializer = true)
        {
            _serializers = new Dictionary<Type, RegisteredSerializer>();
            _headers = new List<ISerializer>();

            // Defaults
            if (useDefaultSerializer)
            {
                RegisterSerializer(new BoolSerializer());
                RegisterSerializer(new ByteSerializer());
                RegisterSerializer(new CharSerializer());
                RegisterSerializer(new DoubleSerializer());
                RegisterSerializer(new FloatSerializer());
                RegisterSerializer(new IntSerializer());
                RegisterSerializer(new LongSerializer());
                RegisterSerializer(new ShortSerializer());
                RegisterSerializer(new StringSerializer());
                RegisterSerializer(new SByteSerializer());
                RegisterSerializer(new UIntSerializer());
                RegisterSerializer(new ULongSerializer());
                RegisterSerializer(new UShortSerializer());
            }
        }

        /// <summary>
        /// Register a writer to handle one certain type of data.
        /// Used to expand on the built-in supported types
        /// </summary>
        public void RegisterSerializer(ISerializer serializer)
        {
            if (_serializers.ContainsKey(serializer.Type))
            {
                return;
            }
        
            _serializers.Add(serializer.Type, new RegisteredSerializer((ushort)_headers.Count, serializer));
            _headers.Add(serializer);

            if (!serializer.Type.IsArray)
            {
                RegisterType(serializer.Type.MakeArrayType());
            }
        }

        /// <summary>
        /// Register a type of object or struct or array.
        ///
        /// If object:
        /// All of its fields with the attribute [FurnaceSerializable] will be considered when passed into Serialize().
        /// All its fields must either have an ISerializer pre-registered or be objects/struct that were registered
        /// via this method.
        ///
        /// If array:
        /// All its elements and its length will be considered when passed into Serialize(), assuming the element type
        /// is also registered.
        /// </summary>
        public void RegisterType(Type type)
        {
            if (type.IsArray)
            {
                RegisterSerializer(new ArraySerializer(type, this));
            }
            else
            {
                RegisterSerializer(new AutoSerializer(type, this));
            }
        }

        /// <summary>
        /// Is a value, object, or struct [de]serializable?
        /// </summary>
        public bool IsRegistered(Type type)
        {
            return _serializers.ContainsKey(type);
        }

        /// <summary>
        /// Find the size in bytes of a registered value or object. Useful for nested values
        /// </summary>
        public int SizeOf(object value) => _serializers[value.GetType()].Serializer.SizeOf(value);

        /// <summary>
        /// Write a registered value or object to the buffer. Useful for nested values.
        /// </summary>
        public bool Write(object value, byte[] buffer, ref int position) =>
            _serializers[value.GetType()].Serializer.Write(value, buffer, ref position);
        
        /// <summary>
        /// Read a registered value or object from the buffer. Useful for nested values.
        /// </summary>
        public object Read(Type type, byte[] buffer, ref int position, bool peek = false) =>
            _serializers[type].Serializer.Read(buffer, ref position, peek);

        /// <summary>
        /// Serialize a registered object. 'length' factors in the offset specified 
        /// </summary>
        public byte[] Serialize(object value, out int length, int offset = 0, byte[] buffer = null)
        {
            if (_serializers.TryGetValue(value.GetType(), out var registered))
            {
                if (buffer == null)
                {
                    buffer = new byte[offset + registered.Serializer.SizeOf(value) + sizeof(ushort)]; // include header
                }

                var position = offset;
                
                SerializerUtil.WriteUShort(registered.Header, buffer, ref position); // Header

                if (registered.Serializer.Write(value, buffer, ref position)) // Value
                {
                    length = position;
                    return buffer;
                }
            }
            throw new Exception("An error occured while serializing...");
        }

        /// <summary>
        /// Serialize a registered object
        /// </summary>
        public byte[] Serialize(object value) => Serialize(value, out _);

        /// <summary>
        /// Deserialize a registered object
        /// </summary>
        public object Deserialize(byte[] buffer, int offset = 0)
        {
            var position = offset;

            var headerIndex = SerializerUtil.ReadUShort(buffer, ref position, false); // Header
            if (headerIndex >= _headers.Count) { throw new Exception("Deserializing type not recognized!"); }
            var serializer = _headers[headerIndex];

            return serializer.Read(buffer, ref position, false);
        }
    }
}