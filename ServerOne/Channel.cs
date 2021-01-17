using System;
using System.Buffers;
using System.Diagnostics;
using System.Net.Sockets;
using System.Threading.Tasks;
using ServerOne.Handlers;

namespace ServerOne
{
    public class Channel : IDisposable
    {
       //Create a server usign TCP
        private TcpClient socket;
        private NetworkStream stream;
        private Protocol protocol;
        private ArrayPool<byte> buffers;
        private bool isDisposed;

        public Channel(TcpClient socket, Protocol protocol)
        {
            this.socket = socket;
            stream = socket.GetStream();
            buffers = ArrayPool<byte>.Shared;
            this.protocol = protocol;
        }

        public async Task Start()
        {
            while (true)
            {
                byte[] buffer = buffers.Rent(512);
                int bytesCount = await stream.ReadAsync(buffer);

                if (bytesCount <= 0)
                {
                    break;
                }

                Tlv tlv = protocol.Decode(buffer);
                IHandle handler = protocol[tlv.Tag];

                if (handler == null)
                {
                    Debug.WriteLine("Invalid Protocol");
                    break;
                }

                Tlv result = handler.Handle(tlv);
                await stream.WriteAsync(protocol.Encode(result));
                buffers.Return(buffer);
            }

            Debug.WriteLine("Client Disconnected");
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (isDisposed)
            {
                return;
            }
            
            if (disposing)
            {
                socket?.Dispose();
                stream?.Dispose();
                Debug.WriteLine($"Disposing: {GetType()}");
            }

            isDisposed = true;
        }
        
        ~Channel()
        {
            Dispose(false);
        }
    }
}
