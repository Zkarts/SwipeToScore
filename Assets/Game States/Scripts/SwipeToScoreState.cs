using UnityEngine;
using System.Collections;

public abstract class SwipeToScoreState : MonoBehaviour {

    protected SwipeToScoreStateMachine stateMachine;
    protected GameData gameData;

    public virtual void Init(SwipeToScoreStateMachine stateMachine) {
        this.stateMachine = stateMachine;
        this.gameData = stateMachine.GameData;
    }

    public abstract void EnterState();
    public abstract void ExitState();

}
