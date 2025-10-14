using Godot;

partial class PerformanceCounter : Control
{
    [Export]
    private CheckButton toggleDisplayButton;

    [Export]
    private Label fpsLabel;

    [Export]
    private Label latencyLabel;

    private void OnPerformanceToggle(bool toggledOn)
    {
        this.Visible = toggledOn;
    }

    public override void _Ready()
    {
        toggleDisplayButton.Toggled += OnPerformanceToggle;

        this.Visible = toggleDisplayButton.ButtonPressed;
    }

    public override void _Process(double delta)
    {
        if (this.Visible)
        {
            double fps = Engine.GetFramesPerSecond();
            fpsLabel.Text = $"FPS: {fps}";

            double latencyMs = 1.0 / fps * 1000.0;
            latencyLabel.Text = $"Latency: {latencyMs:F2} ms";
        }
    }

    public override void _ExitTree()
    {
        base._ExitTree();

        toggleDisplayButton.Toggled -= OnPerformanceToggle;
    }
}