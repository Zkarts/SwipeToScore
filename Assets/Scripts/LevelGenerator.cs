using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour {

    [SerializeField]
    private bool useObstacles = true;

    [SerializeField]
    private ObstacleManager obstacleManager;

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

    //only one field directly under the header attribute to avoid repeating headers in inspector
    [Header("Goal size parameters")]
    [SerializeField]
    private float minWidth = 1f;

    [SerializeField]
    private float maxWidth = 4f, minHeight = 1f, maxHeight = 2f;

    [SerializeField]
    private float fieldWidth = 4.5f;

    [Header("Pre-existing objects")]
    [SerializeField]
    private Goal currentGoal;

    private float goalDistance, ballDistance, ballPosY;
    private Vector3 ballPosition;
    private GameData gameData;

    public void Init(GameData gameData) {
        this.gameData = gameData;

        //get base positions and settings from the initial editor layout
        goalDistance = currentGoal.transform.position.z;

        ballPosition = gameData.CurrentBall.transform.position;
        ballDistance = gameData.CurrentBall.transform.position.z;
        ballPosY = gameData.CurrentBall.transform.position.y;

        gameData.CurrentBall.Init(ballCanvas, swipeHelper, currentGoal.GoalTrigger, mainCamera);
    }

    public void GenerateLevel(int level) {
        Destroy(currentGoal.gameObject);

        //order is important; we need the goal for the ball
        SpawnGoal();

        //generate new ball position
        float ballPosX = Random.Range(-0.5f * fieldWidth, 0.5f * fieldWidth);
        ballPosition = new Vector3(ballPosX, ballPosY, ballDistance);

        if (useObstacles) {
            obstacleManager.ResetObstacles();
            obstacleManager.GenerateObstacles(level);
        }
    }

    public void Restart() {
        //the current ball may have been destroyed through gameplay
        if (gameData.CurrentBall != null) {
            Destroy(gameData.CurrentBall.gameObject);
        }
        SpawnBall();
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
        gameData.CurrentBall = Instantiate<BallBehaviour>(ballPrefab);

        gameData.CurrentBall.transform.position = ballPosition;

        gameData.CurrentBall.Init(ballCanvas, swipeHelper, currentGoal.GoalTrigger, mainCamera);
    }

}