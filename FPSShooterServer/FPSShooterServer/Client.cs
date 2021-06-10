using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FPSShooterServer
{
	class Client
	{
		public static byte lastID = 0;
		public byte ID;

		TcpClient tcpClient;
		NetworkStream stream;
		Thread reading;

		public Client(TcpClient tcpClient)
		{
			this.tcpClient = tcpClient;
			stream = tcpClient.GetStream();

			ID = lastID;
			lastID++;

			reading = new Thread(() => ReadStream(this));
			reading.Start();

			Console.WriteLine(tcpClient.Client.RemoteEndPoint.ToString() + " connected");
		}

		bool readrunning = true;
		private static void ReadStream(Client client)
		{
			while (client.readrunning)
			{
				byte[] data = new byte[13];

				client.stream.Read(data, 0, 13);
				data[0] = client.ID;

				Console.Write("read - ");
				for (int xy = 0; xy < data.Length; xy++)
				{
					Console.Write(data[xy] + "\t");
				}
				Console.WriteLine();

				for (int i = 0; i < Program.players.Count; i++)
				{
					if (Program.players[i].ID != client.ID)
					{
						WriteStream(Program.players[i], data);
					}
				}
			}
		}

		private static void WriteStream(Client client, byte[] data)
		{
			Console.Write("send - ");
			for (int xy = 0; xy < data.Length; xy++)
			{
				Console.Write(data[xy] + "\t");
			}
			Console.WriteLine();
			client.stream.Write(data, 0, data.Length);
			Console.WriteLine("data sent");
		}
	}
}
