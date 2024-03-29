﻿
/* ================= 10.2.1 닷넷 4.5 BCL에 추가된 Async 메서드 ================= */

using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            TcpListener listener = new TcpListener(IPAddress.Any, 11200);

            listener.Start();

            while (true)
            {
                var client = listener.AcceptTcpClient();
                ProcessTcpClient(client);
            }
        }

        private static async void ProcessTcpClient(TcpClient client)
        {
            NetworkStream ns = client.GetStream();

            byte[] buffer = new byte[1024];
            int received = await new TcpListener(IPAddress.Any, 11200).AcceptTcpClient().GetStream().ReadAsync(buffer, 0, buffer.Length);

            string txt = Encoding.UTF8.GetString(buffer, 0, received);

            byte[] sendBuffer = Encoding.UTF8.GetBytes("Hello: " + txt);
            await ns.WriteAsync(sendBuffer, 0, sendBuffer.Length);

            ns.Close();
        }
    }
}