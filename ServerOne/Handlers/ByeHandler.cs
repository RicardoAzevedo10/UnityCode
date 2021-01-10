using System.Text;

namespace ServerOne.Handlers
{
    public class ByeHandler : IHandle
    {
        public Tlv Handle(Tlv tlv)
        {
            string value = $"Bye, {Encoding.ASCII.GetString(tlv.Value)}";
            return new Tlv(tlv.Tag, value);
        }
    }
}