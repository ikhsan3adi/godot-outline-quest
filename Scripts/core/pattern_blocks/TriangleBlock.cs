using Godot;

partial class TriangleBlock : PatternBlock
{
    public const string IconPath = "res://Assets/pattern_blocks/triangle.png";

    public TriangleBlock(Vector2 cartesianPosition, Vector2? scale = null, float rotationDeg = 0, Color? color = null, bool? filled = false) : base(cartesianPosition, scale, rotationDeg, color, filled)
    {
        Color = color ?? Colors.Orange;
    }

    protected override void BuildOriginalVertices()
    {
        OriginalVertices = [
            new(0, TRIANGLE_HEIGHT / 2),
            new(-BASE_LENGTH / 2, -TRIANGLE_HEIGHT / 2),
            new(BASE_LENGTH / 2, -TRIANGLE_HEIGHT / 2),
        ];
    }
}