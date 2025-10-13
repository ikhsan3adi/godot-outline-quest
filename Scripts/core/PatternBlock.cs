using Godot;
using System.Collections.Generic;

public abstract partial class PatternBlock : Node2D
{
    public virtual Color Color { get; set; } = Colors.White;

    /// <summary>
    /// panjang alas dari segitiga sama sisi (Triangle)
    /// panjang sisi persegi (Square)
    /// </summary>
    public const float BASE_LENGTH = 80;

    public static readonly float TRIANGLE_HEIGHT = BASE_LENGTH * Mathf.Sqrt(3) / 2;

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
    /// Menentukan jumlah orientasi simetris yang valid untuk snapping.
    /// 1 = Tidak simetris (trapezoid).
    /// 2 = Simetri 180 derajat (rhombus & thin rhombus).
    /// 3 = Simetri 120 derajat (triangle).
    /// 4 = Simetri 90 derajat (square).
    /// 6 = Simetri 60 derajat (hexagon).
    /// </summary>
    public virtual int RotationalSymmetry => 1;

    /// <summary>
    /// blueprint dari bentuk sebelum di-transformasi.
    /// </summary>
    public List<Vector2> OriginalVertices { get; protected set; } = [];

    /// <summary>
    /// blueprint dari bentuk yang di-transformasi.
    /// </summary>
    public List<Vector2> Vertices { get; protected set; } = [];

    /// <summary>
    /// Titik-titik outline, dipakai untuk membuat outline garis.
    /// Tidak dipakai
    /// </summary>
    public List<Vector2> Points { get; protected set; } = [];

    public bool Filled { get; protected set; } = false;

    private static readonly Primitif primitif = new Primitif();

    private static readonly BentukDasar bentukDasar = new BentukDasar();

    public PatternBlock(Vector2 cartesianPosition, Vector2? scale = null, float rotationDeg = 0, Color? color = null, bool? filled = false)
    {
        this.CartesianPosition = cartesianPosition;
        this.Scale = scale ?? this.Scale;
        this.Color = color ?? this.Color;
        this.Filled = filled ?? this.Filled;
        this._rotationDeg = rotationDeg;

        BuildOriginalVertices();
        RecalculateTransform();

        //? tidak perlu, karena outline sudah di-handle di Challenge & TemplateOutlineNode
        // if (!this.Filled) 
        //     BuildPoints();
    }

    public void RecalculateTransform()
    {
        System.Numerics.Matrix4x4 matrix = TransformasiFast.CreateScale(this.Scale.X, this.Scale.Y);

        float radians = Mathf.DegToRad(_rotationDeg);
        TransformasiFast.RotationCounterClockwise(ref matrix, radians, new(0, 0));

        TransformasiFast.ReflectionToX(ref matrix);

        ApplyTransformations(matrix);

        QueueRedraw();
    }

    /// <summary>
    /// Menerapkan matriks transformasi ke vertex ORIGINAL.
    /// Hasilnya disimpan di Vertices.
    /// </summary>
    /// <param name="matrix">Matriks Komposit</param>
    public void ApplyTransformations(System.Numerics.Matrix4x4 matrix)
    {
        if (matrix != System.Numerics.Matrix4x4.Identity)
        {
            this.Vertices = TransformasiFast.GetTransformPoint(matrix, OriginalVertices);
        }
    }

    protected abstract void BuildOriginalVertices();

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
        this.Filled = isFilled;

        QueueRedraw();
    }

    /// <summary>
    /// unused
    /// </summary>
    protected virtual void BuildPoints()
    {
        Points.Clear();

        Points.AddRange(bentukDasar.Polygon(this.Vertices));
    }

    /// <summary>
    /// unused
    /// </summary>
    private void DrawLines()
    {
        GraphicsUtils.PutPixelAll(this, Points, GraphicsUtils.DrawStyle.DotDot, Color);
    }

    public override void _Ready()
    {
        QueueRedraw();
    }

    public override void _Draw()
    {
        if (Vertices.Count == 0) return;

        if (this.Filled)
            DrawPolygon();
        else
            DrawWhitePolygon();
    }

    public override void _ExitTree()
    {
        base._ExitTree();
    }
}