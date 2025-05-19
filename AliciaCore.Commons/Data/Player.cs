using AliciaCore.Commons.Control;

namespace AliciaCore.Commons.Data;

public class Player(Transform transform)
{
    public Guid Id { get; } = Guid.NewGuid();
    public Transform Transform { get; private set; } =
        transform ?? throw new ArgumentNullException(nameof(transform));

    public Player() : this(new Transform(Vector.Zero, Vector.Zero)) { }

    public void SetTransform(Transform newTransform) =>
        Transform = newTransform ?? throw new ArgumentNullException(nameof(newTransform));

    public void SetPosition(Vector vector) =>
        Transform.Position = vector;
    public void SetPosition(int x, int y, int z) =>
        Transform.Position = new Vector(x, y, z);

    public void SetRotation(Vector vector) =>
        Transform.Rotation = vector;
    public void SetRotation(int x, int y, int z) =>
        Transform.Rotation = new Vector(x, y, z);
}
