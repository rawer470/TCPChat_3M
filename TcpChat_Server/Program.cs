using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TcpChat_Server
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Server";
            Console.WriteLine("[SERVER]");
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress address = IPAddress.Parse("127.0.0.1");
            IPEndPoint endPoint = new IPEndPoint(address, 7632);
            socket.Bind(endPoint);
            socket.Listen(1);
            Console.WriteLine("Ожидаем звонка от клиента...");
            Socket socket_client = socket.Accept();
            Console.WriteLine("Клиент на связи!");
            byte[] bytes = new byte[1024];
           
            while (true)
            {
                int num_bytes = socket_client.Receive(bytes);
                string textFromClient = Encoding.Unicode.GetString(bytes, 0, num_bytes);
                Console.WriteLine(textFromClient);


                //ответное сообщение т сервера клиенту
                string answer = "Server: OK";
                
                byte[] bytes_answer = Encoding.Unicode.GetBytes(answer);
                socket_client.Send(bytes_answer);

            }
            Console.ReadLine();
        }
    }
}
