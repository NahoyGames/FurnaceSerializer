using System;
using System.Numerics;
using FurnaceSerializer.Tests.Packets;
using FurnaceSerializer.Tests.SerializerExtensions;
using NUnit.Framework;

namespace FurnaceSerializer.Tests
{
    [TestFixture]
    public class CustomSerializerTests
    {
        private Serializer _serializer;
        private Random _random;

        [SetUp]
        public void Setup()
        {
            _serializer = new Serializer();
            
            _serializer.RegisterSerializer(new Vector2Serializer());
            _serializer.RegisterType(typeof(PacketCustomSerializer));
            _serializer.RegisterType(typeof(Vector2[]));
            
            _random = new Random();
        }

        [Test]
        public void CustomSerializer()
        {
            for (int i = 0; i < 1000; i++)
            {
                var input = new PacketCustomSerializer
                    (
                        new Vector2(_random.Next(), _random.Next()),
                    new[]
                        {
                            new Vector2(_random.Next(), _random.Next()),
                            new Vector2(_random.Next(), _random.Next())
                        }
                    );
                var result = _serializer.Serialize(input);
                var output = _serializer.Deserialize(result);
                
                Assert.AreEqual(input.GetType(), output.GetType()); // Type
                Assert.AreEqual(input.SomeVector2, ((PacketCustomSerializer) output).SomeVector2); // Single value
                Assert.AreEqual(input.SomeVector2Array, ((PacketCustomSerializer)output).SomeVector2Array); // Array
            }
        }
    }
}