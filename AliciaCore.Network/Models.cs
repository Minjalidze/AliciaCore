namespace AliciaCore.Network;

public enum PacketType
{
    PlayerPosition,
    ServerSnapshot
}

public record MessageDto(PacketType Type, byte[] Data);
