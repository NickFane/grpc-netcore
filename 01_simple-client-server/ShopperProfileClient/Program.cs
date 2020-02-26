using System;
using System.Threading.Tasks;
using Grpc.Net.Client;
using ShopperProfileServer;

namespace ShopperProfileConsumer
{
    public class Program
    {
        static void Main(string[] args)
        {
            new Program().RunConsumer().Wait();
        }

        public async Task RunConsumer()
        {
            // macOS doesn't support ASP.NET Core gRPC with TLS. So we're serving our app on an unsecure endpoint
            // This switch must be set before creating the GrpcChannel/HttpClient.
            // For more information see https://docs.microsoft.com/en-us/aspnet/core/grpc/troubleshoot?view=aspnetcore-3.1
            AppContext.SetSwitch(
            "System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
            
            // The port number(5000) must match the port of the gRPC server.
            var channel = GrpcChannel.ForAddress("http://localhost:5000");
            var client = new ShopperProfile.ShopperProfileClient(channel);
            
            // Our shopper repository contains details of shopper's with IDs ranging from 1-5
            Console.WriteLine("Enter a shopperId to search for (1-5)");
            var profile = await client.GetShopperProfileAsync(new ShopperProfileRequest() { ShopperId = Console.ReadLine() });

            Console.WriteLine($"Shopper profile | Id: {profile.ShopperDetails.ShopperId} | Name: {profile.ShopperDetails.Name}");
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
