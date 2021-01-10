using System.Text;

namespace ServerOne.Handlers
{
    public class HelloHandler : IHandle
    {
        public Tlv Handle(Tlv tlv)
        {
            string value = $"Hello, {Encoding.ASCII.GetString(tlv.Value)}";
            return new Tlv(tlv.Tag, value);
        }
    }
}