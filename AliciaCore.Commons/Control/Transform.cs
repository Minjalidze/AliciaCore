namespace AliciaCore.Commons.Control;

public class Transform(Vector position, Vector rotation)
{
    public Vector Position { get => position; set => position = value; }
    public Vector Rotation { get => rotation; set => rotation = value; }

    public override string ToString() =>
        $"Transform[Position={Position}, Rotation={Rotation}]";

    public override bool Equals(object? obj) =>
        obj is Transform other &&
        Position.Equals(other.Position) &&
        Rotation.Equals(other.Rotation);

    public override int GetHashCode() =>
        HashCode.Combine(Position, Rotation);

    public static bool operator ==(Transform? left, Transform? right) =>
        left?.Equals(right) ?? right is null;

    public static bool operator !=(Transform? left, Transform? right) =>
        !(left == right);
}
