using System;
using System.Collections.Generic;
using FurnaceSerializer.Internal;

namespace FurnaceSerializer
{
    public class FurnaceSerializer
    {
        private byte[] _buffer;

        private readonly Dictionary<Type, ISerializer> _serializers;

        /// <summary>
        /// Creates an instance of the FurnaceSerializer
        /// </summary>
        public FurnaceSerializer(bool useDefaultSerializer = true, params ISerializer[] serializers)
        {


            // Serializers
            _serializers = new Dictionary<Type, ISerializer>();
            if (useDefaultSerializer)
            {
                RegisterWriters
                (
                    new BoolSerializer(), new ByteSerializer(), new CharSerializer(), new DoubleSerializer(),
                    new FloatSerializer(), new IntSerializer(), new LongSerializer(), new ShortSerializer(),
                    new StringSerializer(), new SByteSerializer(), new UIntSerializer(), new ULongSerializer(),
                    new UShortSerializer(), new ArraySerializer(this)
                );
            }

            RegisterWriters(serializers);
        }

        /// <summary>
        /// Register a writer to handle one certain type of data.
        /// Used to expand on the built-in supported types
        /// </summary>
        public void RegisterWriter(ISerializer serializer)
        {
            _serializers.Add(serializer.Type, serializer);
        }

        /// <summary>
        /// Register multiple writers at once.
        /// </summary>
        /// <seealso cref="RegisterWriter"/>
        public void RegisterWriters(params ISerializer[] serializers)
        {
            foreach (var writer in serializers)
            {
                RegisterWriter(writer);
            }
        }

        public ISerializer FindSerializer(Type type) => _serializers.TryGetValue(type, out var value) ? value : null;
    }
}