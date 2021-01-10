using System;
using System.Threading;
using LiteNetLib;
using Serilog;

namespace LiteNetLibServer
{
    class Program
    {
        static void Main(string[] args)
        {
            int int32 = 0;
            Console.WriteLine(int32);
            Run();
        }

        static void Run()
        {
            Log.Logger = new LoggerConfiguration().WriteTo.Console().CreateLogger();
            DemoServer listener = new DemoServer();
            NetManager server = new NetManager(listener);
            server.Start(3000);
            server.DiscoveryEnabled = true;
            listener.Server = server;

            while (!Console.KeyAvailable)
            {
                server.PollEvents();
                Thread.Sleep(15);
            }
            
            server.Stop();
        }
    }
}