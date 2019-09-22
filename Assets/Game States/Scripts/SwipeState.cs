using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SwipeState : SwipeToScoreState {

    [SerializeField]
    private SwipeToScoreState shootState;

    [SerializeField]
    private SwipeUI swipeUI;

    [SerializeField]
    private LevelGenerator levelGenerator;

    public override void Init(SwipeToScoreStateMachine stateMachine) {
        base.Init(stateMachine);

        levelGenerator.Init(gameData);
        gameData.CurrentBall.OnShoot += Shoot;

        swipeUI.Init();
        swipeUI.OnBallTypeChanged += SetBallType;
    }

    private void Shoot() {
        gameData.PayForBallType();
        swipeUI.UpdateStatsText(gameData.Level, gameData.Attempts);
        stateMachine.ChangeState(shootState);
    }

    private void SetBallType(BallType ballType) {
        gameData.CurrentBall.SetBallType(ballType);
    }

    public override void EnterState() {
        if (stateMachine.GameData.Scored) {
            //create new level layout
            stateMachine.GameData.IncrementLevel();
            stateMachine.GameData.ResetAttempts();
            levelGenerator.GenerateLevel(stateMachine.GameData.Level);
        }
        else {
            if (gameData.CurrentBall.AlreadyShot) {
                gameData.IncrementAttempts();
            }
        }

        if (gameData.CurrentBall.AlreadyShot) {
            levelGenerator.Restart();
        }

        gameData.CurrentBall.OnShoot += Shoot;

        gameData.Scored = false;

        swipeUI.Activate(gameData.Level, gameData.Attempts);
    }

    public override void ExitState() {
        gameData.CurrentBall.OnShoot -= Shoot;
        swipeUI.Deactivate();
    }

}