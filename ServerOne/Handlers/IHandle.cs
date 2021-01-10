namespace ServerOne.Handlers {
    public interface IHandle
    {
        Tlv Handle(Tlv tlv);
    }
}