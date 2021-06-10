using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FPSShooterServer
{
	class Program
	{
		public static TcpListener server;
		public static List<Client> players;
		public static Thread clientwaiterthread;

		static void Main(string[] args)
		{
			//65535
			int port = 55000;
			//IPAddress address = IPAddress.Parse("127.0.0.1");
			IPAddress address = IPAddress.Parse("192.168.100.129");

			players = new List<Client>();

			server = new TcpListener(address, port);
			server.Start();

			clientwaiterthread = new Thread(() => ClientWaiter());
			clientwaiterthread.Start();
		}

		public static bool clientWaiter = true;
		private static void ClientWaiter()
		{
			while (clientWaiter)
			{
				TcpClient client = server.AcceptTcpClient();

				Client player = new Client(client);
				players.Add(player);
			}
		}
	}
}
