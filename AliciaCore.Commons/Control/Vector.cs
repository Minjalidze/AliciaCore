namespace AliciaCore.Commons.Control;

public struct Vector(int x, int y, int z) : IComparable<Vector>
{
    public int X { readonly get => x; set => x = value; }
    public int Y { readonly get => y; set => y = value; }
    public int Z { readonly get => z; set => z = value; }

    public readonly double Length() =>
        Math.Sqrt(x * x + y * y + z * z);

    public readonly Vector Normalize()
    {
        double len = Length();
        if (len == 0) return new Vector(0, 0, 0);
        return new Vector((int)(x / len), (int)(y / len), (int)(z / len));
    }

    public static Vector Zero => new(0, 0, 0);

    public readonly int Dot(Vector other) =>
        x * other.X + y * other.Y + z * other.Z;

    public readonly Vector Cross(Vector other) =>
        new(
            y * other.Z - z * other.Y,
            z * other.Z - x * other.Y,
            x * other.Y - y * other.X
        );

    public static Vector operator +(Vector a, Vector b) =>
        new(a.X + b.X, a.Y + b.Y, a.Z + b.Z);

    public static Vector operator -(Vector a, Vector b) =>
        new(a.X - b.X, a.Y - b.Y, a.Z - b.Z);

    public static Vector operator *(Vector v, int scalar) =>
        new(v.X * scalar, v.Y * scalar, v.Z * scalar);

    public static Vector operator *(Vector v, double scalar) =>
        new((int)(v.X * scalar), (int)(v.Y * scalar), (int)(v.Z * scalar));

    public readonly int CompareTo(Vector other)
    {
        int thisLengthSquared = x * x + y * y + z * z;
        int otherLengthSquared = other.X * other.X + other.Y * other.Y + other.Z * other.Z;
        return thisLengthSquared.CompareTo(otherLengthSquared);
    }

    public override readonly bool Equals(object? obj) =>
        obj is Vector other && Equals(other);

    public readonly bool Equals(Vector other) =>
        x == other.X && y == other.Y && z == other.Z;

    public override readonly int GetHashCode() =>
        HashCode.Combine(x, y, z);

    public static bool operator ==(Vector left, Vector right) =>
        left.Equals(right);

    public static bool operator !=(Vector left, Vector right) =>
        !(left == right);

    public override readonly string ToString() =>
        $"Vector({X}, {Y}, {Z})";
}
