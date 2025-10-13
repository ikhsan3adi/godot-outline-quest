using Godot;

/// <summary>
/// aka Diamond
/// </summary>
partial class ThinRhombusBlock : PatternBlock
{
    public const string IconPath = "res://Assets/pattern_blocks/diamond.png";

    public override int RotationalSymmetry => 2;

    public ThinRhombusBlock(Vector2 cartesianPosition, Vector2? scale = null, float rotationDeg = 0, Color? color = null, bool? filled = false) : base(cartesianPosition, scale, rotationDeg, color, filled)
    {
        Color = color ?? Colors.SkyBlue;
    }

    protected override void BuildOriginalVertices()
    {
        const float halfAngleDeg = 15.0f;
        float halfAngleRad = Mathf.DegToRad(halfAngleDeg);

        float halfLongDiagonal = BASE_LENGTH * Mathf.Cos(halfAngleRad);
        float halfShortDiagonal = BASE_LENGTH * Mathf.Sin(halfAngleRad);

        OriginalVertices = [
            new(0, halfShortDiagonal),
            new(halfLongDiagonal, 0),
            new(0, -halfShortDiagonal),
            new(-halfLongDiagonal, 0)
        ];
    }
}