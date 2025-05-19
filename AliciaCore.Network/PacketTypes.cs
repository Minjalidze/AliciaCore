namespace AliciaCore.Network;

using System.Text.Json;

public record PlayerPositionDto(Guid PlayerId, VectorDto Position, VectorDto Rotation);
public record VectorDto(int X, int Y, int Z);

public static class PacketSerializer
{
    private static readonly JsonSerializerOptions Options = new(JsonSerializerDefaults.General);

    public static byte[] Serialize<T>(T packet) where T : class =>
        JsonSerializer.SerializeToUtf8Bytes(packet, Options);

    public static T? Deserialize<T>(byte[] data) where T : class =>
        JsonSerializer.Deserialize<T>(data, Options);
}
