using System;
using Godot;

public class SpiderChallenge : Challenge
{
    public SpiderChallenge()
    {
        const float BASE_LENGTH = PatternBlock.BASE_LENGTH;
        const float HALF_BASE = BASE_LENGTH / 2;
        float TRIANGLE_H = PatternBlock.TRIANGLE_HEIGHT;

        float rad15deg = Mathf.DegToRad(15);
        float rad30deg = Mathf.DegToRad(30);
        float rad45deg = Mathf.DegToRad(45);
        float rad60deg = Mathf.DegToRad(60);
        float rad75deg = Mathf.DegToRad(75);

        // calculate position for top right trapezoid 
        float xTrapezoid = 1.5f * BASE_LENGTH - Mathf.Cos(rad30deg) * (TRIANGLE_H / 2);
        float yTrapezoid = Mathf.Sin(rad30deg) * (TRIANGLE_H / 2);

        float thinRhombusInnerLong = Mathf.Cos(rad15deg) * BASE_LENGTH;
        float thinRhombusInnerShort = Mathf.Sin(rad15deg) * BASE_LENGTH;

        // kaki 1
        float xLeg1trhm1 = BASE_LENGTH + (Mathf.Cos(rad60deg) * thinRhombusInnerLong);
        float yLeg1trhm1 = TRIANGLE_H + (Mathf.Sin(rad60deg) * thinRhombusInnerLong);

        float xLeg1trhm2 = BASE_LENGTH + (Mathf.Cos(rad75deg) * BASE_LENGTH) + (Mathf.Cos(rad60deg) * thinRhombusInnerLong);
        float yLeg1trhm2 = TRIANGLE_H + (Mathf.Sin(rad75deg) * BASE_LENGTH) + (Mathf.Sin(rad60deg) * thinRhombusInnerLong);

        float xLeg1triangle = BASE_LENGTH + (Mathf.Cos(rad75deg) * (BASE_LENGTH * 2 + 2 * TRIANGLE_H / 3));
        float yLeg1triangle = TRIANGLE_H + (Mathf.Sin(rad75deg) * (BASE_LENGTH * 2 + 2 * TRIANGLE_H / 3));

        float xLeg1trhm3 = BASE_LENGTH + (Mathf.Cos(rad75deg) * BASE_LENGTH) + (Mathf.Cos(rad60deg) * thinRhombusInnerLong * 2) - (Mathf.Cos(rad30deg) * thinRhombusInnerLong);
        float yLeg1trhm3 = TRIANGLE_H + (Mathf.Sin(rad75deg) * BASE_LENGTH) + (Mathf.Sin(rad60deg) * thinRhombusInnerLong * 2) + (Mathf.Sin(rad30deg) * thinRhombusInnerLong);

        // kaki 2
        float xLeg2trhm1 = BASE_LENGTH + (Mathf.Cos(rad15deg) * thinRhombusInnerLong);
        float yLeg2trhm1 = TRIANGLE_H + (Mathf.Sin(rad15deg) * thinRhombusInnerLong);

        float xLeg2triangle = BASE_LENGTH + (Mathf.Cos(rad30deg) * BASE_LENGTH) + HALF_BASE;
        float yLeg2triangle = TRIANGLE_H + (Mathf.Sin(rad30deg) * BASE_LENGTH) + TRIANGLE_H / 3;

        float xLeg2trhm2 = BASE_LENGTH * 2 + (Mathf.Cos(rad30deg) * BASE_LENGTH) + (Mathf.Cos(rad45deg) * thinRhombusInnerShort);
        float yLeg2trhm2 = TRIANGLE_H + (Mathf.Sin(rad30deg) * BASE_LENGTH) + (Mathf.Sin(rad45deg) * thinRhombusInnerShort);

        // kaki 3
        float xLeg3trhm1 = HALF_BASE + (Mathf.Cos(rad30deg) * TRIANGLE_H) + thinRhombusInnerLong;
        float yLeg3trhm1 = TRIANGLE_H / 3;

        float xLeg3triangle = HALF_BASE + (Mathf.Cos(rad30deg) * TRIANGLE_H) + (Mathf.Cos(rad15deg) * (BASE_LENGTH + (2 * TRIANGLE_H / 3)));
        float yLeg3triangle = TRIANGLE_H / 3 - (Mathf.Sin(rad15deg) * (BASE_LENGTH + (2 * TRIANGLE_H / 3)));

        float xLeg3trhm2 = HALF_BASE + (Mathf.Cos(rad30deg) * TRIANGLE_H) + thinRhombusInnerLong * 2;
        float yLeg3trhm2 = TRIANGLE_H / 3 - thinRhombusInnerLong;

        // kaki 4
        float xLeg4trhm1 = BASE_LENGTH + HALF_BASE + (Mathf.Cos(rad45deg) * BASE_LENGTH);
        float yLeg4trhm1 = -(Mathf.Sin(rad45deg) * BASE_LENGTH);

        float xLeg4trhm2 = 2 * BASE_LENGTH + (Mathf.Cos(rad45deg) * BASE_LENGTH);
        float yLeg4trhm2 = -(TRIANGLE_H + Mathf.Sin(rad45deg) * BASE_LENGTH);

        float xLeg4triangle = 2 * BASE_LENGTH + (Mathf.Cos(rad60deg) * (BASE_LENGTH + (2 * TRIANGLE_H / 3)));
        float yLeg4triangle = -(TRIANGLE_H + Mathf.Sin(rad60deg) * (BASE_LENGTH + (2 * TRIANGLE_H / 3)));

        float xLeg4trhm3 = 2 * BASE_LENGTH + (Mathf.Cos(rad45deg) * BASE_LENGTH);
        float yLeg4trhm3 = -(TRIANGLE_H + (Mathf.Sin(rad60deg) * BASE_LENGTH) + HALF_BASE + (Mathf.Sin(rad45deg) * thinRhombusInnerLong));

        float xLeg4trhm4 = BASE_LENGTH + HALF_BASE + (Mathf.Cos(rad45deg) * BASE_LENGTH);
        float yLeg4trhm4 = -(TRIANGLE_H + (Mathf.Sin(rad60deg) * BASE_LENGTH) + HALF_BASE + (Mathf.Sin(rad60deg) * BASE_LENGTH) + (Mathf.Sin(rad45deg) * thinRhombusInnerLong));

        PatternBlocks = [
            // Badan
            new TriangleBlock(
                new(0, (3 * TRIANGLE_H) + (2 * TRIANGLE_H / 3)),
                rotationDeg: 180
            ),
            new RhombusBlock(
                new (-HALF_BASE, 3 * TRIANGLE_H),
                rotationDeg: 90
            ),
            new RhombusBlock(
                new (HALF_BASE, 3 * TRIANGLE_H),
                rotationDeg: 90
            ),
            new TriangleBlock(new(0, (2 * TRIANGLE_H) + (TRIANGLE_H / 3))),

            new HexagonBlock(new(0, TRIANGLE_H)),
            new HexagonBlock(new(0, -TRIANGLE_H)),
            // top right
            new TrapezoidBlock(
                new(xTrapezoid, -yTrapezoid),
                rotationDeg: 120
            ),
            // bottom right
            new TrapezoidBlock(
                new(xTrapezoid, -(yTrapezoid + 1.5f * TRIANGLE_H)),
                rotationDeg: 60
            ),
            // bottom center
            new TrapezoidBlock(
                new(0, -(2.5f * TRIANGLE_H)),
                rotationDeg: 0
            ),
            // bottom right
            new TrapezoidBlock(
                new(-xTrapezoid, -(yTrapezoid + 1.5f * TRIANGLE_H)),
                rotationDeg: -60
            ),
            // top right
            new TrapezoidBlock(
                new(-xTrapezoid, -yTrapezoid),
                rotationDeg: -120
            ),

            // kaki kanan 1
            new ThinRhombusBlock(
                new(xLeg1trhm1, yLeg1trhm1),
                rotationDeg: 60
            ),
            new ThinRhombusBlock(
                new(xLeg1trhm2, yLeg1trhm2),
                rotationDeg: 60
            ),
            new TriangleBlock(
                new(xLeg1triangle, yLeg1triangle),
                rotationDeg: 45
            ),
            new ThinRhombusBlock(
                new(xLeg1trhm3, yLeg1trhm3),
                rotationDeg: -30
            ),

            // kaki kiri 1
            new ThinRhombusBlock(
                new(-xLeg1trhm1, yLeg1trhm1),
                rotationDeg: -60
            ),
            new ThinRhombusBlock(
                new(-xLeg1trhm2, yLeg1trhm2),
                rotationDeg: -60
            ),
            new TriangleBlock(
                new(-xLeg1triangle, yLeg1triangle),
                rotationDeg: -45
            ),
            new ThinRhombusBlock(
                new(-xLeg1trhm3, yLeg1trhm3),
                rotationDeg: 30
            ),

            // kaki kanan 2
            new ThinRhombusBlock(
                new(xLeg2trhm1, yLeg2trhm1),
                rotationDeg: 15
            ),
            new TriangleBlock(
                new(xLeg2triangle, yLeg2triangle),
                rotationDeg: 0
            ),
            new ThinRhombusBlock(
                new(xLeg2trhm2, yLeg2trhm2),
                rotationDeg: -45
            ),

            // kaki kiri 2
            new ThinRhombusBlock(
                new(-xLeg2trhm1, yLeg2trhm1),
                rotationDeg: -15
            ),
            new TriangleBlock(
                new(-xLeg2triangle, yLeg2triangle),
                rotationDeg: 0
            ),
            new ThinRhombusBlock(
                new(-xLeg2trhm2, yLeg2trhm2),
                rotationDeg: 45
            ),

            // kaki kanan 3
            new ThinRhombusBlock(
                new(xLeg3trhm1, yLeg3trhm1),
                rotationDeg: 0
            ),
            new TriangleBlock(
                new(xLeg3triangle, yLeg3triangle),
                rotationDeg: -45
            ),
            new ThinRhombusBlock(
                new(xLeg3trhm2, yLeg3trhm2),
                rotationDeg: 90
            ),

            // kaki kiri 3
            new ThinRhombusBlock(
                new(-xLeg3trhm1, yLeg3trhm1),
                rotationDeg: 0
            ),
            new TriangleBlock(
                new(-xLeg3triangle, yLeg3triangle),
                rotationDeg: 45
            ),
            new ThinRhombusBlock(
                new(-xLeg3trhm2, yLeg3trhm2),
                rotationDeg: -90
            ),

            // kaki kanan 4
            new ThinRhombusBlock(
                new(xLeg4trhm1, yLeg4trhm1),
                rotationDeg: -45
            ),
            new ThinRhombusBlock(
                new(xLeg4trhm2, yLeg4trhm2),
                rotationDeg: -45
            ),
            new TriangleBlock(
                new(xLeg4triangle, yLeg4triangle),
                rotationDeg: 30
            ),
            new ThinRhombusBlock(
                new(xLeg4trhm3, yLeg4trhm3),
                rotationDeg: 45
            ),
            new ThinRhombusBlock(
                new(xLeg4trhm4, yLeg4trhm4),
                rotationDeg: 45
            ),

            // kaki kiri 4
            new ThinRhombusBlock(
                new(-xLeg4trhm1, yLeg4trhm1),
                rotationDeg: 45
            ),
            new ThinRhombusBlock(
                new(-xLeg4trhm2, yLeg4trhm2),
                rotationDeg: 45
            ),
            new TriangleBlock(
                new(-xLeg4triangle, yLeg4triangle),
                rotationDeg: -30
            ),
            new ThinRhombusBlock(
                new(-xLeg4trhm3, yLeg4trhm3),
                rotationDeg: -45
            ),
            new ThinRhombusBlock(
                new(-xLeg4trhm4, yLeg4trhm4),
                rotationDeg: -45
            ),
        ];

        BuildOutlines();
    }
}