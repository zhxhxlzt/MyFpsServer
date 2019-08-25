using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Linq;

namespace MyFpsServer
{
    class Waiter
    {
        TcpListener listener;
        TcpClient audience;

        public async void StartListen()
        {
            listener = new TcpListener(IPAddress.Parse("192.168.1.112"), 1234);
            listener.Start();
            Console.WriteLine("开始建立连接");
            audience = await listener.AcceptTcpClientAsync();
            Console.WriteLine("连接建立完成");

            while (true)
            {
                Console.Write("请输入信息: ");
                string words = Console.ReadLine();
                if (words == "exit") break;
                SendWord(words);
            }
        }

        public async void SendWord( string word )
        {
            if (audience != null)
            {
                var bytes = Encoding.UTF8.GetBytes(word);
                await audience.GetStream().WriteAsync(bytes, 0, bytes.Length);
                Console.WriteLine("发送完成");
            }
            else
            {
                Console.WriteLine("没有建立连接");
            }
        }

        public async void RcvWord()
        {
            while (true)
            {
                if (audience != null && audience.GetStream().CanRead)
                {
                    byte[] bytes = new byte[64];
                    await audience.GetStream().ReadAsync(bytes);
                    var str = Encoding.UTF8.GetString(bytes);
                    str = new string(str.TakeWhile(x => x != '\0').ToArray());
                    Console.WriteLine("收到信息: " + Encoding.UTF8.GetString(bytes));
                }
            }
        }
    }
}
