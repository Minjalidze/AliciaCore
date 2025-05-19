using AliciaCore.Server;

namespace AliciaServer
{
    internal class Program
    {
        public static async Task Main(string[] args)
        {
            var serverHost = new ServerHost();
            serverHost.SetResponse(playerPosition =>
            {
                Console.WriteLine($"[SERVER] Reached from {playerPosition.PlayerId}: \n[{playerPosition.Position}, {playerPosition.Rotation}]");
            });
            await serverHost.StartAsync();
        }
    }
}
