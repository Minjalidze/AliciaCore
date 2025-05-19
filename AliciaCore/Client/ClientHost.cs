using AliciaCore.Network;

namespace AliciaCore.Client;

public class ClientHost
{
    private readonly ClientHandler _handler;

    public ClientHost() => _handler = new ClientHandler();
    public ClientHost(string serverIp) => _handler = new ClientHandler(serverIp);
    public ClientHost(int serverPort) => _handler = new ClientHandler(serverPort);
    public ClientHost(Action<MessageDto> replyAction) => _handler = new ClientHandler(replyAction);
    public ClientHost(string serverIp, int serverPort) => _handler = new ClientHandler(serverIp, serverPort);
    public ClientHost(string serverIp, int serverPort, Action<MessageDto> replyAction) => _handler = new ClientHandler(serverIp, serverPort, replyAction);

    public async Task Start()
    {
        var position = new VectorDto(10, 0, 0);
        var rotation = new VectorDto(0, 90, 0);

        var playerState = new PlayerPositionDto(Guid.NewGuid(), position, rotation);

        while (true)
        {
            await _handler.SendPlayerStateAsync(playerState);
            await Task.Delay(1000);
        }
    }

    public void SetReply(Action<MessageDto> replyAction) =>
        _handler.SetReply(replyAction);
} 
