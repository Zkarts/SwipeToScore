using UnityEngine;
using System.Collections;

public class SwipeToScoreStateMachine : MonoBehaviour {

    [SerializeField]
    private SwipeToScoreState initialState;

    [SerializeField]
    private SwipeToScoreState[] allStates;

    private SwipeToScoreState activeState;

    public BallBehaviour CurrentBall;

    //we don't want to edit these values in the Inspector
    [HideInInspector]
    public bool Scored = false;
    [HideInInspector]
    public int Level = 1, Attempts = 0;

    private void Start() {
        //Make sure we start with the current ball set
        if (CurrentBall == null) {
            CurrentBall = GameObject.FindObjectOfType<BallBehaviour>();
        }

        foreach (SwipeToScoreState state in allStates) {
            state.Init(this);
        }

        activeState = initialState;
        activeState.EnterState();
    }

    public void ChangeState(SwipeToScoreState newState) {
        activeState.ExitState();
        activeState = newState;
        newState.EnterState();
    }

}
