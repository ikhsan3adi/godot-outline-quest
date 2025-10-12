using Godot;
using System.Collections.Generic;

public abstract partial class PatternBlock : Node2D
{
    public Color Color { get; set; } = Colors.White;

    /// <summary>
    /// panjang alas dari segitiga sama sisi (Triangle)
    /// panjang sisi persegi (Square)
    /// </summary>
    public const float BASE_LENGTH = 80;

    public static readonly float TRIANGLE_HEIGHT = (BASE_LENGTH * Mathf.Sqrt(3)) / 2;

    private Vector2 _cartesianPosition;

    /// <summary>
    /// Center / Pivot (Cartesian).
    /// Otomatis menyinkronkan Position (pixel) dari Node2D
    /// </summary>
    public Vector2 CartesianPosition
    {
        get => _cartesianPosition;
        set
        {
            _cartesianPosition = value;
            Position = new Vector2(value.X, -value.Y);
        }
    }

    private float _rotationDeg = 0;

    public float RotationDeg
    {
        get => _rotationDeg;
        set
        {
            _rotationDeg = value;
            RecalculateTransform();
        }
    }

    /// <summary>
    /// blueprint dari bentuk sebelum di-transformasi.
    /// </summary>
    public List<Vector2> OriginalVertices { get; protected set; } = [];

    /// <summary>
    /// blueprint dari bentuk yang di-transformasi.
    /// </summary>
    public List<Vector2> Vertices { get; protected set; } = [];

    /// <summary>
    /// Titik-titik outline, dipakai untuk membuat outline garis
    /// </summary>
    public List<Vector2> Points { get; protected set; } = [];

    public bool filled { get; protected set; } = false;

    private static readonly Primitif primitif = new Primitif();

    public PatternBlock(Vector2 cartesianPosition, Vector2? scale = null, float rotationDeg = 0, Color? color = null, bool? filled = false)
    {
        this.CartesianPosition = cartesianPosition;
        this.Scale = scale ?? this.Scale;
        this.Color = color ?? this.Color;
        this.filled = filled ?? this.filled;
        this._rotationDeg = rotationDeg;

        BuildOriginalVertices();
        RecalculateTransform();

        if (!this.filled)
            BuildPoints();
    }

    public override void _Ready()
    {
        QueueRedraw();
    }

    protected abstract void BuildOriginalVertices();

    public void RecalculateTransform()
    {
        System.Numerics.Matrix4x4 matrix = TransformasiFast.CreateScale(this.Scale.X, this.Scale.Y);

        float radians = Mathf.DegToRad(_rotationDeg);
        TransformasiFast.RotationCounterClockwise(ref matrix, radians, new(0, 0));

        TransformasiFast.ReflectionToX(ref matrix);

        this.Vertices = TransformasiFast.GetTransformPoint(matrix, this.OriginalVertices);
        QueueRedraw();
    }

    protected virtual void BuildPoints()
    {
        Points.Clear();

        for (int i = 0; i < Vertices.Count - 1; i++)
        {
            Vector2 a = Vertices[i];
            Vector2 b = Vertices[i + 1];
            Points.AddRange(primitif.LineBresenham(a.X, a.Y, b.X, b.Y));
        }

        Vector2 c = Vertices[Vertices.Count - 1];
        Vector2 d = Vertices[0];
        Points.AddRange(primitif.LineBresenham(c.X, c.Y, d.X, d.Y));
    }

    /// <summary>
    /// Menerapkan matriks transformasi ke vertex ORIGINAL.
    /// </summary>
    /// <param name="matrix">Matriks Kompostit</param>
    public void ApplyTransformations(System.Numerics.Matrix4x4 matrix)
    {
        if (matrix != System.Numerics.Matrix4x4.Identity)
        {
            Vertices = TransformasiFast.GetTransformPoint(matrix, OriginalVertices);
        }

        if (!this.filled)
            BuildPoints();

        QueueRedraw();
    }

    public override void _Draw()
    {
        if (Vertices.Count == 0) return;

        if (this.filled)
            DrawPolygon();
        else
            DrawWhitePolygon();
    }

    /// unused
    private void DrawLines()
    {
        GraphicsUtils.PutPixelAll(this, Points, GraphicsUtils.DrawStyle.DotDot, Color);
    }

    private void DrawPolygon()
    {
        DrawColoredPolygon(this.Vertices.ToArray(), this.Color);
    }

    private void DrawWhitePolygon()
    {
        DrawColoredPolygon(this.Vertices.ToArray(), Colors.WhiteSmoke);
    }

    public void SetFilled(bool isFilled)
    {
        this.filled = isFilled;

        QueueRedraw();
    }

    public override void _ExitTree()
    {
        base._ExitTree();
    }
}