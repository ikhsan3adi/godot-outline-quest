using Godot;
using System.Collections.Generic;

public partial class GameManager : Node2D
{
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

    public override void _Ready()
    {
        GetViewport().SizeChanged += () =>
        {
            if (challengeContainer != null)
                challengeContainer.Position = ScreenUtils.ConvertToPixel(0, 0);
        };

        GoToChallenge(0);
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

        templateOutline = new TemplateOutlineNode();
        templateOutline.Name = "TemplateOutline";
        challengeContainer.AddChild(templateOutline);
        templateOutline.DrawOutline(currentChallenge.Outlines);

        currentChallenge.PatternBlocks.ForEach((pb) =>
        {
            challengeContainer.AddChild(pb);
        });

        currentChallenge.StartChallenge();
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
