using System;
using System.Net;
using System.Net.Sockets;

namespace p2p
{
    public class Program
    {
        static string PeerAddress = "";
        static int PeerPort = 31337;

        public static void Main(string[] args)
        {
            Console.WriteLine();
            Console.ReadLine();
            ReceiveNumber();
        }

        public static int ReceiveNumber()
        {
            TcpListener listener = new TcpListener(IPAddress.Parse("0.0.0.0"), PeerPort);
            Console.WriteLine($"Listening on port {PeerPort}");

            listener.Start();

            Console.WriteLine("Awaiting connection...");
            
            TcpClient tcpClient = listener.AcceptTcpClient();
            Console.WriteLine("Connection established");
            Console.ReadLine();

            return 0;
        }
    }
}