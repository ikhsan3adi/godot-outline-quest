using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

public partial class GameManager : Node2D
{
    private const float SNAP_DISTANCE_THRESHOLD = 20.0f;
    private const float SNAP_ANGLE_THRESHOLD = 7.0f; // toleransi snap sudut dalam derajat
    private const float ROTATION_INCREMENT_DEG = 5.0f; // derajat per scroll

    /// <summary>
    /// Node tak terlihat yang menjadi wadah dan pivot untuk semua block + outline.
    /// </summary>
    private Node2D challengeContainer;
    private TemplateOutlineNode templateOutline;

    private static readonly List<Challenge> challenges = [
        new AntChallenge(),
        new ButterflyChallenge(),
        new SpiderChallenge(),
    ];

    private int currentChallengeIndex = -1;
    private Challenge currentChallenge = null;

    private GameStat _totalStats = new GameStat();

    /// <summary>
    /// Melacak jumlah balok yang tersedia di hotbar.
    /// Key: Tipe balok (typeof(SquareBlock), dll.), Value: Jumlah
    /// </summary>
    private Dictionary<Type, int> availableBlockCounts = [];

    /// <summary>
    /// Referensi ke balok yang sedang diseret oleh pemain.
    /// </summary>
    private PatternBlock draggedBlock = null;

    [Export]
    private GridContainer hotbarGrid;

    [Export]
    private PackedScene hotbarItemScene;

    [Export]
    private Label levelLabel;

    [Export]
    private Label timerLabel;

    [Export]
    private Label scoreLabel;

    private Dictionary<Type, Texture2D> iconCache = [];

    public override void _Ready()
    {
        LoadBlockIcons();

        GetViewport().SizeChanged += () =>
        {
            if (challengeContainer != null)
                challengeContainer.Position = ScreenUtils.ConvertToPixel(0, 0);
        };

        GoToChallenge(0);
    }

    /// <summary>
    /// muat semua ikon yang dibutuhkan dan cache.
    /// </summary>
    private void LoadBlockIcons()
    {
        var allBlockTypesInGame = challenges
            .SelectMany(c => c.PatternBlocks)
            .Select(b => b.GetType())
            .Distinct();

        foreach (Type blockType in allBlockTypesInGame)
        {
            try
            {
                FieldInfo field = blockType.GetField("IconPath");
                string path = (string)field.GetValue(null);

                Texture2D texture = GD.Load<Texture2D>(path);
                iconCache[blockType] = texture;
            }
            catch (Exception e)
            {
                GD.PrintErr($"Gagal memuat ikon untuk {blockType.Name}: {e.Message}");
            }
        }
    }

    private void GoToChallenge(int index)
    {
        challengeContainer?.QueueFree();

        challengeContainer = new Node2D();
        challengeContainer.Name = "ChallengeContainer";
        AddChild(challengeContainer);
        challengeContainer.Position = ScreenUtils.ConvertToPixel(0, 0);

        currentChallengeIndex = index;
        currentChallenge = challenges[currentChallengeIndex];
        levelLabel.Text = $"Level\n{currentChallengeIndex + 1}";
        scoreLabel.Text = $"{currentChallenge.stat.Score}";

        PopulateBlockCounts();

        templateOutline = new TemplateOutlineNode();
        templateOutline.Name = "TemplateOutline";
        challengeContainer.AddChild(templateOutline);
        templateOutline.DrawOutline(currentChallenge.Outlines);

        currentChallenge.PatternBlocks.ForEach((pb) =>
        {
            challengeContainer.AddChild(pb);
        });

        currentChallenge.StartChallenge();
        UpdateHotbarUI();
    }

    private void PopulateBlockCounts()
    {
        availableBlockCounts.Clear();
        if (currentChallenge == null) return;

        // Hitung jumlah kemunculan setiap tipe balok di dalam template challenge
        var counts = currentChallenge.PatternBlocks
            .GroupBy(block => block.GetType())
            .ToDictionary(group => group.Key, group => group.Count());

        availableBlockCounts = counts;
    }

    private void UpdateHotbarUI()
    {
        foreach (Node child in hotbarGrid.GetChildren())
            child.QueueFree();

        foreach (var entry in availableBlockCounts)
        {
            var item = hotbarItemScene.Instantiate<HotbarItem>();

            iconCache.TryGetValue(entry.Key, out Texture2D icon);

            item.Initialize(entry.Key, entry.Value, icon);

            item.BlockDragStarted += OnBlockDragStarted;
            hotbarGrid.AddChild(item);
        }
    }

