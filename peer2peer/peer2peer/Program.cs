using System;
using System.Net;
using System.Net.Sockets;


namespace peer2peer
{
    public class Program
    {
        static string PeerAddress = "172.17.10.12";
        static int PeerPort = 31337;

        public static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Program started");
                Console.WriteLine("Press F1 to start chain");
                Console.WriteLine("Press F2 to start chain, and receive after wards");
                Console.WriteLine("Press anything else to receive number from chain.");
                int number = 0;

                IPAddress sendingAddress;

                string address = "";
                if (Console.ReadKey().Key == ConsoleKey.F1)
                {
                    do
                    {
                        Console.Write("Enter IP address to send number to: ");
                        address = Console.ReadLine();
                    } while (!(IPAddress.TryParse(address, out sendingAddress)));
                    PeerAddress = sendingAddress.ToString();
                    ForwardNumber(0);

                    //number = ReceiveNumber();
                }
                if(Console.ReadKey().Key == ConsoleKey.F2)
                {
                    do
                    {
                        Console.Write("Enter IP address to send number to: ");
                        address = Console.ReadLine();
                    } while (!(IPAddress.TryParse(address, out sendingAddress)));
                    PeerAddress = sendingAddress.ToString();
                    ForwardNumber(0);

                    number = ReceiveNumber();  
                }
                else
                {
                    number = ReceiveNumber();
                    ForwardNumber(number);
                }

                Console.WriteLine("Press ESC to close app...");
                if(Console.ReadKey().Key == ConsoleKey.Escape)
                    Environment.Exit(0);
            }
            
        }

        public static void ForwardNumber(int number)
        {
            number++;
            TcpClient tcpClient = new TcpClient();
            try
            {
                if (tcpClient.Connected)
                {
                    Console.WriteLine($"Connection to {PeerAddress}:{PeerPort} established :)");
                    NetworkStream stream = tcpClient.GetStream();

                    byte[] bytes = BitConverter.GetBytes(number);

                    stream.Write(bytes, 0, bytes.Length);
                    tcpClient.Connect(PeerAddress, PeerPort);
                    tcpClient.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Could no establish a connection to {PeerAddress}:{PeerPort} :(");
            }
                  
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

            int number = BitConverter.ToInt32(buffer, 0);
            Console.WriteLine($"{number} received");
            Console.ReadLine();
            listener.Stop();
            return number;
        }
    }
}