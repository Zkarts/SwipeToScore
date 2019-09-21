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
    private SwipeHelper swipeHelper;

    [SerializeField]
    private BallCanvas ballCanvas;

    [SerializeField]
    private Camera mainCamera;

    [Header("Prefabs")]
    [SerializeField]
    private Goal goalPrefab;

    [SerializeField]
    private BallBehaviour ballPrefab;

    [Header("Goal size parameters")]
    [SerializeField]
    private float minWidth;

    [SerializeField]
    private float maxWidth = 4f, minHeight, maxHeight = 2f;

    [SerializeField]
    private float fieldWidth = 4.5f;

    [Header("Pre-existing objects")]
    [SerializeField]
    private Goal currentGoal;

    private float goalDistance, ballDistance, ballPosY;
    private Vector3 ballPosition;

    public override void Init(SwipeToScoreStateMachine stateMachine) {
        base.Init(stateMachine);

        goalDistance = currentGoal.transform.position.z;

        ballPosition = stateMachine.CurrentBall.transform.position;
        ballDistance = stateMachine.CurrentBall.transform.position.z;
        ballPosY = stateMachine.CurrentBall.transform.position.y;

        stateMachine.CurrentBall.OnShoot += Shoot;
        stateMachine.CurrentBall.Init(ballCanvas, swipeHelper, currentGoal.GoalTrigger, mainCamera);
        swipeUI.Init();
    }

    private void Shoot() {
        stateMachine.ChangeState(shootState);
    }

    public override void EnterState() {
        if (stateMachine.Scored) {
            stateMachine.Level++;
            stateMachine.Attempts = 0;
            Destroy(currentGoal.gameObject);

            //order is important; we need the goal for the ball
            SpawnGoal();

            //generate new ball position
            float ballPosX = Random.Range(-0.5f * fieldWidth, 0.5f * fieldWidth);
            ballPosition = new Vector3(ballPosX, ballPosY, ballDistance);
        }
        else {
            if (stateMachine.CurrentBall.AlreadyShot) {
                stateMachine.Attempts++;
            }
        }

        if (stateMachine.CurrentBall.AlreadyShot) {
            Destroy(stateMachine.CurrentBall.gameObject);
            SpawnBall();
        }

        stateMachine.Scored = false;

        swipeUI.Activate(stateMachine.Level, stateMachine.Attempts);
    }

    public override void ExitState() {
        stateMachine.CurrentBall.OnShoot -= Shoot;
        swipeUI.Deactivate();
    }

    private void SpawnGoal() {
        currentGoal = Instantiate<Goal>(goalPrefab);

        float width = Random.Range(minWidth, maxWidth);
        float height = Random.Range(minHeight, maxHeight);

        currentGoal.Init(width, height);

        float leftoverWidth = 0.5f * (fieldWidth - width);
        float goalXPos = Random.Range(-leftoverWidth, leftoverWidth);
        currentGoal.transform.position = new Vector3(goalXPos, 0, goalDistance);
    }

    private void SpawnBall() {
        stateMachine.CurrentBall = Instantiate<BallBehaviour>(ballPrefab);

        stateMachine.CurrentBall.transform.position = ballPosition;

        stateMachine.CurrentBall.OnShoot += Shoot;
        stateMachine.CurrentBall.Init(ballCanvas, swipeHelper, currentGoal.GoalTrigger, mainCamera);
    }

}