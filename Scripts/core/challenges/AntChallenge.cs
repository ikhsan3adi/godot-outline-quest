public class AntChallenge : Challenge
{
    public AntChallenge()
    {
        PatternBlocks = [
            new SquareBlock(new (0, PatternBlock.BASE_LENGTH / 2)),
            new SquareBlock(new (0, -PatternBlock.BASE_LENGTH / 2)),
            new HexagonBlock(new (0, - (PatternBlock.BASE_LENGTH + PatternBlock.TRIANGLE_HEIGHT))),
            new HexagonBlock(new (0, PatternBlock.BASE_LENGTH * 2), rotationDeg: -30),

            // kaki kanan
            new DiamondBlock(new(
                    PatternBlock.BASE_LENGTH + PatternBlock.TRIANGLE_HEIGHT / 2 - PatternBlock.BASE_LENGTH / 20,
                    PatternBlock.BASE_LENGTH
                ), rotationDeg: 24
            ),
            new DiamondBlock(new(
                    PatternBlock.BASE_LENGTH + PatternBlock.TRIANGLE_HEIGHT / 2,
                    0
                )
            ),
            new DiamondBlock(new(
                    PatternBlock.BASE_LENGTH + PatternBlock.TRIANGLE_HEIGHT / 2 - PatternBlock.BASE_LENGTH / 20,
                    -PatternBlock.BASE_LENGTH
                ),
                rotationDeg: -24
            ),

            // kaki kiri
            new DiamondBlock(new(
                    -(PatternBlock.BASE_LENGTH + PatternBlock.TRIANGLE_HEIGHT / 2 - PatternBlock.BASE_LENGTH / 20),
                    PatternBlock.BASE_LENGTH
                ), rotationDeg: -24
            ),
            new DiamondBlock(new(
                    -(PatternBlock.BASE_LENGTH + PatternBlock.TRIANGLE_HEIGHT / 2),
                    0
                )
            ),
            new DiamondBlock(new(
                    -(PatternBlock.BASE_LENGTH + PatternBlock.TRIANGLE_HEIGHT / 2 - PatternBlock.BASE_LENGTH / 20),
                    -PatternBlock.BASE_LENGTH
                ),
                rotationDeg: 24
            )
        ];

        BuildOutlines();
    }
}