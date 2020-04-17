using System.Collections.Generic;
using GrpcServer;
using Grpc.Core;
using Grpc.Core.Utils;
using System.Threading.Tasks;
using System;

namespace Grpc.ConsoleApp
{
    public class GreeterService : Greeter.GreeterBase
    {
        public GreeterService()
        {
        }

        public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
        {
            Console.WriteLine("Receive hello from: {0}", request.Name);
            return Task.FromResult(new HelloReply() { Message = $"Hello {request.Name}" });
        }
    }
}