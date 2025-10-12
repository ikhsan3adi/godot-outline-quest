using Godot;

partial class HexagonBlock : PatternBlock
{
    public HexagonBlock(Vector2 cartesianPosition, Vector2? scale = null, float rotationDeg = 0, Color? color = null, bool? filled = false) : base(cartesianPosition, scale, rotationDeg, color, filled)
    {
        Color = color ?? Colors.Red;
    }

    protected override void BuildOriginalVertices()
    {
        Vertices = [
            new(-BASE_LENGTH, 0),
            new(-BASE_LENGTH / 2, TRIANGLE_HEIGHT),
            new(BASE_LENGTH / 2, TRIANGLE_HEIGHT),
            new(BASE_LENGTH, 0),
            new(BASE_LENGTH / 2, -TRIANGLE_HEIGHT),
            new(-BASE_LENGTH / 2, -TRIANGLE_HEIGHT)
        ];
    }
}