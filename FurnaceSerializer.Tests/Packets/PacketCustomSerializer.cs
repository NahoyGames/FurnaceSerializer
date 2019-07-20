using System.Numerics;

namespace FurnaceSerializer.Tests.Packets
{
    public struct PacketCustomSerializer
    {
        [FurnaceSerializable] public Vector2 SomeVector2;
        [FurnaceSerializable] public Vector2[] SomeVector2Array;

        public PacketCustomSerializer(Vector2 someVector2, Vector2[] someVector2Array)
        {
            SomeVector2 = someVector2;
            SomeVector2Array = someVector2Array;
        }
    }
}