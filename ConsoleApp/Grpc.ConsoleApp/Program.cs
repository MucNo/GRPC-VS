using System;
using Grpc.Core;
using GrpcServer;

namespace Grpc.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var boundHost = "0.0.0.0";
            var boundPort = 5001;

            Console.WriteLine("Hello World!");
            Server server = new Server
            {
                Services = { Greeter.BindService(new GreeterService()) },
                Ports = { new ServerPort(boundHost, boundPort, ServerCredentials.Insecure) }
            };
            server.Start();

            Console.WriteLine("RouteGuide server listening on port " + boundPort);
            Console.WriteLine("Press any key to stop the server...");
            Console.ReadKey();

            server.ShutdownAsync().Wait();
        }
    }
}
