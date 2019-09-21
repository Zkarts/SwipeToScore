using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreState : SwipeToScoreState {

    [SerializeField]
    private SwipeToScoreState swipeState;

    [SerializeField]
    private ScoreUI scoreUI;

    public override void Init(SwipeToScoreStateMachine stateMachine) {
        base.Init(stateMachine);
        scoreUI.Init();
        scoreUI.OnRetry += Retry;
    }

    private void Retry() {
        stateMachine.ChangeState(swipeState);
    }

    public override void EnterState() {
        scoreUI.Activate();
    }

    public override void ExitState() {
        scoreUI.Deactivate();
    }

}