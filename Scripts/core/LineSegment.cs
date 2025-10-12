using Godot;
using System;

public readonly struct LineSegment : IEquatable<LineSegment>
{
    public readonly Vector2 A;
    public readonly Vector2 B;

    public LineSegment(Vector2 a, Vector2 b)
    {
        if (a.X < b.X || (a.X == b.X && a.Y < b.Y))
        {
            A = a;
            B = b;
        }
        else
        {
            A = b;
            B = a;
        }
    }

    public bool Equals(LineSegment other)
    {
        return A.Equals(other.A) && B.Equals(other.B);
    }

    public override bool Equals(object obj)
    {
        return obj is LineSegment other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(A, B);
    }

    public static bool operator ==(LineSegment left, LineSegment right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(LineSegment left, LineSegment right)
    {
        return !(left == right);
    }
}