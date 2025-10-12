using Godot;

partial class ParallelogramBlock : PatternBlock
{
    public ParallelogramBlock(Vector2 cartesianPosition, Vector2? scale = null, float rotationDeg = 0, Color? color = null, bool? filled = false) : base(cartesianPosition, scale, rotationDeg, color, filled)
    {
        Color = color ?? Colors.Green;
    }

    protected override void BuildOriginalVertices()
    {
        Vertices = [
            new(-TRIANGLE_HEIGHT, 0),
            new(0, BASE_LENGTH / 2),
            new(TRIANGLE_HEIGHT, 0),
            new(0, -BASE_LENGTH / 2)
        ];
    }
}