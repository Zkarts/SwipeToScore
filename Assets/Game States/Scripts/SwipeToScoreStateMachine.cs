using UnityEngine;
using System.Collections;

public class SwipeToScoreStateMachine : MonoBehaviour {

    [SerializeField]
    private SwipeToScoreState initialState;

    [SerializeField]
    private SwipeToScoreState[] allStates;

    private SwipeToScoreState activeState;

    private GameData gameData = new GameData();

    public GameData GameData {
        get { return gameData; }
    }

    private void Start() {
        //Make sure we start with the current ball set
        if (gameData.CurrentBall == null) {
            gameData.CurrentBall = GameObject.FindObjectOfType<BallBehaviour>();
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
