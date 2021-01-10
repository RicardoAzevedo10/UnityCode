using System.Collections.Generic;
using ServerOne.Handlers;

namespace ServerOne
{
    public class Protocol
    {
        private readonly Dictionary<byte, IHandle> handlers;

        public Protocol()
        {
            handlers = new Dictionary<byte, IHandle>();
        }
        
        public void RegisterHandler(byte key, IHandle handler)
        {
            handlers.Add(key, handler);
        }
        
        public IHandle this[byte key]
        {
            get
            {
                IHandle handler;

                if (handlers.TryGetValue(key, out handler))
                {
                    return handler;
                }

                return null;
            }
        }
        
        public Tlv Decode(byte[] buffer)
        {
            return Tlv.Decode(buffer);
        }

        public byte[] Encode(Tlv tlv)
        {
            return tlv.Encode();
        }
    }
}