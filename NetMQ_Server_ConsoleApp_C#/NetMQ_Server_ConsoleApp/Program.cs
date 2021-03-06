using NetMQ;
using NetMQ.Sockets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NetMQ_Server_ConsoleApp
{

	//Server Code to Test python Client with C#
    //class Program
    //{
    //    static void Main(string[] args)
    //    {
    //        using (var server = new ResponseSocket())
    //        {
    //            server.Bind("tcp://*:5577");
    //            while (true)
    //            {
    //                var message = server.ReceiveFrameString();
    //                Console.WriteLine("Received {0}", message);
    //                // processing the request
    //                Thread.Sleep(100);
    //                Console.WriteLine("Sending World");
    //                server.SendFrame("World");
    //            }
    //        }
    //    }
    //}

	//Code to Test ZeroMQ push pull with python
    public class Program
    {
        public static void Main(string[] args)
        {
            // Task Worker
            // Connects PULL socket to tcp://localhost:5557
            // collects workload for socket from Ventilator via that socket
            // Connects PUSH socket to tcp://localhost:5558
            // Sends results to Sink via that socket
            Console.WriteLine("====== WORKER ======");
            using (var receiver = new PullSocket(">tcp://127.0.0.1:5557"))
            using (var sender = new PushSocket(">tcp://127.0.0.1:5558"))
            {
                //process tasks forever
                while (true)
                {
                    //workload from the vetilator is a simple delay
                    //to simulate some work being done, see
                    //Ventilator.csproj Proram.cs for the workload sent
                    //In real life some more meaningful work would be done
                    string workload = receiver.ReceiveFrameString();
                    //simulate some work being done
                    Thread.Sleep(int.Parse(workload));
                    //send results to sink, sink just needs to know worker
                    //is done, message content is not important, just the presence of
                    //a message means worker is done.
                    //See Sink.csproj Proram.cs
                    Console.WriteLine("Sending to Sink");
                    sender.SendFrame(string.Empty);
                }
            }
        }
    }
}
