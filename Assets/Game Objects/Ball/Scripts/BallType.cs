﻿
public enum BallType {
    Normal = 0,
    Fireball = 1,
    SmallBall = 2, //not implemented
    SplitBall = 3, //not implemented
}

public static class BallTypeExtensions {

    public static int ToCost(this BallType ballType) {
        switch (ballType) {
            case BallType.Normal:
                return 0;
            case BallType.Fireball:
                return 3;
            case BallType.SmallBall:
                return 2;
            case BallType.SplitBall:
                return 4;
            default:
                return 0;
        }
    }

}