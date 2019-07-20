namespace FurnaceSerializer.Tests.Packets
{
    public struct PacketComplete
    {
        [FurnaceSerializable] public string SomeString;
        [FurnaceSerializable] public float SomeFloat;
        [FurnaceSerializable] public int[] SomeIntArray;
        [FurnaceSerializable] public string[] SomeStringArray;
        [FurnaceSerializable] public int[][] SomeNestedIntArray;

        public PacketComplete(string someString, float someFloat, int[] someIntArray, string[] someStringArray, int[][] someNestedIntArray)
        {
            SomeString = someString;
            SomeFloat = someFloat;
            SomeIntArray = someIntArray;
            SomeStringArray = someStringArray;
            SomeNestedIntArray = someNestedIntArray;
        }
    }
}