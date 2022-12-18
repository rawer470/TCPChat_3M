using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TcpChat_Client
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Client";
            Console.WriteLine("[CLIENT]");

            Socket socket_sender = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress address = IPAddress.Parse("127.0.0.1");
            IPEndPoint endRemoitePoint = new IPEndPoint(address, 7632);

            Console.WriteLine("Нажмите Enter для подключения");
            Console.ReadLine();
            socket_sender.Connect(endRemoitePoint);
            Console.Write("Пожалуйста, введите ваше имя");
            string name = Console.ReadLine();
            SendMessage(socket_sender, name);

            Action<Socket> taskSendMessage = SendMessageForTask;
            Action<Socket> taskReciveMessage = ReciveMessageForTask;
            IAsyncResult res = taskSendMessage.BeginInvoke(socket_sender, null, null);
            IAsyncResult resreceive =taskReciveMessage.BeginInvoke(socket_sender,null,null);
          taskSendMessage.EndInvoke(res);
            taskReciveMessage.EndInvoke(resreceive);

            Console.ReadLine();
        }

        public static void SendMessageForTask(Socket socket)
        {
            while (true)
            {
                string message = Console.ReadLine();
                SendMessage(socket,message);
            }
        }

        public static void ReciveMessageForTask(Socket socket)
        {
            while (true)
            {
                string answer = ReciveMessage(socket);
                Console.WriteLine(answer);
            }
        }
        public static void SendMessage(Socket socket, string message)
        {
            byte[] bytes_answer = Encoding.Unicode.GetBytes(message);
            socket.Send(bytes_answer);
        }

        public static string ReciveMessage(Socket socket_client)
        {
            byte[] bytes = new byte[1024];
            int num_bytes = socket_client.Receive(bytes);
            return Encoding.Unicode.GetString(bytes, 0, num_bytes);
        }

    }
}
