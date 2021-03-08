using Grpc.Core;
using Grpc.Net.Client;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Threading.Tasks;

namespace GrpcApm
{
    public class GreeterService : Greeter.GreeterBase
    {
        private readonly Greeter.GreeterClient client;

        public GreeterService()
        {
            client = new Greeter.GreeterClient(GrpcChannel.ForAddress("http://localhost:5000", new GrpcChannelOptions { HttpClient = new HttpClient()}));
        }

        public override async Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
        {
            return await client.SayHello2Async(request);
        }

        public override Task<HelloReply> SayHello2(HelloRequest request, ServerCallContext context)
        {
            return Task.FromResult(new HelloReply
            {
                Message = "Hello " + request.Name
            });
        }
    }
}
