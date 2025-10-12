using Godot;
using System.Collections.Generic;
using System.Linq;

abstract public class Challenge
{
    /// <summary>
    /// Lines of outline blocks
    /// </summary>
    public List<LineSegment> Outlines { get; protected set; } = [];

    /// <summary>
    /// Outline Blocks Destination
    /// </summary>
    public List<PatternBlock> PatternBlocks { get; protected set; } = [];

    /// <summary>
    /// Spawned dragged blocks
    /// </summary>
    public List<PatternBlock> PlacedPatternBlock { get; protected set; } = [];

    public GameStat stat = new GameStat();

    public ulong StartTimeMsec { get; private set; } // waktu mulai dalam milidetik

    public bool IsActive { get; private set; } = false;

    protected void BuildOutlines()
    {
        var edgeCounts = new Dictionary<LineSegment, int>();

        foreach (var block in PatternBlocks)
        {
            List<Vector2> localVertices = block.Vertices;
            Vector2 blockPosition = block.Position;

            for (int i = 0; i < localVertices.Count; i++)
            {
                Vector2 p1 = localVertices[i] + blockPosition;
                Vector2 p2 = localVertices[(i + 1) % localVertices.Count] + blockPosition;

                var segment = new LineSegment(p1, p2);

                edgeCounts.TryGetValue(segment, out int currentCount);
                edgeCounts[segment] = currentCount + 1;
            }
        }

        // Filter
        Outlines = edgeCounts
            .Where(pair => pair.Value == 1)
            .Select(pair => pair.Key)
            .ToList();
    }

    public void ApplyTransformations(System.Numerics.Matrix4x4 matrix)
    {
        PatternBlocks.ForEach((pb) => pb.ApplyTransformations(matrix));
        PlacedPatternBlock.ForEach((pb) => pb.ApplyTransformations(matrix));
    }

    public void StartChallenge()
    {
        IsActive = true;
        StartTimeMsec = Time.GetTicksMsec();
    }

    public void EndChallenge()
    {
        if (!IsActive) return;

        IsActive = false;
        ulong durationMsec = Time.GetTicksMsec() - StartTimeMsec;
        stat.ElapsedTimeSeconds = durationMsec / 1000.0;
    }
}