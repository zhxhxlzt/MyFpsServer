using System;
using System.Net;
using System.Net.Sockets;

namespace MyFpsServer
{
    class Program
    {
        static Waiter waiter;
        static void Main( string[] args )
        {
            string m = "abccd";
            waiter = new Waiter();
            waiter.StartListen();
            Console.WriteLine("main: 开始监听");
            waiter.RcvWord();
            Console.WriteLine("main: 开始接收");
            while (true) ;
        }
    }
}