    private void OnBlockDragStarted(string blockTypeString)
    {
        if (draggedBlock != null) return;

        Type blockType = Type.GetType(blockTypeString);

        if (availableBlockCounts.TryGetValue(blockType, out int count) && count > 0)
        {
            availableBlockCounts[blockType]--;
            UpdateHotbarUI();

            draggedBlock = (PatternBlock)Activator.CreateInstance(blockType, new Vector2(0, 0), null, 0f, null, true);
            challengeContainer.AddChild(draggedBlock);
        }
    }

    public override void _Process(double delta)
    {
        if (draggedBlock != null)
        {
            Vector2 globalMousePos = GetGlobalMousePosition();
            Vector2 localToContainerPos = challengeContainer.ToLocal(globalMousePos);

            draggedBlock.CartesianPosition = new Vector2(localToContainerPos.X, -localToContainerPos.Y);
        }

        // untuk memperbarui timer
        if (currentChallenge != null && currentChallenge.IsActive)
        {
            ulong currentTimeMsec = Time.GetTicksMsec();

            double elapsedSeconds = (currentTimeMsec - currentChallenge.StartTimeMsec) / 1000.0;

            TimeSpan time = TimeSpan.FromSeconds(elapsedSeconds);
            string timerText = time.ToString(@"mm\:ss");

            timerLabel.Text = timerText;
        }
    }

    public override void _Input(InputEvent @event)
    {
        // rotasi dengan scroll wheel
        if (draggedBlock != null && @event is InputEventMouseButton mouseEvent)
        {
            if (mouseEvent.ButtonIndex == MouseButton.WheelUp)
            {
                // CCW
                draggedBlock.RotationDeg += ROTATION_INCREMENT_DEG;

            }
            else if (mouseEvent.ButtonIndex == MouseButton.WheelDown)
            {
                // CW
                draggedBlock.RotationDeg -= ROTATION_INCREMENT_DEG;
            }
        }

        // lepaskan/drop block yang sedang diseret
        if (@event is InputEventMouseButton mouseButtonEvent && !mouseButtonEvent.Pressed && mouseButtonEvent.ButtonIndex == MouseButton.Left)
        {
            if (draggedBlock != null)
            {
                TrySnapDraggedBlock();
            }
        }
    }

    private void TrySnapDraggedBlock()
    {
        bool snapped = false;
        foreach (var templateBlock in currentChallenge.PatternBlocks)
        {
            if (!templateBlock.filled && templateBlock.GetType() == draggedBlock.GetType())
            {
                float distance = draggedBlock.CartesianPosition.DistanceTo(templateBlock.CartesianPosition);

                float angleA = draggedBlock.RotationDeg;
                float angleB = templateBlock.RotationDeg;
                float angleDiff = Mathf.Abs(Mathf.RadToDeg(Mathf.AngleDifference(Mathf.DegToRad(angleA), Mathf.DegToRad(angleB))));

                // snap berhasil
                if (distance < SNAP_DISTANCE_THRESHOLD && angleDiff < SNAP_ANGLE_THRESHOLD)
                {
                    templateBlock.SetFilled(true);

                    draggedBlock.QueueFree();
                    draggedBlock = null;

                    snapped = true;

                    UpdateScoreOnSnap();

                    break;
                }
            }
        }

        // snap gagal
        if (!snapped)
        {
            availableBlockCounts[draggedBlock.GetType()]++;
            UpdateHotbarUI();

            draggedBlock.QueueFree();
            draggedBlock = null;
        }
    }

    private void UpdateScoreOnSnap()
    {
        currentChallenge.stat.Score += 10;
        scoreLabel.Text = $"{currentChallenge.stat.Score}";

        // Jika semua block di template sudah terisi, complete challenge
        if (currentChallenge.PatternBlocks.All(block => block.filled))
        {
            GD.Print("LEVEL COMPLETE!");
            CompleteCurrentChallenge();
        }
    }

    // menang satu challenge
    public void CompleteCurrentChallenge()
    {
        if (currentChallenge == null) return;

        currentChallenge.EndChallenge();

        // TODO Tampilkan statistik challenge yang baru selesai

        // akumulasi stat
        _totalStats.ElapsedTimeSeconds += currentChallenge.stat.ElapsedTimeSeconds;
        _totalStats.Score += currentChallenge.stat.Score;

        GoToChallenge(currentChallengeIndex + 1);
    }

    private void ShowFinalStats()
    {
        // menampilkan UI layar kemenangan
    }


    public override void _ExitTree()
    {
        base._ExitTree();
    }
}
