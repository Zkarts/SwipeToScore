using UnityEngine;
using System.Collections;

public abstract class SwipeToScoreState : MonoBehaviour {

    protected SwipeToScoreStateMachine stateMachine;

    public virtual void Init(SwipeToScoreStateMachine stateMachine) {
        this.stateMachine = stateMachine;
    }

    public abstract void EnterState();
    public abstract void ExitState();

}
