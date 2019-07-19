using System;
using System.Collections.Generic;
using FurnaceSerializer.Internal;

namespace FurnaceSerializer
{
    /// <summary>
    /// A modular serializer with networking in mind
    /// </summary>
    public class FurnaceSerializer
    {
        private byte[] _buffer;
        
        private readonly Dictionary<Type, RegisteredSerializer> _serializers;
        private List<ISerializer> _headers; // Index is header

        /// <summary>
        /// Creates an instance of the FurnaceSerializer
        /// </summary>
        public FurnaceSerializer(bool useDefaultSerializer = true, params ISerializer[] serializers)
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

            // User-defined
            foreach (var serializer in serializers)
            {
                RegisterSerializer(serializer);
            }
        }

        /// <summary>
        /// Register a writer to handle one certain type of data.
        /// Used to expand on the built-in supported types
        /// </summary>
        public void RegisterSerializer(ISerializer serializer)
        {
            _serializers.Add(serializer.Type, new RegisteredSerializer((ushort)_headers.Count, serializer));
            _headers.Add(serializer);
        }

        /// <summary>
        /// Register a type of object or struct.
        /// 
        /// All of its fields with the attribute [FurnaceSerializable] will be considered when passed into Serialize().
        /// All its fields must either have an ISerializer pre-registered or be objects/struct that were registered
        /// via this method.
        /// </summary>
        public void RegisterType(Type type)
        {
            RegisterSerializer(new AutoSerializer(type, this));
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
        public int SizeOf<T>(T value) => _serializers[typeof(T)].Serializer.SizeOf(value);

        /// <summary>
        /// Write a registered value or object to the buffer. Useful for nested values.
        /// </summary>
        public bool Write<T>(T value, byte[] buffer, ref int position) =>
            _serializers[typeof(T)].Serializer.Write(value, buffer, ref position);

        /// <summary>
        /// Read a registered value or object from the buffer. Useful for nested values.
        /// </summary>
        public object Read<T>(byte[] buffer, ref int position, bool peek = false) =>
            _serializers[typeof(T)].Serializer.Read(buffer, ref position, peek);

        /// <summary>
        /// Read a registered value or object from the buffer. Useful for nested values.
        /// </summary>
        public object Read(Type type, byte[] buffer, ref int position, bool peek = false) =>
            _serializers[type].Serializer.Read(buffer, ref position, peek);

        /// <summary>
        /// Serialize a registered object
        /// </summary>
        public byte[] Serialize(object value)
        {
            if (_serializers.TryGetValue(value.GetType(), out var registered))
            {
                var buffer = new byte[registered.Serializer.SizeOf(value) + sizeof(ushort)]; // include header
                var position = 0;
                
                SerializerUtil.WriteUShort(registered.Header, buffer, ref position); // Header

                if (registered.Serializer.Write(value, buffer, ref position)) // Value
                {
                    return buffer;
                }
            }
            throw new Exception("An error occured while serializing...");
        }

        /// <summary>
        /// Deserialize a registered object
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public object Deserialize(byte[] value)
        {
            var buffer = value;
            var position = 0;

            var headerIndex = SerializerUtil.ReadUShort(buffer, ref position, false); // Header
            var serializer = _headers[headerIndex];

            return serializer.Read(buffer, ref position, false);
        }
    }
}