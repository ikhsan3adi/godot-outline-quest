using Godot;

partial class TrapezoidBlock : PatternBlock
{
    public const string IconPath = "res://Assets/pattern_blocks/trapezoid.png";

    public TrapezoidBlock(Vector2 cartesianPosition, Vector2? scale = null, float rotationDeg = 0, Color? color = null, bool? filled = false) : base(cartesianPosition, scale, rotationDeg, color, filled)
    {
        Color = color ?? Colors.Yellow;
    }

    protected override void BuildOriginalVertices()
    {
        Vertices = [
            new(-BASE_LENGTH / 2, TRIANGLE_HEIGHT / 2),
            new(BASE_LENGTH / 2, TRIANGLE_HEIGHT / 2),
            new(BASE_LENGTH, -TRIANGLE_HEIGHT / 2),
            new(-BASE_LENGTH, -TRIANGLE_HEIGHT / 2),
        ];
    }
}