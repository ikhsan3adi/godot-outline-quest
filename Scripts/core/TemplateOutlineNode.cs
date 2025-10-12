using Godot;
using System.Collections.Generic;

public partial class TemplateOutlineNode : Node2D
{
    private List<LineSegment> _segmentsToDraw = [];
    private Color _outlineColor = Colors.DarkGray;

    private static readonly Primitif primitif = new Primitif();

    /// <summary>
    /// data outline yang sudah digabung untuk digambar.
    /// </summary>
    public void DrawOutline(List<LineSegment> segments)
    {
        _segmentsToDraw = segments;
        QueueRedraw();
    }

    public override void _Draw()
    {
        if (_segmentsToDraw.Count == 0) return;

        int lineThickness = 4;

        foreach (var segment in _segmentsToDraw)
        {
            var points = primitif.LineBresenham(segment.A.X, segment.A.Y, segment.B.X, segment.B.Y);

            // GraphicsUtils.PutPixelAll(this, points, GraphicsUtils.DrawStyle.StripStrip, _outlineColor);

            foreach (var point in points)
            {
                GraphicsUtils.DrawBrush(this, point.X, point.Y, lineThickness, _outlineColor);
            }
        }
    }
}