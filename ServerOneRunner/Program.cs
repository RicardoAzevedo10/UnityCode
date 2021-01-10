using System;
using ServerOne;

namespace ServerOneRunner
{
    class Program
    {
        static void Main(string[] args)
        {
            /*MemoryStream ms = new MemoryStream();
            int len = Encoding.ASCII.GetByteCount("Teste");
            ms.WriteByte(0xF1);
            ms.WriteByte((byte)len);
            ms.Write(Encoding.ASCII.GetBytes("Teste"));
            Tlv tlv = Tlv.Decode(ms.ToArray());
            Console.WriteLine(Encoding.ASCII.GetString(tlv.Value));*/
            
            DemoServer server = new DemoServer();
            server.OnStart += () => Console.WriteLine("Server Started");
            server.OnConnect += () => Console.WriteLine("Client Connected");
            server.OnDisconnect += () => Console.WriteLine("Client Disconnected");
            server.Start().GetAwaiter().GetResult();
        }
    }
}