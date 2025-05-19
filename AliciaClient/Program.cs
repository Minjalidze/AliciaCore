using AliciaCore.Client;

namespace AliciaClient
{
    internal class Program
    {
        public static async Task Main(string[] args)
        {
            var client = new ClientHost();
            client.SetReply(response =>
            {
                Console.WriteLine($"[CLIENT] Reached from server: {response?.Type}");
            });
            await client.Start();
        }
    }
}
