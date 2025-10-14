using Godot;

public class AntChallenge : Challenge
{
    public AntChallenge()
    {
        float BASE_LENGTH = PatternBlock.BASE_LENGTH;
        float TRIANGLE_H = PatternBlock.TRIANGLE_HEIGHT;

        float rad60deg = Mathf.DegToRad(60);

        PatternBlocks = [
            // kepala
            new RhombusBlock(new(0, 2.5f * BASE_LENGTH)),

            new TriangleBlock(
                new(2 * TRIANGLE_H / 3, 2 * BASE_LENGTH),
                rotationDeg: -30
            ),
            new TriangleBlock(
                new(
                    Mathf.Cos(rad60deg) * 2 * TRIANGLE_H / 3,
                    Mathf.Sin(rad60deg) * 2 * TRIANGLE_H / 3 + BASE_LENGTH
                ),
                rotationDeg: 30
            ),
            new TriangleBlock(
                new(-2 * TRIANGLE_H / 3, 2 * BASE_LENGTH),
                rotationDeg: 30
            ),
            new TriangleBlock(
                new(
                    -Mathf.Cos(rad60deg) * 2 * TRIANGLE_H / 3,
                    Mathf.Sin(rad60deg) * 2 * TRIANGLE_H / 3 + BASE_LENGTH
                ),
                rotationDeg: -30
            ),

            // badan tengah
            new SquareBlock(new (0, BASE_LENGTH / 2)),
            new SquareBlock(new (0, -BASE_LENGTH / 2)),

            // badan bawah
            new HexagonBlock(new (0, - (BASE_LENGTH + TRIANGLE_H))),

            // kaki kanan
            new ThinRhombusBlock(new(
                    BASE_LENGTH + TRIANGLE_H / 2 - BASE_LENGTH / 20,
                    BASE_LENGTH
                ), rotationDeg: 24
            ),
            new ThinRhombusBlock(new(BASE_LENGTH + TRIANGLE_H / 2, 0)),
            new ThinRhombusBlock(new(
                    BASE_LENGTH + TRIANGLE_H / 2 - BASE_LENGTH / 20,
                    -BASE_LENGTH
                ),
                rotationDeg: -24
            ),

            // kaki kiri
            new ThinRhombusBlock(new(
                    -(BASE_LENGTH + TRIANGLE_H / 2 - BASE_LENGTH / 20),
                    BASE_LENGTH
                ), rotationDeg: -24
            ),
            new ThinRhombusBlock(new(-(BASE_LENGTH + TRIANGLE_H / 2), 0)),
            new ThinRhombusBlock(new(
                    -(BASE_LENGTH + TRIANGLE_H / 2 - BASE_LENGTH / 20),
                    -BASE_LENGTH
                ),
                rotationDeg: 24
            )
        ];

        BuildOutlines();
    }
}