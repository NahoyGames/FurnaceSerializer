namespace FurnaceSerializer
{
    public class RegisteredSerializer
    {
        public readonly ushort Header;
        public readonly ISerializer Serializer;

        public RegisteredSerializer(ushort header, ISerializer serializer)
        {
            Header = header;
            Serializer = serializer;
        }
    }
}