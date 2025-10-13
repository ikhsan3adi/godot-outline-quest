using Godot;

/// <summary>
/// aka Parallelogram
/// </summary>
partial class RhombusBlock : PatternBlock
{
    public const string IconPath = "res://Assets/pattern_blocks/parallelogram.png";

    public override int RotationalSymmetry => 2;

    public override Color Color { get; set; } = Colors.Green;

    public RhombusBlock(Vector2 cartesianPosition, Vector2? scale = null, float rotationDeg = 0, Color? color = null, bool? filled = false) : base(cartesianPosition, scale, rotationDeg, color, filled)
    {
    }

    protected override void BuildOriginalVertices()
    {
        OriginalVertices = [
            new(-TRIANGLE_HEIGHT, 0),
            new(0, BASE_LENGTH / 2),
            new(TRIANGLE_HEIGHT, 0),
            new(0, -BASE_LENGTH / 2)
        ];
    }
}