namespace FurnaceSerializer.Tests.Packets
{
    public struct PacketSimpleStruct
    {
        [FurnaceSerializable] public int MyPublicNumber;
        [FurnaceSerializable] private int _myPrivateNumber;

        public int MyPrivateNumber => _myPrivateNumber;

        public PacketSimpleStruct(int myPublicNumber, int myPrivateNumber)
        {
            MyPublicNumber = myPublicNumber;
            _myPrivateNumber = myPrivateNumber;
        }
    }
}