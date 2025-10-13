using Godot;

partial class HexagonBlock : PatternBlock
{
    public const string IconPath = "res://Assets/pattern_blocks/hexagon.png";

    public override int RotationalSymmetry => 6;

    public override Color Color { get; set; } = Colors.Red;

    public HexagonBlock(Vector2 cartesianPosition, Vector2? scale = null, float rotationDeg = 0, Color? color = null, bool? filled = false) : base(cartesianPosition, scale, rotationDeg, color, filled)
    {
    }

    protected override void BuildOriginalVertices()
    {
        OriginalVertices = [
            new(-BASE_LENGTH, 0),
            new(-BASE_LENGTH / 2, TRIANGLE_HEIGHT),
            new(BASE_LENGTH / 2, TRIANGLE_HEIGHT),
            new(BASE_LENGTH, 0),
            new(BASE_LENGTH / 2, -TRIANGLE_HEIGHT),
            new(-BASE_LENGTH / 2, -TRIANGLE_HEIGHT)
        ];
    }
}