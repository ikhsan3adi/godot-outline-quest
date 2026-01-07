namespace Godot;

public partial class Welcome : Control
{
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        ScreenUtils.Initialize(GetViewport());
    }

    private void _on_play_btn_pressed()
    {
        SwitchTo("Game");
    }

    private void _on_about_btn_pressed()
    {
        SwitchTo("About");
    }

    private void _on_guide_btn_pressed()
    {
        SwitchTo("Guide");
    }

    private void _on_exit_btn_pressed()
    {
        GetTree().Quit();
    }

    private void SwitchTo(string sceneName) => GetTree().ChangeSceneToFile($"res://Scenes/{sceneName}.tscn");
}
