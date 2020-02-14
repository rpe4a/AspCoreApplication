using System;
using System.Threading.Tasks;
using Grpc.Net.Client;
using SimpleGrpcService;

namespace SimpleGrpcClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using var channel = GrpcChannel.ForAddress("https://localhost:5001");

            var client = new Greeter.GreeterClient(channel);

            Console.WriteLine("Введите ваше имя:");

            string name = Console.ReadLine();

            var reply = await client.SayHelloAsync(new HelloRequest() {Name = name});
            Console.WriteLine("Ответ сервера: " + reply.Message);
            Console.ReadKey();
        }
    }
}
