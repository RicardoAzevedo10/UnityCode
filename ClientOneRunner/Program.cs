using System;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ClientOneRunner
{
    class Program
    {
        static void Main(string[] args)
        {
            Run().GetAwaiter().GetResult();
        }

        static async Task Run()
        {
            //create a client tcp 
            TcpClient client = new TcpClient("127.0.0.1", 8080);
            NetworkStream stream = client.GetStream();//stream client

            Task.Run(async () =>
            {
                while (true)
                {
                    byte[] readBuffer = new byte[128];
                    int bytesCount = await stream.ReadAsync(readBuffer);

                    if (bytesCount <= 0)
                    {
                        break;
                    }
                    
                    Console.WriteLine(Encoding.ASCII.GetString(readBuffer[2..(2 + bytesCount)]));
                }
            });
            
            while (true)
            {
                byte[] writeBuffer = new byte[128];
                string input = Console.ReadLine();
                int len =  Encoding.ASCII.GetByteCount(input);
                writeBuffer[0] = 0x41;
                writeBuffer[1] = (byte) len;
                Encoding.ASCII.GetBytes(input, 0, len, writeBuffer,2);
                await stream.WriteAsync(writeBuffer);
            }
        }
    }
}
