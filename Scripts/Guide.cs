namespace Godot;

public partial class Guide : Control
{
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
    }

    private void _on_back_btn_pressed()
    {
        GetTree().ChangeSceneToFile("res://Scenes/Welcome.tscn");
    }
}
