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
		public byte HUE;

		TcpClient tcpClient;
		NetworkStream stream;
		Thread reading;

		public Client(TcpClient tcpClient)
		{
			Random rnd = new Random();
			HUE = (byte)rnd.Next(0, 256);
			Console.WriteLine(HUE);

			this.tcpClient = tcpClient;
			stream = tcpClient.GetStream();

			ID = lastID;
			lastID++;

			reading = new Thread(() => ReadStream(this));
			reading.Start();

			Console.WriteLine(tcpClient.Client.RemoteEndPoint.ToString() + " connected");




			for (int i = 0; i < Program.players.Count; i++)
			{
				byte[] data = new byte[13];
				for (int j = 0; j < data.Length; j++)
				{
					data[j] = 0;
				}
				data[0] = ID;
				data[1] = 255;
				data[2] = 0;
				data[3] = 100;
				data[4] = HUE;

				if (Program.players[i].ID != ID)
				{
					try
					{
						WriteStream(Program.players[i], data);

					}
					catch
					{
						//akinek most küldjük lecsatlakozott
						Program.players[i].Disconnect();

						i--;
					}
				}
			}
		}

		public void Disconnect()
		{
			stream.Close();
			readrunning = false;
			Program.players.Remove(this);

			byte[] data = new byte[13];
			for (int i = 0; i < data.Length; i++)
			{
				data[i] = 0;
			}
			data[0] = ID;
			data[1] = 254;

			for (int i = 0; i < Program.players.Count; i++)
			{
				try
				{
					if (Program.players[i].ID != ID)
					{
						WriteStream(Program.players[i], data);
					}
				}
				catch
				{

				}
			}
		}

		bool readrunning = true;
		private static void ReadStream(Client client)
		{
			while (client.readrunning)
			{
				try
				{
					byte[] data = new byte[13];

					client.stream.Read(data, 0, 13);

					if (data[0] != 255)
					{
						data[0] = client.ID;

						if (data[1] == 255)
						{
							data[4] = client.HUE;
						}
					}


					//Debug
					string message = "read - ";
					if (data[9] == 0 && data[10] == 0 && data[11] == 0 && data[12] == 0)
					{
						for (int xy = 0; xy < data.Length; xy++)
						{
							message += data[xy] + "\t";
						}
						Console.WriteLine(message);
					}
					//Debug
					

					for (int i = 0; i < Program.players.Count; i++)
					{
						if (Program.players[i].ID != client.ID)
						{
							try
							{
								WriteStream(Program.players[i], data);

							}
							catch
							{
								//akinek most küldjük lecsatlakozott
								Program.players[i].Disconnect();

								i--;
							}
						}
					}
				}
				catch
				{
					//error at reading
				}
			}
		}

		private static void WriteStream(Client client, byte[] data)
		{
			client.stream.Write(data, 0, data.Length);
		}
	}
}
