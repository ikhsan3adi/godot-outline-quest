using Godot;
using System;

public partial class InteractiveButton : Control
{
    [Export]
    private float scaleUp = 1.1f;

    [Export]
    private float scaleDown = 0.9f;

    public override void _Ready()
    {
        base._Ready();
        this.PivotOffset = this.Size / 2;
    }

    public void _start_hover()
    {
        this.Scale = new(scaleUp, scaleUp);
    }

    public void _reset_button()
    {
        this.Scale = new(1.0f, 1.0f);
    }

    public void _button_down()
    {
        this.Scale = new(scaleDown, scaleDown);
    }
}
