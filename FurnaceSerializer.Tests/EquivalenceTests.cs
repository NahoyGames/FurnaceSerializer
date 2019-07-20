using System;
using FurnaceSerializer.Tests.Packets;
using NUnit.Framework;

namespace FurnaceSerializer.Tests
{
    public class Tests
    {
        private FurnaceSerializer _serializer;
        private Random _random;

        private PacketComplete _packetComplete;
        
        [SetUp]
        public void Setup()
        {
            _serializer = new FurnaceSerializer(); // Default w/ no customs
            
            _serializer.RegisterType(typeof(PacketSimpleStruct));
            _serializer.RegisterType(typeof(PacketSimpleClass));
            _serializer.RegisterType(typeof(string[]));
            _serializer.RegisterType(typeof(int[]));
            _serializer.RegisterType(typeof(int[][]));
            _serializer.RegisterType(typeof(PacketComplete));
            
            _random = new Random();
            
            // Packets
            _packetComplete = new PacketComplete(
                "hello world",
                3.14f,
                new[] {4, 2, 0},
                new[] {"lorem", "ipsum", "what follows this again?"},
                new[] { new[] {1, 0, 0}, new[] {0, 1, 0}, new[] {0, 0, 1} }); // identity mat3x3
        }

        [Test]
        public void SimpleStruct()
        {
            for (int i = 0; i < 1000; i++)
            {
                var input = new PacketSimpleStruct(_random.Next(), _random.Next());
                var result = _serializer.Serialize(input);
                var output = _serializer.Deserialize(result);

                Assert.AreEqual(input.GetType(), output.GetType()); // Type
                Assert.AreEqual(input.MyPublicNumber, ((PacketSimpleStruct) output).MyPublicNumber); // Public member
                Assert.AreEqual(input.MyPrivateNumber, ((PacketSimpleStruct) output).MyPrivateNumber); // Private member
            }
        }
        
        [Test]
        public void SimpleClass()
        {
            for (int i = 0; i < 1000; i++)
            {
                var input = new PacketSimpleClass(_random.Next(), _random.Next());
                var result = _serializer.Serialize(input);
                var output = _serializer.Deserialize(result);

                Assert.AreEqual(input.GetType(), output.GetType()); // Type
                Assert.AreEqual(input.MyPublicNumber, ((PacketSimpleClass) output).MyPublicNumber); // Public member
                Assert.AreEqual(input.MyPrivateNumber, ((PacketSimpleClass) output).MyPrivateNumber); // Private member
            }
        }

        [Test]
        public void Complete()
        {
            for (int i = 0; i < 1000; i++)
            {
                var input = _packetComplete;
                var result = _serializer.Serialize(input);
                var output = _serializer.Deserialize(result);
                
                Assert.AreEqual(input.GetType(), output.GetType()); // Type
                Assert.AreEqual(input.SomeFloat, ((PacketComplete)output).SomeFloat); // SomeFloat
                Assert.AreEqual(input.SomeString, ((PacketComplete)output).SomeString); // SomeString
                Assert.AreEqual(input.SomeIntArray, ((PacketComplete)output).SomeIntArray); // SomeIntArray
                Assert.AreEqual(input.SomeStringArray, ((PacketComplete)output).SomeStringArray); // SomeStringArray
                Assert.AreEqual(input.SomeNestedIntArray, ((PacketComplete)output).SomeNestedIntArray); // SomeStringArray
            }
        }
    }
}