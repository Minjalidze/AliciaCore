using System.Net;
using System.Net.Sockets;
using AliciaCore.Network;

namespace AliciaCore.Client;

public class ClientHandler
{
    private readonly string _serverIp = "127.0.0.1";
    private readonly int _serverPort = 9000;
    private readonly UdpClient _updClient = new();

    private Action<MessageDto>? _whenReply;

    internal ClientHandler() { }
    internal ClientHandler(string serverIp) => _serverIp = serverIp;
    internal ClientHandler(int serverPort) => _serverPort = serverPort;
    internal ClientHandler(Action<MessageDto> replyAction) => _whenReply = replyAction;

    internal ClientHandler(string serverIp, int serverPort)
    {
        _serverIp = serverIp;
        _serverPort = serverPort;
    }

    internal ClientHandler(string serverIp, int serverPort, Action<MessageDto> replyAction)
    {
        _serverIp = serverIp;
        _serverPort = serverPort;
        _whenReply = replyAction;
    }

    public async Task SendPlayerStateAsync(PlayerPositionDto state)
    {
        var message = new MessageDto(PacketType.PlayerPosition, PacketSerializer.Serialize(state));
        var bytes = PacketSerializer.Serialize(message);

        await _updClient.SendAsync(bytes, new IPEndPoint(IPAddress.Parse(_serverIp), _serverPort));

        if (_whenReply is not null)
        {
            var reply = await _updClient.ReceiveAsync();
            var response = PacketSerializer.Deserialize<MessageDto>(reply.Buffer);

            _whenReply(response!);
        }
    }

    public void SetReply(Action<MessageDto> replyAction) =>
        _whenReply = replyAction;
}
