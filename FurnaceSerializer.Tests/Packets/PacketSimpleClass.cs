namespace FurnaceSerializer.Tests.Packets
{
    public class PacketSimpleClass
    {
        [FurnaceSerializable] public int MyPublicNumber;
        [FurnaceSerializable] private int _myPrivateNumber;

        public int MyPrivateNumber => _myPrivateNumber;

        public PacketSimpleClass(int myPublicNumber, int myPrivateNumber)
        {
            MyPublicNumber = myPublicNumber;
            _myPrivateNumber = myPrivateNumber;
        }

        public PacketSimpleClass()
        {
        }
    }
}