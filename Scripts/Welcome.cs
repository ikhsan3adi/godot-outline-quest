namespace Godot;

public partial class Welcome : Control
{
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
    }

    private void _on_play_btn_pressed()
    {
        GetTree().ChangeSceneToFile("res://Scenes/Karya1.tscn");
    }

    private void _on_about_btn_pressed()
    {
        GetTree().ChangeSceneToFile("res://Scenes/About.tscn");
    }

    private void _on_guide_btn_pressed()
    {
        GetTree().ChangeSceneToFile("res://Scenes/Guide.tscn");
    }

    private void _on_exit_btn_pressed()
    {
        GetTree().Quit();
    }
}
