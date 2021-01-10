using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using ServerOne.Handlers;

namespace ServerOne
{
    public class DemoServer
    {
        public event Action OnDisconnect = delegate { };
        public event Action OnConnect = delegate { };
        public event Action OnStart = delegate { };

        private bool running;
        private Protocol protocol;

        public DemoServer()
        {
            protocol = new Protocol();
            protocol.RegisterHandler(0x41, new HelloHandler());
            protocol.RegisterHandler(key: 0x42, new ByeHandler());
        }

        public async Task Start()
        {
            //TcpListener socket = TcpListener.Create(8080);
            TcpListener socket = new TcpListener(IPAddress.Parse("127.0.0.1"), 8080);
            socket.Start();
            running = true;
            OnStart();

            Debug.WriteLine("Server listening");
            await Loop(socket);
            Debug.WriteLine("Terminating Server");
        }

        public void Stop()
        {
            running = false;
        }

        private async Task HandleConnection(TcpClient socket)
        {
            Debug.WriteLine($"Client Connected: {socket.Client.RemoteEndPoint}");
            OnConnect();
            using Channel channel = new Channel(socket, protocol);
            await channel.Start();
            OnDisconnect();
        }

        private async Task Loop(TcpListener socket)
        {
            while (running)
            {
                TcpClient client = await socket.AcceptTcpClientAsync();
                Task task = HandleConnection(client);

                // rethrow exceptions
                if (task.IsFaulted)
                {
                    await task;
                }
            }
        }
    }
}