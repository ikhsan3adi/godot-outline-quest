using Godot;
using System;

public partial class HotbarItem : InteractiveButton
{
    [Signal]
    public delegate void BlockDragStartedEventHandler(string blockType);

    private TextureButton _button;
    private TextureRect _icon;
    private Label _countLabel;

    private Type _blockType;
    private int _count;
    private Texture2D _iconTexture;

    public void Initialize(Type blockType, int count, Texture2D iconTexture)
    {
        _blockType = blockType;
        _count = count;
        _iconTexture = iconTexture;
    }

    public override void _Ready()
    {
        base._Ready();

        _button = GetNode<TextureButton>("HotbarItem");
        _icon = GetNode<TextureRect>("HotbarItem/Icon");
        _countLabel = GetNode<Label>("HotbarItem/ItemCount");

        _icon.Texture = _iconTexture;
        UpdateCount(_count);

        _button.ButtonDown += OnButtonDown;
    }

    public void UpdateCount(int count)
    {
        _countLabel.Text = $"x{count}";

        _button.Disabled = count <= 0;

        if (_button.Disabled)
        {
            _icon.Modulate = new Color(1, 1, 1, 0.4f);
        }
        else
        {
            _icon.Modulate = new Color(1, 1, 1, 1);
        }
    }

    private void OnButtonDown()
    {
        EmitSignal(SignalName.BlockDragStarted, _blockType.AssemblyQualifiedName);
    }
}