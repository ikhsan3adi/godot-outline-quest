using Godot;

partial class SquareBlock : PatternBlock
{
    public const string IconPath = "res://Assets/pattern_blocks/square.png";

    public SquareBlock(Vector2 cartesianPosition, Vector2? scale = null, float rotationDeg = 0, Color? color = null, bool? filled = false) : base(cartesianPosition, scale, rotationDeg, color, filled)
    {
        Color = color ?? Colors.MediumPurple;
    }

    protected override void BuildOriginalVertices()
    {
        Vertices = [
            new(-BASE_LENGTH / 2, -BASE_LENGTH / 2),
            new(BASE_LENGTH / 2, -BASE_LENGTH / 2),
            new(BASE_LENGTH / 2, BASE_LENGTH / 2),
            new(-BASE_LENGTH / 2, BASE_LENGTH / 2)
        ];
    }
}