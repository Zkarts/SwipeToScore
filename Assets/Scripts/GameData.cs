using UnityEngine;
using System.Collections;

public class GameData {

    public BallBehaviour CurrentBall;

    public bool Scored = false;

    private int level = 1, attempts = 0;

    public int Level {
        get { return level; }
    }

    public int Attempts {
        get { return attempts; }
    }

    public void IncrementLevel() {
        level++;
    }

    public void IncrementAttempts() {
        attempts++;
    }

    public void ResetAttempts() {
        attempts = 0;
    }

    public void PayForBallType() {
        attempts -= CurrentBall.BallType.GetCost();
    }

}
