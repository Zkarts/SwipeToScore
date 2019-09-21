using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShootState : SwipeToScoreState {

    [SerializeField]
    private SwipeToScoreState swipeState, scoreState;

    [SerializeField]
    private ShootUI shootUI;
    
    public override void Init(SwipeToScoreStateMachine stateMachine) {
        base.Init(stateMachine);
        shootUI.Init();
        shootUI.OnRetry += Retry;
    }

    private void Retry() {
        stateMachine.ChangeState(swipeState);
    }

    private void Score() {
        stateMachine.Scored = true;
        stateMachine.ChangeState(scoreState);
    }

    public override void EnterState() {
        shootUI.Activate();
        stateMachine.CurrentBall.OnScore += Score;
    }

    public override void ExitState() {
        stateMachine.CurrentBall.OnScore -= Score;
        shootUI.Deactivate();
    }

}