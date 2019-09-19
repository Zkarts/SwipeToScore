using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//this handles different states, but the current size of the game does not merit a full state machine
public class SwipeToScoreUI : MonoBehaviour {

    public event Action OnRetry;

    [SerializeField]
    private RetryButton retryButton;

    [SerializeField]
    private SwipeUI swipeUI;

    [SerializeField]
    private ScoreUI scoreUI;

    public void Init() {
        swipeUI.Init();
        scoreUI.Init(this);
        retryButton.Init();

        retryButton.OnRetry += Retry;
        scoreUI.OnRetry += Retry;

        Reload();
    }

    private void Retry() {
        OnRetry?.Invoke();
    }

    //Once the stage is reset
    public void Reload() {
        scoreUI.Deactivate();
        swipeUI.Activate();
    }

    //Once the player has swiped and the ball is moving
    public void Shoot() {
        swipeUI.Deactivate();
        scoreUI.Deactivate();
        retryButton.Activate();
    }

    //Once the player scored
    public void Score() {
        scoreUI.Activate();
    }

}