using System;

namespace Godot;

public static class ScreenUtils
{
    private static Viewport Viewport;

    public static int ScreenWidth { get; private set; }
    public static int ScreenHeight { get; private set; }
    public static int MarginLeft { get; private set; } = 50;
    public static int MarginTop { get; private set; } = 50;
    public static int MarginRight { get; private set; }
    public static int MarginBottom { get; private set; }
    public static int XMax { get; private set; }
    public static int XMin { get; private set; }
    public static int YMax { get; private set; }
    public static int YMin { get; private set; }

    public static float UniversalScaleFactor { get; private set; } = 1.0f;

    public static readonly int BASE_RESOLUTION_WIDTH = 1280;

    public static readonly int BASE_RESOLUTION_HEIGHT = 720;

    public static event Action UniversalScaleFactorChanged;

    private static bool isInitialized = false;

    public static void Initialize(Viewport viewport)
    {
        if (isInitialized)
            return;

        Vector2 windowSize = viewport.GetVisibleRect().Size;
        ScreenWidth = (int)windowSize.X;
        ScreenHeight = (int)windowSize.Y;
        MarginRight = ScreenWidth - MarginLeft;
        MarginBottom = ScreenHeight - MarginTop;
        XMax = MarginRight;
        XMin = MarginLeft;
        YMax = MarginTop;
        YMin = MarginBottom;
        Viewport = viewport;

        Viewport.SizeChanged += OnResize;
        isInitialized = true;
    }

    public static Vector2 ConvertToCartesian(float x, float y, float marginLeft = 0, float marginTop = 0)
    {
        float axis = (float)(Mathf.Ceil((float)ScreenWidth / 2) - marginLeft);
        float ordinat = (float)(Mathf.Ceil((float)ScreenHeight / 2) - marginTop);

        x = x - axis;
        y = ordinat - y;

        return new(x, y);
    }

    public static Vector2 ConvertToPixel(float x, float y, float marginLeft = 0, float marginTop = 0)
    {
        float axis = (float)(Mathf.Ceil((float)ScreenWidth / 2) - marginLeft);
        float ordinat = (float)(Mathf.Ceil((float)ScreenHeight / 2) - marginTop);

        x = axis + x;
        y = ordinat - y;

        return new(x, y);
    }

    private static void OnResize()
    {
        ScreenWidth = (int)Viewport.GetVisibleRect().Size.X;
        ScreenHeight = (int)Viewport.GetVisibleRect().Size.Y;

        float scaleX = (float)Viewport.GetWindow().Size.X / BASE_RESOLUTION_WIDTH;
        float scaleY = (float)Viewport.GetWindow().Size.Y / BASE_RESOLUTION_HEIGHT;

        float newScaleFactor = Mathf.Min(scaleX, scaleY);

        // Optimasi performa, hanya update jika perbedaan scale > 0.025
        if (Mathf.Abs(newScaleFactor - UniversalScaleFactor) > 0.025f)
        {
            UniversalScaleFactor = Mathf.Min(scaleX, scaleY);

            UniversalScaleFactorChanged?.Invoke();
        }
    }
}