using Godot;

public class ButterflyChallenge : Challenge
{
    public ButterflyChallenge()
    {
        const float BASE_LENGTH = PatternBlock.BASE_LENGTH;
        const float HALF_BASE = BASE_LENGTH / 2;
        float TRIANGLE_H = PatternBlock.TRIANGLE_HEIGHT;

        Vector2 bottomTrapezoidCenter = calculateCenterTrapezoid(150f);

        PatternBlocks =
        [
            // Badan Kupu-kupu dari atas ke bawah
            new SquareBlock(new(0, HALF_BASE + BASE_LENGTH)),
            new SquareBlock(new(0, HALF_BASE)),
            new SquareBlock(new(0, -HALF_BASE)),
            new SquareBlock(new(0, -(HALF_BASE + BASE_LENGTH))),
            new TriangleBlock(
                new(0, -2 * BASE_LENGTH - TRIANGLE_H / 3),
                rotationDeg: -60
            ),

            // Antena
            new ThinRhombusBlock(
                new(TRIANGLE_H, 2 * BASE_LENGTH + TRIANGLE_H),
                rotationDeg: 60
            ),
            new ThinRhombusBlock(
                new(-TRIANGLE_H, 2 * BASE_LENGTH + TRIANGLE_H),
                rotationDeg: -60
            ),

            // Sayap kanan atas
            new HexagonBlock(
                new(HALF_BASE + TRIANGLE_H, BASE_LENGTH),
                rotationDeg: -30
            ),
            new TrapezoidBlock(
                new(2.5f * TRIANGLE_H + HALF_BASE, HALF_BASE + BASE_LENGTH),
                rotationDeg: -90
            ),
            new TriangleBlock(
                new(2 * TRIANGLE_H / 3 + TRIANGLE_H + HALF_BASE, 2 * BASE_LENGTH),
                rotationDeg: 90
            ),

            // Sayap kiri atas
            new HexagonBlock(
                new(-(HALF_BASE + TRIANGLE_H), BASE_LENGTH),
                rotationDeg: 30
            ),
            new TrapezoidBlock(
                new(-(2.5f * TRIANGLE_H + HALF_BASE), HALF_BASE + BASE_LENGTH),
                rotationDeg: 90
            ),
            new TriangleBlock(
                new(-(2 * TRIANGLE_H / 3 + TRIANGLE_H + HALF_BASE), 2 * BASE_LENGTH),
                rotationDeg: -90
            ),

            // Sayap kanan bawah
            new TrapezoidBlock(
                bottomTrapezoidCenter,
                rotationDeg: 150
            ),
            new TriangleBlock(
                new(HALF_BASE + TRIANGLE_H / 3, -BASE_LENGTH),
                rotationDeg: -90
            ),
            new TriangleBlock(
                new(HALF_BASE + TRIANGLE_H + 2 * TRIANGLE_H / 3, -BASE_LENGTH),
                rotationDeg: 90
            ),
            new RhombusBlock(
                new(HALF_BASE + TRIANGLE_H, -(BASE_LENGTH + HALF_BASE))
            ),

            // Sayap kiri bawah
            new TrapezoidBlock(
                new(-bottomTrapezoidCenter.X, bottomTrapezoidCenter.Y),
                rotationDeg: -150
            ),
            new TriangleBlock(
                new(-(HALF_BASE + TRIANGLE_H / 3), -BASE_LENGTH),
                rotationDeg: 90
            ),
            new TriangleBlock(
                new(-(HALF_BASE + TRIANGLE_H + 2 * TRIANGLE_H / 3), -BASE_LENGTH),
                rotationDeg: -90
            ),
            new RhombusBlock(
                new(-(HALF_BASE + TRIANGLE_H), -(BASE_LENGTH + HALF_BASE))
            ),
        ];

        BuildOutlines();
    }

    private static Vector2 calculateCenterTrapezoid(float deg)
    {
        float rotationRad = Mathf.DegToRad(deg);
        float halfBase = PatternBlock.BASE_LENGTH / 2;
        float halfTriHeight = PatternBlock.TRIANGLE_HEIGHT / 2;

        Vector2 contactVertex = new(halfBase, halfTriHeight);

        float minRotatedX = contactVertex.X * Mathf.Cos(rotationRad) - contactVertex.Y * Mathf.Sin(rotationRad);

        float centerX = halfBase - minRotatedX;

        return new Vector2(centerX, -halfTriHeight + 4.65f);
    }
}