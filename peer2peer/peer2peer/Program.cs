using System;
using System.Net;
using System.Net.Sockets;

namespace peer2peer
{
    public class Program
    {
        static string PeerAddress = "";
        static int PeerPort = 31337;

        public static void Main(string[] args)
        {
            Console.WriteLine();

            ReceiveNumber();
        }

        public static void ForwardNumber()
        {

        }

        public static int ReceiveNumber()
        {
            TcpListener listener = new TcpListener(IPAddress.Parse("0.0.0.0"), PeerPort);
            Console.WriteLine($"Listening on port {PeerPort}");

            listener.Start();

            Console.WriteLine("Awaiting connection...");
            
            TcpClient tcpClient = listener.AcceptTcpClient();
            Console.WriteLine("Connection established");
            
            NetworkStream stream = tcpClient.GetStream();

            byte[] buffer = new byte[1024];
            int read = stream.Read(buffer, 0, buffer.Length);

            //Console.WriteLine(System.Text.Encoding.UTF8.GetString(buffer,0,read));
            int number = BitConverter.ToInt32(buffer, 0);
            Console.WriteLine($"{number} received");
            Console.ReadLine();

            return number;
        }
    }
}