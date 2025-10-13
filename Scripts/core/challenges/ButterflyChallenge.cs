using Godot;

public class ButterflyChallenge : Challenge
{
    public ButterflyChallenge()
    {
        const float BASE_LENGTH = PatternBlock.BASE_LENGTH;
        const float HALF_BASE = BASE_LENGTH / 2;
        float TRIANGLE_H = PatternBlock.TRIANGLE_HEIGHT;

        // calculate position for right trapezoid 
        float rad30deg = Mathf.DegToRad(30);
        float xTrapezoid = HALF_BASE + TRIANGLE_H - (Mathf.Sin(rad30deg) * TRIANGLE_H / 2);
        float yTrapezoid = -(Mathf.Cos(rad30deg) * TRIANGLE_H / 2);

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
                new (xTrapezoid, yTrapezoid),
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
                new (-xTrapezoid, yTrapezoid),
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
}