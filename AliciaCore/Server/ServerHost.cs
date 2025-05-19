using System.Net.Sockets;
using AliciaCore.Network;

namespace AliciaCore.Server;

public class ServerHost
{
    private readonly int Port = 9000;
    private readonly UdpClient UdpClient;

    private Action<PlayerPositionDto>? _whenResponse;

    public ServerHost() => UdpClient = new UdpClient(Port);

    public ServerHost(int port)
    {
        Port = port;
        UdpClient = new UdpClient(Port);
    }

    public async Task StartAsync()
    {
        while (true)
        {
            UdpReceiveResult result = await UdpClient.ReceiveAsync();
            HandleIncoming(result);
        }
    }

    public void SetResponse(Action<PlayerPositionDto> whenResponse) => _whenResponse = whenResponse;

    private void HandleIncoming(UdpReceiveResult result)
    {
        var message = PacketSerializer.Deserialize<MessageDto>(result.Buffer);
        if (message?.Type == PacketType.PlayerPosition)
        {
            if (_whenResponse != null)
            {
                var playerData = PacketSerializer.Deserialize<PlayerPositionDto>(message.Data);
                _whenResponse(playerData!);
            }

            var response = new MessageDto(PacketType.ServerSnapshot, []);
            var responseBytes = PacketSerializer.Serialize(response);
            UdpClient.Send(responseBytes, responseBytes.Length, result.RemoteEndPoint);
        }
    }
}
