﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameHandler : MonoBehaviour {

    [SerializeField]
    private Goal goalPrefab;

    [SerializeField]
    private BallBehaviour ballPrefab;

    [SerializeField]
    private SwipeToScoreUI swipeToScoreUI;

    [SerializeField]
    private SwipeHelper swipeHelper;

    [SerializeField]
    private Camera mainCamera;

    [SerializeField]
    private float fieldWidth = 5f;

    [Header("Goal size parameters")]
    [SerializeField]
    private float minWidth;

    [SerializeField]
    private float maxWidth = 4f, minHeight, maxHeight = 2f;

    [Header("Pre-existing objects")]
    [SerializeField]
    private Goal currentGoal;

    [SerializeField]
    private BallBehaviour currentBall;

    private float elapsedTime = 0f;
    private float goalDistance;
    private Vector3 ballPosition;
    private bool scored = false;

    private void Start() {
        goalDistance = currentGoal.transform.position.z;
        ballPosition = currentBall.transform.position;

        currentBall.Init(this, swipeHelper, currentGoal.GoalTrigger, mainCamera);
        currentBall.OnShoot += Shoot;
        swipeToScoreUI.Init();
        swipeToScoreUI.OnRetry += Reload;
    }

    public void Shoot() {
        swipeToScoreUI.Shoot();
    }

    public void Score() {
        swipeToScoreUI.Score();
        scored = true;
    }

    public void Reload() {
        if (scored) {
            Destroy(currentGoal.gameObject);

            //order is important; we need the goal for the ball
            SpawnGoal();
        }
        Destroy(currentBall.gameObject);
        SpawnBall();

        swipeToScoreUI.Reload();
        scored = false;
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
        currentBall = Instantiate<BallBehaviour>(ballPrefab);
        currentBall.transform.position = ballPosition;

        currentBall.OnShoot += Shoot;
        currentBall.Init(this, swipeHelper, currentGoal.GoalTrigger, mainCamera);
    }

}