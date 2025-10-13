using Godot;

partial class SquareBlock : PatternBlock
{
    public const string IconPath = "res://Assets/pattern_blocks/square.png";

    public override int RotationalSymmetry => 4;

    public override Color Color { get; set; } = Colors.MediumPurple;

    public SquareBlock(Vector2 cartesianPosition, Vector2? scale = null, float rotationDeg = 0, Color? color = null, bool? filled = false) : base(cartesianPosition, scale, rotationDeg, color, filled)
    {
    }

    protected override void BuildOriginalVertices()
    {
        OriginalVertices = [
            new(-BASE_LENGTH / 2, -BASE_LENGTH / 2),
            new(BASE_LENGTH / 2, -BASE_LENGTH / 2),
            new(BASE_LENGTH / 2, BASE_LENGTH / 2),
            new(-BASE_LENGTH / 2, BASE_LENGTH / 2)
        ];
    }
}