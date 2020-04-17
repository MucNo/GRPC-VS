using System;
using Grpc.Core;

namespace Grpc.ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var channel = new Channel("127.0.0.1:5001", ChannelCredentials.Insecure);
            var client = new GrpcServer.Greeter.GreeterClient(channel);

            while (true)
            {
                Console.Write("What's your name? (Q = quit) ");
                var name = Console.ReadLine();
                if (name.Equals("Q", StringComparison.InvariantCultureIgnoreCase)) {
                    break;
                }
                var reply = client.SayHello(new GrpcServer.HelloRequest() { Name = name });
                Console.WriteLine("Server replied: {0}", reply.Message);
            }
            channel.ShutdownAsync().Wait();
        }
    }
}
